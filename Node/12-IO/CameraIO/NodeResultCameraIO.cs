using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.ImageSrc.CameraIO
{
    internal class NodeResultCameraIO : INodeResult
    {
        public NodeStatus Status { get ; set ; }
        public long RunTime { get ; set ; }
        public NodeRunStatusCode RunStatusCode { get; set; }
    }
}
