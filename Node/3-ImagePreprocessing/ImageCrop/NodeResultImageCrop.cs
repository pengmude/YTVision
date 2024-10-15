using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.ImagePreprocessing.ImageCrop
{
    internal class NodeResultImageCrop : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }

        public Bitmap Image { get; set; }
    }
}
