using HslCommunication;
using HslCommunication.Profinet.Panasonic;
using System;
using System.Text;

namespace YTVisionPro.Hardware.PLC
{
    internal class PlcPanasonic : IPlc
    {
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
            else
                _panasonicMewtocol = new PanasonicMewtocol();
        }

        public bool Connect()
        {
            if (PLCParms.PlcConType == PlcConType.ETHERNET)
            {
                if (PLCParms.EthernetParms.IP == null)
                {
                    //LogHelper.AddLog(MsgLevel.Exception, "PLC网口连接参数为空！", true);
                    throw new Exception("PLC网口连接参数为空！");
                }
                _panasonicMcNet.IpAddress = PLCParms.EthernetParms.IP;
                _panasonicMcNet.Port = PLCParms.EthernetParms.Port;
                //_panasonicMcNet.ConnectServer();
                _isOpen = _panasonicMcNet.ConnectServer().IsSuccess;
                return _isOpen;
            }
            else 
            {
                if (PLCParms.SerialParms.PortName == null)
                {
                    //LogHelper.AddLog(MsgLevel.Exception, "PLC串口连接参数为空！", true);
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
                //var res = _panasonicMewtocol.Open();
                _isOpen = _panasonicMewtocol.Open().IsSuccess;
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
        }

        public void Release()
        {
            if(PLCParms.PlcConType == PlcConType.COM)
                _panasonicMewtocol.Dispose();
            else
                _panasonicMcNet.Dispose();
        }

        public OperateResult<bool> ReadBool(string address) 
        {
            if(PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address);
            else
                return _panasonicMcNet.ReadBool(address);
        }

        public OperateResult<string> ReadString(string address, ushort length, Encoding encoding) 
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadString(address, length, encoding);
            else
                return _panasonicMcNet.ReadString(address, length, encoding);

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
    }
}
