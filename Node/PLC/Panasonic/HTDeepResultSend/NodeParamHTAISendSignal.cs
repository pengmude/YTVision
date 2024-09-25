﻿using JsonSubTypes;
using System.Collections.Generic;
using YTVisionPro.Node.Light;

namespace YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend
{
    internal class NodeParamHTAISendSignal : INodeParam
    {
        public List<SignalRowData> Data;
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
