using OpenCvSharp;
using System.Collections.Generic;

namespace YTVisionPro.Node._4_Measurement.ParallelLines
{
    internal class NodeParamParallelLines : INodeParam
    {
        public List<LineSegmentPoint> Lines1 = new List<LineSegmentPoint>();
        public List<LineSegmentPoint> Lines2 = new List<LineSegmentPoint>();
        /// <summary>
        /// 超过这个角度认定为不平行
        /// </summary>
        public double MaxAngle {  get; set; }
        public bool MaxAngleEnable { get; set; }
        /// <summary>
        /// 超过这个距离偏差认定为不平行
        /// “距离偏差”定义：更长的线段两端点分别到更短线段的距离d1和d2,取d1-d2的绝对值
        /// </summary>
        public double MaxDistanceDeviation { get; set; }
        public bool MaxDistanceDeviationEnable { get; set; }
        public string Text1 {  get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }

        public string Text5 { get; set; }

        public string Text6 { get; set; }
    }
}
