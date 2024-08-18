using HslCommunication;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YTVisionPro.Hardware.PLC
{
    public interface IPlc : IDevice
    {
        /// <summary>
        /// PLC参数
        /// </summary>
        PLCParms PLCParms { get; set; }

        /// <summary>
        /// PLC连接
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// PLC是否连接
        /// </summary>
        /// <returns></returns>
        bool IsOpen();

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 释放Plc资源
        /// </summary>
        void Release();

    }

    /// <summary>
    /// PLC参数
    /// </summary>
    public struct PLCParms
    {
        public string UserDefinedName; //PLC用户自定义名称
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
        public StopBits StopBits;  // 停止位
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
}
