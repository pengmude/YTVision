using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ShapeDraw
{
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
        public static (PointF Center, int Radius) GetMaxInscribedCircleAfterRotation(RectangleF rect, float angleDegrees = 0)
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
