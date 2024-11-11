using OpenCvSharp;
using System.Drawing;

namespace YTVisionPro.Node._3_Detection.FindLine
{
    internal class NodeResultFindLine : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// 检测出来的直线
        /// </summary>
        public LineSegmentPoint Line { get; set; }
        /// <summary>
        /// 输出图像
        /// </summary>
        public Bitmap OutputImage { get; set; }
    }
}
