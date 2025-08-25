using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcRead
{
    public class NodePlcRead : NodeBase
    {
        public NodePlcRead(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormPlcRead();
            Result = new NodeResultPlcRead();
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

            var param = (NodeParamPlcRead)ParamForm.Params;

            if (Result is NodeResultPlcRead res)
            {
                try
                {
                    SetStatus(NodeStatus.Unexecuted, "*");
                    base.CheckTokenCancel(token);

                    //如果没有连接则不运行
                    if (!param.Plc.IsConnect)
                        throw new Exception("设备尚未连接！");

                    #region 读取PLC代码
                    int flag = -1;
                    if (param.DataType == typeof(bool).Name)
                    {
                        // 地址中有“-”隔开是多地址
                        string[] adds = param.Address.Split('-');
                        if (adds.Length > 1)
                        {
                            flag = 0;
                            var result = await param.Plc.ReadBoolAsync(adds);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content, typeof(bool[]).Name);
                        }
                        else if (adds.Length == 1)
                        {
                            flag = 1;
                            var result = await param.Plc.ReadBoolAsync(param.Address, 1);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content[0], typeof(bool).Name);
                        }
                    }
                    else if (param.DataType == typeof(int).Name)
                    {
                        // 地址中有“-”隔开是多地址
                        if (param.Address.Contains("-"))
                        {
                            flag = 2;
                            string[] adds = param.Address.Split('-');
                            var result = await param.Plc.ReadIntAsync(adds);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content, typeof(bool[]).Name);
                        }
                        else
                        {
                            flag = 3;
                            var result = await param.Plc.ReadIntAsync(param.Address);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content, typeof(int).Name);
                        }
                    }
                    else if (param.DataType == typeof(float).Name)
                    {
                        // 地址中有“-”隔开是多地址
                        if (param.Address.Contains("-"))
                        {
                            throw new Exception("暂不支持浮点数多地址读功能！");
                        }
                        else
                        {
                            flag = 4;
                            var result = await param.Plc.ReadFloatAsync(param.Address);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content, param.DataType);
                        }
                    }
                    else if (param.DataType == typeof(string).Name)
                    {
                        // 地址中有“-”隔开是多地址
                        if (param.Address.Contains("-"))
                        {
                            throw new Exception("暂不支持浮点数多地址读功能！");
                        }
                        else
                        {
                            flag = 5;
                            var result = await param.Plc.ReadStringAsync(param.Address, param.Length);
                            if (!result.IsSuccess) throw new Exception("读取失败！");
                            res.ReadResult = new PlcReadResult(result.Content, param.DataType);
                        }
                    }
                    else
                        throw new Exception("不支持的类型");
                    #endregion

                    var time = SetRunResult(startTime, NodeStatus.Successful);
                    res.RunTime = time;
                    Result = res;
                    string resStr = "[";
                    if (showLog)
                    {
                        #region 构造读取结果字符串
                        if (flag == 0)
                        {
                            foreach (var item in (bool[])res.ReadResult.Data)
                            {
                                resStr += (item.ToString() + ",");
                            }
                            resStr = resStr.TrimEnd(',') + "]";
                        }
                        else if (flag == 1)
                        {
                            resStr += (((bool)res.ReadResult.Data).ToString() + "]");
                        }
                        else if (flag == 2)
                        {
                            foreach (var item in (int[])res.ReadResult.Data)
                            {
                                resStr += (item.ToString() + ",");
                            }
                            resStr = resStr.TrimEnd(',') + "]";
                        }
                        else if (flag == 3)
                        {
                            resStr += (((int)res.ReadResult.Data).ToString() + "]");
                        }
                        else if (flag == 4)
                        {
                            resStr += (((float)res.ReadResult.Data).ToString() + "]");
                        }
                        else if (flag == 5)
                        {
                            resStr += (res.ReadResult.Data.ToString() + "]");
                        }
                        #endregion
                    }
                    LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms, 读取结果：{resStr})", true);
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
            return new NodeReturn(NodeRunFlag.StopRun);
        }
    }
}
