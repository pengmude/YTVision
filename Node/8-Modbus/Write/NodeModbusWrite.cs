using Logger;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Node.Modbus.Read;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeModbusWrite : NodeBase
    {
        public NodeModbusWrite(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormModbusWrite();
            form.RunHandler += RunHandler;
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultModbusWrite();
        }

        private async Task RunHandler(object sender, EventArgs e)
        {
            await Run(CancellationToken.None);
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

                //如果没有连接则不运行
                if (!param.Device.IsConnect)
                    throw new Exception("设备尚未连接！");

                //如果是订阅的数据，需要先获取订阅的值
                if (param.IsSubscribed)
                    param.Data = ((ParamFormModbusWrite)ParamForm).GetSubValue();

                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        string[] datas = param.Data.Split(',');
                        bool[] boolArray = datas.Select(part => part.Trim() != "0").ToArray();
                        if(param.IsAsync)
                            ((ModbusPoll)param.Device).WriteMultipleCoilsAsync(param.StartAddress, boolArray);
                        else
                            ((ModbusPoll)param.Device).WriteMultipleCoils(param.StartAddress, boolArray);
                        break;
                    case RegistersType.HoldingRegisters:
                        string[] parts = param.Data.Split(',');
                        ushort[] ushortArray = Array.ConvertAll(parts, part => ushort.Parse(part.Trim()));
                        if (param.IsAsync)
                            ((ModbusPoll)param.Device).WriteMultipleRegistersAsync(param.StartAddress, ushortArray);
                        else
                            ((ModbusPoll)param.Device).WriteMultipleRegisters(param.StartAddress, ushortArray);
                        break;
                    default:
                        break;
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
