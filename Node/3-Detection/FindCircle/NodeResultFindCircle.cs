using OpenCvSharp;
using System.ComponentModel;
using System.Drawing;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._3_Detection.FindCircle
{
    internal class NodeResultFindCircle : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("找到的圆")]
        public CircleSegment Circle { get; set; }

        [DisplayName("输出图像")]
        public Bitmap OutputImage { get; set; }

        [DisplayName("算法结果")]
        public ResultViewData Result { get; set; }
    }
}
