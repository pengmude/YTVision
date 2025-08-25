using System;
using Newtonsoft.Json;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcRead
{
    public class NodeParamPlcRead : INodeParam
    {
        [JsonIgnore]
        public IPlc Plc { get; set; }
        public string PlcName { get; set; }
        public ushort Length { get; set; } = 0;
        public string Address { get; set; } // 多个地址使用-间隔
        public string DataType { get; set; }
    }
}
