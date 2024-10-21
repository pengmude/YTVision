using HslCommunication;
using Logger;
using Sunny.UI;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node.Modbus.Read;
using YTVisionPro.Node.PLC.PanasonicRead;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeModbusWrite : NodeBase
    {
        public NodeModbusWrite(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormModbusWrite();
            Result = new NodeResultModbusWrite();
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

            var param = (NodeParamModbusWrite)ParamForm.Params;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                throw new NotImplementedException("未实现NodeModbusWrite");
                object data = null;
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        //data = await param.Device.WriteMultipleCoilsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.DiscreteInput:
                        //data = await param.Device.WriteInputsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.InputRegisters:
                        //data = await param.Device.WriteInputRegistersAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.HoldingRegisters:
                        //data = await param.Device.WriteHoldingRegistersAsync(param.StartAddress, param.Count);
                        break;
                    default:
                        break;
                }
                if(Result is NodeResultPlcRead result)
                {
                    result.ReadData = data;
                    Result = result;
                }
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
