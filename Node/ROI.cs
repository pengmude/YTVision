using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static YTVisionPro.Node.ImageROIEditControl;
using Newtonsoft.Json.Converters;

namespace YTVisionPro.Node
{

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

    public class RectangleROI : ROI
    {
        public override ROIType ROIType { get; set; } = ROIType.Rectangle;

        public override RectangleF Rectangle { get => _rect; set => _rect = value; }
        public RectangleROI(RectangleF rect, float angle)
        {
            _rect = rect;
            RotationAngle = angle;
            int i = 0;
            foreach (var point in GetControlPoints())
            {
                ControlPointRects[i] = new RectangleF(point.X - ControlPointSize / 2, point.Y - ControlPointSize / 2, ControlPointSize, ControlPointSize);
                i++;
            }
        }



        public override void Draw(Graphics g, Pen pen)
        {
            if (IsSelected)
            {
                // 画出矩形框
                g.DrawRectangle(pen, new Rectangle((int)_rect.X, (int)_rect.Y, (int)_rect.Width, (int)_rect.Height));
                // 画出控制点小矩形
                foreach (var rect in GetControlPointRects())
                {
                    g.FillRectangle(Brushes.White, rect);
                    // 画出控制点坐标
                    //Font drawFont = new Font("Arial", 16);
                    //g.DrawString($"({rect.X},{rect.Y})", drawFont, Brushes.Green, new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
                }
            }
            else
            {
                // 正常绘制
                g.DrawRectangle(pen, new Rectangle((int)_rect.X, (int)_rect.Y, (int)_rect.Width, (int)_rect.Height));
            }
        }

        public override Bitmap GetROIImage(Bitmap sourceImage, PictureBox pictureBox)
        {
            try
            {
                Rectangle imgRect = ConvertClientRectToImageRect(_rect, pictureBox);
                return sourceImage.Clone(imgRect, sourceImage.PixelFormat);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 客户端框选的矩形转化为图像上选中的实际矩形
        /// </summary>
        /// <param name="clientRect"></param>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        private Rectangle ConvertClientRectToImageRect(RectangleF clientRect, PictureBox pictureBox)
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

        public override bool Contains(PointF point)
        {
            return _rect.Contains(point.X, point.Y);
        }

        public override void Resize(SizeF delta)
        {
            //UpdateRect(new RectangleF(0, 0, Rectangle.Width + delta.Width, Rectangle.Height + delta.Height));
            _rect = new RectangleF(0, 0, Rectangle.Width + delta.Width, Rectangle.Height + delta.Height);
        }

        public override void Move(PointF delta)
        {
            //UpdateRect(new RectangleF(Rectangle.X + delta.X, Rectangle.Y + delta.Y, Rectangle.Width, Rectangle.Height));
            _rect = new RectangleF(Rectangle.X + delta.X, Rectangle.Y + delta.Y, Rectangle.Width, Rectangle.Height);
        }

        public override void Rotate(float angle)
        {
            // 对于矩形来说，旋转可能涉及更复杂的计算
            // 这里简化为直接设置角度
            RotationAngle = angle;
        }
    }

    public class CircleROI : ROI
    {
        public override ROIType ROIType { get; set; } = ROIType.Circle;
        public PointF Center { get; set; }
        public int Radius { get; set; }

        public override RectangleF Rectangle { get => _rect; set { UpdateCircle(value); } }

        /// <summary>
        /// 通过外接矩形调整圆形ROI的大小
        /// </summary>
        /// <param name="rectangleF"></param>
        private void UpdateCircle(RectangleF rectangleF)
        {
            _rect = rectangleF;
            var values = GetMaxInscribedCircleAfterRotation(rectangleF);
            Center = values.Center;
            Radius = values.Radius;
        }

        public CircleROI(RectangleF rectangleF, float angle)
        {
            _rect = rectangleF;
            RotationAngle = angle;
            var values = GetMaxInscribedCircleAfterRotation(rectangleF, angle);
            Center = values.Center;
            Radius = values.Radius;
        }


        public override void Draw(Graphics g, Pen pen)
        {
            if (IsSelected)
            {
                g.DrawEllipse(pen, new RectangleF(Center.X - Radius, Center.Y - Radius, 2 * Radius, 2 * Radius));
                pen.Color = Color.Blue;
                pen.DashStyle = DashStyle.Dot;
                // 画出矩形框
                g.DrawRectangle(pen, new Rectangle((int)Rectangle.X, (int)Rectangle.Y, (int)Rectangle.Width, (int)Rectangle.Height));
                // 画出控制点小矩形
                foreach (var rect in GetControlPointRects())
                {
                    g.FillRectangle(Brushes.White, rect);
                }
            }
            else
            {
                g.DrawEllipse(pen, new RectangleF(Center.X - Radius, Center.Y - Radius, 2 * Radius, 2 * Radius));
            }
        }

        public override Bitmap GetROIImage(Bitmap sourceImage, PictureBox pictureBox)
        {
            return ExtractCircularROI(pictureBox, Center, Radius);
        }

        /// <summary>
        /// 客户端框选的圆形转化为图像上选中的实际圆形
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="clientCenter"></param>
        /// <param name="clientRadius"></param>
        /// <returns></returns>
        private Bitmap ExtractCircularROI(PictureBox pictureBox, PointF clientCenter, int clientRadius)
        {
            // 计算当前缩放比例
            float zoomFactor = (float)pictureBox.Image.Width / pictureBox.ClientSize.Width;
            if (pictureBox.Image.Height * pictureBox.ClientSize.Width > pictureBox.Image.Width * pictureBox.ClientSize.Height)
            {
                zoomFactor = (float)pictureBox.Image.Height / pictureBox.ClientSize.Height;
            }

            // 转换客户端坐标到图像坐标
            Point imageCenter = new Point(
                (int)((clientCenter.X - (pictureBox.ClientSize.Width - pictureBox.Image.Width / zoomFactor) / 2) * zoomFactor),
                (int)((clientCenter.Y - (pictureBox.ClientSize.Height - pictureBox.Image.Height / zoomFactor) / 2) * zoomFactor)
            );
            int imageRadius = (int)(clientRadius * zoomFactor);

            // 创建新的Bitmap用于保存圆形区域
            int diameter = 2 * imageRadius;
            Bitmap roiImage = new Bitmap(diameter, diameter);
            using (Graphics g = Graphics.FromImage(roiImage))
            {
                // 使用GraphicsPath定义圆形路径
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, diameter, diameter);

                    // 设置抗锯齿模式
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // 使用Region设置绘图区域为圆形
                    using (Region region = new Region(path))
                    {
                        g.Clip = region;
                        // 绘制原始图像到新Bitmap中，注意调整绘制位置
                        g.DrawImage(pictureBox.Image,
                            new Rectangle(0, 0, diameter, diameter),
                            new Rectangle(imageCenter.X - imageRadius, imageCenter.Y - imageRadius, diameter, diameter),
                            GraphicsUnit.Pixel);
                    }
                }
            }

            return roiImage;
        }

