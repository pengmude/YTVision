using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Camera.HiK.WaitSoftTrigger
{
    internal class NodeParamWaitSoftTrigger : INodeParam
    {
        public IPlc Plc { get; set; }
        public string Address { get; set; }
    }
}
