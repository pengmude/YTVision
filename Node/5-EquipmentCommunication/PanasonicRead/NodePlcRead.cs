using HslCommunication;
using Logger;
using Sunny.UI;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.PanasonicRead
{
    internal class NodePlcRead : NodeBase
    {
        public NodePlcRead(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
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


                    //如果没有连接则不运行
                    if (!param.Plc.IsConnect)
                        throw new Exception("设备尚未连接！");

                    PlcResult<bool, int, string, byte[]> data = new PlcResult<bool, int, string, byte[]>();
                    await Task.Run(() =>
                    {
                        do
                        {
                            switch (param.DataType)
                            {
                                case DataType.BOOL:
                                    var res1 = param.Plc.ReadBool(param.Address);
                                    data.Content1 = res1.Content;
                                    data.IsSuccess = res1.IsSuccess;
                                    LogHelper.AddLog(MsgLevel.Info, $"bool值为: {data.Content1}", true);
                                    break;
                                case DataType.INT:
                                    var res2 = param.Plc.ReadInt(param.Address);
                                    data.Content2 = res2.Content;
                                    data.IsSuccess = res2.IsSuccess;
                                    LogHelper.AddLog(MsgLevel.Info, $"int值为: {data.Content2}", true);
                                    break;
                                case DataType.STRING:
                                    var res3 = param.Plc.ReadString(param.Address, param.Length);
                                    data.Content3 = res3.Content;
                                    data.IsSuccess = res3.IsSuccess;
                                    LogHelper.AddLog(MsgLevel.Info, $"string值为: {data.Content3}", true);
                                    break;
                                case DataType.Bytes:
                                    var res4 = param.Plc.ReadBytes(param.Address, param.Length);
                                    data.Content4 = res4.Content;
                                    data.IsSuccess = res4.IsSuccess;
                                    LogHelper.AddLog(MsgLevel.Info, $"byte[]值为: {data.Content4}", true);
                                    break;
                                default:
                                    break;
                            }

                            long timeTotal = (long)(DateTime.Now - startTime).TotalMilliseconds;
                            if (timeTotal > 5000)
                                throw new Exception("PLC读取超时！请检查PLC端是否正常！");
                        } while (!data.IsSuccess);
                    });
                    
                    res.ReadData = data;
                    Result = res;
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
