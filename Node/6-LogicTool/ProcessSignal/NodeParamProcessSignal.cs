
namespace TDJS_Vision.Node._6_LogicTool.ProcessSignal
{
    public class NodeParamProcessSignal : INodeParam
    {
        /// <summary>
        /// 新建信号名称
        /// </summary>
        public string NewSignalName { get; set; }
        /// <summary>
        /// 新建信号发送次数
        /// </summary>
        public int NewSignalSendTimes { get; set; }
        /// <summary>
        /// 是发送信号，还是等待信号
        /// </summary>
        public bool IsSendSignal { get; set; }
        /// <summary>
        /// 发送信号的名称
        /// </summary>
        public string SendSignalName { get; set; }
        /// <summary>
        /// 等待信号的名称
        /// </summary>
        public string WaitSignalName { get; set; }
    }
}
