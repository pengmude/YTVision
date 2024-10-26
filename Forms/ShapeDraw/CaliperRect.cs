using System;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ShapeDraw
{
    internal class CaliperRect : ROI
    {
        public CaliperRect(RectangleF rect, float angle)
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
        public override ROIType ROIType { get; set; } = ROIType.CaliperRect;

        public override bool Contains(PointF point)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g, Pen pen)
        {
            throw new NotImplementedException();
        }

        public override Bitmap GetROIImage(Bitmap sourceImage, PictureBox pictureBox)
        {
            throw new NotImplementedException();
        }

        public override void Move(PointF delta)
        {
            throw new NotImplementedException();
        }

        public override void Resize(SizeF delta)
        {
            throw new NotImplementedException();
        }

        public override void Rotate(float angle)
        {
            throw new NotImplementedException();
        }
    }
}
