using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static YTVisionPro.Forms.ShapeDraw.ImageROIEditControl;

namespace YTVisionPro.Forms.ShapeDraw
{
    public enum ROIType
    {
        /// <summary>
        /// 空类型
        /// </summary>
        None,
        /// <summary>
        /// 单纯矩形框
        /// </summary>
        Rectangle,
        /// <summary>
        /// 单纯一个圆
        /// </summary>
        Circle,
        /// <summary>
        /// 带卡尺的圆
        /// </summary>
        CaliperCircle,
        /// <summary>
        /// 带卡尺的矩形
        /// </summary>
        CaliperRect
    }
    public abstract class ROI
    {
        /// <summary>
        /// ROI 是否选中
        /// </summary>
        public bool IsSelected { get; set; }
        protected RectangleF _rect { get; set; }
        /// <summary>
        /// ROI的最小外接矩形
        /// </summary>
        public virtual RectangleF Rectangle { get; set; }
        /// <summary>
        /// ROI旋转角度
        /// </summary>
        public float RotationAngle { get; set; }
        /// <summary>
        /// 4个正方形缩放控制点边长的像素大小
        /// </summary>
        public static readonly int ControlPointSize = 8;
        /// <summary>
        /// 四个控制点矩形
        /// </summary>
        protected RectangleF[] ControlPointRects = new RectangleF[4];

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract ROIType ROIType { get; set; }

        public abstract void Draw(Graphics g, Pen pen);
        public abstract Bitmap GetROIImage(Bitmap sourceImage, PictureBox pictureBox);

        /// <summary>
        /// 获取图像坐标系下的ROI
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        public Rectangle GetROIRect(PictureBox pictureBox)
        {
            return ConvertClientRectToImageRect(_rect, pictureBox);
        }

        /// <summary>
        /// 客户端框选的矩形转化为图像上选中的实际矩形
        /// </summary>
        /// <param name="clientRect"></param>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        protected Rectangle ConvertClientRectToImageRect(RectangleF clientRect, PictureBox pictureBox)
        {
            // 获取当前缩放比例
            float zoomFactor = (float)pictureBox.Image.Width / pictureBox.ClientSize.Width;

            // 如果图片的高度比宽度更限制，则以高度计算缩放比例
            if (pictureBox.Image.Height * pictureBox.ClientSize.Width > pictureBox.Image.Width * pictureBox.ClientSize.Height)
            {
                zoomFactor = (float)pictureBox.Image.Height / pictureBox.ClientSize.Height;
            }

            // 计算图像上的左上角点位置
            Point imageTopLeft = new Point(
                (int)((clientRect.Left - (pictureBox.ClientSize.Width - pictureBox.Image.Width / zoomFactor) / 2) * zoomFactor),
                (int)((clientRect.Top - (pictureBox.ClientSize.Height - pictureBox.Image.Height / zoomFactor) / 2) * zoomFactor)
            );

            // 创建并返回图像坐标中的矩形
            return new Rectangle(
                imageTopLeft,
                new Size((int)(clientRect.Width * zoomFactor), (int)(clientRect.Height * zoomFactor))
            );
        }

        public static RectangleF ConvertImageRectToClientRect(RectangleF imageRect, PictureBox pictureBox)
        {
            // 获取当前缩放比例
            float zoomFactor = (float)pictureBox.Image.Width / pictureBox.ClientSize.Width;

            // 如果图片的高度比宽度更限制，则以高度计算缩放比例
            if (pictureBox.Image.Height * pictureBox.ClientSize.Width > pictureBox.Image.Width * pictureBox.ClientSize.Height)
            {
                zoomFactor = (float)pictureBox.Image.Height / pictureBox.ClientSize.Height;
            }

            // 计算图像上的左上角点位置相对于客户端区域的偏移
            PointF clientOffset = new PointF(
                (pictureBox.ClientSize.Width - pictureBox.Image.Width / zoomFactor) / 2,
                (pictureBox.ClientSize.Height - pictureBox.Image.Height / zoomFactor) / 2
            );

            // 将图像坐标转换为客户区坐标
            float x = (imageRect.X / zoomFactor) + clientOffset.X;
            float y = (imageRect.Y / zoomFactor) + clientOffset.Y;
            float width = imageRect.Width / zoomFactor;
            float height = imageRect.Height / zoomFactor;

            // 创建并返回新的RectangleF
            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// 获取控制点的位置
        /// </summary>
        /// <returns></returns>
        public PointF[] GetControlPoints()
        {
            return new PointF[]
            {
            new PointF(_rect.Left, _rect.Top),          // 左上
            new PointF(_rect.Right, _rect.Top),         // 右上
            new PointF(_rect.Right, _rect.Bottom),      // 右下
            new PointF(_rect.Left, _rect.Bottom),       // 左下
            //new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2) // 中心
            };
        }

        // 实现ROI的鼠标单击选中、设置大小、移动和旋转功能
        public abstract bool Contains(PointF point);

        public void UpdateRect(RectangleF rectangleF)
        {
            _rect = rectangleF;
            Rectangle = rectangleF;
        }

        /// <summary>
        /// 获取四个控制点的矩形
        /// </summary>
        /// <returns></returns>
        public RectangleF[] GetControlPointRects()
        {
            int i = 0;
            foreach (var point in GetControlPoints())
            {
                ControlPointRects[i] = new RectangleF(point.X - ControlPointSize / 2, point.Y - ControlPointSize / 2, ControlPointSize, ControlPointSize);
                i++;
            }
            return ControlPointRects;
        }
        public abstract void Resize(SizeF delta);
        public abstract void Move(PointF delta);
        public abstract void Rotate(float angle);
    }
}
