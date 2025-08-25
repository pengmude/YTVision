using Newtonsoft.Json;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcWirte
{
    public class NodeParamPlcWrite : INodeParam
    {
        [JsonIgnore]
        public IPlc Plc { get; set; }
        public string PlcName { get; set; }
        public string Address { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
    }
}
