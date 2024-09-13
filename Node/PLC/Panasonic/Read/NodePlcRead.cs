using Logger;
using Sunny.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal class NodePlcRead : NodeBase
    {
        public NodePlcRead(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType)
        {
            ParamForm = new ParamFormPlcRead();
            Result = new NodeResultPlcRead();
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

            var param = (NodeParamPlcRead)ParamForm.Params;

            if (Result is NodeResultPlcRead res)
            {
                try
                {
                    SetStatus(NodeStatus.Unexecuted, "*");
                    base.Run(token);

                    res.CodeText = param.Plc.ReadPLCData(param.Address, DataType.STRING, param.Length);
                    if (res.CodeText.ToString().IsNullOrEmpty())
                        throw new Exception("读码为空！");

                    Result = res;
                    SetRunResult(startTime, NodeStatus.Successful);
                    LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);

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
