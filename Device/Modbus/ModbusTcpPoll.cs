using Logger;
using Modbus.Device;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TDJS_Vision.Device.Modbus
{
    /// <summary>
    /// Modbus通信设备类
    /// </summary>
    public class ModbusTcpPoll : IModbus
    {

        private TcpClient _tcpClient;//客户端
        private ModbusIpMaster _master;//主站

        public string DevName { get; set; }
        public string UserDefinedName { get; set; }

        public IModbusParam ModbusParam { get; set; }

        public bool IsConnect {  get; set; }

        public DevType DevType { get; set; } = DevType.ModbusTcpPoll;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(ModbusTcpPoll).FullName;

        public event EventHandler<bool> ConnectStatusEvent;


        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public ModbusTcpPoll() { }

        public void CreateDevice()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"{ex.Message}", true);
            }
        }


        #endregion

        public ModbusTcpPoll(ModbusTcpParam param)
        {
            ModbusParam = param;
            DevName = param.DevName;
            UserDefinedName = param.UserDefinedName;
        }

        /// <summary>
        /// 连接Modbus
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        /// <exception cref="Exception"></exception>
        public void  Connect()
        {
            try
            {
                var param = ModbusParam as ModbusTcpParam;
                // 创建 TCP 客户端
                _tcpClient = new TcpClient(param.IP, param.Port);

                // 创建 Modbus 主站
                _master = ModbusIpMaster.CreateIp(_tcpClient);

                ConnectStatusEvent?.Invoke(this, true);
                
                IsConnect = true;
            }
            catch (Exception ex)
            {
                ConnectStatusEvent?.Invoke(this, false);
                IsConnect = false;
                throw new Exception($"Mobus设备【{DevName}】连接失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 关闭Modbus
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Disconnect()
        {
            if (_tcpClient != null && _tcpClient.Connected)
            {
                _tcpClient.Close();
            }
            _tcpClient = null;
            _master = null;
            IsConnect = false;
        }

        #region 读取操作

        /// <summary>
        /// 同步读取多个线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadCoils(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 异步读取多个线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadCoilsAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 同步读取多个离散量的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputs(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 异步读取多个离散量的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputsAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 同步读取多个输入寄存器的值（单位字）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputRegisters(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 异步读取多个输入寄存器的值（单位字）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputRegistersAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 同步读取多个保持寄存器的值（单位字）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadHoldingRegisters(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }
        /// <summary>
        /// 异步读取多个保持寄存器的值（单位字）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        /// <returns></returns>
        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadHoldingRegistersAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, numberOfPoints);
        }

        #endregion

        #region 写入操作

        /// <summary>
        /// 同步写入单个线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="coilAddress"></param>
        /// <param name="value"></param>
        public void WriteSingleCoil(ushort coilAddress, bool value)
        {
            _master.WriteSingleCoil(((ModbusTcpParam)ModbusParam).ID, coilAddress, value);
        }
        /// <summary>
        /// 异步写入单个线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="coilAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteSingleCoilAsync(ushort coilAddress, bool value) 
            => _master.WriteSingleCoilAsync(((ModbusTcpParam)ModbusParam).ID, coilAddress, value);
        /// <summary>
        /// 同步写入单个保存寄存器的值（单位字节）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="registerAddress"></param>
        /// <param name="value"></param>
        public void WriteSingleRegister(ushort registerAddress, ushort value)
        {
            _master.WriteSingleRegister(((ModbusTcpParam)ModbusParam).ID, registerAddress, value);
        }
        /// <summary>
        /// 异步写入单个保持寄存器的值（单位字节）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="registerAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteSingleRegisterAsync(ushort registerAddress, ushort value) 
            => _master.WriteSingleRegisterAsync(((ModbusTcpParam)ModbusParam).ID, registerAddress, value);
        /// <summary>
        /// 同步写入多个连续保持寄存器的值（单位字节）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="data"></param>
        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
        {
            _master.WriteMultipleRegisters(((ModbusTcpParam)ModbusParam).ID, startAddress, data);
        }
        /// <summary>
        /// 异步写入多个连续保持寄存器的值（单位字节）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data) 
            => _master.WriteMultipleRegistersAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, data);
        /// <summary>
        /// 同步写入多个连续线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="data"></param>
        public void WriteMultipleCoils(ushort startAddress, bool[] data)
        {
            _master.WriteMultipleCoils(((ModbusTcpParam)ModbusParam).ID, startAddress, data);
        }
        /// <summary>
        /// 异步写入多个连续线圈的值（单位比特）
        /// </summary>
        /// <param name="ModbusParam.ID"></param>
        /// <param name="startAddress"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data) 
            => _master.WriteMultipleCoilsAsync(((ModbusTcpParam)ModbusParam).ID, startAddress, data);
        /// <summary>
        /// 同步先写后读多个寄存器
        /// </summary>
        /// <param name="ModbusParam.ID">读取值的设备地址</param>
        /// <param name="startReadAddress">开始读取的地址（保持寄存器从0开始寻址）</param>
        /// <param name="numberOfPointsToRead">要读取的寄存器数量</param>
        /// <param name="startWriteAddress">开始写入的地址（保持寄存器从0开始寻址）</param>
        /// <param name="writeData">寄存器要写入的值</param>
        /// <returns></returns>
        public ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
        {
            return _master.ReadWriteMultipleRegisters(((ModbusTcpParam)ModbusParam).ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);
        }
        /// <summary>
        /// 异步先写后读多个寄存器
        /// </summary>
        /// <param name="ModbusParam.ID">读取值的设备地址</param>
        /// <param name="startReadAddress">开始读取的地址（保持寄存器从0开始寻址）</param>
        /// <param name="numberOfPointsToRead">要读取的寄存器数量</param>
        /// <param name="startWriteAddress">开始写入的地址（保持寄存器从0开始寻址）</param>
        /// <param name="writeData">寄存器要写入的值</param>
        /// <returns></returns>
        public Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
            =>  _master.ReadWriteMultipleRegistersAsync(((ModbusTcpParam)ModbusParam).ID, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);

        #endregion
    }
}
