using System.Collections.Generic;
using YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByPLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByModbus
{
    internal class NodeParamSignalSendByModbus : INodeParam
    {
        //信号列表
        public List<SignalRowData> Data;

        //Modbus名称
        public string ModbusName;

        //文件路径
        public string Path;

        //OK信号的ModBus地址
        public string OKModbus;
        /// <summary>
        /// 信号保持时间
        /// </summary>
        public double StayTime;

        //NG信号的ModBus地址
        public string NGModbus;

        //保存订阅节点
        public string Text1;
        public string Text2;
    }
}
