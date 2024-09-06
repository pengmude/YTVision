using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Forms.ResultView;
using YTVisionPro.Node.Light.PPX;
using static YTVisionPro.Node.AI.HTAI.HTAPI;
using static YTVisionPro.Node.AI.HTAI.NGTypePara;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeHTAI : NodeBase
    {
        NodeResult[] PredictResult;
        public NodeHTAI(string nodeName, Process process) : base(nodeName, process)
        {
            ParamForm = new ParamFormHTAI();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultHTAI();
            NodeHTAI.NodeDeletedEvent += NodeHTAI_NodeDeletedEvent;
        }

        private void NodeHTAI_NodeDeletedEvent(object sender, int e)
        {
            if(PredictResult == null && PredictResult.Length != 0)
                HTAPI.ReleasePredictResult(PredictResult, ((NodeParamHTAI)ParamForm.Params).TestNum);
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override void Run()
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunStatus(startTime, true);
                return;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }

            if(ParamForm is ParamFormHTAI form)
            {
                if (form.Params is NodeParamHTAI param)
                {
                    if (Result is NodeResultHTAI res)
                    {
                        try
                        {
                            // 获取订阅的图像
                            Bitmap srcImg = form.GetImage();
                            // 转换为汇图图像
                            ImageHt Frame = BitmapConvert.BitmapToImageHt(srcImg);
                            Bitmap renderImg = null;
                            PredictResult = DeepLearningDetection(param.TreePredictHandle, ref Frame, param.TestNum, out renderImg);
                            res.ResultData = DeepStudyResult_Judge(PredictResult, param.ngConfigs, param.TestNum);
                            res.RenderImage = renderImg;
                            Result = res;
                            SetRunStatus(startTime, true);
                            LogHelper.AddLog(MsgLevel.Info, $"节点({NodeName})运行成功！", true);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行失败！原因:{ex.Message}", true);
                            SetRunStatus(startTime, false);
                            throw new Exception($"节点({NodeName})运行失败！原因:{ex.Message}");
                        }
                    }
                }
            }
        }

        // 深度学习检测
        private NodeResult[] DeepLearningDetection(IntPtr handle, ref ImageHt pFrame, int testNum, out Bitmap renderImage)
        {
            int ret = -1;
            NodeResult[] pstNodeRst = new NodeResult[testNum];
            bool draw_result = true;
            //* 调用检测
            ret = TreePredict(handle, ref pFrame, pstNodeRst, testNum);
            if (0 == ret)
            {
                for (int i = 0; i < testNum; i++)
                {
                    //* 调用结果绘制
                    DrawNodeResultCSharp(ref pFrame, pstNodeRst, i, draw_result);
                }
            }
            renderImage = BitmapConvert.ImageHtToBitmap(pFrame);
            return pstNodeRst;
        }


        //判断出NG结果的信息 TODO:排查定位节点无法过滤的问题
        private AiResult DeepStudyResult_Judge(NodeResult[] pstNodeRst, NGTypePara.NGType ngConfigs, int testNum)
        {
            AiResult aiResult = new AiResult();
            for (int i = 0; i < testNum; i++)
            {
                //写入判断                      
                string nodename = ConvertCharArrayToString(pstNodeRst[i].node_name);
                int nodetype = pstNodeRst[i].node_type;
                int detect_results_num = pstNodeRst[i].detect_results_num;
                for (int j = 0; j < detect_results_num; j++)
                {
                    string classname;
                    classname = ClassNameTostring(pstNodeRst[i].detect_results[j].class_name);
                    NGTypePara.NGTypeConfig ngconfig = ngConfigs.NGTypeConfigs.Find(c => c.NodeName == nodename && c.CLassName == classname);
                    ngconfig.Num = 0;
                    int area = pstNodeRst[i].detect_results[j].area;
                    float score = pstNodeRst[i].detect_results[j].score;
                    // 每个节点的每个类别的OK数和分数
                    if (nodetype == 0)
                    {
                        if (area >= ngconfig.MinArea  && area < ngconfig.MaxArea && score >= ngconfig.MinScore && score < ngconfig.MaxScore)
                        {
                            ngconfig.Num++;
                            ngconfig.Area[j] = area;
                            ngconfig.Score[j] = score;
                        }
                    }
                    else
                    {
                        if (score >= ngconfig.MinScore && score < ngconfig.MaxScore)
                        {
                            ngconfig.Num++;
                            ngconfig.Score[j] = score;
                        }
                    }
                }
            }
            int countOK = 0;
            foreach (var ngconfig in ngConfigs.NGTypeConfigs)
            {
                //保存的结果包含了用户在初始化模型前选择的节点的所有结果
                SingleResultViewData result = new SingleResultViewData();
                result.NodeName = ngconfig.NodeName;
                result.ClassName = ngconfig.CLassName;
                result.IsOk = ngconfig.ForceOk ? true : ngconfig.IsOk;
                result.DetectResult = $"{ngconfig.Score} {ngconfig.Area}";
                aiResult.DeepStudyResult.Add(result);
                if (result.IsOk)
                    countOK++;
            }
            if(countOK == ngConfigs.NGTypeConfigs.Count)
                aiResult.IsAllOk = true;

            return aiResult;
        }

        // char[]转字符串并截断
        static string ConvertCharArrayToString(char[] chars)
        {
            int length = 0;
            while (length < chars.Length && chars[length] != '\0')
            {
                length++;
            }
            return new string(chars, 0, length);
        }

        private void SetRunStatus(DateTime startTime, bool isOk)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            long elapsedMi11iseconds = (long)elapsed.TotalMilliseconds;
            Result.RunTime = elapsedMi11iseconds;
            Result.Success = isOk;
            Result.RunStatusCode = isOk ? NodeRunStatusCode.OK : NodeRunStatusCode.UNKNOW_ERROR;
        }
    }
}
