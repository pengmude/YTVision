using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal class NodeParamPlcRead : INodeParam
    {
        public IPlc Plc { get; set; }
        public ushort Length { get; set; } = 0;
        public string Address { get; set; }

        public DataType DataType { get; set; }
    }
}
