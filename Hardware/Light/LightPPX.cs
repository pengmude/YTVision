using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace YTVisionPro.Hardware.Light
{
    /// <summary>
    /// 磐鑫光源控制类
    /// 2024-7-30
    /// by pengmude
    /// </summary>
    public class LightPPX : ILight
    {
        public static int DevNameId = 0;

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
        public byte ChannelValue { get; set; }

        public LightPPX()
        {
            _serialPort = new SerialPort();
            DevNameId++;
            DevName = "光源"+DevNameId;
        }

        public LightPPX(string userName)
        {
            _serialPort = new SerialPort();
            UserDefinedName = userName;
            DevNameId++;
            DevName = "光源" + DevNameId;
        }

        public LightPPX(string userName, string port)
        {
            _serialPort = new SerialPort();
            UserDefinedName = userName;
            PortName = port;
            DevNameId++;
            DevName = "光源" + DevNameId;
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

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Disconnect()
        {
            _serialPort.Close();
        }

        /// <summary>
        /// 打开光源
        /// </summary>
        public void TurnOn()
        {
            SetValue(255);
        }

        /// <summary>
        /// 关闭光源
        /// </summary>
        public void TurnOff()
        {
            SetValue(0);
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

        /// <summary>
        /// 设置光源亮度
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            if (_serialPort.IsOpen && value != null)
            {
                byte[] buff = new byte[4];
                buff[0] = 0x24;
                buff[1] = Convert.ToByte(Sn);
                buff[2] = Convert.ToByte(value);
                buff[3] = (byte)(buff[0] ^ buff[1] ^ buff[2]);
                try
                {
                    _serialPort.Write(buff, 0, 4);
                }
                catch
                {
                    MessageBox.Show("光源控制器线缆连接不正常或松动", "连接异常");
                }
                System.Threading.Thread.Sleep(10);
                int j = _serialPort.BytesToRead;
                try
                {
                    _serialPort.Read(buff, 0, j);
                }
                catch { }
            }
        }

        /// <summary>
        /// 读取光源亮度
        /// </summary>
        /// <returns></returns>
        public byte ReadValue()
        {
            byte[] value = new byte[20];
            byte[] Buffer = new byte[3]; //发送缓冲区
            byte[] buf = new byte[20]; //接收缓冲区
            Buffer[0] = 0X27;
            Buffer[1] = 0XA5;
            Buffer[2] = (byte)(Buffer[0] ^ Buffer[1]);
            try
            {
                _serialPort.Write(Buffer, 0, 3); //发送
            }
            catch
            {
                MessageBox.Show("光源控制器线缆连接不正常或松动", "连接异常");
            }
            System.Threading.Thread.Sleep(20);
            int j = _serialPort.BytesToRead;
            try
            {
                _serialPort.Read(buf, 0, j); //接收
            }
            catch { }
            if (4 == j && buf[0] == 0x27 && buf[3] == (buf[0] ^ buf[1] ^ buf[2]))
            {
                for (int i = 1; i < 3; i++)
                    value[i] = buf[i];
                return 1;
            }
            if (6 == j && buf[0] == 0x27 && buf[5] == (buf[0] ^ buf[1] ^ buf[2] ^ buf[3] ^ buf[4]))
            {
                for (int i = 1; i < 5; i++)
                {
                    value[i] = buf[i];
                }
                return value[int.Parse(Sn)];
            }
            if (10 == j && buf[0] == 0x27 && buf[9] == (buf[0] ^ buf[1] ^ buf[2] ^ buf[3] ^ buf[4]
                ^ buf[5] ^ buf[6] ^ buf[7] ^ buf[8]))
            {
                for (int i = 1; i < 9; i++)
                {
                    value[i] = buf[i];
                }
                return 1;
            }
            if (18 == j && buf[0] == 0x27 && buf[17] == (buf[0] ^ buf[1] ^ buf[2] ^ buf[3]
                ^ buf[4] ^ buf[5] ^ buf[6] ^ buf[7] ^ buf[8] ^ buf[9] ^ buf[10] ^ buf[11]
                ^ buf[12] ^ buf[13] ^ buf[14] ^ buf[15] ^ buf[16]))
            {
                for (int i = 1; i < 17; i++)
                {
                    value[i] = buf[i];
                }
                return 1;
            }
            return 0;
        }
    }
}
