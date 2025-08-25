using Logger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TDJS_Vision.Node._6_LogicTool.ProcessTrigger
{
    public class NodeProcessTrigger : NodeBase
    {
        Process _process = null;
        public NodeProcessTrigger(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormProcessTrigger(process);
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultProcessTrigger();
            _process = process;
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task<NodeReturn> Run(CancellationToken token, bool showLog)
        {
            DateTime startTime = DateTime.Now;

            // 参数合法性校验
            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return new NodeReturn(NodeRunFlag.StopRun);
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is NodeParamFormProcessTrigger form)
            {
                if (form.Params is NodeParamProcessTrigger param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        // 是否是点击流程编辑界面运行的
                        if (_process.IsHandRun)
                        {
                            var passiveTasks = new ConcurrentBag<Task>(); // 用于追踪本组被触发的被动流程任务
                            // 用于注册触发器的方法（暴露给流程内部）
                            Action<Process> triggerAction = (targetProcess) =>
                            {
                                if (targetProcess == null || !targetProcess.IsPassiveTriggered)
                                    return;

                                var task = targetProcess.RunInternal(
                                    isCyclical: false,
                                    isTriggered: true,
                                    ct: token
                                );

                                passiveTasks.Add(task);
                            };
                            // 注册触发方法
                            _process.SetTriggerAction(triggerAction);

                            // 直接触发还是订阅条件触发
                            if (param.UseOkOrNg)
                            {
                                var isTrue = form.GetBoolValue();
                                if(isTrue)
                                    _process.TriggerProcess(param.OKProcessName);
                                else
                                    _process.TriggerProcess(param.NGProcessName);
                            }
                            else
                            {
                                // 直接触发流程
                                _process.TriggerProcess(param.ProcessName);
                            }
                        }
                        else
                        {
                            // 直接触发还是订阅条件触发
                            if (param.UseOkOrNg)
                            {
                                var isTrue = form.GetBoolValue();
                                if (isTrue)
                                    _process.TriggerProcess(param.OKProcessName);
                                else
                                    _process.TriggerProcess(param.NGProcessName);
                            }
                            else
                            {
                                // 直接触发流程
                                _process.TriggerProcess(param.ProcessName);
                            }
                        }

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
                        throw new Exception($"节点({ID}.{NodeName})运行失败，原因：{ex.Message}");
                    }
                }
            }
            return new NodeReturn(NodeRunFlag.StopRun);
        }
    }
}
