using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YTVisionPro.Node._4_Detection.FindCircle
{
    public class CircleMerger
    {
        /// <summary>
        /// 检测出来的圆选择
        /// </summary>
        public enum CircleSelectionCriteria
        {
            /// <summary>
            /// 所有圆
            /// </summary>
            All,
            /// <summary>
            /// 最大的圆
            /// </summary>
            Largest,
            /// <summary>
            /// 最小的圆
            /// </summary>
            Smallest,
            /// <summary>
            /// 最上面圆
            /// </summary>
            Topmost,
            /// <summary>
            /// 最下面圆
            /// </summary>
            Bottommost,
            /// <summary>
            /// 最左边圆
            /// </summary>
            Leftmost,
            /// <summary>
            /// 最右边圆
            /// </summary>
            Rightmost
        }

        public static double PointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static CircleSegment MergeCircles(List<CircleSegment> circles, CircleSelection criteria = CircleSelection.Largest)
        {
            List<CircleSegment> allMergedCircles = new List<CircleSegment>();

            while (circles.Count > 0)
            {
                CircleSegment currentCircle = circles[0];
                List<CircleSegment> similarCircles = new List<CircleSegment> { currentCircle };

                for (int i = 1; i < circles.Count; i++)
                {
                    if (AreCirclesSimilar(currentCircle, circles[i]))
                    {
                        similarCircles.Add(circles[i]);
                    }
                }

                CircleSegment mergedCircle = MergeSimilarCircles(similarCircles);
                allMergedCircles.Add(mergedCircle);

                // 移除已经合并的圆
                circles.RemoveAll(circle => similarCircles.Contains(circle));
            }

            // 根据传入的参数筛选出一个圆
            return SelectFinalCircle(allMergedCircles, criteria);
        }

        private static CircleSegment SelectFinalCircle(List<CircleSegment> circles, CircleSelection criteria)
        {
            switch (criteria)
            {
                case CircleSelection.Largest:
                    return circles.OrderByDescending(circle => circle.Radius).First();
                case CircleSelection.Smallest:
                    return circles.OrderBy(circle => circle.Radius).First();
                case CircleSelection.Topmost:
                    return circles.OrderBy(circle => circle.Center.Y).First();
                case CircleSelection.Bottommost:
                    return circles.OrderByDescending(circle => circle.Center.Y).First();
                case CircleSelection.Leftmost:
                    return circles.OrderBy(circle => circle.Center.X).First();
                case CircleSelection.Rightmost:
                    return circles.OrderByDescending(circle => circle.Center.X).First();
                default:
                    throw new ArgumentException("Invalid CircleSelectionCriteria");
            }
        }

        private static CircleSegment MergeSimilarCircles(List<CircleSegment> circles)
        {
            // 收集所有相似圆的圆心和半径
            List<Point> centers = circles.Select(circle => (Point)circle.Center).ToList();
            List<double> radii = circles.Select(circle => (double)circle.Radius).ToList();

            if (centers.Count < 1)
            {
                // 如果圆心的数量少于1，无法拟合圆
                return circles.First();
            }

            // 计算所有圆心的平均值
            double centerX = centers.Average(p => p.X);
            double centerY = centers.Average(p => p.Y);

            // 计算所有半径的平均值
            double averageRadius = radii.Average();

            Point center = new Point((int)centerX, (int)centerY);
            double radius = averageRadius;

            return new CircleSegment(center, (float)radius);
        }

        private static bool AreCirclesSimilar(CircleSegment circle1, CircleSegment circle2)
        {
            // 定义圆心距离的阈值
            const double CenterDistanceThreshold = 10.0;
            // 定义半径差异的阈值
            const double RadiusThreshold = 5.0;
            // 计算圆心之间的距离
            double centerDistance = PointDistance((Point)circle1.Center, (Point)circle2.Center);
            // 计算半径之间的差异
            double radiusDiff = Math.Abs(circle1.Radius - circle2.Radius);
            // 判断圆心距离和半径差异是否在阈值范围内
            return centerDistance < CenterDistanceThreshold && radiusDiff < RadiusThreshold;
        }
    }
}
