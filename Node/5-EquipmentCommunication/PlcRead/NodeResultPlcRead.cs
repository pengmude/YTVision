using System.ComponentModel;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcRead
{
    public class NodeResultPlcRead : INodeResult
    {
        public int RunTime { get; set; }
        [DisplayName("读取结果")]
        public PlcReadResult ReadResult { get; set; }
    }
    /// <summary>
    /// plc读取结果类
    /// </summary>
    public class PlcReadResult
    {
        public object Data;
        public string DataType;
        public PlcReadResult(object data, string dataType)
        {
            Data = data;
            DataType = dataType;
        }
    }

}
