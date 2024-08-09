using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node
{
    public interface INode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        int ID {  get; }

        /// <summary>
        /// 节点是否启用
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// 节点是否选中
        /// </summary>
        bool Selected {  get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        string NodeName {  get; set; }

        /// <summary>
        /// 节点所属的流程
        /// </summary>
        Process Process { get; }

        /// <summary>
        /// 节点的参数
        /// </summary>
        INodeParam NodeParam { get; set; }

        /// <summary>
        /// 节点的结果
        /// </summary>
        INodeResult NodeResult { get; set; }
    }

    /// <summary>
    /// 节点参数
    /// </summary>
    public interface INodeParam 
    {
        
    }

    /// <summary>
    /// 节点结果
    /// </summary>
    public interface INodeResult
    {
        bool Success { get; set; }
    }
}
