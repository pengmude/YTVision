using System;
using System.Windows.Forms;

namespace TDJS_Vision.Node
{
    public partial class FormAddNotes : FormBase
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
