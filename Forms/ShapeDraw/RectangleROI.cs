using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace TDJS_Vision.Forms.ShapeDraw
{
    public class RectangleROI : ROI
    {
        public string ClassName { get; set; } = typeof(RectangleROI).FullName;
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

        public override Mat GetROIImage(Bitmap sourceImage, PictureBox pictureBox)
        {
            try
            {
                var mat = sourceImage.ToMat();
                Rect imgRect = ConvertClientRectToImageRect(_rect, pictureBox);
                var ret = new Mat(mat, imgRect);
                return ret;
                //return sourceImage.Clone(imgRect, sourceImage.PixelFormat);
            }
            catch (Exception)
            {
                return null;
            }
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
}
