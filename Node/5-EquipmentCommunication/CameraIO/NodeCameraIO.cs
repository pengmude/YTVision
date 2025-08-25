using Logger;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._5_EquipmentCommunication.LightOpen
{
    public class NodeCameraIO: NodeBase
    {

        public NodeCameraIO(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormCameraIO();
            Result = new NodeResultCameraIO();
            ParamForm.SetNodeBelong(this);
        }

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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is ParamFormCameraIO from)
            {
                if (ParamForm.Params is NodeParamCameraIO param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        //获取AI检测条件
                        bool con = from.GetCondition();
                        if (con)
                        {
                            // 设置相机线选择器
                            param.Camera.SetLineSelector(param.LineSelector);
                            // 设置相机IO线模式
                            param.Camera.SetLineMode(param.LineMode);
                            // 设置电平反转启用
                            param.Camera.SetLineInverter(true);
                            // 保持时间微秒
                            await MicrosecondDelayAsync(param.HoldTime);
                            // 设置电平反转关闭
                            param.Camera.SetLineInverter(false);
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
                    catch (Exception)
                    {
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败！");
                    }
                }
            }
            return new NodeReturn(NodeRunFlag.StopRun);
        }
        private async Task MicrosecondDelayAsync(int microseconds)
        {
            if (microseconds <= 0)
            {
                return;
            }

            // 使用 Task.Run 来创建一个非阻塞的任务
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // 忙等待直到达到所需的微秒时间
                while (stopwatch.Elapsed.TotalMilliseconds < microseconds / 1000.0)
                {
                    // 使用 Thread.SpinWait(1) 来减少 CPU 占用
                    Thread.SpinWait(1);
                }

                stopwatch.Stop();
            });
        }
    }
}
