using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node
{/// <summary>
 /// 鼠标按下结果
 /// </summary>
    public enum ROISelectionState
    {
        None,  // 没有选中 ROI 或 ROI 控制点
        ROIOnly,  // 只选中了 ROI
        TopLeft,  // 选中了左上控制点
        TopRight,  // 选中了右上控制点
        BottomLeft,  // 选中了左下控制点
        BottomRight  // 选中了右下控制点
    }

    public class ROIManager
    {
        private PictureBox pictureBox;
        public List<ROI> ROIs;

        public ROIManager(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            ROIs = new List<ROI>();
        }

        public void AddROI(ROI roi)
        {
            // 清空前面的选中效果
            foreach (var item in ROIs)
            {
                item.IsSelected = false;
            }
            roi.IsSelected = true;
            // 添加ROI
            ROIs.Add(roi);
            pictureBox.Invalidate(); // 重绘 PictureBox
        }

        public void RemoveROI(ROI roi)
        {
            ROIs.Remove(roi);
            pictureBox.Invalidate(); // 重绘 PictureBox
        }

        public void Clear()
        {
            ROIs.Clear();
            pictureBox.Invalidate(); // 重绘 PictureBox
        }

        public List<Bitmap> GetROIImages()
        {
            var roiImages = new List<Bitmap>();
            if (pictureBox.Image != null)
            {
                // 如果没有ROI就返回原图
                if (ROIs.Count == 0)
                {
                    roiImages.Add((Bitmap)pictureBox.Image);
                    return roiImages;
                }

                foreach (var roi in ROIs)
                {
                    roiImages.Add(roi.GetROIImage((Bitmap)pictureBox.Image, pictureBox));
                }
            }
            return roiImages;
        }

        public void DrawROIs(Graphics g, Pen pen)
        {
            foreach (var roi in ROIs)
            {
                roi.Draw(g, pen);
            }
        }

        /// <summary>
        /// 获取PictrueBox的图像缩放倍数
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        private float GetImageScaleFactor(PictureBox pictureBox)
        {
            if (pictureBox.Image == null) return 1.0f;

            // 获取 PictureBox 和图像的实际尺寸
            SizeF pbSize = new SizeF(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
            SizeF imgSize = new SizeF(pictureBox.Image.Width, pictureBox.Image.Height);

            // 计算缩放比例
            float scaleX = pbSize.Width / imgSize.Width;
            float scaleY = pbSize.Height / imgSize.Height;

            // 根据 PictureBox 的 SizeMode 确定实际的缩放比例
            switch (pictureBox.SizeMode)
            {
                case PictureBoxSizeMode.Normal:
                    return 1.0f; // 图像不缩放
                case PictureBoxSizeMode.StretchImage:
                    return Math.Min(scaleX, scaleY); // 按最小的比例缩放
                case PictureBoxSizeMode.CenterImage:
                    return 1.0f; // 图像居中但不缩放
                case PictureBoxSizeMode.Zoom:
                    return Math.Min(scaleX, scaleY); // 按最小的比例缩放
                case PictureBoxSizeMode.AutoSize:
                    return 1.0f; // 控件自动调整大小以适应图像
                default:
                    return 1.0f;
            }
        }

        /// <summary>
        /// 获取鼠标位置对应图像的像素坐标
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="clientPoint"></param>
        /// <returns></returns>
        public Point GetImagePoint(PictureBox pictureBox, Point clientPoint)
        {
            if (pictureBox.Image == null) return clientPoint;

            float scaleFactor = GetImageScaleFactor(pictureBox);

            // 计算图像在 PictureBox 中的位置
            int imageX = (pictureBox.ClientSize.Width - (int)(pictureBox.Image.Width * scaleFactor)) / 2;
            int imageY = (pictureBox.ClientSize.Height - (int)(pictureBox.Image.Height * scaleFactor)) / 2;

            // 考虑偏移量来转换坐标
            Point imagePoint = new Point(
                (int)((clientPoint.X - imageX) / scaleFactor + 0.5f), // 四舍五入
                (int)((clientPoint.Y - imageY) / scaleFactor + 0.5f)); // 四舍五入

            return imagePoint;
        }

        public Point GetClientPoint(PictureBox pictureBox, Point imagePoint)
        {
            if (pictureBox.Image == null) return imagePoint;

            float scaleFactor = GetImageScaleFactor(pictureBox);

            // 计算图像在 PictureBox 中的位置
            int imageX = (pictureBox.ClientSize.Width - (int)(pictureBox.Image.Width * scaleFactor)) / 2;
            int imageY = (pictureBox.ClientSize.Height - (int)(pictureBox.Image.Height * scaleFactor)) / 2;

            // 将图像坐标转换为客户区坐标
            Point clientPoint = new Point(
                (int)(imageX + imagePoint.X * scaleFactor + 0.5f), // 四舍五入
                (int)(imageY + imagePoint.Y * scaleFactor + 0.5f)); // 四舍五入

            return clientPoint;
        }

        /// <summary>
        /// 获取ROI被点击的元素
        /// </summary>
        /// <param name="roi"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static ROISelectionState GetClickedROIItem(ROIManager roiManager, Point point, PictureBox pictrueBox, ref ROI roiSelected)
        {
            ROISelectionState state = ROISelectionState.None;
            // 先清除之前的选中状态
            foreach (var roi in roiManager.ROIs)
                roi.IsSelected = false;

            foreach (var roiItem in roiManager.ROIs)
            {
                if(roiItem is ROI roi)
                {
                    // 先按下了ROI区域
                    if (roi.Contains(point))
                    {
                        roiSelected = roi;
                        state = ROISelectionState.ROIOnly;

                        // 进一步判断按下的是不是控制点小矩形
                        var rects = roi.GetControlPointRects();
                        for (int i = 0; i < rects.Length; i++)
                        {
                            if (rects[i].Contains(point))
                            {
                                switch (i)
                                {
                                    case 0:
                                        state = ROISelectionState.TopLeft;
                                        break;
                                    case 1:
                                        state = ROISelectionState.TopRight;
                                        break;
                                    case 2:
                                        state = ROISelectionState.BottomRight;
                                        break;
                                    case 3:
                                        state = ROISelectionState.BottomLeft;
                                        break;
                                }
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        roiSelected = null;
                        // 判断按下的是不是控制点小矩形
                        var rects = roi.GetControlPointRects();

                        bool flag = false;
                        for (int i = 0; i < rects.Length; i++)
                        {
                            if (rects[i].Contains(point))
                            {
                                switch (i)
                                {
                                    case 0:
                                        state = ROISelectionState.TopLeft;
                                        break;
                                    case 1:
                                        state = ROISelectionState.TopRight;
                                        break;
                                    case 2:
                                        state = ROISelectionState.BottomRight;
                                        break;
                                    case 3:
                                        state = ROISelectionState.BottomLeft;
                                        break;
                                }
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            roiSelected = roi;
                            break;
                        }
                    }
                }
            }
            if (roiSelected != null)
                roiSelected.IsSelected = true;
            pictrueBox.Invalidate();
            return state;
        }
    }
}
