using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Hardware.Modbus;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node.Modbus.Read;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeParamModbusWrite : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public ModbusDevice Device { get; set; }
        public ushort Count { get; set; } = 0;
        public ushort StartAddress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RegistersType DataType { get; set; }
    }
}
