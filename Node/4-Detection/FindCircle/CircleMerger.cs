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

        public static CircleSegment MergeCircles(List<CircleSegment> circles, CircleSelection criteria = CircleSelection.Largest)
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
    }
}