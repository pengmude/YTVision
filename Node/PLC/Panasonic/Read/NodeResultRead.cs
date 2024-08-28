using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal class NodeResultRead : INodeResult
    {
        public bool Success { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public Object CodeText { get; set; }
    }
}
