using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using YTVisionPro.Forms.ResultView;
using static YTVisionPro.Node.AI.HTAI.HTAPI;

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
            if(PredictResult != null && PredictResult.Length != 0)
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
                            res.ResultData = DeepStudyResult_Judge(PredictResult, param.AllNgConfigs, param.TestNum);
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


        /// <summary>
        /// 判断出NG结果的信息
        /// </summary>
        /// <param name="pstNodeRst"></param>
        /// <param name="allNgConfigs"></param>
        /// <param name="testNum"></param>
        /// <returns></returns>
        private AiResult DeepStudyResult_Judge(NodeResult[] pstNodeRst, List<NGTypeConfig> allNgConfigs, int testNum)
        {
            AiResult aiResult = new AiResult();

            for (int i = 0; i < testNum; i++)
            {
                // 节点名称和节点类型
                string nodeName = ConvertCharArrayToString(pstNodeRst[i].node_name);
                int nodeType = pstNodeRst[i].node_type;
                int detect_results_num = pstNodeRst[i].detect_results_num;

                // 保存单个节点下符合NG的所有类别结果
                List<DetectResult> detectResultsList = new List<DetectResult>();

                // 保存要返回的结果
                SingleResultViewData result = new SingleResultViewData();
                result.NodeName = nodeName;

                for (int j = 0; j < detect_results_num; j++)
                {
                    string className = ClassNameTostring(pstNodeRst[i].detect_results[j].class_name);
                    result.ClassName = className;
                    var ngconfig = allNgConfigs.Find(c => c.NodeName == nodeName && c.CLassName == className);


                    // 首先判断当前节点当前类别是否设置为强制OK,强制OK跳过保存NG结果
                    if (ngconfig == null || ngconfig.ForceOk)
                        break;

                    // 然后筛选当前类别检出结果个数，
                    // 检出个数都不符合就不用看分数和面积了
                    // 直接跳过当前类别，判断下一个类别
                    if (detect_results_num < ngconfig.MinNum && detect_results_num >= ngconfig.MaxNum)
                        break;

                    // 获取检测结果的面积、分数个数信息
                    int area = pstNodeRst[i].detect_results[j].area;
                    float score = pstNodeRst[i].detect_results[j].score;


                    // 非缺陷节点只有分数和个数（定位、分类、定位字符等……）
                    if (score >= ngconfig.MinScore && score < ngconfig.MaxScore && detect_results_num >= ngconfig.MinNum && detect_results_num < ngconfig.MaxNum)
                    {
                        // 缺陷节点（节点类型为0）才有面积结果
                        if (nodeType == 0)
                        {
                            if (area >= ngconfig.MinArea && area < ngconfig.MaxArea)
                                detectResultsList.Add(pstNodeRst[i].detect_results[j]);
                        }
                        else
                            detectResultsList.Add(pstNodeRst[i].detect_results[j]);
                    }
                }

                result.IsOk = detectResultsList.Count == 0 ? true : false;

                string tmp = "";
                foreach (DetectResult classResult in detectResultsList)
                {
                    if (nodeType == 0)
                        tmp += $"({classResult.score},{classResult.area})\n";
                    else
                        tmp += $"({classResult.score})\n";
                }
                result.DetectResult = tmp;
                aiResult.DeepStudyResult.Add(result);
            }

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
