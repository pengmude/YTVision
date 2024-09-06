using System.Drawing;

namespace YTVisionPro.Node.ImageRead
{
    internal class NodeResultImageRead : INodeResult
    {
        public bool Success { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }

        public Bitmap Bitmap { get; set; }
    }
}
