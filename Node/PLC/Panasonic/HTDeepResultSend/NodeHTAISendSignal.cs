using Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node.PLC.Panasonic.Read;

namespace YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend
{
    internal class NodeHTAISendSignal : NodeBase
    {
        public NodeHTAISendSignal(string nodeName, Process process) : base(nodeName, process) 
        {
            ParamForm = new ParamFormHTAISendSignal();
            Result = new NodeResultHTAISendSignal();
        }
        /// <summary>
        /// 节点运行
        /// </summary>
        public override Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            if (!Active)
            {
                SetRunStatus(startTime, true);
                return Task.CompletedTask;
            }

            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({NodeName})运行参数未设置或保存！", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({NodeName})运行参数未设置或保存！");
            }
            
            var param = (NodeParamHTAISendSignal)ParamForm.Params;

            try
            {
                base.Run(token);

                //深度学习结果(测试数据)
                Dictionary<string, string> DeepLearnResult = new Dictionary<string, string>
                {
                { "dingwei", "1" },
                { "fenlei", "断焊" },
                { "jiance", "焊洞" }
                };
                List<SignalRowData> allMatchingRows = new List<SignalRowData>();

                foreach (var item in DeepLearnResult)
                {
                    string nodeName = item.Key;
                    string className = item.Value;
                    var matchingRows = FindMatchSignalRow(param.Data, nodeName, className);
                    if (matchingRows != null && matchingRows.Count > 0)
                    {
                        allMatchingRows.AddRange(matchingRows);
                    }
                }

                // 按PLC分组，并在每个组中找到信号等级最大的行
                var maxSignalRowsByPlc = allMatchingRows
                .GroupBy(row => row.UserNamePlc)
                .Select(group => group.OrderByDescending(row => row.SignalLevel).FirstOrDefault())
                .ToList();
                // 发送信号到对应的PLC
                foreach (var maxSignalRow in maxSignalRowsByPlc)
                {
                    SendSignalToPlc(maxSignalRow);
                }
                SetRunStatus(startTime, true);
                LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
            }
            catch (OperationCanceledException)
            {
                LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                SetRunStatus(startTime, false);
                throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                SetRunStatus(startTime, false);
                throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
            }
            

            return Task.CompletedTask;
        }

        public List<SignalRowData> FindMatchSignalRow(List<SignalRowData> list, string nodeName, string className)
        {
            var matchingRows = list.Where(row => row.NodeName == nodeName && row.ClassName == className).ToList();
            return matchingRows;
        }

        public static void SendSignalToPlc(SignalRowData dataRow)
        {
            if (dataRow != null)
            {
                foreach (var plcTmp in Solution.Instance.PlcDevices)
                {
                    if (plcTmp.UserDefinedName == dataRow.UserNamePlc)
                    {
                        do
                        {
                            plcTmp.WritePLCData(dataRow.SignalAddress, true);
                            LogHelper.AddLog(MsgLevel.Info, $"{dataRow.SignalAddress}信号发送成功", true);

                        } while (!(bool)plcTmp.ReadPLCData(dataRow.SignalAddress, 0, DataType.BOOL));
                        
                        do
                        {
                            plcTmp.WritePLCData(dataRow.SignalAddress, false);
                            LogHelper.AddLog(MsgLevel.Info, $"{dataRow.SignalAddress}信号断开成功", true);

                        } while (!(bool)plcTmp.ReadPLCData(dataRow.SignalAddress, 0, DataType.BOOL));
                        break;
                    }
                }
            }
            else
            {
                
            }
        }

        private void SetRunStatus(DateTime startTime, bool isOk)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            long elapsedMi11iseconds = (long)elapsed.TotalMilliseconds;
            Result.RunTime = elapsedMi11iseconds;
            Result.Success = isOk;
            Result.RunStatusCode = isOk ? NodeRunStatusCode.OK : NodeRunStatusCode.UNKNOW_ERROR;
        }
    }
}
