using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._4_Measurement.InjectionHole
{
    internal class NodeInjectionHoleMeasurement : NodeBase
    {
        public NodeInjectionHoleMeasurement(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new NodeParamFormInjectionHoleMeasurement(process, this);
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultInjectionHoleMeasurement();
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

            if (ParamForm is NodeParamFormInjectionHoleMeasurement form)
            {
                if (ParamForm.Params is NodeParamInjectionHoleMeasurement param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        string res = string.Empty;
                        var (Circle, image) = form.DetectCircle();
                        if (Circle == null || image == null) { throw new Exception("注液孔查找失败！"); }

                        if (!param.OKEnable)
                            res = "未启用";
                        else if (Circle.Radius >= param.OKMinR && Circle.Radius <= param.OKMaxR)
                            res = "OK";
                        else
                            res = "NG";

                        // 输出节点结果
                        ((NodeResultInjectionHoleMeasurement)Result).OutputImage = image;
                        ((NodeResultInjectionHoleMeasurement)Result).Result = new ResultViewData();
                        ((NodeResultInjectionHoleMeasurement)Result).Result.SingleRowDataList.Add(new Forms.ResultView.SingleResultViewData
                                                                    ("", "", NodeName, res, res == "OK" ? true : false));

                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，圆半径：{Circle.Radius} 像素, 圆心：({Circle.Center.X},{Circle.Center.Y}), 判定：{res}", true);
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
