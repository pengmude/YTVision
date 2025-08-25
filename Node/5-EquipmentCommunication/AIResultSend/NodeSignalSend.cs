using Logger;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Device.PLC;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._5_EquipmentCommunication.AIResultSend
{
    public class NodeSignalSend : NodeBase
    {
        public NodeSignalSend(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormSignalSend();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultSignalSend();
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

            var param = (NodeParamSignalSend)ParamForm.Params;

            if(ParamForm is ParamFormSignalSend form)
            {
                try
                {
                    // 初始化运行状态
                    SetStatus(NodeStatus.Unexecuted, "*");
                    base.CheckTokenCancel(token);

                    // 获取AI检测结果
                    AlgorithmResult aiResult = form.GetAiResult();

                    // 获取“检测项-地址”映射关系
                    var map = form.GetMapping();

                    // 获取信号配置
                    BindingList<DetectItemRow> signalConfigs = param.BindingData;

                    #region 判断检测项是否配置了信号

                    var (isExist, missingNames) = CheckAiResultExistInSignalConfig(aiResult, signalConfigs);
                    if(!isExist)
                    {
                        string str = "";
                        for (int i = 0; i < missingNames.Count; i++)
                        {
                            if (i != (missingNames.Count - 1))
                                str += $"“{missingNames[i]}”、";
                            else
                                str += $"“{missingNames[i]}”";
                        }
                        LogHelper.AddLog(MsgLevel.Exception, $"识别到检测项{str}尚未配置信号！");
                        throw new Exception($"识别到检测项{str}尚未配置信号！");
                    }

                    #endregion

                    // 执行结果发送
                    Task.Run(() =>
                    {
                        SendToModbus(aiResult, map, param.SignalHoldTime, param.IsAutoReset, token, param.IsRegister, showLog);
                    });
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
            return new NodeReturn(NodeRunFlag.StopRun);
        }

        /// <summary>
        /// 判断检出结果是否配置了信号
        /// </summary>
        /// <param name="aiResult">AI检出结果</param>
        /// <param name="signalConfigs">信号配置列表</param>
        /// <returns>(是否全部存在, 不存在的名称列表)</returns>
        private (bool, List<string>) CheckAiResultExistInSignalConfig(AlgorithmResult aiResult, BindingList<DetectItemRow> signalConfigs)
        {
            // 收集所有有效的检测项名称（支持多个用 ; 分隔的情况）
            var allValidNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in signalConfigs)
            {
                if (!string.IsNullOrWhiteSpace(row.DetectItems))
                {
                    string[] items = row.DetectItems.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in items)
                    {
                        string trimmed = item.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed))
                        {
                            allValidNames.Add(trimmed);
                        }
                    }
                }
            }

            // 获取 AI 检测结果中所有的检测项名称
            var missingNames = new List<string>();

            foreach (var kvp in aiResult.DetectResults)
            {
                // kvp.Key 是检测项名称，kvp.Value 是 SingleDetectResult 的列表
                if (!allValidNames.Contains(kvp.Key.Trim()))
                {
                    // 如果该检测项名称不在有效名称集合中，则添加到缺失名称列表中
                    missingNames.Add(kvp.Key);
                }
                else
                {
                    // 如果需要进一步检查每个 SingleDetectResult 是否符合某些条件，可以在此处进行
                    // 这里假设只需要检查检测项名称的存在性
                }
            }

            if (missingNames.Count == 0)
            {
                return (true, new List<string>());
            }
            else
            {
                return (false, missingNames);
            }
        }

        /// <summary>
        /// 发送检测项对应的 Modbus 信号
        /// </summary>
        /// <param name="aiResult">AI检出结果</param>
        /// <param name="mapping">检测项与Modbus地址的映射表</param>
        /// <param name="holdTime">保持时间（毫秒）</param>
        /// <param name="isAutoReset">是否自动复位</param>
        /// <param name="token">取消令牌</param>
        private async Task SendToModbus(AlgorithmResult aiResult, Dictionary<string, DetectItemAddress> mapping, double holdTime, bool isAutoReset, CancellationToken token, bool isRegister, bool showLog)
        {
            try
            {
                // 使用字典来按 (设备名, 地址) 为Key分组
                var grouped = new Dictionary<(string DeviceName, string SignalAddress), List<SingleDetectResult>>();

                foreach (var kvp in aiResult.DetectResults)
                {
                    string itemName = kvp.Key;
                    List<SingleDetectResult> results = kvp.Value;

                    if (!mapping.TryGetValue(itemName, out var config))
                    {
                        LogHelper.AddLog(MsgLevel.Exception, $"[未配置] 检测项 [{itemName}] 没有找到对应的 Modbus 配置", true);
                        continue;
                    }

                    var key = (config.DeviceName, config.SignalAddress);

                    if (!grouped.ContainsKey(key))
                    {
                        grouped[key] = new List<SingleDetectResult>();
                    }

                    // 将该组下的所有 SingleDetectResult 加入同一个组
                    grouped[key].AddRange(results);
                }

                // 遍历每个分组
                foreach (var group in grouped)
                {
                    var key = group.Key;
                    var results = group.Value;

                    // 获取设备
                    var device = Solution.Instance.AllDevices.Find(dev => dev.UserDefinedName == key.DeviceName);
                    if (device == null)
                        throw new Exception($"不存在名称为 {key.DeviceName} 的Modbus设备，请检查该设备是否已经被移除！");

                    // 获取写入值（取任意一个检测项的Ok和Ng值即可，因为它们共用同一设备和信号地址）
                    var valueOk = mapping.FirstOrDefault(x => x.Key == results[0].Name).Value.OkValue;
                    var valueNg = mapping.FirstOrDefault(x => x.Key == results[0].Name).Value.NgValue;

                    // 判断这个组中是否所有检测项都通过
                    bool allOk = results.All(r => r.IsOk);

                    // 确定发送的值
                    var valueToSend = allOk ? valueOk : valueNg;

                    // 写入信号
                    if (device is IModbus mod)
                    {
                        if (isRegister)
                            await mod.WriteSingleRegisterAsync(ushort.Parse(key.SignalAddress), valueToSend);
                        else
                            await mod.WriteSingleCoilAsync(ushort.Parse(key.SignalAddress), valueToSend != 0);
                    }
                    else if (device is IPlc plc)
                    {
                        if (isRegister)
                            await plc.WriteIntAsync(key.SignalAddress, valueToSend);
                        else
                            await plc.WriteBoolAsync(key.SignalAddress, new bool[] { valueToSend != 0 });
                    }
                    if (showLog)
                        LogHelper.AddLog(MsgLevel.Info, ($"地址：{key.SignalAddress}, 值：{valueToSend}"), true);
                }

                // 信号重置
                if (isAutoReset)
                {
                    // 保持时间
                    await Task.Delay((int)holdTime);
                    foreach (var group in grouped)
                    {
                        var key = group.Key;
                        var results = group.Value;

                        // 获取设备
                        var device = Solution.Instance.AllDevices.Find(dev => dev.UserDefinedName == key.DeviceName);
                        if (device == null)
                            throw new Exception($"不存在名称为 {key.DeviceName} 的Modbus设备，请检查该设备是否已经被移除！");

                        if (device is IModbus mod1)
                        {
                            if (isRegister)
                                await mod1.WriteSingleRegisterAsync(ushort.Parse(key.SignalAddress), 0);
                            else
                                await mod1.WriteSingleCoilAsync(ushort.Parse(key.SignalAddress), false);
                        }
                        else if (device is IPlc plc)
                        {
                            if (isRegister)
                                await plc.WriteIntAsync(key.SignalAddress, 0);
                            else
                                await plc.WriteBoolAsync(key.SignalAddress, new bool[] { false });
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                LogHelper.AddLog(MsgLevel.Warn, "Modbus发送被取消");
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"Modbus发送异常：{ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
    /// <summary>
    /// 检测项地址
    /// </summary>
    public class DetectItemAddress
    {
        /// <summary>
        /// 设备名
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 信号地址
        /// </summary>
        public string SignalAddress { get; set; }
        /// <summary>
        /// Ok信号值
        /// </summary>
        public ushort OkValue {  get; set; }
        /// <summary>
        /// Ng信号值
        /// </summary>
        public ushort NgValue { get; set; }
    }
}