        public override bool Contains(PointF point)
       {
            // 计算点到圆心的距离平方
            double distanceSquared = Math.Pow((double)(point.X - Center.X), 2) + Math.Pow((point.Y - Center.Y), 2);

            // 比较距离平方与半径平方
            return distanceSquared <= Radius * Radius;
        }

        public override void Resize(SizeF delta)
        {
            _rect = new RectangleF(0, 0, Rectangle.Width + delta.Width, Rectangle.Height + delta.Height);
            (Center, Radius) = GetMaxInscribedCircleAfterRotation(_rect);
        }

        public override void Move(PointF delta)
        {
            _rect = new RectangleF(Rectangle.X + delta.X, Rectangle.Y + delta.Y, Rectangle.Width, Rectangle.Height);
            (Center, Radius) = GetMaxInscribedCircleAfterRotation(_rect);
        }

        public override void Rotate(float angle)
        {
            // 对于圆形来说，旋转和不旋转一个样的
        }

        /// <summary>
        /// 更新圆心和半径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="angleDegrees"></param>
        /// <returns></returns>
        public static (PointF Center, int Radius) GetMaxInscribedCircleAfterRotation(RectangleF rect, float angleDegrees=0)
        {
            // 计算旋转前矩形的中心点
            PointF center = new PointF(
                rect.Left + (rect.Width / 2),
                rect.Top + (rect.Height / 2)
            );

            // 将角度从度转换为弧度
            float angleRadians = angleDegrees * (float)(Math.PI / 180.0);

            // 创建一个矩阵来进行旋转变换
            Matrix matrix = new Matrix();
            matrix.RotateAt(angleDegrees, center);

            // 获取矩形的四个顶点
            PointF[] points = new PointF[]
            {
            new PointF(rect.Left, rect.Top),
            new PointF(rect.Right, rect.Top),
            new PointF(rect.Right, rect.Bottom),
            new PointF(rect.Left, rect.Bottom)
            };

            // 应用旋转变换到顶点
            matrix.TransformPoints(points);

            // 计算旋转后矩形的最小外接矩形
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (PointF point in points)
            {
                if (point.X < minX) minX = point.X;
                if (point.X > maxX) maxX = point.X;
                if (point.Y < minY) minY = point.Y;
                if (point.Y > maxY) maxY = point.Y;
            }

            // 计算旋转后矩形的宽度和高度
            float width = maxX - minX;
            float height = maxY - minY;

            // 计算旋转后矩形的最大内接圆的半径
            int radius = (int)Math.Min(width, height) / 2;

            // 返回旋转后的中心点和半径
            return (center, radius);
        }
    }
}
