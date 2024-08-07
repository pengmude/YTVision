using System;
using System.IO.Ports;
using System.Net;

namespace YTVisionPro.Hardware.PLC
{
    /// <summary>
    /// 松下Plc
    /// 2024-7-31
    /// by pengmude
    /// </summary>
    public class PlcPanasonic : IPlc
    {
        public PlcPanasonic(string userName)
        {
            UserDefinedName = userName;
        }

        /// <summary>
        /// PLC是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        public string DevName { get; set; }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        public string UserDefinedName { get; set; }

        public DevType DevType { get; } = DevType.PLC;
        public PlcConType _plcConType { get; set; }

        /// <summary>
        /// 串口连接Plc
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        public bool Connenct(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            return false;
        }

        /// <summary>
        /// 网口连接Plc
        /// </summary>
        /// <param name="iPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(IPAddress iPAddress, UInt16 port)
        {

            return false;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public void Disconnect()
        {

        }
    }
}
