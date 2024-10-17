namespace YTVisionPro.Hardware.Modbus
{
    internal class ModbusParam
    {
        public string IP;
        public int Port;
        public string DevName;
        public string UserDefinedName;

        public ModbusParam() { }
        public ModbusParam(string ip, int port, string devName, string userDefinedName) 
        {
            IP = ip;
            Port = port;
            DevName = devName;
            UserDefinedName = userDefinedName;
        }
    }
}
