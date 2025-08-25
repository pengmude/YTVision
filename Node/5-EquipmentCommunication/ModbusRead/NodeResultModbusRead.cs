using System.ComponentModel;

namespace TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead
{
    public class NodeResultModbusRead : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("读取结果")]
        public ModbusReadResult ReadData { get; set; }
    }
    /// <summary>
    /// Modbus读取结果类
    /// </summary>
    public class ModbusReadResult
    {
        public object Data;
        public string DataType;
        public ModbusReadResult(object data, string dataType)
        {
            Data = data;
            DataType = dataType;
        }
    }

}
