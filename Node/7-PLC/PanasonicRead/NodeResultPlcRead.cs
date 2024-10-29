using HslCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node.PLC.PanasonicRead
{
    internal class NodeResultPlcRead : INodeResult
    {
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }
        public NodeRunStatusCode RunStatusCode { get; set; }
        public PlcResult<bool, int, string, byte[]> ReadData { get; set; }
    }
}
