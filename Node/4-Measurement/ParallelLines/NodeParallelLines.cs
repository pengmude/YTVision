using Logger;
using OpenCvSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._4_Measurement.ParallelLines
{
    /// <summary>
    /// 直线查找节点
    /// </summary>
    internal class NodeParallelLines : NodeBase
    {
        public NodeParallelLines(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormParallelLines();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultParallelLines();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;
            // 参数合法性校验
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

            if (ParamForm is NodeParamFormParallelLines form)
            {
                if (ParamForm.Params is NodeParamParallelLines param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        var line1 = form.GetLinesSrc1();
                        var line2 = form.GetLinesSrc2();

                        double angle, distanceDifference;
                        string result = string.Empty;
                        if (line1 != default(LineSegmentPoint) && line2 != default(LineSegmentPoint))
                        {
                            (angle, distanceDifference) = LineUtils.CalculateParallelism(line1, line2);

                            // 判断直线是否平行
                            if (param.MaxAngleEnable && !param.MaxDistanceDeviationEnable)
                            {
                                result = angle > param.MaxAngle ? "不平行" : "平行";
                            }
                            else if (!param.MaxAngleEnable && param.MaxDistanceDeviationEnable)
                            {
                                result = distanceDifference > param.MaxDistanceDeviation ? "不平行" : "平行";
                            }
                            else if (param.MaxAngleEnable && param.MaxDistanceDeviationEnable)
                            {
                                if (angle <= param.MaxAngle && distanceDifference <= param.MaxDistanceDeviation)
                                    result = "平行";
                                else
                                    result = "不平行";
                            }
                            else
                            {
                                throw new Exception("没有启用两直线平行的判定标准");
                            }

                            ((NodeResultParallelLines)Result).Result = new ResultViewData();
                            ((NodeResultParallelLines)Result).Image = form.DrawLines(line1, line2, result);
                            ((NodeResultParallelLines)Result).Result.SingleRowDataList.Add(new Forms.ResultView.SingleResultViewData
                                                                        ("", "", $"{ID}.{NodeName}", result, result == "平行" ? true : false));

                        }
                        else
                        {
                            throw new Exception("找不到足够数量的直线进行平行度检测！");
                        }

                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，角度：{angle.ToString("F2")}，距离偏差：{distanceDifference.ToString("F2")}，判定：{result})", true);
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
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }

        }
    }
}
