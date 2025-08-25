using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDJS_Vision
{
    /// <summary>
    /// 这个类是为了替换Form不能更改标题栏背景颜色而定义的
    /// 提供一个基础窗体类，包含自定义标题栏和拖动功能。
    /// </summary>
    public partial class FormBase : Form
    {
        private bool dragging = false;
        private Point offset;
        public FormBase()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 确保此时 Icon 已经被子类设置
            if (Icon != null)
            {
                pictureBoxIcon.Image = Icon.ToBitmap();
            }
            labelTitle.Text = Text; // 设置标题栏文本为窗体标题
            labelMinBox.Visible = MinimizeBox;
            labelMaxBox.Visible = MaximizeBox;
            FormBorderStyle = FormBorderStyle.None; // 去掉默认边框
        }
        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 最大化或还原窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                labelMaxBox.Text = "🗗"; // 可选：还原图标
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                labelMaxBox.Text = "🗖"; // 可选：最大化图标
            }
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 鼠标在标题栏按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                offset = new Point(e.X, e.Y);
            }
        }
        /// <summary>
        /// 鼠标在标题栏移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }
        /// <summary>
        /// 鼠标在标题栏松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        /// <summary>
        /// 双击标题栏切换窗口状态（最大化/还原）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableLayoutPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(!labelMaxBox.Visible)
                    return;
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                    labelMaxBox.Text = "🗗"; // 可选：还原图标
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    labelMaxBox.Text = "🗖"; // 可选：最大化图标
                }
            }
        }
    }
}
