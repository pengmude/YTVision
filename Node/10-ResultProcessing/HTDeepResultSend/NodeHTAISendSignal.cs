using HslCommunication;
using Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ResultProcessing.HTDeepResultSend
{
    internal class NodeHTAISendSignal : NodeBase
    {
        public NodeHTAISendSignal(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormHTAISendSignal();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultHTAISendSignal();
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
            
            var param = (NodeParamHTAISendSignal)ParamForm.Params;

            if(ParamForm is ParamFormHTAISendSignal form)
            {
                try
                {
                    // 初始化运行状态
                    SetStatus(NodeStatus.Unexecuted, "*");
                    base.Run(token);

                    ResultViewData aiResult = form.GetAiResult();

                    List<SignalRowData> allMatchingRows = new List<SignalRowData>();

                    if (aiResult.IsAllOk)
                    {
                        string nodeName = "OK";
                        string className = "OK";
                        var matchingRows = FindMatchSignalRow(param.Data, nodeName, className);
                        if (matchingRows != null && matchingRows.Count > 0)
                        {
                            allMatchingRows.AddRange(matchingRows);
                        }
                        else
                        {
                            // 记录没有找到匹配信号行的日志
                            throw new Exception($"节点({ID}.{NodeName})没有设置{nodeName}-{className}的信号！");
                        }
                    }
                    else
                    {
                        foreach (var item in aiResult.SingleRowDataList)
                        {
                            if (item.IsOk)
                                continue;
                            string nodeName = item.NodeName;
                            string className = item.ClassName;
                            var matchingRows = FindMatchSignalRow(param.Data, nodeName, className);
                            if (matchingRows != null && matchingRows.Count > 0)
                            {
                                allMatchingRows.AddRange(matchingRows);
                            }
                            else
                            {
                                throw new Exception($"节点({ID}.{NodeName})没有设置{nodeName}-{className}的信号！");
                            }
                        }
                    }

                    // 按PLC分组，并在每个组中找到信号等级最大的行
                    var maxSignalRowsByPlc = allMatchingRows
                    .GroupBy(row => row.UserNamePlc)
                    .Select(group => group.OrderByDescending(row => row.SignalLevel).FirstOrDefault())
                    .ToList();

                    if (maxSignalRowsByPlc.Count == 0)
                    {
                        throw new Exception($"节点({ID}.{NodeName})没有匹配到对应的信号！");
                    }

                    await Task.Run(() =>
                    {
                        // 发送信号到对应的PLC
                        foreach (var maxSignalRow in maxSignalRowsByPlc)
                        {
                            SendSignalToPlc(maxSignalRow, param.StayTime);
                        }
                    });
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

        private List<SignalRowData> FindMatchSignalRow(List<SignalRowData> list, string nodeName, string className)
        {
            var matchingRows = list.Where(row => row.NodeName == nodeName && row.ClassName == className).ToList();
            return matchingRows;
        }

        private async void SendSignalToPlc(SignalRowData dataRow, double time)
        {
            if (dataRow != null)
            {
                foreach (var plcTmp in Solution.Instance.PlcDevices)
                {
                    if (plcTmp.UserDefinedName == dataRow.UserNamePlc)
                    {
                        try
                        {
                            OperateResult writeResult;
                            DateTime startTime = DateTime.Now;

                            //将信号地址字符串以逗号分隔成信号地址列表
                            string[] signals = dataRow.SignalAddress.Split(',');

                            // 初始化信号数组
                            bool[] trueVals = Enumerable.Repeat(true, signals.Length).ToArray();
                            bool[] falseVals = Enumerable.Repeat(false, signals.Length).ToArray();

                            do
                            {
                                writeResult = plcTmp.WriteMultipleBool(signals, trueVals);

                                long timeTotal = (long)(DateTime.Now - startTime).TotalMilliseconds;
                                if (timeTotal > 5000)
                                    throw new Exception("发送AI检测结果信号存在超时，请检查PLC通信是否正常！");

                            } while (!writeResult.IsSuccess);

                            // 信号保持时间
                            await ExecuteSleepAsync((int)time);

                            do
                            {
                                writeResult = plcTmp.WriteMultipleBool(signals, falseVals);

                                long timeTotal = (long)(DateTime.Now - startTime).TotalMilliseconds;
                                if (timeTotal > 5000)
                                    throw new Exception("重置AI检测结果信号出现超时，请检查PLC通信是否正常！");
                            } while (!writeResult.IsSuccess);
                            LogHelper.AddLog(MsgLevel.Info, $"{dataRow.SignalAddress}信号发送成功!", true);
                            break;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private async Task ExecuteSleepAsync(int timeInMilliseconds)
        {
            // 使用 Task.Delay 在后台线程上执行异步睡眠操作
            await Task.Delay(timeInMilliseconds);
        }
    }
}
