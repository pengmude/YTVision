using System.ComponentModel;

namespace TDJS_Vision.Node._5_EquipmentCommunication.AIResultSend
{
    public class NodeParamSignalSend : INodeParam
    {
        /// <summary>
        /// 订阅的AI结果文本
        /// </summary>
        public string Text1;
        public string Text2;

        ///// <summary>
        ///// 设备对象
        ///// </summary>
        //[JsonIgnore]
        //public IModbus ModbusDevice;

        ///// <summary>
        ///// Modbus设备名称
        ///// </summary>
        //public string ModbusName;

        /// <summary>
        /// 是否发送到寄存器
        /// </summary>
        public bool IsRegister;
        
        /// <summary>
        /// 信号自动重置
        /// </summary>
        public bool IsAutoReset;

        /// <summary>
        /// 信号保持时间
        /// </summary>
        public int SignalHoldTime;

        /// <summary>
        /// 信号列表绑定的数据
        /// </summary>
        public BindingList<DetectItemRow> BindingData;
    }
}
