using System;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    internal partial class FrmNodeRename : Form
    {
        public static event EventHandler<RenameResult> RenameChangeEvent;
        private string nodeNameOld;
        private NodeBase node;
        public FrmNodeRename(NodeBase node)
        {
            InitializeComponent();
            textBox1.Text = node.NodeName;
            nodeNameOld = node.NodeName;
            this.node = node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nodeNameNew = textBox1.Text;
            RenameChangeEvent?.Invoke(this, new RenameResult(nodeNameOld, nodeNameNew, node.ID, node.NodeType));
            nodeNameOld = nodeNameNew;
            Hide();
        }
    }
    /// <summary>
    /// 重命名结果
    /// </summary>
    internal struct RenameResult 
    {
        public string NodeNameOld;
        public string NodeNameNew;
        public int NodeId;
        public NodeType Type;
        public RenameResult(string oldName, string newName, int id, NodeType type)
        {
            NodeNameOld = oldName;
            NodeNameNew = newName;
            NodeId = id;
            Type = type;
        }
    }

}
