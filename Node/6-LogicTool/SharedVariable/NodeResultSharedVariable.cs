using System;
using System.ComponentModel;

namespace YTVisionPro.Node._6_LogicTool.SharedVariable
{
    internal class NodeResultSharedVariable : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("读到的共享变量")]
        public object Value { get; set; }

        [DisplayName("共享变量的类型")]
        public SharedVarTypeEnum Type { get; set; }
    }
}
