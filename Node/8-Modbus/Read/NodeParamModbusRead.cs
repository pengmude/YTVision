using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Hardware.Modbus;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Modbus.Read
{
    internal class NodeParamModbusRead : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public ModbusDevice Device { get; set; }
        public ushort Count { get; set; } = 0;
        public ushort StartAddress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RegistersType DataType { get; set; }
    }

    /// <summary>
    /// Modbus四种寄存器类别
    /// </summary>
    internal enum RegistersType
    {
        /// <summary>
        /// 线圈，访问长度bit，读写
        /// </summary>
        Coils,
        /// <summary>
        /// 离散量输入，访问长度bit，只读
        /// </summary>
        DiscreteInput,
        /// <summary>
        /// 输入寄存器，访问长度word，只读
        /// </summary>
        InputRegisters,
        /// <summary>
        /// 保存寄存器，访问长度word，读写
        /// </summary>
        HoldingRegisters
    }

}
