using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeResultModbusWrite : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public object ReadData { get; set; }
    }
}
