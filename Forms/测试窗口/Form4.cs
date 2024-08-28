using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Forms.测试窗口
{
    internal partial class Form4 : Form
    {
        Stack<Control> stack = new Stack<Control>();
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nodePreviewControl1.AddItem(textBox7.Text, Node.NodeType.CameraShot);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);

                Button newLabel = new Button();
                newLabel.Size = new Size(this.panel1.Size.Width - 5, 42);
                newLabel.Text = text;
                newLabel.Dock = DockStyle.Top;
                stack.Push(newLabel);
                Update();
            }
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void Update()
        {
            panel1.Controls.Clear();
            foreach (var item in stack)
            {
                panel1.Controls.Add(item);
            }
        }
    }
}
