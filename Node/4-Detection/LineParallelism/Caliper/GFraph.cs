using Logger;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Point = OpenCvSharp.Point;

namespace YTVisionPro.Node._4_Detection.Caliper
{
    /// <summary>
    /// 卡尺类(对应一个卡尺)
    /// </summary>
    public class GFraph
    {
        public int numPoints;  //矩形数量
        public Rectangle[] rectangles; //矩形数组
        public static List<Point[]> LinePoint = new List<Point[]>(); //(拟合成功的线段)端点  多个直线检测节点公用一个直线端点集合
        public List<Point> allEdgePoints = new List<Point>(); //所有边缘点

        public GFraph(int numPoints)
        {
            this.numPoints = numPoints;
            rectangles = new Rectangle[numPoints];

            for (int i = 0; i < this.numPoints; i++)
            {
                rectangles[i] = new Rectangle();
            }
        }

        /// <summary>
        /// 获取直线夹角
        /// </summary>
        public static double AngleOfLine(List<Point[]> linePoints)
        {
            if (linePoints.Count < 2)
            {
                //LogHelper.AddLog(MsgLevel.Exception, "直线太少", true);
                return 0;
            }

            // 取最后两个直线段
            Point[] lastLine = linePoints[linePoints.Count - 1];
            Point[] secondLastLine = linePoints[linePoints.Count - 2];

            // 获取最后一条直线的端点
            double x1 = lastLine[0].X;
            double y1 = lastLine[0].Y;
            double x2 = lastLine[1].X;
            double y2 = lastLine[1].Y;

            // 获取倒数第二条直线的端点
            double X1 = secondLastLine[0].X;
            double Y1 = secondLastLine[0].Y;
            double X2 = secondLastLine[1].X;
            double Y2 = secondLastLine[1].Y;

            // 计算方向向量
            double dx1 = x2 - x1;
            double dy1 = y2 - y1;
            double dx2 = X2 - X1;
            double dy2 = Y2 - Y1;

            // 计算夹角的余弦值
            double dotProduct = (dx1 * dx2) + (dy1 * dy2);
            double magnitude1 = Math.Sqrt(dx1 * dx1 + dy1 * dy1);
            double magnitude2 = Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            double cosTheta = dotProduct / (magnitude1 * magnitude2);

            // 计算角度
            double angleRad = Math.Acos(cosTheta);
            double angleDeg = angleRad * (180 / Math.PI);

            // 显示夹角
            LogHelper.AddLog(MsgLevel.Info, $"夹角为{angleDeg}°", true);

            return angleDeg;
        }


        /// <summary>
        /// 获取直线距离
        /// </summary>
        /// <param name="LinePoint"></param>
        /// <returns></returns>
        public static double StraightLineDistance(List<Point[]> linePoints)
        {
            if (linePoints.Count < 2)
            {
                //LogHelper.AddLog(MsgLevel.Exception, "直线太少", true);
                return 0;
            }

            // 取最后两条直线段
            Point[] lastLine = linePoints[linePoints.Count - 1];
            Point[] secondLastLine = linePoints[linePoints.Count - 2];

            // 获取最后一条直线的端点
            double x1 = lastLine[0].X;
            double y1 = lastLine[0].Y;
            double x2 = lastLine[1].X;
            double y2 = lastLine[1].Y;

            // 获取倒数第二条直线的端点
            double X1 = secondLastLine[0].X;
            double Y1 = secondLastLine[0].Y;
            double X2 = secondLastLine[1].X;
            double Y2 = secondLastLine[1].Y;

            // 计算直线的参数 a, b, c
            double a = Y2 - Y1;
            double b = X1 - X2;
            double c = X2 * Y1 - X1 * Y2;

            // 计算每个端点到直线的垂直距离
            double d1 = Math.Abs(a * x1 + b * y1 + c) / Math.Sqrt(a * a + b * b);
            double d2 = Math.Abs(a * x2 + b * y2 + c) / Math.Sqrt(a * a + b * b);

            // 求出两个距离的差，并取绝对值
            double distanceDifference = Math.Abs(d1 - d2);

            // 显示距离
            LogHelper.AddLog(MsgLevel.Info, $"一条直线两端点分别到另一条直线的距离之差的绝对值：{distanceDifference}", true);

            return distanceDifference;
        }

