using System.Collections.Generic;

namespace YTVisionPro.Node._5_EquipmentCommunication.SendResultByPLC
{
    internal class NodeParamSendResultByPLC : INodeParam
    {
        public List<SignalRowData> Data;
        // PLC名称
        public string PlcName;

        //文件路径
        public string Path;

        //OK信号的PLC地址
        public string OKPLC;

        //NG信号的PLC地址
        public string NGPLC;

        // 传统算法NG信号的PLC地址
        public string AlgorithmNG;

        //保存订阅节点
        public string Text1;
        public string Text2;
        public string Text3;
        public string Text4;
        /// <summary>
        /// 信号保持时间
        /// </summary>
        public double StayTime;
    }

    public class SignalRowData
    {
        public string DevName;
        public string NodeName;
        public string ClassName;
        public string DetectName;
        public int SignalLevel;
        public string SignalAddress;
    }

    public class NodeToClassName
    {
        public string NodeName;
        public string[] ClassNames;
    }

    public class NodeInfos
    {
        public NodeInfo[] NodeInfo { get; set; }
    }

    internal enum SiganlType
    {
        /// <summary>
        /// OK信号
        /// </summary>
        OK,
        /// <summary>
        /// AI NG信号
        /// </summary>
        AING,
        /// <summary>
        /// 非AI NG信号
        /// </summary>
        NonAING
    }
}
