using System.Drawing;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._4_Measurement.ParallelLines
{
    internal class NodeResultParallelLines : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }

        public ResultViewData Result {  get; set; }

        public Bitmap Image { get; set; }
    }
}
