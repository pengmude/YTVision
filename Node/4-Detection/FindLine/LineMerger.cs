using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YTVisionPro.Node._4_Detection.FindLine
{
    public class LineMerger
    {
        private const double AngleThreshold = Math.PI / 18; // 角度阈值，例如10度
        private const double DistanceThreshold = 20; // 距离阈值

        /// <summary>
        /// 检测出来的直线选择
        /// </summary>
        public enum LineSelectionCriteria
        {
            /// <summary>
            /// 所有直线
            /// </summary>
            All,
            /// <summary>
            /// 最长那条
            /// </summary>
            Longest,
            /// <summary>
            /// 最短那条
            /// </summary>
            Shortest,
            /// <summary>
            /// 最上面那条
            /// </summary>
            Topmost,
            /// <summary>
            /// 最下面那条
            /// </summary>
            Bottommost,
            /// <summary>
            /// 最左边那条
            /// </summary>
            Leftmost,
            /// <summary>
            /// 最右边那条
            /// </summary>
            Rightmost
        }

        public static double PointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static LineSegmentPoint MergeLines(List<LineSegmentPoint> lines, LineSelection criteria = LineSelection.Longest)
        {
            List<LineSegmentPoint> allMergedLines = new List<LineSegmentPoint>();

            while (lines.Count > 0)
            {
                LineSegmentPoint currentLine = lines[0];
                List<LineSegmentPoint> similarLines = new List<LineSegmentPoint> { currentLine };

                for (int i = 1; i < lines.Count; i++)
                {
                    if (AreLinesSimilar(currentLine, lines[i]))
                    {
                        similarLines.Add(lines[i]);
                    }
                }

                LineSegmentPoint mergedLine = MergeSimilarLines(similarLines);
                allMergedLines.Add(mergedLine);

                // 移除已经合并的线段
                lines.RemoveAll(line => similarLines.Contains(line));
            }

            // 根据传入的参数筛选出一条直线
            return SelectFinalLine(allMergedLines, criteria);
        }

        private static LineSegmentPoint SelectFinalLine(List<LineSegmentPoint> lines, LineSelection criteria)
        {
            switch (criteria)
            {
                case LineSelection.Longest:
                    return lines.OrderByDescending(line => PointDistance(line.P1, line.P2)).First();
                case LineSelection.Shortest:
                    return lines.OrderBy(line => PointDistance(line.P1, line.P2)).First();
                case LineSelection.Topmost:
                    return lines.OrderBy(line => (line.P1.Y + line.P2.Y) / 2.0).First();
                case LineSelection.Bottommost:
                    return lines.OrderByDescending(line => (line.P1.Y + line.P2.Y) / 2.0).First();
                case LineSelection.Leftmost:
                    return lines.OrderBy(line => (line.P1.X + line.P2.X) / 2.0).First();
                case LineSelection.Rightmost:
                    return lines.OrderByDescending(line => (line.P1.X + line.P2.X) / 2.0).First();
                default:
                    throw new ArgumentException("Invalid LineSelectionCriteria");
            }
        }

        private static LineSegmentPoint MergeSimilarLines(List<LineSegmentPoint> lines)
        {
            // 收集所有相似直线的端点
            List<Point> points = lines.SelectMany(line => new[] { line.P1, line.P2 }).ToList();

            if (points.Count < 2)
            {
                // 如果点的数量少于2个，无法拟合直线
                return lines.First();
            }

            // 计算所有点的平均值
            double centerX = points.Average(p => p.X);
            double centerY = points.Average(p => p.Y);

            // 计算斜率和截距
            double numerator = 0;
            double denominator = 0;

            foreach (var point in points)
            {
                numerator += (point.X - centerX) * (point.Y - centerY);
                denominator += (point.X - centerX) * (point.X - centerX);
            }

            double slope = denominator == 0 ? 0 : numerator / denominator;
            double intercept = centerY - slope * centerX;

            // 确定直线的两个端点
            int minX = points.Min(p => p.X);
            int maxX = points.Max(p => p.X);

            Point startPoint = new Point(minX, (int)(slope * minX + intercept));
            Point endPoint = new Point(maxX, (int)(slope * maxX + intercept));

            return new LineSegmentPoint(startPoint, endPoint);
        }


        //public static List<LineSegmentPoint> MergeLines(List<LineSegmentPoint> lines)
        //{
        //    List<LineSegmentPoint> mergedLines = new List<LineSegmentPoint>();

        //    while (lines.Count > 0)
        //    {
        //        LineSegmentPoint currentLine = lines[0];
        //        List<LineSegmentPoint> similarLines = new List<LineSegmentPoint> { currentLine };

        //        for (int i = 1; i < lines.Count; i++)
        //        {
        //            if (AreLinesSimilar(currentLine, lines[i]))
        //            {
        //                similarLines.Add(lines[i]);
        //            }
        //        }

        //        LineSegmentPoint mergedLine = MergeSimilarLines(similarLines);
        //        mergedLines.Add(mergedLine);

        //        // 移除已经合并的线段
        //        lines.RemoveAll(line => similarLines.Contains(line));
        //    }

        //    return mergedLines;
        //}

        private static bool AreLinesSimilar(LineSegmentPoint line1, LineSegmentPoint line2)
        {
            double angle1 = Math.Atan2(line1.P2.Y - line1.P1.Y, line1.P2.X - line1.P1.X);
            double angle2 = Math.Atan2(line2.P2.Y - line2.P1.Y, line2.P2.X - line2.P1.X);

            double angleDiff = Math.Abs(angle1 - angle2);
            if (angleDiff > Math.PI) angleDiff = 2 * Math.PI - angleDiff;

            if (angleDiff > AngleThreshold) return false;

            double distance1 = PointDistance(line1.P1, line2.P1);
            double distance2 = PointDistance(line1.P1, line2.P2);
            double distance3 = PointDistance(line1.P2, line2.P1);
            double distance4 = PointDistance(line1.P2, line2.P2);

            if (Math.Min(Math.Min(distance1, distance2), Math.Min(distance3, distance4)) < DistanceThreshold)
            {
                return true;
            }

            return false;
        }
    }
}
