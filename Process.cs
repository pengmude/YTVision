using Logger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YTVisionPro.Node;

namespace YTVisionPro
{
    public struct ProcessRunResult 
    {
        public bool IsRunning;
        public bool IsSuccess;
        public string ProcessName;
        public ProcessRunResult(bool  isRunning, bool isSuccess, string name)
        {
            IsRunning = isRunning;
            IsSuccess = isSuccess;
            ProcessName = name;
        }
    }

    /// <summary>
    /// 检测流程类
    /// 一个方案可以拥有多个流程
    /// 每个流程也可以单独执行
    /// </summary>
    internal class Process
    {
        /// <summary>
        /// 流程包含的节点
        /// </summary>
        private List<NodeBase> _nodes = new List<NodeBase>();
        /// <summary>
        /// 流程名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 流程包含的节点
        /// </summary>
        public List<NodeBase> Nodes { get => _nodes;}

        /// <summary>
        /// 流程是否正在运行
        /// </summary>
        public bool IsRuning { get; set; } = false;

        /// <summary>
        /// 流程运行优先级
        /// </summary>
        public ProcessLvEnum RunLv { get; set; } = ProcessLvEnum.Lv5;

        /// <summary>
        /// 流程组别
        /// </summary>
        public ProcessGroup processGroup { get; set; } = ProcessGroup.Group1;

        /// <summary>
        /// 流程运行时间
        /// </summary>
        public long RunTime { get; private set; } = 0;

        /// <summary>
        /// 流程运行是否成功
        /// </summary>
        public bool Success { get; private set; }
        /// <summary>
        /// 流程是否启用
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 流程运行完更新流程界面和主界面的运行按钮Enable
        /// </summary>
        public static EventHandler<ProcessRunResult> UpdateRunStatus;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="solution"></param>
        public Process(string processName)
        {
            ProcessName = processName;
        }

        public void AddNode(NodeBase node)
        {
            _nodes.Add(node);
        }

        /// <summary>
        /// 流程开始运行
        /// </summary>
        public async Task Run(bool isCyclical)
        {
            // 节点数为0或流程不启用则不运行
            if (Nodes.Count == 0 || !Enable)
                return;
            RunTime = 0;
            // 界面运行按钮Enable设置为运行中
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false, ProcessName));

            LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
            IsRuning = true;
            Success = false;
            foreach (var node in _nodes)
            {
                try
                {
                    // 点检模式（非循环运行方案模式）需要跳过流程中第一个节点是“PLC软触发”或“Modbus软触发”节点
                    if (node == _nodes[0] && (node.NodeType == NodeType.WaitSoftTrigger || node.NodeType == NodeType.ModbusSoftTrigger) && !isCyclical)
                        continue;
                    await node.Run(Solution.Instance.CancellationToken);
                    RunTime += node.Result.RunTime;
                }
                catch (OperationCanceledException ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
                catch (Exception ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
            }

            Success = true;
            IsRuning = false;
            LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
        }

        /// <summary>
        /// 用于硬触发拍照的流程运行，特点是拍照节点设置为硬触发
        /// 不需要手动点击运行按钮，流程的运行由硬触发信号给到就执行下去
        /// 参数传入硬触发的拍照节点对象和它的耗时，用于跳过该节点，只执行之后的节点
        /// </summary>
        public async Task RunForHardTrigger(NodeBase nodeShot, long time)
        {
            // 重置运行取消令牌
            Solution.Instance.ResetTokenSource();

            // 节点数为0或流程不启用则不运行
            if (Nodes.Count == 0 || !Enable)
                return;

            RunTime = 0;
            // 更新运行状态
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false, ProcessName));
            IsRuning = true;
            Success = false;
            foreach (var node in _nodes)
            {
                try
                {
                    if (nodeShot == node)
                    {
                        RunTime += time;
                        continue;
                    }
                    await node.Run(Solution.Instance.CancellationToken);
                    RunTime += node.Result.RunTime;
                }
                catch (OperationCanceledException ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
                catch (Exception ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
            }

            Success = true;
            IsRuning = false;
            LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
        }

        /// <summary>
        /// 专用于需要订阅刷新前一个节点图像的节点
        /// 要在参数节点处停止运行
        /// </summary>
        public async Task RunForUpdateImages(NodeBase node2Stop)
        {
            // 重置运行取消令牌
            Solution.Instance.ResetTokenSource();

            // 节点数为0或流程不启用则不运行
            if (Nodes.Count == 0 || !Enable)
                return;

            RunTime = 0;
            // 更新运行状态
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false, ProcessName));
            LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
            IsRuning = true;
            Success = false;
            foreach (var node in _nodes)
            {
                try
                {
                    if (node2Stop == node)
                    {
                        break;
                    }
                    await node.Run(Solution.Instance.CancellationToken);
                    RunTime += node.Result.RunTime;
                }
                catch (OperationCanceledException ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
                catch (Exception ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
            }

            Success = true;
            IsRuning = false;
            LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
        }
    }

    /// <summary>
    /// 流程运行优先级别，同级并行运行
    /// </summary>
    internal enum ProcessLvEnum 
    {
        /// <summary>
        /// 1级最先运行
        /// </summary>
        Lv1,
        Lv2,
        Lv3,
        Lv4,
        /// <summary>
        /// 5级最后运行
        /// </summary>
        Lv5,
    }

    internal enum ProcessGroup
    {
        Group1,
        Group2,
        Group3,
        Group4,
        Group5
        //Group6,
        //Group7,
        //Group8,
        //Group9,
        //Group10,
        //Group11,
        //Group12,
        //Group13,
        //Group14,
        //Group15,
        //Group16
    }
}
