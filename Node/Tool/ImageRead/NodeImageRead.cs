using Logger;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ImageRead
{
    internal class NodeImageRead : NodeBase
    {
        // 读写锁保护相同图像文件的多线程访问安全
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        public NodeImageRead(string nodeName, Process process, NodeType nodeType) : base(nodeName, process, nodeType) 
        {
            ParamForm = new ParamFormImageRead();
            Result = new NodeResultImageRead();
        }

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
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if(ParamForm.Params is NodeParamImageRead param)
            {
                if (Result is NodeResultImageRead res)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        _lock.EnterReadLock();
                        await Task.Run(() =>
                        {
                            res.Image = new Bitmap(param.ImagePath);
                            Result = res;
                        }, token);
                        _lock.ExitReadLock();

                        SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！", true);
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
            
        }
    }
}
