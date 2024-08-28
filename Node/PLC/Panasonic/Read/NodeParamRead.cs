using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal class NodeParamRead : INodeParam
    {
        public IPlc Plc { get; set; }
        public ushort Length { get; set; }
        public string Address { get; set; }
        public DataType DataType { get; set; }
    }
}
