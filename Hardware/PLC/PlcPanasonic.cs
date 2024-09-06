using HslCommunication;
using HslCommunication.Profinet.Panasonic;
using System;
using System.Text;

namespace YTVisionPro.Hardware.PLC
{
    internal class PlcPanasonic : IPlc
    {
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;
        public PLCParms PLCParms { get; set; } = new PLCParms();

        /// <summary>
        /// 串口plc对象
        /// </summary>
        PanasonicMewtocol _panasonicMewtocol = null;

        /// <summary>
        /// 网口plc对象
        /// </summary>
        PanasonicMcNet _panasonicMcNet = null;

        private bool _isOpen { get; set; }
        private int _id;

        public int ID { get =>_id; }
        public string DevName { get ; set ; }
        public string UserDefinedName { get; set; }

        public DevType DevType { get; } = DevType.PLC;

        public PlcPanasonic(PLCParms parms)
        {
            _id = ++Solution.DeviceCount;
            PLCParms = parms;
            DevName = parms.UserDefinedName;
            UserDefinedName = parms.UserDefinedName;
            if (PLCParms.PlcConType == PlcConType.COM)
                _panasonicMewtocol = new PanasonicMewtocol();
            else if(PLCParms.PlcConType == PlcConType.ETHERNET)
                _panasonicMcNet = new PanasonicMcNet();
        }

        public bool Connect()
        {
            if (PLCParms.PlcConType == PlcConType.ETHERNET)
            {
                if (PLCParms.EthernetParms.IP == null)
                {
                    throw new Exception("PLC网口连接参数为空！");
                }
                _panasonicMcNet.IpAddress = PLCParms.EthernetParms.IP;
                _panasonicMcNet.Port = PLCParms.EthernetParms.Port;
                //_panasonicMcNet.ConnectServer();
                _isOpen = _panasonicMcNet.ConnectServer().IsSuccess;
                ConnectStatusEvent?.Invoke(this, _isOpen);
                return _isOpen;
            }
            else 
            {
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

                if (_panasonicMewtocol.IsOpen())
                    return true;
                _isOpen = _panasonicMewtocol.Open().IsSuccess;
                ConnectStatusEvent?.Invoke(this, _isOpen);
                return _isOpen;
            }
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void Disconnect()
        {
            if (!_isOpen) { return; }

            if(PLCParms.PlcConType == PlcConType.COM)
            {
                _panasonicMewtocol.Close();
                _isOpen = false;
            }
            else
            {
                _isOpen = !_panasonicMcNet.ConnectClose().IsSuccess;
            }
            ConnectStatusEvent?.Invoke(this, _isOpen);
        }

        public void Release()
        {
            if(PLCParms.PlcConType == PlcConType.COM)
                _panasonicMewtocol.Dispose();
            else
                _panasonicMcNet.Dispose();
        }

        /// <summary>
        /// 读取PLC寄存器
        /// </summary>
        /// <returns></returns>
        public object ReadPLCData(string address, ushort length, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.BOOL:
                    return ReadBool(address).Content;
                case DataType.INT:
                    return ReadInt(address).Content;
                case DataType.STRING:
                    return ReadString(address, length).Content;
                case DataType.Bytes:
                    return ReadBytes(address, length).Content;
                default:
                    throw new ArgumentException("不支持的数据类型");
            }
        }
        /// <summary>
        /// 写入PLC寄存器
        /// </summary>
        /// <returns></returns>
        public void WritePLCData(string address, object value)
        {
            if (value is bool bValue)
                WriteBool(address, bValue);
            else if (value is int iValue)
                WriteInt(address, iValue);
            else
                throw new ArgumentException("暂不支持写入的数据类型");
        }

        private OperateResult<int> ReadInt(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadInt32(address);
            else
                return _panasonicMcNet.ReadInt32(address);
        }

        private OperateResult<bool> ReadBool(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address);
            else
                return _panasonicMcNet.ReadBool(address);
        }

        private OperateResult<string> ReadString(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadString(address, length);
            else
                return _panasonicMcNet.ReadString(address, length);

        }

        private OperateResult<byte[]> ReadBytes(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Read(address, length);
            else
                return _panasonicMcNet.Read(address, length);

        }

        private OperateResult WriteBool(string address, bool value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);

        }

        private OperateResult WriteInt(string address, int value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }
    }
}
