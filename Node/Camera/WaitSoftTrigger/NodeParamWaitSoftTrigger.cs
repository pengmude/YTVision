using JsonSubTypes;
using Newtonsoft.Json;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Camera.HiK.WaitSoftTrigger
{
    internal class NodeParamWaitSoftTrigger : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public IPlc Plc { get; set; }
        public string Address { get; set; }
    }
}
