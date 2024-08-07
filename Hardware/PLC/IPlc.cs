using System;
using System.IO.Ports;
using System.Net;

namespace YTVisionPro.Hardware.PLC
{
    public interface IPlc : IDevice
    {
        /// <summary>
        /// PLC是否打开
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 硬件硬件名称
        /// </summary>
        string DevName { get; set; }

        /// <summary>
        /// 用户自定义设备名
        /// </summary>
        string UserDefinedName { get; set; }

        DevType DevType { get; }
        /// <summary>
        /// Plc连接方式
        /// </summary>
        PlcConType _plcConType { get; set; }

        /// <summary>
        /// 串口连接
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        bool Connenct(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity);

        /// <summary>
        /// 网口连接
        /// </summary>
        /// <param name="iPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        bool Connect(IPAddress iPAddress, UInt16 port);

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        void Disconnect();

        // TODO:读写PLC的操作

    }

    /// <summary>
    /// Plc连接方式
    /// </summary>
    public enum PlcConType
    {
        COM,
        ETHERNET
    }

    public struct Signal
    {
        /// <summary>
        /// 准备信号
        /// </summary>
        public string Prepare { get; set; }

        /// <summary>
        /// 拍照信号
        /// </summary>
        public string Shot { get; set; }

        //public int  { get; set;}

    }
}
