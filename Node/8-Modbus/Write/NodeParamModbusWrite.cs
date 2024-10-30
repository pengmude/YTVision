using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Node.Modbus.Read;

namespace YTVisionPro.Node.Modbus.Write
{
    internal class NodeParamModbusWrite : INodeParam
    {
        [JsonIgnore]
        public IModbus Device { get; set; }
        public string DeviceName { get; set; }
        /// <summary>
        /// 待发送的数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 标记写入的数据是订阅节点的还是自定义的
        /// </summary>
        public bool IsSubscribed {  get; set; }

        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public ushort StartAddress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RegistersType DataType { get; set; }
        /// <summary>
        /// 异步写入
        /// </summary>
        public bool IsAsync {  get; set; }
    }
}
