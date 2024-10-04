using HslCommunication;
using Logger;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.PLC.Panasonic.Read;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend
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
                            SendSignalToPlc(maxSignalRow);
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

        private static void SendSignalToPlc(SignalRowData dataRow)
        {
            if (dataRow != null)
            {
                foreach (var plcTmp in Solution.Instance.PlcDevices)
                {
                    if (plcTmp.UserDefinedName == dataRow.UserNamePlc)
                    {
                        OperateResult writeResult;
                        do
                        {
                            writeResult = plcTmp.WritePLCData(dataRow.SignalAddress, true);

                        } while (!writeResult.IsSuccess);

                        do
                        {
                            writeResult = plcTmp.WritePLCData(dataRow.SignalAddress, false);
                        } while (!writeResult.IsSuccess);
                        LogHelper.AddLog(MsgLevel.Info, $"{dataRow.SignalAddress}信号发送成功!", true);
                        break;
                    }
                }
            }
        }
    }
}
