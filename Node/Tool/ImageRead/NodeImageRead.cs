using Logger;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.Camera.HiK;

namespace YTVisionPro.Node.ImageRead
{
    internal class NodeImageRead : NodeBase
    {
        /// <summary>
        /// 图像发布事件
        /// </summary>
        public static event EventHandler<Paramsa> ImageShowChanged;
        // 读写锁保护相同图像文件的多线程访问安全
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        public NodeImageRead(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType) 
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
                        Bitmap bitmap = new Bitmap(param.ImagePath);
                        ImageShowChanged?.Invoke(this, new Paramsa(param.WindowName, bitmap));
                        res.Image = bitmap;
                        Result = res;
                        _lock.ExitReadLock();

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
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
