using Newtonsoft.Json;
using YTVisionPro.Device.Modbus;

namespace YTVisionPro.Node.Modbus.ModbusSoftTrigger
{
    internal class NodeParamModbusSoftTrigger : INodeParam
    {
        /// <summary>
        /// modbus对象
        /// </summary>
        [JsonIgnore]
        public IModbus modbus { get; set; }
        /// <summary>
        /// 当前选择用户名称
        /// </summary>
        public string ModBusName { get; set; }
        /// <summary>
        /// 触点地址
        /// </summary>
        public string Address { get; set; }
    }
}
