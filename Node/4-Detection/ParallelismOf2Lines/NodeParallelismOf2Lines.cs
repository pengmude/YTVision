using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.ParallelismOf2Lines
{
    /// <summary>
    /// 直线查找节点
    /// </summary>
    internal class NodeParallelismOf2Lines : NodeBase
    {
        public NodeParallelismOf2Lines(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormParallelismOf2Lines();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultParallelismOf2Lines();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;
            // 参数合法性校验
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

            if (ParamForm is NodeParamFormParallelismOf2Lines form)
            {
                if (ParamForm.Params is NodeParamParallelismOf2Lines param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);


                        //form.UpdataImage();
                        //var roiImg = form.GetROIImage();
                        //NodeResultImageCrop nodeResultImageCrop = new NodeResultImageCrop();
                        //nodeResultImageCrop.Image = roiImg;
                        //Result = nodeResultImageCrop;

                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
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
