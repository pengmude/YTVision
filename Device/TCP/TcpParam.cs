using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJS_Vision.Device.TCP
{
    /// <summary>
    /// TCP设备参数
    /// </summary>
    public class TcpParam
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType;
        public string IP;
        public int Port;
        public string DevName;
        public string UserDefinedName;

        public TcpParam() { }
        public TcpParam(DevType type, string ip, int port, string devName, string userDefinedName)
        {
            DevType = type;
            IP = ip;
            Port = port;
            DevName = devName;
            UserDefinedName = userDefinedName;
        }
    }
}
