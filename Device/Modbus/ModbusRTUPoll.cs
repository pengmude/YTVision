using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Logger;
using Modbus.Device;
using Newtonsoft.Json;

namespace TDJS_Vision.Device.Modbus
{
    /// <summary>
    /// Modbus RTU 串口通信从站类（主站主动轮询）
    /// </summary>
    public class ModbusRTUPoll : IModbus
    {
        private SerialPort _serialPort;
        private ModbusSerialMaster _master;

        public string DevName { get; set; }
        public string UserDefinedName { get; set; }

        public IModbusParam ModbusParam { get; set; }

        public bool IsConnect { get; set; }

        public DevType DevType { get; set; } = DevType.ModbusRTUPoll;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(ModbusRTUPoll).FullName;

        public event EventHandler<bool> ConnectStatusEvent;

        #region 构造函数

        [JsonConstructor]
        public ModbusRTUPoll() { }

        public void CreateDevice()
        {
            try
            {
                // 可用于初始化参数等
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"{ex.Message}", true);
            }
        }

        public ModbusRTUPoll(ModbusRTUParam param)
        {
            ModbusParam = param;
            DevName = param.DevName;
            UserDefinedName = param.UserDefinedName;
        }

        #endregion

        #region 连接管理

        public void Connect()
        {
            try
            {
                var param = ModbusParam as ModbusRTUParam;
                // 创建并配置串口
                _serialPort = new SerialPort(
                    portName: param.PortName,
                    baudRate: param.BaudRate,
                    parity: param.Parity,
                    dataBits: param.DataBits,
                    stopBits: param.StopBits
                )
                {
                    //ReadTimeout = 500,
                    //WriteTimeout = 500,
                    Encoding = System.Text.Encoding.ASCII
                };

                // 打开串口
                _serialPort.Open();

                // 创建 Modbus RTU 主站
                _master = ModbusSerialMaster.CreateRtu(_serialPort);

                ConnectStatusEvent?.Invoke(this, true);
                IsConnect = true;
            }
            catch (Exception ex)
            {
                ConnectStatusEvent?.Invoke(this, false);
                IsConnect = false;
                throw new Exception($"Modbus串口设备【{DevName}】连接失败: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            if (_master != null)
            {
                _master.Dispose();
                _master = null;
            }

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            _serialPort = null;
            IsConnect = false;
        }

        #endregion

        #region 读取操作

        public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadCoils(1, startAddress, numberOfPoints);
        }

        public Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints)
        {
            Task<bool[]> res;
            lock (this)
            {
                res = _master.ReadCoilsAsync(1, startAddress, numberOfPoints);
            }
            return res;
        }

        public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputs(1, startAddress, numberOfPoints);
        }

        public Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints)
        {
            Task<bool[]> res;
            lock (this)
            {
                res = _master.ReadInputsAsync(1, startAddress, numberOfPoints);
            }
            return res;
        }

        public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputRegisters(1, startAddress, numberOfPoints);
        }

        public Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadInputRegistersAsync(1, startAddress, numberOfPoints);
        }

        public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadHoldingRegisters(1, startAddress, numberOfPoints);
        }

        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints)
        {
            return _master.ReadHoldingRegistersAsync(1, startAddress, numberOfPoints);
        }

        #endregion

        #region 写入操作

        public void WriteSingleCoil(ushort coilAddress, bool value)
        {
            _master.WriteSingleCoil(1, coilAddress, value);
        }

        public Task WriteSingleCoilAsync(ushort coilAddress, bool value) =>
            _master.WriteSingleCoilAsync(1, coilAddress, value);

        public void WriteSingleRegister(ushort registerAddress, ushort value)
        {
            _master.WriteSingleRegister(1, registerAddress, value);
        }

        public Task WriteSingleRegisterAsync(ushort registerAddress, ushort value) =>
            _master.WriteSingleRegisterAsync(1, registerAddress, value);

        public void WriteMultipleRegisters(ushort startAddress, ushort[] data)
        {
            _master.WriteMultipleRegisters(1, startAddress, data);
        }

        public Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data) =>
            _master.WriteMultipleRegistersAsync(1, startAddress, data);

        public void WriteMultipleCoils(ushort startAddress, bool[] data)
        {
            _master.WriteMultipleCoils(1, startAddress, data);
        }

        public Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data) =>
            _master.WriteMultipleCoilsAsync(1, startAddress, data);

        #endregion

        #region 读写混合操作

        public ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
        {
            return _master.ReadWriteMultipleRegisters(1, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);
        }

        public Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData)
        {
            return _master.ReadWriteMultipleRegistersAsync(1, startReadAddress, numberOfPointsToRead, startWriteAddress, writeData);
        }

        #endregion
    }
}
