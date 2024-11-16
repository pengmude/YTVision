using OpenCvSharp;
using System.ComponentModel;
using System.Drawing;

namespace YTVisionPro.Node._3_Detection.MatchTemplate
{
    internal class NodeResultMatchTemplate : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("匹配结果图像")]
        public Bitmap Bitmap { get; set; }

        [DisplayName("匹配的位置")]
        public Rect Rect { get; set; }

        [DisplayName("匹配得分")]
        public double Score { get; set; }

        [DisplayName("匹配是否OK")]
        public bool IsOk { get; set; }
    }
}
