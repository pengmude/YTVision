using System;

namespace YTVisionPro.Device.Modbus
{
    internal interface IModbus : IDevice
    {
        string DevName { get; set; }
        string UserDefinedName { get; set; }

        ModbusParam ModbusParam { get; set; }

        bool IsConnect { get; set; }
        
        DevType DevType { get; set; }
        DeviceBrand Brand { get; set; }
        string ClassName { get; set; }

        event EventHandler<bool> ConnectStatusEvent;

        void Connect();

        void Disconnect();
    }
}
