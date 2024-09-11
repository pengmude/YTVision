using Logger;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Camera.HiK.WaitHardTrigger
{
    internal class NodeWaitHardTrigger : NodeBase
    {
        public NodeWaitHardTrigger(string nodeName, Process process) : base(nodeName, process)
        {
            ParamForm = new ParamFormWaitHardTrigger();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultWaitHardTrigger();
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

            if (ParamForm is ParamFormWaitHardTrigger form)
            {
                if (form.Params is NodeParamWaitHardTrigger param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        // 设置超时时间为10秒
                        int timeoutMilliseconds = 10;

                        // 空图像对象
                        Bitmap bitmap = null;

                        // 循环等待，当硬触发的图像不空才放行去执行后面的节点
                        while (bitmap == null)
                        {
                            DateTime endTime = DateTime.Now;
                            TimeSpan elapsed = endTime - startTime;
                            long seconds = (long)elapsed.TotalSeconds;
                            if (seconds > 10)
                                throw new Exception("运行超时！");

                            bitmap = form.GetImage();
                        }
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
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
        }
    }
}
