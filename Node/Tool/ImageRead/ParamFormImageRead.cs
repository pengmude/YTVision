using Logger;
using System;
using System.Windows.Forms;

namespace YTVisionPro.Node.ImageRead
{
    internal partial class ParamFormImageRead : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }

        public ParamFormImageRead()
        {
            InitializeComponent();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node) { }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "BMP图片|*.BMP|所有图片|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("未选择图片");
                LogHelper.AddLog(MsgLevel.Fatal, "未选择图片", true);
                return;
            }
            NodeParamImageRead nodeParamReadImage = new NodeParamImageRead();
            nodeParamReadImage.ImagePath = this.textBox1.Text;
            Params = nodeParamReadImage;
            Hide();
        }
    }
}
