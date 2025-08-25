using Logger;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageSplit
{
    public class NodeImageSplit : NodeBase
    {
        public NodeImageSplit(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormImageSplit(process, this);
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultImageSplit();
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
            if (ParamForm is ParamFormImageSplit paramForm)
            {
                if (ParamForm.Params is NodeParamImageSplit param)
                {
                    try
                    {
                        // 初始化状态
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        NodeResultImageSplit res = new NodeResultImageSplit();
                        var img = paramForm.GetImage();
                        var imgs = SplitImage(img, param.Rows, param.Cols);
                        res.OutputImage.Bitmaps = new List<Mat>(imgs);
                        var time = SetRunResult(startTime, NodeStatus.Successful);
                        res.RunTime = time;
                        Result = res;
                        if (showLog)
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

            return new NodeReturn(NodeRunFlag.StopRun);
        }

        /// <summary>
        /// 将一个 Bitmap 图像分成 m 行 n 列的小图像，丢弃剩余的行和列像素。
        /// </summary>
        /// <param name="originalImage">原始图像。</param>
        /// <param name="rows">行数。</param>
        /// <param name="cols">列数。</param>
        /// <returns>包含分割后图像的 List<Bitmap>。</returns>
        public static List<Mat> SplitImage(Bitmap originalImage, int rows, int cols)
        {
            if (originalImage == null)
                throw new ArgumentNullException(nameof(originalImage), "原始图像不能为空。");

            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("行数和列数必须是正整数。");

            // 将 Bitmap 转换为 Mat
            Mat matImage = BitmapConverter.ToMat(originalImage);

            // 计算每个子图像的最大整除宽度和高度
            int baseHeight = matImage.Height / rows * rows;
            int baseWidth = matImage.Width / cols * cols;

            // 每个子图像的实际宽度和高度
            int subHeight = baseHeight / rows;
            int subWidth = baseWidth / cols;

            List<Mat> subImages = new List<Mat>();

            // 遍历行和列来提取子图像
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // 计算当前子图像的左上角坐标
                    int y = row * subHeight;
                    int x = col * subWidth;

                    // 定义 ROI（感兴趣区域）
                    Rect roi = new Rect(x, y, subWidth, subHeight);

                    // 提取子图像
                    Mat subMat = new Mat(matImage, roi);

                    // 将子图像添加到列表中
                    subImages.Add(subMat);
                }
            }

            return subImages;
        }
    }
}
