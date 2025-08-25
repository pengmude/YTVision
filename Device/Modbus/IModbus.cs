using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TDJS_Vision.Device.Modbus
{
    public interface IModbus : IDevice
    {
        string DevName { get; set; }
        string UserDefinedName { get; set; }

        [JsonConverter(typeof(PolyConverter))]
        IModbusParam ModbusParam { get; set; }

        bool IsConnect { get; set; }

        DevType DevType { get; set; }
        DeviceBrand Brand { get; set; }
        string ClassName { get; set; }

        event EventHandler<bool> ConnectStatusEvent;

        void Connect();

        #region 读取操作

        bool[] ReadCoils(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadCoilsAsync(ushort startAddress, ushort numberOfPoints);
        bool[] ReadInputs(ushort startAddress, ushort numberOfPoints);
        Task<bool[]> ReadInputsAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadInputRegistersAsync(ushort startAddress, ushort numberOfPoints);
        ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints);
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort numberOfPoints);

        #endregion

        #region 写入操作

        void WriteSingleCoil(ushort coilAddress, bool value);
        Task WriteSingleCoilAsync(ushort coilAddress, bool value);
        void WriteSingleRegister(ushort registerAddress, ushort value);
        Task WriteSingleRegisterAsync(ushort registerAddress, ushort value);
        void WriteMultipleRegisters(ushort startAddress, ushort[] data);
        Task WriteMultipleRegistersAsync(ushort startAddress, ushort[] data);
        void WriteMultipleCoils(ushort startAddress, bool[] data);
        Task WriteMultipleCoilsAsync(ushort startAddress, bool[] data);
        ushort[] ReadWriteMultipleRegisters(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);
        Task<ushort[]> ReadWriteMultipleRegistersAsync(ushort startReadAddress, ushort numberOfPointsToRead, ushort startWriteAddress, ushort[] writeData);

        #endregion

        void Disconnect();
    }
    /// <summary>
    /// Modbus参数接口
    /// </summary>
    public interface IModbusParam
    {
        [JsonConverter(typeof(StringEnumConverter))]
        DevType DevType { get; set; }
        string DevName { get; set; }
        string UserDefinedName { get; set; }
        ushort HoldTime { get; set; }
        string ClassName { get; set; } //反序列化用来标识接口类型
    }
}
