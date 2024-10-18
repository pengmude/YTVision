using JsonSubTypes;
using Logger;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.IO.Ports;
using System.Timers;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Device.Camera;

namespace YTVisionPro.Device.Light
{
    /// <summary>
    /// 磐鑫光源控制类
    /// 2024-7-30
    /// by pengmude
    /// </summary>
    internal class LightPPX : ILight
    {
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;
        /// <summary>
        /// 光源设置参数
        /// </summary>
        public LightParam LightParam { get; set; }

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
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.LIGHT;

        /// <summary>
        /// 光源品牌-磐鑫
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.PPX;
        public string ClassName { get; set; } = typeof(LightPPX).FullName;

        /// <summary>
        /// 光源是否打开
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 光源串口是否打开
        /// </summary>
        public bool IsComOpen { get; set; }

        /// <summary>
        /// 光源亮度值
        /// </summary>
        public int Brightness { get; set; }

        /// <summary>
        /// 光源所连接的串口对象
        /// </summary>
        private SerialPort _serialPort;

        private Timer _timer;


        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public LightPPX() { }

        public void CreateDevice()
        {
            try
            {
                foreach (var serialPort in FrmLightListView.OccupiedComList)
                {
                    if (serialPort.PortName == LightParam.Port)
                    {
                        _serialPort = serialPort;
                        break;
                    }
                }
                if (_serialPort == null)
                {
                    _serialPort = new SerialPort();
                    FrmLightListView.OccupiedComList.Add(_serialPort);
                }
                Connenct();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion


        public LightPPX(LightParam lightParam)
        {
            try
            {
                foreach (var serialPort in FrmLightListView.OccupiedComList)
                {
                    if (serialPort.PortName == lightParam.Port)
                    {
                        _serialPort = serialPort;
                        break;
                    }
                }
                if (_serialPort == null)
                {
                    _serialPort = new SerialPort();
                    FrmLightListView.OccupiedComList.Add(_serialPort);
                }
                DevName = lightParam.LightName;
                UserDefinedName = DevName;
                LightParam = lightParam;
                Connenct();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 连接光源串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="parity"></param>
        /// <returns></returns>
        public void Connenct()
        {
            if(_serialPort.IsOpen) { return ; }
            try
            {
                _serialPort.PortName = LightParam.Port;
                _serialPort.BaudRate = LightParam.BaudRate;
                _serialPort.DataBits = LightParam.DataBits;
                _serialPort.StopBits = LightParam.StopBits;
                _serialPort.Parity = LightParam.Parity;
                _serialPort.Open();

                IsComOpen = true;
                return;
            }
            catch (Exception ex)
            {
                IsComOpen = false;
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Disconnect()
        {
            //删除光源前先判断是否需要释放占用的串口
            //（当除了待删除的光源外，均没有使用和待删除光源的串口，则需要释放串口）
            int count = 0;
            foreach (var light in Solution.Instance.LightDevices)
            {
                if (light.LightParam.Port == this.LightParam.Port)
                    count++;
            }
            if (count == 1)
            {
                _serialPort.Close();
                IsComOpen = false;
                FrmLightListView.OccupiedComList.Remove(_serialPort);
            }
        }

        /// <summary>
        /// 打开光源特定时间
        /// </summary>
        public void TurnOn(int value, int time = -1)
        {
            try
            {
                if (!IsComOpen)
                    Connenct();
                SetValue(value);
                IsOpen = true;
                ConnectStatusEvent?.Invoke(this, true);
                if (time == -1)
                    return;
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Dispose();
                    _timer = null;
                }
                _timer = new Timer(time);
                _timer.Elapsed += OnTimedEvent;
                _timer.Start();
            }
            catch (Exception ex)
            {
                IsOpen = false;
                throw ex;
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TurnOff();
        }

        /// <summary>
        /// 关闭光源
        /// </summary>
        public void TurnOff()
        {
            try
            {
                if (!IsComOpen)
                    Connenct();
                SetValue(0);
                IsOpen = false;
                ConnectStatusEvent?.Invoke(this, false);
            }
            catch (Exception ex)
            {
                IsOpen = true;
                throw ex;
            }
        }

        /// <summary>
        /// 设置光源亮度
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(int value)
        {
            if (_serialPort.IsOpen)
            {
                byte[] buff = new byte[4];
                buff[0] = 0x24;
                buff[1] = Convert.ToByte(LightParam.Channel);
                buff[2] = Convert.ToByte(value);
                buff[3] = (byte)(buff[0] ^ buff[1] ^ buff[2]);
                try
                {
                    _serialPort.Write(buff, 0, 4);
                }
                catch(Exception e)
                {
                    throw new Exception($"{LightParam.LightName}亮度设置失败！原因：{e.Message}");
                }
            }
            else
            {
                throw new Exception($"{LightParam.LightName}对应串口{LightParam.Port}尚未打开！");
            }
            Brightness = value;
        }
    }
}
