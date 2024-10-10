using System.Drawing;

namespace YTVisionPro.Node.ImageSrc.ImageRead
{
    internal class NodeResultImageRead : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public Bitmap Image { get; set; } = null;
    }
}
