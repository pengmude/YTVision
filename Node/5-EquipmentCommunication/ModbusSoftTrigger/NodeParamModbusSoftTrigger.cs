using Newtonsoft.Json;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead;

namespace TDJS_Vision.Node._5_EquipmentCommunication.ModbusSoftTrigger
{
    public class NodeParamModbusSoftTrigger : INodeParam
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
        /// <summary>
        /// 寄存器类型
        /// </summary>
        public RegistersType Type { get; set; }
        /// <summary>
        /// 是否重置信号
        /// </summary>
        public bool Reset { get; set; }

    }
}
