using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Forms.ResultView;
using static YTVisionPro.Node.AI.HTAI.HTAPI;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeHTAI : NodeBase
    {
        NodeResult[] PredictResult;
        /// <summary>
        /// 图像发布事件
        /// </summary>
        public static event EventHandler<ImageShowPamra> ImageShowChanged;
        public NodeHTAI(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormHTAI();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultHTAI();
            NodeDeletedEvent += NodeHTAI_NodeDeletedEvent;
        }

        /// <summary>
        /// AI节点删除时候要释放AI检测结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeHTAI_NodeDeletedEvent(object sender, NodeBase e)
        {
            if (e.Equals(this) && PredictResult != null && PredictResult.Length != 0)
                ReleaseAIResult();
        }

        /// <summary>
        /// 提供主窗口等外部释放AI检测结果
        /// </summary>
        public void ReleaseAIResult()
        {
            if(((NodeParamHTAI)ParamForm.Params) != null && PredictResult != null)
                ReleasePredictResult(PredictResult, ((NodeParamHTAI)ParamForm.Params).TestNum);
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
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
                            SetStatus(NodeStatus.Unexecuted, "*");
                            base.Run(token);

                            // 获取订阅的图像
                            Bitmap srcImg = form.GetImage();
                            // 转换为汇图图像
                            ImageHt Frame = BitmapConvert.BitmapToImageHt(srcImg);
                            Bitmap renderImg = null;
                            if (param.TreePredictHandle == IntPtr.Zero)
                                throw new Exception("AI模型句柄尚未加载完成！");
                            PredictResult = DeepLearningDetection(param.TreePredictHandle, ref Frame, param.TestNum, out renderImg);
                            var data = DeepStudyResult_Judge(PredictResult, param.AllNgConfigs, param.TestNum);
                            res.ResultData = data;
                            res.IsAllOk = data.IsAllOk;
                            res.RenderImage = renderImg;
                            Result = res;
                            ImageShowChanged?.Invoke(this, new ImageShowPamra(param.WindowName, renderImg));
                            long time = SetRunResult(startTime, NodeStatus.Successful);
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                        }
                        catch (OperationCanceledException)
                        {
                            LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                            SetRunResult(startTime, NodeStatus.Unexecuted);
                            throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                        }
                        catch (Exception ex)
                        {
                            LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                            SetRunResult(startTime, NodeStatus.Failed);
                            throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
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
        private ResultViewData DeepStudyResult_Judge(NodeResult[] pstNodeRst, List<NGTypeConfig> allNgConfigs, int testNum)
        {
            ResultViewData aiResult = new ResultViewData();
            List<AiClassResult>  ResList = SaveResult(pstNodeRst, testNum);

            // 存在定位失败的结果直接将所有结果设置成NG
            var locFailResList = ResList.Where(res => res.IsLocFail).ToList();
            if(locFailResList.Count != 0)
            {
                // 处理定位失败的结果
                HandleLocationFailure(allNgConfigs, ResList, locFailResList, aiResult);
            }
            else
            {
                #region 添加NG部分结果

                AddNGResult(ResList, allNgConfigs, aiResult);
                
                #endregion
            }

            #region 添加OK部分结果(在allNgConfigs所有类别中除了前面添加的NG剩下都当做OK)

            AddOKResult(allNgConfigs, aiResult);

            #endregion

            return aiResult;
        }

        /// <summary>
        /// 添加OK部分结果
        /// 只要没添加到NG的结果都当做OK
        /// </summary>
        /// <param name="allNgConfigs"></param>
        /// <param name="aiResult"></param>
        private void AddOKResult(List<NGTypeConfig> allNgConfigs, ResultViewData aiResult)
        {
            List<SingleResultViewData> okRes = new List<SingleResultViewData>();
            bool locNodesWithoutResults = false;
            foreach (var ngConfig in allNgConfigs)
            {
                var isExist = aiResult.SingleRowDataList.Exists(r => r.NodeName == ngConfig.NodeName && r.ClassName == ngConfig.ClassName);
                if (!isExist)
                {
                    if (ngConfig.NodeType == 1)
                    {
                        locNodesWithoutResults = true;
                    }
                    SingleResultViewData data = new SingleResultViewData();
                    data.IsOk = true;
                    data.DetectResult = "";
                    data.DetectName = "";
                    data.NodeName = ngConfig.NodeName;
                    data.ClassName = ngConfig.ClassName;
                    aiResult.SingleRowDataList.Add(data);
                }
            }
        }

        /// <summary>
        /// 添加NG结果
        /// </summary>
        /// <param name="ResList"></param>
        /// <param name="allNgConfigs"></param>
        /// <param name="aiResult"></param>
        private void AddNGResult(List<AiClassResult> ResList, List<NGTypeConfig> allNgConfigs, ResultViewData aiResult)
        {
            foreach (var item in ResList)
            {
                // 找到对应节点类别的筛选配置
                var ngconfig = allNgConfigs.Find(c => c.NodeName == item.NodeName && c.ClassName == item.ClassName);
                if (ngconfig == null || ngconfig.ForceOk)
                    continue;

                // 保存要返回的结果
                SingleResultViewData result = new SingleResultViewData();
                result.NodeName = item.NodeName;
                result.ClassName = item.ClassName;

                // 判断当前类别的IsOk
                result.IsOk = true;
                string tmp = "";
                int ngCount = 0;
                foreach (var item1 in item.Results)
                {
                    // 获取检测结果的面积、分数个数信息
                    int area = item1.area;
                    float score = item1.score;
                    // 非缺陷节点只有分数和个数（定位、分类、定位字符等……）
                    if (score >= ngconfig.MinScore && score < ngconfig.MaxScore)
                    {
                        // 缺陷节点（节点类型为0）才有面积结果
                        if (item.NodeType == 0)
                        {
                            if (area >= ngconfig.MinArea && area < ngconfig.MaxArea)
                            {
                                ngCount++;
                                tmp += $"({item1.score},{item1.area})\n";
                            }
                        }
                        else
                        {
                            ngCount++;
                            tmp += $"({item1.score},{item1.area})\n";
                        }
                    }
                }

                if (ngCount != 0 && item.Results.Count >= ngconfig.MinNum && item.Results.Count < ngconfig.MaxNum)
                    result.IsOk = false;

                // 添加一条类别结果
                result.DetectResult = tmp;
                aiResult.SingleRowDataList.Add(result);
            }
        }


        private void HandleLocationFailure(List<NGTypeConfig> allNgConfigs, List<AiClassResult> ResList, List<AiClassResult> locFailResList, ResultViewData aiResult)
        {
            int _count1 = 0, _count2 = 0;
            foreach (var result in locFailResList)
            {
                // 统计定位识别类别结果的个数，以及它被设置为强制OK的个数，
                // 如果相等就说明所有节定位节点结果被设置为强制OK了，也就忽略掉定位不到的NG
                _count1 += allNgConfigs.Count(ng => ng.NodeName == result.NodeName);
                _count2 += allNgConfigs.Count(ng => ng.NodeName == result.NodeName && ng.ForceOk);
            }

            // 如果相等说明定位节点每个类别都被强制OK了
            if (_count1 == _count2)
            {
                AddNGResult(ResList, allNgConfigs, aiResult);
                AddOKResult(allNgConfigs, aiResult);
            }
            else
            {
                #region 有定位节点下的类别定位失败时，作为NG添加到结果中

                // 第一种：定位不到时仅仅把定位的结果设置为NG
                //foreach (var res in locFailResList)
                //{
                //    // 找到对应的ClassName,并添加
                //    var ngconfigList = allNgConfigs.Where(c => c.NodeName == res.NodeName).ToList();
                //    foreach (var ngConfig in ngconfigList)
                //    {
                //        // 保存要返回的结果
                //        SingleResultViewData result = new SingleResultViewData();
                //        result.NodeName = ngConfig.NodeName;
                //        result.ClassName = ngConfig.ClassName;
                //        result.IsOk = false;
                //        result.DetectResult = "定位失败";
                //        aiResult.SingleRowDataList.Add(result);
                //    }
                //}


                //第二种：定位不到时把所有的结果设置为NG
                foreach (var ngConfig in allNgConfigs)
                {
                    if(ngConfig.ForceOk)
                        continue;
                    // 保存要返回的结果
                    SingleResultViewData result = new SingleResultViewData();
                    result.NodeName = ngConfig.NodeName;
                    result.ClassName = ngConfig.ClassName;
                    result.IsOk = false;
                    result.DetectResult = "定位失败";
                    aiResult.SingleRowDataList.Add(result);
                }

                #endregion
            }
        }

        /// <summary>
        /// 基于节点类型保存AI检测出来的结果
        /// </summary>
        private List<AiClassResult> SaveResult(NodeResult[] pstNodeRst, int testNum)
        {
            List<AiClassResult> aiResultList= new List<AiClassResult>();
            for (int i = 0; i < testNum; i++)
            {
                // 节点名称和节点类型
                string nodeName = ConvertCharArrayToString(pstNodeRst[i].node_name);
                int nodeType = pstNodeRst[i].node_type;
                int detect_results_num = pstNodeRst[i].detect_results_num;

                // 如果是定位类型且定位数为0，属于图像被遮挡情况，直接返回当NG处理
                if(nodeType == 1 && detect_results_num == 0)
                {
                    aiResultList.Add(new AiClassResult
                    {
                        NodeName = nodeName,
                        NodeType = nodeType,
                        ClassName = "",
                        IsLocFail = true,
                    });
                    continue;
                }

                for (int j = 0; j < detect_results_num; j++)
                {
                    string className = ClassNameTostring(pstNodeRst[i].detect_results[j].class_name);
                    AddDetectResult(aiResultList, nodeName, nodeType, className, pstNodeRst[i].detect_results[j]);
                }
            }
            return aiResultList;
        }

        /// <summary>
        /// 添加单个类别结果到所有类别结果列表中
        /// </summary>
        /// <param name="aiResultList"></param>
        /// <param name="nodeName"></param>
        /// <param name="className"></param>
        /// <param name="detectResult"></param>
        public static void AddDetectResult(List<AiClassResult> aiResultList, string nodeName, int nodeType, string className, DetectResult detectResult)
        {
            // 查找已存在的 AiClassResult 实例
            AiClassResult existingResult = aiResultList
                .FirstOrDefault(ar => ar.NodeName == nodeName && ar.NodeType == nodeType && ar.ClassName == className);

            // 如果找到了匹配的 AiClassResult 实例
            if (existingResult != null)
            {
                // 将新的 DetectResult 添加到现有实例的 Results 列表中
                existingResult.Results.Add(detectResult);
            }
            else
            {
                // 如果没有找到匹配的实例，则创建一个新的 AiClassResult 实例
                AiClassResult newResult = new AiClassResult
                {
                    NodeName = nodeName,
                    NodeType = nodeType,
                    ClassName = className,
                    IsLocFail = false,
                    Results = new List<DetectResult> { detectResult }
                };

                // 将新实例添加到 aiResultList 中
                aiResultList.Add(newResult);
            }
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

    }
}
