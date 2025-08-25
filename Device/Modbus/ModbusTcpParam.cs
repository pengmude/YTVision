using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using TDJS_Vision.Device.Camera;

namespace TDJS_Vision.Device.Modbus
{
    /// <summary>
    /// Modbus TCP 主站和从站共用参数类
    /// </summary>
    public class ModbusTcpParam : IModbusParam
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; }
        public string DevName { get; set; }
        public string UserDefinedName { get; set; }
        public ushort HoldTime { get; set; } = 30;
        /// <summary>
        /// 站号
        /// </summary>
        public byte ID { get; set; }
        public string IP;
        public int Port;

        public string ClassName { get; set; } = typeof(ModbusTcpParam).FullName;

        public ModbusTcpParam() { }
        public ModbusTcpParam(DevType type, string ip, int port, string devName, string userDefinedName) 
        {
            DevType = type;
            IP = ip;
            Port = port;
            DevName = devName;
            UserDefinedName = userDefinedName;
        }
    }
}
