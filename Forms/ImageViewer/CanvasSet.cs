using System;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ImageViewer
{
    public partial class CanvasSet : Form
    {
        /// <summary>
        /// 画布数量改变事件
        /// </summary>
        public static event EventHandler<int> WindowNumChangeEvent;
        /// <summary>
        /// 保存画布布局事件
        /// </summary>
        public static EventHandler SaveDockPanelEvent;

        public CanvasSet()
        {
            InitializeComponent();
            foreach (var control in tableLayoutPanel17.Controls)
            {
                if(control is TableLayoutPanel tableLayoutPanel)
                {
                    tableLayoutPanel.MouseClick += TableLayoutPanel_MouseClick;
                    foreach (var con in tableLayoutPanel.Controls)
                    {
                        if (con is Label label)
                        {
                            label.MouseClick += Label_MouseClick;
                        }
                    }
                }
            }
        }

        private void TableLayoutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)sender;
            int selected = int.Parse(tableLayoutPanel.Name.Remove(0, 16));
            foreach (var control in tableLayoutPanel17.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    if (radioButton.Name == $"radioButton{selected}")
                    {
                        radioButton.Checked = true;
                    }
                }
            }
        }

        private void Label_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            int selected = int.Parse(label.Parent.Name.Remove(0, 16));
            foreach (var control in tableLayoutPanel17.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    if (radioButton.Name == $"radioButton{selected}")
                    {
                        radioButton.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var control in tableLayoutPanel17.Controls)
            {
                if(control is RadioButton radioButton)
                {
                    if(radioButton.Checked)
                    {
                        string text = radioButton.Text;
                        text = text.Remove(0, 2);
                        int num = int.Parse(text);
                        WindowNumChangeEvent?.Invoke(this, num);
                    }
                }
            }
            SaveDockPanelEvent?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
