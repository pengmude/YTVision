using HslCommunication;
using HslCommunication.Profinet.Panasonic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using Logger;
using System.Threading.Tasks;
using System.Linq;
using HslCommunication.Profinet.Melsec;

namespace TDJS_Vision.Device.PLC
{
    public class PlcPanasonic : IPlc
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
                LogHelper.AddLog(MsgLevel.Exception, $"{ex.Message}", true);
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

        /// <summary>
        /// 解析地址数组，返回可批量读取的信息
        /// </summary>
        /// <param name="addresses">输入的地址数组</param>
        /// <returns>起始地址、长度、各地址在读取结果中的相对索引</returns>
        private (string startAddress, ushort length, int[] indices) AnalyzeAddressRange(string[] addresses)
        {
            if (addresses == null || addresses.Length == 0)
                throw new ArgumentException("地址数组不能为空");

            var parsed = new (string prefix, ushort offset)[addresses.Length];
            for (int i = 0; i < addresses.Length; i++)
            {
                var addr = addresses[i];
                if (string.IsNullOrEmpty(addr))
                    throw new ArgumentException($"地址不能为空，索引{i}");

                var match = System.Text.RegularExpressions.Regex.Match(addr, @"^([a-zA-Z]+)(\d+)$");
                if (!match.Success)
                    throw new ArgumentException($"地址格式无效: {addr}");

                parsed[i] = (match.Groups[1].Value.ToUpper(), ushort.Parse(match.Groups[2].Value));
            }

            // 检查前缀一致性
            string prefix = parsed[0].prefix;
            if (!parsed.All(p => p.prefix == prefix))
                throw new ArgumentException("所有地址必须属于同一区域（如 R、M、X 等）");

            ushort minOffset = parsed.Min(p => p.offset);
            ushort maxOffset = parsed.Max(p => p.offset);
            ushort length = (ushort)(maxOffset - minOffset + 1);

            // 计算每个地址在读取结果中的索引
            int[] indices = parsed.Select(p => p.offset - minOffset).ToArray();

            return (prefix + minOffset, length, indices);
        }


        #region 读取多个字节
        public OperateResult<byte[]> ReadBytes(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Read(address, length);
            else
                return _panasonicMcNet.Read(address, length);

        }

        public async Task<OperateResult<byte[]>> ReadBytesAsync(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadAsync(address, length);
            else
                return await _panasonicMcNet.ReadAsync(address, length);

        }
        #endregion

        #region 读取单个/多个布尔值

