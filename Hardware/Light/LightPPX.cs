using System;
using System.IO.Ports;

namespace YTVisionPro.Hardware.Light
{
    /// <summary>
    /// 磐鑫光源控制类
    /// 2024-7-30
    /// by pengmude
    /// </summary>
    public class LightPPX : ILight
    {
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

        /// <summary>
        /// 设备类型
        /// </summary>
        public DevType DevType { get; } = DevType.LIGHT;

        /// <summary>
        /// 光源品牌-磐鑫
        /// </summary>
        public LightBrand Brand { get; } = LightBrand.PPX;

        /// <summary>
        /// 光源是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 光源亮度值
        /// </summary>
        public int Brightness { get; set; }

        /// <summary>
        /// 光源连接的COM号
        /// </summary>
        public string PortName { get; set; }

        private SerialPort _serialPort;

        /// <summary>
        /// 光源序列号
        /// </summary>
        public string Sn { get; set; }

        public LightPPX()
        {
            _serialPort = new SerialPort();
        }

        public LightPPX(string userName)
        {
            _serialPort = new SerialPort();
            UserDefinedName = userName;
        }

        public LightPPX(string userName, string port)
        {
            _serialPort = new SerialPort();
            UserDefinedName = userName;
            PortName = port;
        }

        /// <summary>
        /// 光源连接
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        public bool Connenct(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            try
            {
                _serialPort.PortName = portName;
                _serialPort.BaudRate = baudRate;
                _serialPort.DataBits = dataBits;
                _serialPort.StopBits = stopBits;
                _serialPort.Parity = parity;
                _serialPort.Open();
                PortName = _serialPort.PortName;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            _serialPort.Close();
        }

        public void TurnOn()
        {
            SendCommand("ON");
        }

        public void TurnOff()
        {
            SendCommand("OFF");
        }

        public void SetValue(int value)
        {
            // 假设光源的亮度命令格式为 "BRIGHTNESS 128"
            string command = $"BRIGHTNESS {value}";
            SendCommand(command);
        }

        private void SendCommand(string command)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Write(command + Environment.NewLine);
                Console.WriteLine($"Sent command: {command}");
            }
            else
            {
                throw new InvalidOperationException("Serial port is not open.");
            }
        }
    }
}
