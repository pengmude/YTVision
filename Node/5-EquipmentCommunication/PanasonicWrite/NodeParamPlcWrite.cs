﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.PanasonicWirte
{
    internal class NodeParamPlcWrite : INodeParam
    {
        [JsonIgnore]
        public IPlc Plc { get; set; }
        public string PlcName { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DataType DataType { get; set; }
        public object Value { get; set; }

        public bool IsSubscribed { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
    }
}
