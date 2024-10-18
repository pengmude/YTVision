﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node.PLC.PanasonicRead
{
    internal class NodeParamPlcRead : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public IPlc Plc { get; set; }
        public ushort Length { get; set; } = 0;
        public string Address { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataType DataType { get; set; }
    }
}
