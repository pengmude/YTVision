using System;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    internal partial class FormAddNotes : Form
    {
        private NodeBase _node;
        public FormAddNotes(NodeBase node)
        {
            InitializeComponent();
            _node = node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _node.SetNotes(textBox1.Text);
            Close();
        }
    }
}
