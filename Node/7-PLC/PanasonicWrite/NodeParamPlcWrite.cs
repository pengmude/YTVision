using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node.PLC.PanasonicWirte
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
