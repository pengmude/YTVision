using Logger;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Node.PLC.PanasonicRead;

namespace YTVisionPro.Node.Modbus.Read
{
    internal class NodeModbusRead : NodeBase
    {
        public NodeModbusRead(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            var form = new ParamFormModbusRead();
            form.RunHandler += RunHandler;
            ParamForm = form;
            Result = new NodeResultModbusRead();
        }
        /// <summary>
        /// 节点界面点击执行Modbus读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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

            var param = (NodeParamModbusRead)ParamForm.Params;

            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.Run(token);

                //如果没有连接则不运行
                if (!param.Device.IsConnect)
                    throw new Exception("设备尚未连接！");

                object data = null;
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        data = await ((ModbusPoll)param.Device).ReadCoilsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.DiscreteInput:
                        data = await ((ModbusPoll)param.Device).ReadInputsAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.InputRegisters:
                        data = await ((ModbusPoll)param.Device).ReadInputRegistersAsync(param.StartAddress, param.Count);
                        break;
                    case RegistersType.HoldingRegisters:
                        data = await ((ModbusPoll)param.Device).ReadHoldingRegistersAsync(param.StartAddress, param.Count);
                        break;
                    default:
                        break;
                }

                #region 数据显示
                string resultStr = string.Empty;
                if(ParamForm is ParamFormModbusRead form)
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

                #endregion

                if (Result is NodeResultModbusRead result)
                {
                    result.ReadData = data;
                    Result = result;
                }
                long time = SetRunResult(startTime, NodeStatus.Successful);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms, 读取结果：{resultStr})", true);

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
