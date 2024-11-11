using System;
using OpenCvSharp;


namespace YTVisionPro.Node._4_Measurement.ParallelLines
{
    public class LineUtils
    {
        public static (double angle, double distanceDifference) CalculateParallelism(LineSegmentPoint line1, LineSegmentPoint line2)
        {
            // 计算两条直线的夹角
            double angle = CalculateAngleBetweenLines(line1, line2);

            // 计算更长线段两端点到更短线段的距离差值
            double distanceDifference = CalculateDistanceDifference(line1, line2);

            return (angle, distanceDifference);
        }

        private static double CalculateAngleBetweenLines(LineSegmentPoint line1, LineSegmentPoint line2)
        {
            // 计算两条直线的方向向量
            Point direction1 = new Point(line1.P2.X - line1.P1.X, line1.P2.Y - line1.P1.Y);
            Point direction2 = new Point(line2.P2.X - line2.P1.X, line2.P2.Y - line2.P1.Y);

            // 计算方向向量的模
            double magnitude1 = Math.Sqrt(direction1.X * direction1.X + direction1.Y * direction1.Y);
            double magnitude2 = Math.Sqrt(direction2.X * direction2.X + direction2.Y * direction2.Y);

            // 计算方向向量的点积
            double dotProduct = direction1.X * direction2.X + direction1.Y * direction2.Y;

            // 计算夹角的余弦值
            double cosTheta = dotProduct / (magnitude1 * magnitude2);

            // 计算夹角（弧度）
            double theta = Math.Acos(cosTheta);

            // 将弧度转换为角度
            double angleInDegrees = theta * (180.0 / Math.PI);

            // 返回0-90度范围内的夹角
            return Math.Min(angleInDegrees, 180.0 - angleInDegrees);
        }

        private static double CalculateDistanceDifference(LineSegmentPoint line1, LineSegmentPoint line2)
        {
            // 计算两条线段的长度
            double length1 = PointDistance(line1.P1, line1.P2);
            double length2 = PointDistance(line2.P1, line2.P2);

            // 确定哪条线段更长
            LineSegmentPoint longerLine = length1 >= length2 ? line1 : line2;
            LineSegmentPoint shorterLine = length1 < length2 ? line1 : line2;

            // 计算更长线段的两个端点到更短线段的距离
            double d1 = PointToLineDistance(longerLine.P1, shorterLine.P1, shorterLine.P2);
            double d2 = PointToLineDistance(longerLine.P2, shorterLine.P1, shorterLine.P2);

            // 返回距离差值的绝对值
            return Math.Abs(d1 - d2);
        }

        public static double PointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private static double PointToLineDistance(Point point, Point lineStart, Point lineEnd)
        {
            // 计算线段的方向向量
            double dx = lineEnd.X - lineStart.X;
            double dy = lineEnd.Y - lineStart.Y;

            // 计算点到线段的垂足
            double t = ((point.X - lineStart.X) * dx + (point.Y - lineStart.Y) * dy) / (dx * dx + dy * dy);
            if (t < 0) t = 0;
            else if (t > 1) t = 1;

            // 计算垂足的坐标
            double footX = lineStart.X + t * dx;
            double footY = lineStart.Y + t * dy;

            // 计算点到垂足的距离
            return Math.Sqrt(Math.Pow(point.X - footX, 2) + Math.Pow(point.Y - footY, 2));
        }
    }
}
