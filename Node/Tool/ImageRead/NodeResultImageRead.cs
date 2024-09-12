using System.Drawing;
using YTVisionPro.Node.Tool.ImageShow;

namespace YTVisionPro.Node.ImageRead
{
    internal class NodeResultImageRead : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public Bitmap Image { get; set; } = null;
    }
}
