using Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YTVisionPro.Device.Camera;
using YTVisionPro.Device.Light;
using YTVisionPro.Device.PLC;
using YTVisionPro.Node;

namespace YTVisionPro
{
    public struct ProcessRunResult 
    {
        public bool IsRunning;
        public bool IsSuccess;
        public ProcessRunResult(bool  isRunning, bool isSuccess)
        {
            IsRunning = isRunning;
            IsSuccess = isSuccess;
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
        public async Task Run(bool isCyclical, CancellationToken token = default(CancellationToken))
        {
            if (Nodes.Count == 0)
                return;

            do
            {
                RunTime = 0;
                if (Enable)
                {
                    // 界面运行按钮Enable设置为运行中
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false));

                    LogHelper.AddLog(MsgLevel.Info, $"-----------------------------------------------------  【{ProcessName}】（开始）  -----------------------------------------------------", true);
                    IsRuning = true;
                    Success = false;
                    foreach (var node in _nodes)
                    {
                        try
                        {
                            await node.Run(token);
                            RunTime += node.Result.RunTime;
                        }
                        catch (OperationCanceledException ex)
                        {
                            Success = false;
                            IsRuning = false;
                            RunTime += node.Result.RunTime;
                            LogHelper.AddLog(MsgLevel.Warn, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（运行中断）  ---------------------------------", true);
                            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false)); 
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            Success = false;
                            IsRuning = false;
                            RunTime += node.Result.RunTime;
                            LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                            UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false));
                            throw ex;
                        }
                    }

                    Success = true;
                    IsRuning = false;
                    LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
                    UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true));
                }
                if (isCyclical)
                {
                    await Task.Delay(Solution.Instance.RunInterval, token);
                }
            } while (isCyclical && !token.IsCancellationRequested);
            
            // 重置运行取消令牌
            Solution.Instance.ResetTokenSource();
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
            // 节点数为0不运行
            if (Nodes.Count == 0)
                return;

            RunTime = 0;
            if (Enable)
            {
                // 更新运行状态
                UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false));
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
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false)); 
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        Success = false;
                        IsRuning = false;
                        RunTime += node.Result.RunTime;
                        LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false)); 
                        throw ex;
                    }
                }

                Success = true;
                IsRuning = false;
                LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
                UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true));
            }
        }

        /// <summary>
        /// 专用于需要订阅刷新前一个节点图像的节点
        /// 要在参数节点处停止运行
        /// </summary>
        public async Task RunForUpdateImages(NodeBase node2Stop)
        {
            // 重置运行取消令牌
            Solution.Instance.ResetTokenSource();
            // 节点数为0不运行
            if (Nodes.Count == 0)
                return;

            RunTime = 0;
            if (Enable)
            {
                // 更新运行状态
                UpdateRunStatus?.Invoke(this, new ProcessRunResult(true, false));
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
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false));
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        Success = false;
                        IsRuning = false;
                        RunTime += node.Result.RunTime;
                        LogHelper.AddLog(MsgLevel.Exception, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（失败）  ---------------------------------", true);
                        UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, false));
                        throw ex;
                    }
                }

                Success = true;
                IsRuning = false;
                LogHelper.AddLog(MsgLevel.Info, $"---------------------------------  【{ProcessName}】（结束） 【耗时】（{RunTime}ms） 【状态】（成功）  ---------------------------------", true);
                UpdateRunStatus?.Invoke(this, new ProcessRunResult(false, true));
            }
        }
    }
}
