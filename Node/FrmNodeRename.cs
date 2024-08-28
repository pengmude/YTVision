using System;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    internal partial class FrmNodeRename : Form
    {
        public event EventHandler<string> RenameChangeEvent;

        public FrmNodeRename(NodeBase node)
        {
            InitializeComponent();
            textBox1.Text = node.NodeName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RenameChangeEvent?.Invoke(this, textBox1.Text);
            Hide();
        }
    }
}
