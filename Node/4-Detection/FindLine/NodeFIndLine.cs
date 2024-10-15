using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.FindLine
{
    /// <summary>
    /// 直线查找节点
    /// </summary>
    internal class NodeFIndLine : NodeBase
    {
        public NodeFIndLine(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new NodeParamFormFindLine();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultFindLine();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {

        }
    }
}
