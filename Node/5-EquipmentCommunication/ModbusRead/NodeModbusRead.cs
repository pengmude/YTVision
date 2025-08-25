using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Device.Modbus;

namespace TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead
{
    public class NodeModbusRead : NodeBase
    {
        private Process _process;
        public NodeModbusRead(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormModbusRead();
            form.RunHandler += RunHandler;
            ParamForm = form;
            Result = new NodeResultModbusRead();
            _process = process;
        }
        /// <summary>
        /// 节点界面点击执行Modbus读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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

            var param = (NodeParamModbusRead)ParamForm.Params;
            var modbus = param.Device as IModbus;
            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.CheckTokenCancel(token);

                //如果没有连接则不运行
                if (param.Device == null || !param.Device.IsConnect)
                    throw new Exception("Modbus设备资源已释放或尚未连接！");

                object data = null;
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        data = await modbus.ReadCoilsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.DiscreteInput:
                        data = await modbus.ReadInputsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.InputRegisters:
                        data = await modbus.ReadInputRegistersAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.HoldingRegisters:
                        data = await modbus.ReadHoldingRegistersAsync(param.StartAddress, param.Count);
                        break;
                    default:
                        break;
                }

                #region 数据显示

                string resultStr = string.Empty;
                if (showLog)
                {
                    if (ParamForm is ParamFormModbusRead form)
                    {
                        form.SetReadResult("[开始]");
                        switch (param.DataType)
                        {
                            case RegistersType.Coils:
                            case RegistersType.DiscreteInput:
                                if (data is bool[] dataBoolArr)
                                {
                                    for (int i = 0; i < dataBoolArr.Length; i++)
                                    {
                                        resultStr += $"[{param.StartAddress + i}: {dataBoolArr[i]}] ";
                                        form.SetReadResult($"{param.StartAddress + i}: {dataBoolArr[i]} ");
                                    }
                                }
                                break;
                            case RegistersType.InputRegisters:
                            case RegistersType.HoldingRegisters:
                                if (data is ushort[] dataUShortArr)
                                {
                                    for (int i = 0; i < dataUShortArr.Length; i++)
                                    {
                                        resultStr += $"[{param.StartAddress + i}: 0x{dataUShortArr[i]:X4}] ";
                                        form.SetReadResult($"{param.StartAddress + i}: 0x{dataUShortArr[i]:X4} ");
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        form.SetReadResult("[结束]");
                        form.SetReadResult("-------------------------------");
                    }
                }
                #endregion

                if (Result is NodeResultModbusRead result)
                {
                    if(param.DataType == RegistersType.Coils || param.DataType == RegistersType.DiscreteInput)
                        result.ReadData = new ModbusReadResult(data, typeof(bool[]).Name);
                    else
                        result.ReadData = new ModbusReadResult(data, typeof(ushort[]).Name);
                    Result = result;
                }
                var time = SetRunResult(startTime, NodeStatus.Successful);
                Result.RunTime = time;
                if(showLog)
                    LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms, 读取结果：{resultStr})", true);
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