        public OperateResult<bool> ReadBool(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address);
            else
                return _panasonicMcNet.ReadBool(address);
        }

        public async Task<OperateResult<bool>> ReadBoolAsync(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadBoolAsync(address);
            else
                return await _panasonicMcNet.ReadBoolAsync(address);
        }

        public OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadBool(address, length);
            else
                return _panasonicMcNet.ReadBool(address, length);
        }

        public async Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadBoolAsync(address, length);
            else
                return await _panasonicMcNet.ReadBoolAsync(address, length);
        }

        public OperateResult<bool[]> ReadBool(string[] address)
        {
            if (address == null || address.Length == 0)
                throw new Exception("输入的地址数组为空！");

            try
            {
                // 一步获取读取参数和索引映射
                var (startAddress, length, indices) = AnalyzeAddressRange(address);

                // 一次性读取
                OperateResult<bool[]> readResult;
                if (PLCParms.PlcConType == PlcConType.COM)
                    readResult = _panasonicMewtocol.ReadBool(startAddress, length);
                else
                    readResult = _panasonicMcNet.ReadBool(startAddress, length);
                if (!readResult.IsSuccess) throw new Exception(readResult.Message);

                // 按原始顺序提取对应值
                return OperateResult.CreateSuccessResult(
                    indices.Select(index => readResult.Content[index]).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OperateResult<bool[]>> ReadBoolAsync(string[] address)
        {
            if (address == null || address.Length == 0)
                throw new Exception("输入的地址数组为空！");

            try
            {
                // 一步获取读取参数和索引映射
                var (startAddress, length, indices) = AnalyzeAddressRange(address);

                // 一次性读取
                OperateResult<bool[]> readResult;
                if (PLCParms.PlcConType == PlcConType.COM)
                    readResult = await _panasonicMewtocol.ReadBoolAsync(startAddress, length);
                else
                    readResult = await _panasonicMcNet.ReadBoolAsync(startAddress, length);
                if (!readResult.IsSuccess) throw new Exception(readResult.Message);

                // 按原始顺序提取对应值
                return OperateResult.CreateSuccessResult(
                    indices.Select(index => readResult.Content[index]).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 读取单个/多个整型值
        public OperateResult<int> ReadInt(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadInt32(address);
            else
                return _panasonicMcNet.ReadInt32(address);
        }

        public async Task<OperateResult<int>> ReadIntAsync(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadInt32Async(address);
            else
                return await _panasonicMcNet.ReadInt32Async(address);
        }

        public OperateResult<int[]> ReadInt(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadInt32(address, length);
            else
                return _panasonicMcNet.ReadInt32(address, length);
        }

        public async Task<OperateResult<int[]>> ReadIntAsync(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadInt32Async(address, length);
            else
                return await _panasonicMcNet.ReadInt32Async(address, length);
        }

        public OperateResult<int[]> ReadInt(string[] address)
        {
            if (address == null || address.Length == 0)
                throw new Exception("输入的地址数组为空！");

            try
            {
                // 一步获取读取参数和索引映射
                var (startAddress, length, indices) = AnalyzeAddressRange(address);

                // 一次性读取
                OperateResult<int[]> readResult;
                if (PLCParms.PlcConType == PlcConType.COM)
                    readResult = _panasonicMewtocol.ReadInt32(startAddress, length);
                else
                    readResult = _panasonicMcNet.ReadInt32(startAddress, length);
                if (!readResult.IsSuccess) throw new Exception(readResult.Message);

                // 按原始顺序提取对应值
                return OperateResult.CreateSuccessResult(
                    indices.Select(index => readResult.Content[index]).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OperateResult<int[]>> ReadIntAsync(string[] address)
        {
            if (address == null || address.Length == 0)
                throw new Exception("输入的地址数组为空！");

            try
            {
                // 一步获取读取参数和索引映射
                var (startAddress, length, indices) = AnalyzeAddressRange(address);

                // 一次性读取
                OperateResult<int[]> readResult;
                if (PLCParms.PlcConType == PlcConType.COM)
                    readResult = await _panasonicMewtocol.ReadInt32Async(startAddress, length);
                else
                    readResult = await _panasonicMcNet.ReadInt32Async(startAddress, length);
                if (!readResult.IsSuccess) throw new Exception(readResult.Message);

                // 按原始顺序提取对应值
                return OperateResult.CreateSuccessResult(
                    indices.Select(index => readResult.Content[index]).ToArray()
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 读取单个/多个浮点数
        public OperateResult<float> ReadFloat(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadFloat(address);
            else
                return _panasonicMcNet.ReadFloat(address);
        }

        public async Task<OperateResult<float>> ReadFloatAsync(string address)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadFloatAsync(address);
            else
                return await _panasonicMcNet.ReadFloatAsync(address);
        }

        public OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadFloat(address, length);
            else
                return _panasonicMcNet.ReadFloat(address, length);
        }

        public async Task<OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadFloatAsync(address, length);
            else
                return await _panasonicMcNet.ReadFloatAsync(address, length);
        }
        #endregion

        #region 读取单个字符串
        public OperateResult<string> ReadString(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.ReadString(address, length);
            else
                return _panasonicMcNet.ReadString(address, length);

        }

        public async Task<OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.ReadStringAsync(address, length);
            else
                return await _panasonicMcNet.ReadStringAsync(address, length);

        }
        #endregion

        #region 写入多个字节
        public OperateResult WriteBytes(string address, byte[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteBytesAsync(string address, byte[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个/多个布尔值
        public OperateResult WriteBool(string address, bool value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
            {
                return _panasonicMewtocol.Write(address, value);
            }
            else
                return _panasonicMcNet.Write(address, value);

        }

        public async Task<OperateResult> WriteBoolAsync(string address, bool value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
            {
                return await _panasonicMewtocol.WriteAsync(address, value);
            }
            else
                return await _panasonicMcNet.WriteAsync(address, value);

        }

        public OperateResult WriteBool(string address, bool[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
            {
                return _panasonicMewtocol.Write(address, value);
            }
            else
                return _panasonicMcNet.Write(address, value);

        }

        public async Task<OperateResult> WriteBoolAsync(string address, bool[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
            {
                return await _panasonicMewtocol.WriteAsync(address, value);
            }
            else
                return await _panasonicMcNet.WriteAsync(address, value);

        }
        #endregion

        #region 写入单个/多个整型值
        public OperateResult WriteInt(string address, int value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteIntAsync(string address, int value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }

        public OperateResult WriteInt(string address, int[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteIntAsync(string address, int[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个/多个整型值
        public OperateResult WriteFloat(string address, float value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteFloatAsync(string address, float value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }

        public OperateResult WriteFloat(string address, float[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteFloatAsync(string address, float[] value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个字符串
        public OperateResult WriteString(string address, string value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return _panasonicMewtocol.Write(address, value);
            else
                return _panasonicMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteStringAsync(string address, string value)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WriteAsync(address, value);
            else
                return await _panasonicMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 异步等待某个地址的值变为指定的值

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            if (PLCParms.PlcConType == PlcConType.COM)
                return await _panasonicMewtocol.WaitAsync(address, waitValue, readInterval, waitTimeout);
            else
                return await _panasonicMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        #endregion

    }
}
