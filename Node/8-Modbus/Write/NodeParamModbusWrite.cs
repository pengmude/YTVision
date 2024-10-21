using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Node.Modbus.Read;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeParamModbusWrite : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public IModbus Device { get; set; }
        public ushort Count { get; set; } = 0;
        public ushort StartAddress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RegistersType DataType { get; set; }
    }
}
