using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Tool.ImageSave
{
    internal class NodeResultImageSave : INodeResult
    {
        public bool Success { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
