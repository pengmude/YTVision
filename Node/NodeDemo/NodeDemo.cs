using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.NodeDemo
{
    public class NodeDemo : NodeBase, INode<NodeParamSetDemo, NodeParamDemo, NodeResultDemo>
    {
        /// <summary>
        /// 创建一个指定名称的节点
        /// </summary>
        /// <param name="nodeText"></param>
        public NodeDemo(string nodeText)
        {
            SetNodeText(nodeText);
        }

        /// <summary>
        /// 参数设置窗口
        /// </summary>
        NodeParamSetDemo INode<NodeParamSetDemo, NodeParamDemo, NodeResultDemo>.ParamForm
        {
            get => (NodeParamSetDemo)base.ParamForm;
            set => base.ParamForm = value;
        }

        /// <summary>
        /// 节点参数
        /// </summary>
        public NodeParamDemo Param { get; set; }

        /// <summary>
        /// 节点结果
        /// </summary>
        public NodeResultDemo Result { get; private set; }

        /// <summary>
        /// 节点运行
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Run()
        {
            Result = new NodeResultDemo();
        }

        /// <summary>
        /// 给节点设置文本
        /// </summary>
        /// <param name="text"></param>
        void INode<NodeParamSetDemo, NodeParamDemo, NodeResultDemo>.SetNodeText(string text)
        {
            base.SetNodeText(text);
        }

    }
}
