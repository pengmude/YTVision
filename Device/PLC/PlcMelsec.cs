using System;
using HslCommunication;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using HslCommunication.Profinet.Panasonic;
using Logger;
using HslCommunication.Profinet.Melsec;
using System.Threading.Tasks;
using System.Linq;
using static OfficeOpenXml.ExcelErrorValue;

namespace TDJS_Vision.Device.PLC
{
    /// <summary>
    /// 三菱PLC
    /// </summary>
    public class PlcMelsec : IPlc
    {
        /// <summary>
        /// 使用TCP通信的三菱plc对象
        /// </summary>
        private MelsecMcNet melsecMcNet = null;
        /// <summary>
        /// 设备参数
        /// </summary>
        public PLCParms PLCParms { get; set; } = new PLCParms();
        /// <summary>
        /// 是否连接
        /// </summary>
        public bool IsConnect { get; set; }
        /// <summary>
        /// 设备名
        /// </summary>
        public string DevName { get; set; }
        /// <summary>
        /// 设备品牌
        /// </summary>
        public DeviceBrand Brand { get; set; } = DeviceBrand.Melsec;
        /// <summary>
        /// 用户自定义名称
        /// </summary>
        public string UserDefinedName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.PLC;
        /// <summary>
        /// 反序列化使用标志
        /// </summary>
        public string ClassName { get; set; } = typeof(PlcMelsec).FullName;
        /// <summary>
        /// 连接状态改变事件
        /// </summary>
        public event EventHandler<bool> ConnectStatusEvent;


        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public PlcMelsec() { }

        /// <summary>
        /// 反序列化PLC必须调用
        /// </summary>
        public void CreateDevice()
        {
            try
            {
                if (PLCParms.PlcConType == PlcConType.ETHERNET)
                {
                    melsecMcNet = new MelsecMcNet();
                    if (PLCParms.EthernetParms.IP == null)
                    {
                        throw new Exception("PLC网口连接参数为空！");
                    }
                    melsecMcNet.IpAddress = PLCParms.EthernetParms.IP;
                    melsecMcNet.Port = PLCParms.EthernetParms.Port;
                }
                else
                    throw new Exception("不支持的通信方式！");
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"{ex.Message}", true);
            }
        }

        #endregion


        public PlcMelsec(PLCParms parms)
        {
            PLCParms = parms;
            DevName = parms.UserDefinedName;
            UserDefinedName = parms.UserDefinedName;
            if (PLCParms.PlcConType == PlcConType.ETHERNET)
            {
                melsecMcNet = new PanasonicMcNet();
                if (PLCParms.EthernetParms.IP == null)
                {
                    throw new Exception("PLC网口连接参数为空！");
                }
                melsecMcNet.IpAddress = PLCParms.EthernetParms.IP;
                melsecMcNet.Port = PLCParms.EthernetParms.Port;
            }
            else
                throw new Exception("不支持的通信方式！");
        }

        public bool Connect()
        {
            try
            {
                if (PLCParms.PlcConType == PlcConType.ETHERNET)
                {
                    IsConnect = melsecMcNet.ConnectServer().IsSuccess;
                    ConnectStatusEvent?.Invoke(this, IsConnect);
                    return IsConnect;
                }
                else
                {
                    throw new Exception("不支持的通信方式！");
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

            if (PLCParms.PlcConType == PlcConType.COM)
            {
                melsecMcNet.ConnectClose();
                IsConnect = false;
            }
            else
            {
                IsConnect = !melsecMcNet.ConnectClose().IsSuccess;
            }
            ConnectStatusEvent?.Invoke(this, IsConnect);
        }

        public void Release()
        {
            if (PLCParms.PlcConType == PlcConType.ETHERNET)
                melsecMcNet.Dispose();
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
            return melsecMcNet.Read(address, length);
        }

        public async Task<OperateResult<byte[]>> ReadBytesAsync(string address, ushort length)
        {
            return await melsecMcNet.ReadAsync(address, length);
        }
        #endregion

        #region 读取单个/多个布尔值
        public OperateResult<bool> ReadBool(string address)
        {
            return melsecMcNet.ReadBool(address);
        }

        public async Task<OperateResult<bool>> ReadBoolAsync(string address)
        {
            return await melsecMcNet.ReadBoolAsync(address);
        }

        public OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            return melsecMcNet.ReadBool(address, length);
        }

        public async Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            return await melsecMcNet.ReadBoolAsync(address, length);
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
                var readResult = melsecMcNet.ReadBool(startAddress, length);
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
                var readResult = await melsecMcNet.ReadBoolAsync(startAddress, length);
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

        #region 读取单个/多个整型
        public OperateResult<int> ReadInt(string address)
        {
            return melsecMcNet.ReadInt32(address);
        }

        public async Task<OperateResult<int>> ReadIntAsync(string address)
        {
            return await melsecMcNet.ReadInt32Async(address);
        }

        public OperateResult<int[]> ReadInt(string address, ushort length)
        {
            return melsecMcNet.ReadInt32(address, length);
        }

        public async Task<OperateResult<int[]>> ReadIntAsync(string address, ushort length)
        {
            return await melsecMcNet.ReadInt32Async(address, length);
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
                var readResult = melsecMcNet.ReadInt32(startAddress, length);
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
                var readResult = await melsecMcNet.ReadInt32Async(startAddress, length);
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

        #region 读取单个/多个浮点型
        public OperateResult<float> ReadFloat(string address)
        {
            return melsecMcNet.ReadFloat(address);
        }

        public async Task<OperateResult<float>> ReadFloatAsync(string address)
        {
            return await melsecMcNet.ReadFloatAsync(address);
        }

        public OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            return melsecMcNet.ReadFloat(address, length);
        }

        public async Task<OperateResult<float[]>> ReadFloatAsync(string address, ushort length)
        {
            return await melsecMcNet.ReadFloatAsync(address, length);
        }
        #endregion

        #region 读取单个字符串

        public OperateResult<string> ReadString(string address, ushort length)
        {
            return melsecMcNet.ReadString(address, length);
        }

        public async Task<OperateResult<string>> ReadStringAsync(string address, ushort length)
        {
            return await melsecMcNet.ReadStringAsync(address, length);
        }
        #endregion

        
        #region 写入多个字节
        public OperateResult WriteBytes(string address, byte[] value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteBytesAsync(string address, byte[] value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个/多个布尔值
        public OperateResult WriteBool(string address, bool value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteBoolAsync(string address, bool value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }

        public OperateResult WriteBool(string address, bool[] value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteBoolAsync(string address, bool[] value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个/多个整型值
        public OperateResult WriteInt(string address, int value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteIntAsync(string address, int value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        public OperateResult WriteInt(string address, int[] value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteIntAsync(string address, int[] value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个/多个浮点值
        public OperateResult WriteFloat(string address, float value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteFloatAsync(string address, float value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        public OperateResult WriteFloat(string address, float[] value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteFloatAsync(string address, float[] value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 写入单个字符串
        public OperateResult WriteString(string address, string value)
        {
            return melsecMcNet.Write(address, value);
        }

        public async Task<OperateResult> WriteStringAsync(string address, string value)
        {
            return await melsecMcNet.WriteAsync(address, value);
        }
        #endregion

        #region 异步等待某个地址的值变为指定值

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, bool waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, short waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ushort waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, int waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, uint waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, long waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        public async Task<OperateResult<TimeSpan>> WaitAsync(string address, ulong waitValue, int readInterval = 100, int waitTimeout = -1)
        {
            return await melsecMcNet.WaitAsync(address, waitValue, readInterval, waitTimeout);
        }

        #endregion
    }
}
