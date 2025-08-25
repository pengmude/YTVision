using HslCommunication;
using Logger;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcWirte
{
    public class NodePlcWrite : NodeBase
    {
        public NodePlcWrite(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormPlcWrite();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultPlcWrite();
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

            var param = (NodeParamPlcWrite)ParamForm.Params;
            try
            {
                SetStatus(NodeStatus.Unexecuted, "*");
                base.CheckTokenCancel(token);

                //如果没有连接则不运行
                if (!param.Plc.IsConnect)
                    throw new Exception("设备尚未连接！");

                base.CheckTokenCancel(token);

                OperateResult res = new OperateResult();
                // 移除前后无效字符
                param.Value = param.Value.Trim('\n').Trim('\r').Trim(' ');
                // 不同的数据类型
                if (param.DataType == typeof(bool).Name)
                {
                    // 包含间隔符号“-”的是写入多个bool值
                    if (param.Value.Contains("-"))
                    {
                        bool[] vals = ConvertToBooleanArray(param.Value);
                        res = await param.Plc.WriteBoolAsync(param.Address, vals);
                    }
                    else
                    {
                        if (param.Value == "1" || param.Value.Equals("True", StringComparison.OrdinalIgnoreCase))
                            res = await param.Plc.WriteBoolAsync(param.Address, new bool[] { true });
                        else
                            res = await param.Plc.WriteBoolAsync(param.Address, new bool[] { false });
                    }
                }
                else if (param.DataType == typeof(int).Name)
                {
                    // 包含间隔符号-的是写入多个int值
                    if (param.Value.Contains("-"))
                    {
                        int[] vals = param.Value.Split('-').Select(int.Parse).ToArray();
                        res = await param.Plc.WriteIntAsync(param.Address, vals);
                    }
                    else
                        res = await param.Plc.WriteIntAsync(param.Address, int.Parse(param.Value));
                }
                else if (param.DataType == typeof(float).Name)
                {
                    // 包含间隔符号-的是写入多个float值
                    if (param.Value.Contains("-"))
                    {
                        float[] vals = param.Value.Split('-').Select(float.Parse).ToArray();
                        res = await param.Plc.WriteFloatAsync(param.Address, vals);
                    }
                    else
                        res = await param.Plc.WriteFloatAsync(param.Address, float.Parse(param.Value));
                }
                else if (param.DataType == typeof(string).Name)
                {
                    // 包含间隔符号-的是写入多个string值
                    if (param.Value.Contains("-"))
                    {
                        throw new NotImplementedException(); // 尚未实现写入多个string值
                    }
                    else
                        res = await param.Plc.WriteStringAsync(param.Address, param.Value);
                }
                else
                    throw new Exception("没有匹配的类型！");

                if (!res.IsSuccess) throw new Exception($"信号写入失败！原因：{res.Message}");
                var time = SetRunResult(startTime, NodeStatus.Successful);
                Result.RunTime = time;
                if(showLog)
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
        /// <summary>
        /// 将用 '-' 分隔的字符串转换为 bool 数组："1" → true, "0" → false，其他值可按需处理（默认 false）
        /// </summary>
        /// <param name="input">输入字符串，如 "1-0-1-0"</param>
        /// <returns>对应的 bool[] 数组</returns>
        public static bool[] ConvertToBooleanArray(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new bool[0];

            return input.Split('-')
                        .Select(item => ReplaceBinaryValueAsBool(item))
                        .ToArray();
        }

        /// <summary>
        /// 将单个字符串值转换为布尔值："1" → true, "0" → false，其他默认 false（可修改）
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns>对应的布尔值</returns>
        private static bool ReplaceBinaryValueAsBool(string value)
        {
            // 可选：严格模式下，遇到非 "1"/"0" 抛异常或返回 false
            return value == "1" || value.Equals("True", StringComparison.OrdinalIgnoreCase);
            // 说明：只有 "1" 和忽略大小写的“True”都返回 true，其余所有情况（包括 "0"、"abc"、""）都返回 false
        }
    }
}
