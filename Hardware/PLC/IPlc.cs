using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO.Ports;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Hardware.PLC
{
    internal interface IPlc : IDevice
    {
        /// <summary>
        /// PLC参数
        /// </summary>
        PLCParms PLCParms { get; set; }

        /// <summary>
        /// PLC连接状态
        /// </summary>
        bool IsConnect { get; set; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        string DevName { get; set; }

        /// <summary>
        /// 设备品牌
        /// </summary>
        DeviceBrand Brand { get; set; }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        string UserDefinedName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        DevType DevType { get; set; }
        /// <summary>
        /// 创建设备，反序列化用
        /// </summary>
        void CreateDevice();

        /// <summary>
        /// PLC连接
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 释放Plc资源
        /// </summary>
        void Release();

        /// <summary>
        /// 读取PLC寄存器
        /// </summary>
        /// <returns></returns>
        object ReadPLCData(string address, DataType dataType, ushort length = 0);

        /// <summary>
        /// 写入PLC寄存器
        /// </summary>
        /// <returns></returns>
        void WritePLCData(string address, object value);

    }

    /// <summary>
    /// PLC参数
    /// </summary>
    public struct PLCParms
    {
        public string UserDefinedName; //PLC用户自定义名称
        [JsonConverter(typeof(StringEnumConverter))]
        public PlcConType PlcConType; //PLC通信方式
        public SerialParms SerialParms; //串口连接参数
        public EthernetParms EthernetParms; //网口连接参数
    }

    /// <summary>
    /// 串口连接参数
    /// </summary>
    public struct SerialParms
    {
        public string PortName; // 串口号
        public int BaudRate;  // 波特率
        public int DataBits;  // 数据位
        [JsonConverter(typeof(StringEnumConverter))]
        public StopBits StopBits;  // 停止位
        [JsonConverter(typeof(StringEnumConverter))]
        public Parity Parity; // 校验位
        public SerialParms(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }
    }

    /// <summary>
    /// 网口连接参数
    /// </summary>
    public struct EthernetParms
    {
        public string IP; //IP地址
        public int Port;  //端口号
        public EthernetParms(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
    }

    /// <summary>
    /// PLC通信方式
    /// </summary>
    public enum PlcConType
    {
        COM,
        ETHERNET
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        BOOL,
        INT,
        STRING,
        Bytes
    }
}
