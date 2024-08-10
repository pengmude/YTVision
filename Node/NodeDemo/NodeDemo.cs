using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Node.NodeDemo
{
    public class NodeDemo : NodeBase, INode<NodeParamDemo, NodeResultDemo>
    {
        public override INodeParamForm ParamForm { get; set; }

        public NodeParamDemo Param { get; set; }
        public NodeResultDemo Result { get; private set; }

        public void Run()
        {
            Result = new NodeResultDemo();
            throw new NotImplementedException();
        }

        protected override void ShowSettingsWindow()
        {
            if (base.Active)
                ((NodeParamSetDemo)ParamForm).ShowDialog();
        }

        //TODO: 目前 方案保存List<NodeBase>就能够兼容保存所有节点
        // 节点运行时间、是否成功标志、运行状态码等必要结果，设在INodeResult接口类中必须实现

    }
}
