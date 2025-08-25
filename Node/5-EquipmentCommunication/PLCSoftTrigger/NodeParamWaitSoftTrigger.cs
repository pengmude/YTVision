using JsonSubTypes;
using Newtonsoft.Json;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PLCSoftTrigger
{
    public class NodeParamWaitSoftTrigger : INodeParam
    {
        [JsonIgnore]
        public IPlc Plc { get; set; }
        /// <summary>
        /// 用于PLC反序列化
        /// </summary>
        public string PlcName {  get; set; }
        public string Address { get; set; }
        public bool Reset { get; set; }

    }
}
