using OpenCvSharp;
using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._3_Detection.FindLine
{
    internal class NodeResultFindLine : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("直线结果")]
        public LineSegmentPoint Line { get; set; }

        [DisplayName("输出图像")]
        public Bitmap OutputImage { get; set; }
    }
}
