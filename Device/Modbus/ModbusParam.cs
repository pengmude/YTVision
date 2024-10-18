namespace YTVisionPro.Device.Modbus
{
    internal class ModbusParam
    {
        public DevType DevType;
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
