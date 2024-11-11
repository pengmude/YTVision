using Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageCrop
{
    internal class NodeImageCrop : NodeBase
    {

        public NodeImageCrop(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormImageCrop(process, this);
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageCrop();
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

            if (ParamForm is NodeParamFormImageCrop form)
            {
                if (ParamForm.Params is NodeParamImageCrop param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.Run(token);

                        form.UpdataImage();
                        var roiImg = form.GetROIImage();
                        if (roiImg == null)
                        {
                            throw new Exception("裁切的图像为空，请检查是否超出图像区域！");
                        }
                        var rect = form.GetImageROIRect();
                        NodeResultImageCrop nodeResultImageCrop = new NodeResultImageCrop();
                        nodeResultImageCrop.Image = roiImg;
                        nodeResultImageCrop.Rectangle = rect;
                        Result = nodeResultImageCrop;
                        SetRunResult(startTime, NodeStatus.Successful);
                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，ROI图像坐标：({rect.X},{rect.Y})，宽：{rect.Width}，高：{rect.Height} )", true);
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
