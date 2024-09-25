using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Wirte
{
    internal class NodeParamPlcWrite : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public IPlc Plc { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DataType DataType { get; set; }
        public object Value { get; set; }
    }
}
