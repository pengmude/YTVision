using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Wirte
{
    internal class NodeParamPlcWrite : INodeParam
    {
        public IPlc Plc { get; set; }
        public string Address { get; set; }
        public DataType DataType { get; set; }
        public object Value { get; set; }
    }
}