        ///<summary>
        ///把每个顶点，按照angle旋转一定角度(旋转矩形，直线)
        ///</summary>
        ///<param name="center"></param>
        ///<param name="point"></param>
        ///<param name="angle"></param>
        ///<returns></returns>
        public Point RotatePoint(Point center, Point point, double angle)
        {
            double cosTheta = Math.Cos(angle);
            double sinTheta = Math.Sin(angle);
            int x = (int)(cosTheta * (point.X - center.X) - sinTheta * (point.Y - center.Y) + center.X);
            int y = (int)(sinTheta * (point.X - center.X) + cosTheta * (point.Y - center.Y) + center.Y);
            return new Point(x, y);
        }

        ///<summary>
        ///计算在一条由起点到终点的直线上，某个比例处的点的位置
        ///</summary>
        ///<param name="start"></param>
        ///<param name="end"></param>
        ///<param name="t"></param>
        ///<returns></returns>
        public PointF GetPointOnLine(PointF start, PointF end, float t)
        {
            float x = start.X + t * (end.X - start.X);
            float y = start.Y + t * (end.Y - start.Y);
            return new PointF(x, y);
        }

        ///<summary>
        ///画一个矩形
        ///</summary>
        ///<param name="image"></param>
        ///<param name="start"></param>
        ///<param name="end"></param>
        public double DrawCaliperRectangles(Mat image, Point start, Point end, GFraph gFraph, int i)
        {
            int rectWidth = gFraph.rectangles[0].rectWidth;  //矩形宽
            int rectHeight = gFraph.rectangles[0].rectHeight; //矩形长

            float t = (float)i / numPoints;
            //计算矩形中心点
            PointF pointOnLine = gFraph.GetPointOnLine(new PointF(start.X, start.Y), new PointF(end.X, end.Y), t);
            //转换为需要的类型
            Point center = new Point((int)pointOnLine.X, (int)pointOnLine.Y);
            gFraph.rectangles[i].center = center;

            //计算旋转角度
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X) + Math.PI / 2; //垂直于直线的角度
                                                                                       //计算矩形的四个角点
            Point[] rectanglePoints = new Point[4];
            rectanglePoints[0] = gFraph.RotatePoint(center, new Point(center.X - rectWidth, center.Y - rectHeight / 4), angle);
            rectanglePoints[1] = gFraph.RotatePoint(center, new Point(center.X + rectWidth, center.Y - rectHeight / 4), angle);
            rectanglePoints[2] = gFraph.RotatePoint(center, new Point(center.X + rectWidth, center.Y + rectHeight / 4), angle);
            rectanglePoints[3] = gFraph.RotatePoint(center, new Point(center.X - rectWidth, center.Y + rectHeight / 4), angle);

            //绘制矩形
            Cv2.Polylines(image, new[] { rectanglePoints }, true, Scalar.Blue, 1); //使用蓝色绘制矩形边框
            gFraph.rectangles[i].points.Add(rectanglePoints);
            return angle;
        }

        /// <summary>
        /// 获取一个矩形对应的直线坐标
        /// </summary>
        public (Point, Point) DrawStraightLine(GFraph gFraph, int i, double angle)
        {
            //绘制与矩形角度一致的直线
            Point lineStart = new Point(gFraph.rectangles[i].center.X - gFraph.rectangles[i].rectWidth, gFraph.rectangles[i].center.Y);
            Point lineEnd = new Point(gFraph.rectangles[i].center.X + gFraph.rectangles[i].rectWidth, gFraph.rectangles[i].center.Y);
            lineStart = gFraph.RotatePoint(gFraph.rectangles[i].center, lineStart, angle);
            lineEnd = gFraph.RotatePoint(gFraph.rectangles[i].center, lineEnd, angle);
            return (lineStart, lineEnd);
        }

        /// <summary>
        /// 获取直线的所有像素值并存储灰度值及其位置
        /// </summary>
        public void StoresGrayscaleLocations(Mat colorMat, Point lineStart, Point lineEnd, GFraph gFraph, int i)
        {
            Dictionary<Point, byte> linePixel = PictureProcessing.GetLinePixels(colorMat, lineStart, lineEnd);
            LineInfo lineInfo = new LineInfo
            {
                LinePixels = new List<Dictionary<Point, byte>> { linePixel }
            };
            gFraph.rectangles[i].lineInfo = lineInfo;
        }
    }
}
