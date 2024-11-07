using System.Collections.Generic;

namespace YTVisionPro.Node.ResultProcessing.AIResultSendByPLC
{
    internal class NodeParamHTAISendSignal : INodeParam
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

        //保存订阅节点
        public string Text1;
        public string Text2;
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
}
