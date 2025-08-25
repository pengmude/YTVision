using Logger;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageCrop
{
    public class NodeImageCrop : NodeBase
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
                        base.CheckTokenCancel(token);

                        var src = form.UpdataImage();
                        var roiImg = form.GetROIImages();
                        if (roiImg == null || roiImg.Count == 0)
                        {
                            throw new Exception("裁切的图像为空，请检查是否超出图像区域！");
                        }
                        var rects = form.GetImageROIRects();
                        NodeResultImageCrop nodeResultImageCrop = new NodeResultImageCrop();
                        nodeResultImageCrop.OutputImage.SrcImg = src;
                        nodeResultImageCrop.OutputImage.Bitmaps = roiImg;
                        nodeResultImageCrop.OutputImage.Rectangles = rects;
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        nodeResultImageCrop.RunTime = time;
                        Result = nodeResultImageCrop;
                        if (showLog)
                            LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms，ROI数量：{rects.Count} 个）", true);
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

            return new NodeReturn(NodeRunFlag.ContinueRun);
        }
    }
}
