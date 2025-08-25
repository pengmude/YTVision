using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._6_LogicTool.SleepTool
{
    public class NodeSleepTool : NodeBase
    {
        public NodeSleepTool(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormSleepTool();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultConditionRun();
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

            if (ParamForm is NodeParamFormSleepTool form)
            {
                if (form.Params is NodeParamSleepTool param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        // 异步执行睡眠操作
                        await ExecuteSleepAsync(param.Time, token);
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

        private async Task ExecuteSleepAsync(int timeInMilliseconds, CancellationToken token)
        {
            try
            {
                int interval = 200; // 每隔 200 毫秒检查一次取消状态
                int remainingTime = timeInMilliseconds;

                while (remainingTime > 0)
                {
                    // 计算本次等待的时间
                    int thisDelay = Math.Min(remainingTime, interval);

                    // 等待 100 毫秒或剩余时间
                    await Task.Delay(thisDelay, token);

                    // 检查取消状态
                    token.ThrowIfCancellationRequested();

                    // 更新剩余时间
                    remainingTime -= thisDelay;
                }
            }
            catch (OperationCanceledException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
