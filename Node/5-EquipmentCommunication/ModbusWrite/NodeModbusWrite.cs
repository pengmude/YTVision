using Logger;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead;

namespace TDJS_Vision.Node._5_EquipmentCommunication.ModbusWrite
{
    public class NodeModbusWrite : NodeBase
    {
        private Process _process;
        public NodeModbusWrite(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormModbusWrite();
            form.RunHandler += RunHandler;
            form.SetNodeBelong(this);
            ParamForm = form;
            Result = new NodeResultModbusWrite();
            _process = process;
        }

        private async Task RunHandler(object sender, EventArgs e)
        {
            await Run(CancellationToken.None, _process.ShowLog);
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return new NodeReturn(NodeRunFlag.StopRun);
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
                base.CheckTokenCancel(token);

                //如果没有连接则不运行
                if (param.Device == null || !param.Device.IsConnect)
                    throw new Exception("Modbus设备资源已释放或尚未连接！");

                //如果是订阅的数据，需要先获取订阅的值
                if (param.IsSubscribed)
                    param.Data = ((ParamFormModbusWrite)ParamForm).GetSubValue();

                var modbus = param.Device as IModbus;
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        string[] datas = param.Data.Split(',');
                        bool[] boolArray = datas.Select(part => part.Trim() != "0").ToArray();
                        bool[] falseArray = new bool[boolArray.Length];
                        if (param.IsAsync)
                            modbus.WriteMultipleCoilsAsync(param.StartAddress, boolArray);
                        else
                            modbus.WriteMultipleCoils(param.StartAddress, boolArray);
                        //线圈寄存器保持时间
                        Thread.Sleep(modbus.ModbusParam.HoldTime);
                        // 是否自动重置线圈
                        if (param.IsAutoReset)
                        {
                            if (param.IsAsync)
                                modbus.WriteMultipleCoilsAsync(param.StartAddress, falseArray);
                            else
                                modbus.WriteMultipleCoils(param.StartAddress, falseArray);
                        }
                        break;
                    case RegistersType.HoldingRegisters:
                        string[] parts = param.Data.Split(',');
                        ushort[] ushortArray = Array.ConvertAll(parts, part => ushort.Parse(part.Trim()));
                        if (param.IsAsync)
                            modbus.WriteMultipleRegistersAsync(param.StartAddress, ushortArray);
                        else
                            modbus.WriteMultipleRegisters(param.StartAddress, ushortArray);
                        break;
                    default:
                        break;
                }

                var time = SetRunResult(startTime, NodeStatus.Successful);
                Result.RunTime = time;
                if (showLog)
                    LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                return new NodeReturn(NodeRunFlag.ContinueRun);

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
