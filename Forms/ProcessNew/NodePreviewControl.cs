using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ProcessNew
{
    public partial class NodeComboBox : UserControl
    {
        private bool _expanded = false;
        private static int _count = 0;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("设置当前是否展开")]
        public bool Expanded { get { return _expanded; } set { SetShowStatus(value); } }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("设置标题文本")]
        public string Text
        { 
            get=>button1.Text.Trim('◀').Trim('▼');
            set
            {
                string tmp = value.ToString();
                if (Expanded)
                    button1.Text = "▼" + tmp;
                else
                    button1.Text = "◀" + tmp;
            }

        }

        public NodeComboBox()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 点击展开/折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _expanded = !_expanded;
            SetShowStatus(_expanded);
        }

        /// <summary>
        /// 设置展开和折叠的状态
        /// </summary>
        /// <param name="visible"></param>
        public void SetShowStatus(bool visible)
        {
            _expanded = visible;
            panel2.Visible = visible;
            if (visible)
                button1.Text = button1.Text.Replace("◀", "▼");
            else
                button1.Text = button1.Text.Replace("▼", "◀");
        }

        /// <summary>
        /// 添加一项
        /// </summary>
        /// <param name="itemName"></param>
        public void AddItem(string itemName)
        {
            Label label = new Label();
            label.BackColor = SystemColors.ButtonHighlight;
            label.Dock = DockStyle.Top;
            label.Name = $"label{++_count}";
            label.Size = new Size(290, 32);
            label.Text = itemName;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.MouseEnter += label1_MouseEnter;
            label.MouseLeave += label1_MouseLeave;
            label.MouseDown += label1_MouseDown;
            label.MouseMove += label1_MouseMove;
            panel2.Controls.Add(label);
        }

        /// <summary>
        /// 鼠标进入时改变背景颜色和光标样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.BackColor = SystemColors.ControlDark;
            label.Cursor = Cursors.SizeAll;
        }

        /// <summary>
        /// 鼠标离开时恢复原来的颜色和光标样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.BackColor = SystemColors.ButtonHighlight;
            label.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 拖拽功能——鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(label.Text, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// 鼠标拖拽移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (e.Clicks > 0))
            {
                DoDragDrop(this.Text, DragDropEffects.Move);
            }
        }
    }
}
