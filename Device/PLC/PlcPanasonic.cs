using HslCommunication;
using HslCommunication.Profinet.Panasonic;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Device.Camera;
using YTVisionPro.Device.Light;

namespace YTVisionPro.Device.PLC
{
    internal class PlcPanasonic : IPlc
    {
        /// <summary>
        /// 串口plc对象
        /// </summary>
        private PanasonicMewtocol _panasonicMewtocol = null;

        /// <summary>
        /// 网口plc对象
        /// </summary>
        private PanasonicMcNet _panasonicMcNet = null;
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;
        /// <summary>
        /// PLC连接参数
        /// </summary>
        public PLCParms PLCParms { get; set; } = new PLCParms();
        /// <summary>
        /// PLC连接状态
        /// </summary>
        public bool IsConnect { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get ; set ; }
        /// <summary>
        /// 用户自定义名称
        /// </summary>
        public string UserDefinedName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.PLC;
        public string ClassName { get; set; } = typeof(PlcPanasonic).FullName;

        /// <summary>
        /// 设备品牌
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.Panasonic;

        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public PlcPanasonic() { }

        /// <summary>
        /// 反序列化PLC必须调用
        /// </summary>
        public void CreateDevice()
        {
            try
            {
                if (PLCParms.PlcConType == PlcConType.COM)
                {
                    _panasonicMewtocol = new PanasonicMewtocol();
                    if (PLCParms.SerialParms.PortName == null)
                    {
                        throw new Exception("PLC串口连接参数为空！");
                    }
                    _panasonicMewtocol.SerialPortInni(sp =>
                    {
                        sp.PortName = PLCParms.SerialParms.PortName;
                        sp.BaudRate = PLCParms.SerialParms.BaudRate;
                        sp.DataBits = PLCParms.SerialParms.DataBits;
                        sp.StopBits = PLCParms.SerialParms.StopBits;
                        sp.Parity = PLCParms.SerialParms.Parity;
                    });
                }
                else if (PLCParms.PlcConType == PlcConType.ETHERNET)
                {
                    _panasonicMcNet = new PanasonicMcNet();
                    if (PLCParms.EthernetParms.IP == null)
                    {
                        throw new Exception("PLC网口连接参数为空！");
                    }
                    _panasonicMcNet.IpAddress = PLCParms.EthernetParms.IP;
                    _panasonicMcNet.Port = PLCParms.EthernetParms.Port;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        public PlcPanasonic(PLCParms parms)
        {
            PLCParms = parms;
            DevName = parms.UserDefinedName;
            UserDefinedName = parms.UserDefinedName;
            if (PLCParms.PlcConType == PlcConType.COM)
            {
                _panasonicMewtocol = new PanasonicMewtocol();
                if (PLCParms.SerialParms.PortName == null)
                {
                    throw new Exception("PLC串口连接参数为空！");
                }
                _panasonicMewtocol.SerialPortInni(sp =>
                {
                    sp.PortName = PLCParms.SerialParms.PortName;
                    sp.BaudRate = PLCParms.SerialParms.BaudRate;
                    sp.DataBits = PLCParms.SerialParms.DataBits;
                    sp.StopBits = PLCParms.SerialParms.StopBits;
                    sp.Parity = PLCParms.SerialParms.Parity;
                });
            }
            else if(PLCParms.PlcConType == PlcConType.ETHERNET)
            {
                _panasonicMcNet = new PanasonicMcNet();
                if (PLCParms.EthernetParms.IP == null)
                {
                    throw new Exception("PLC网口连接参数为空！");
                }
                _panasonicMcNet.IpAddress = PLCParms.EthernetParms.IP;
                _panasonicMcNet.Port = PLCParms.EthernetParms.Port;
            }
        }

        public bool Connect()
        {
            try
            {
                if (PLCParms.PlcConType == PlcConType.ETHERNET)
                {
                    IsConnect = _panasonicMcNet.ConnectServer().IsSuccess;
                    ConnectStatusEvent?.Invoke(this, IsConnect);
                    return IsConnect;
                }
                else
                {
                    if (_panasonicMewtocol.IsOpen())
                        return true;
                    IsConnect = _panasonicMewtocol.Open().IsSuccess;
                    ConnectStatusEvent?.Invoke(this, IsConnect);
                    return IsConnect;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            if (!IsConnect) { return; }

            if(PLCParms.PlcConType == PlcConType.COM)
            {
                _panasonicMewtocol.Close();
                IsConnect = false;
            }
            else
            {
                IsConnect = !_panasonicMcNet.ConnectClose().IsSuccess;
            }
            ConnectStatusEvent?.Invoke(this, IsConnect);
        }

        public void Release()
        {
            if(PLCParms.PlcConType == PlcConType.COM)
                _panasonicMewtocol.Dispose();
            else
                _panasonicMcNet.Dispose();
        }

        public OperateResult<int> ReadInt(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadInt32(address);
            else
                return _panasonicMcNet.ReadInt32(address);
        }

        public OperateResult<bool> ReadBool(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address);
            else
                return _panasonicMcNet.ReadBool(address);
        }

        public OperateResult<string> ReadString(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadString(address, length);
            else
                return _panasonicMcNet.ReadString(address, length);

        }

        public OperateResult<byte[]> ReadBytes(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Read(address, length);
            else
                return _panasonicMcNet.Read(address, length);

        }

        /// <summary>
        /// 读取多个连续地址的bool值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public OperateResult<bool[]> ReadMultipleBool(string[] address, ushort legth = 0)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address); //串口的批量读可以不连续地址
            else
                return _panasonicMcNet.ReadBool(address[0], legth); //网口的批量读只能连续地址，未测试(无法测试)
        }

        /// <summary>
        /// 写入多个连续地址的bool值
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public OperateResult WriteMultipleBool(string[] address, bool[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value); //串口的批量写可以不连续地址
            else
                return _panasonicMcNet.Write(address[0], value); //网口的批量写只能连续地址，未测试(无法测试)
        }

        public OperateResult WriteBool(string address, bool value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);

        }

        public OperateResult WriteInt(string address, int value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public OperateResult WriteString(string address, string value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public OperateResult WriteBytes(string address, byte[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }
    }
}
