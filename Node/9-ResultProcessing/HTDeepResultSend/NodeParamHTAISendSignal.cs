using System.Collections.Generic;

namespace YTVisionPro.Node.ResultProcessing.HTDeepResultSend
{
    internal class NodeParamHTAISendSignal : INodeParam
    {
        public List<SignalRowData> Data;

        //文件路径
        public string Path;

        //OK信号的PLC地址
        public string OKPLC;

        //NG信号的PLC地址
        public string NGPLC;

        //保存订阅节点
        public string Text1;
        public string Text2;
    }

    public class SignalRowData
    {
        public string UserNamePlc;
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
