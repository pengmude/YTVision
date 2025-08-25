using Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TDJS_Vision.Node;

namespace TDJS_Vision
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
    public class Process
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
        /// 流程运行时是否输出日志
        /// </summary>
        public bool ShowLog { get; set; } = true;
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
        public ProcessGroup Group { get; set; } = ProcessGroup.Group1;
        /// <summary>
        /// 流程运行时间
        /// </summary>
        public long RunTime { get; private set; } = 0;

        /// <summary>
        /// 流程运行是否成功
        /// </summary>
        public bool Success { get; private set; }
        /// <summary>
        /// 是否跳过Else逻辑
        /// </summary>
        public bool SkipNextElseBlock { get; set; }
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


        #region 实现被触发执行的逻辑

        /// <summary>
        /// 是否是在流程编辑界面中手动点击运行
        /// </summary>
        public bool IsHandRun { get; set; }

        /// <summary>
        /// 是否为被动触发流程
        /// </summary>
        public bool IsPassiveTriggered { get; set; }


        /// <summary>
        /// 可选：添加触发方法
        /// </summary>
        /// <param name="passiveProcesses"></param>
        /// <param name="ct"></param>
        public void TriggerPassiveProcesses(IEnumerable<Process> passiveProcesses, CancellationToken ct)
        {
            foreach (var p in passiveProcesses)
            {
                if (p == null)
                    continue;
                // 可在这里加条件判断是否可触发等
                p.RunInternal(isCyclical: false, isTriggered: true, ct: ct);
            }
        }

        private Action<Process> _triggerAction; // 由外部注入

        public void SetTriggerAction(Action<Process> action)
        {
            _triggerAction = action;
        }

        // 在流程的某个节点需要触发其他流程时调用
        public async Task TriggerProcess(string processName)
        {
            foreach(var pro in Solution.Instance.AllProcesses)
            {
                if (pro.ProcessName == processName)
                {
                    // 触发某个流程
                    _triggerAction?.Invoke(pro);
                    break;
                }
            }
        }

        // 包装 Run 方法，支持标记“是否被触发”
        public async Task RunInternal(bool isCyclical, bool isTriggered, CancellationToken ct)
        {
            using (ct.Register(() => { /* 可选：清理 */ }))
            {
                try
                {
                    await Run(isCyclical); // 实际执行
                }
                catch (OperationCanceledException) when (ct.IsCancellationRequested)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"被动流程执行取消！");
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"被动流程执行异常: {ex.Message}");
                }
            }
        }

        #endregion

        /// <summary>
        /// 流程开始运行
        /// </summary>
        public async Task Run(bool isCyclical)
        {
            if (Nodes.Count == 0 || !Enable)
                return;

            RunTime = 0;
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false, ProcessName));

            if (ShowLog)
                LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
            IsRuning = true;
            Success = false;

            for (int i = 0; i < _nodes.Count; i++)
            {
                NodeBase node = _nodes[i];
                try
                {
                    if (!node.Active)
                        continue;

                    NodeReturn result = await node.Run(Solution.Instance.CancellationToken, ShowLog);
                    RunTime += node.Result.RunTime;

                    // 检查还要不要继续运行
                    if (result.Flag == NodeRunFlag.StopRun)
                    {
                        // 当前节点要求停止后续节点执行
                        Success = true; // 可以设置为成功或其他状态
                        IsRuning = false;

                        if (ShowLog)
                            LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（提前完成）  ---------------------------------", true);
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
                        return; // 提前退出整个流程
                    }

                    #region 实现IF^Else逻辑

                    // ✅ 让节点返回“下一个要执行的索引”
                    //    -1 表示顺序执行下一个（i+1）
                    //    其他表示跳转到指定索引
                    if (result.NextIndex == -1)
                    {
                        // 顺序执行下一个
                        continue;
                    }
                    else
                    {
                        // 跳转
                        i = result.NextIndex - 1; // for 循环会 +1，所以先 -1
                    }
                    #endregion
                }
                catch (OperationCanceledException ex)
                {
                    Success = false;
                    IsRuning = false;
                    if (ShowLog)
                        LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
                catch (Exception ex)
                {
                    Success = false;
                    IsRuning = false;
                    if (ShowLog)
                        LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
            }

            // 所有节点都执行完毕
            Success = true;
            IsRuning = false;
            if (ShowLog)
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
            if(ShowLog)
                LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
            IsRuning = true;
            Success = false;

            for (int i = 0; i < _nodes.Count; i++)
            {
                NodeBase node = _nodes[i];
                try
                {
                    if (node2Stop == node)
                    {
                        break;
                    }

                    NodeReturn result = await node.Run(Solution.Instance.CancellationToken, ShowLog);
                    RunTime += node.Result.RunTime;

                    // 检查还要不要继续运行
                    if (result.Flag == NodeRunFlag.StopRun)
                    {
                        // 当前节点要求停止后续节点执行
                        Success = true; // 可以设置为成功或其他状态
                        IsRuning = false;

                        if (ShowLog)
                            LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（提前完成）  ---------------------------------", true);
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
                        return; // 提前退出整个流程
                    }

                    #region 实现IF^Else逻辑

                    // ✅ 让节点返回“下一个要执行的索引”
                    //    -1 表示顺序执行下一个（i+1）
                    //    其他表示跳转到指定索引
                    if (result.NextIndex == -1)
                    {
                        // 顺序执行下一个
                        continue;
                    }
                    else
                    {
                        // 跳转
                        i = result.NextIndex - 1; // for 循环会 +1，所以先 -1
                    }
                    #endregion
                }
                catch (OperationCanceledException ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    if (ShowLog)
                        LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
                catch (Exception ex)
                {
                    Success = false;
                    IsRuning = false;
                    RunTime += node.Result.RunTime;
                    if (ShowLog)
                        LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false, ProcessName));
                    throw ex;
                }
            }

            Success = true;
            IsRuning = false;
            if (ShowLog)
                LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true, ProcessName));
        }
    }

    /// <summary>
    /// 流程运行优先级别，同级并行运行
    /// </summary>
    public enum ProcessLvEnum 
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

    public enum ProcessGroup
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
