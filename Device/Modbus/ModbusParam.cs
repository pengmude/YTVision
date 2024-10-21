using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace YTVisionPro.Device.Modbus
{
    internal class ModbusParam
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType;
        /// <summary>
        /// 站号
        /// </summary>
        public byte ID;
        public string IP;
        public int Port;
        public string DevName;
        public string UserDefinedName;

        public ModbusParam() { }
        public ModbusParam(DevType type, string ip, int port, string devName, string userDefinedName) 
        {
            DevType = type;
            IP = ip;
            Port = port;
            DevName = devName;
            UserDefinedName = userDefinedName;
        }
    }
}
