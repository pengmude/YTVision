using Logger;
using System;
using System.Windows.Forms;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.Tool.DataShow;

namespace YTVisionPro.Node.ImageRead
{
    internal partial class ParamFormImageRead : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }

        public ParamFormImageRead()
        {
            InitializeComponent();
            // 图像显示窗口名称
            WindowNameList.Items.Add("[未设置]");
            for (int i = 0; i < FrmImageViewer.CurWindowsNum; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i + 1}");
            }
            WindowNameList.SelectedIndex = 0;
            CanvasSet.WindowNumChangeEvent += CanvasSet_WindowNumChangeEvent;
        }

        private void CanvasSet_WindowNumChangeEvent(object sender, int e)
        {
            WindowNameList.Items.Clear();
            WindowNameList.Items.Add("[未设置]");
            for (int i = 0; i < e; i++)
            {
                WindowNameList.Items.Add($"图像窗口{i + 1}");
            }
            WindowNameList.SelectedIndex = 0;
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
            nodeParamReadImage.WindowName = WindowNameList.Text;
            Params = nodeParamReadImage;
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamImageRead param)
            {

            }
        }
    }
}
