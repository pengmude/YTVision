using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.ImageViewer
{
    public partial class YTPictrueBox : UserControl
    {
        Point _srcDragLoc = new Point();                                    // 图像拖动前鼠标按下的位置
        bool _isMove = false;                                               // 是否拖拽移动
        int _zoomStep = 100;                                                // 缩放步长
        Point _srcResizeLoc = new Point();                                  // 保存缩放前的位置
        Size _srcResizeSize = new Size();                                   // 保存缩放前的大小

        public YTPictrueBox()
        {
            InitializeComponent();
            // 设置图像的最小尺寸防止缩小到看不见
            pictureBox1.MinimumSize = new Size(100, 100);
            //绑定事件处理函数
            this.MouseDown += YTPictrueBox_MouseDown;
            this.MouseMove += YTPictrueBox_MouseMove;
            this.MouseUp += YTPictrueBox_MouseUp;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            // 绑定上下文菜单
            this.ContextMenuStrip = contextMenuStrip1;
        }

        /// <summary>
        /// 设置要显示的图片
        /// </summary>
        public Image Image
        {
            get => pictureBox1.Image;
            set
            {
                try
                {
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    pictureBox1.Image = value;
                    _srcResizeLoc = pictureBox1.Location;
                    _srcResizeSize = pictureBox1.Size;
                    SetLocationCenter();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// 设置图像居中显示
        /// </summary>
        private async void SetLocationCenter()
        {
            if (pictureBox1.InvokeRequired)
            {
                // 使用 BeginInvoke 异步地执行设置位置的操作
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        int picWidth = pictureBox1.Width;
                        int picHeight = pictureBox1.Height;
                        int left = (this.Width - picWidth) / 2;
                        int top = (this.Height - picHeight) / 2;
                        pictureBox1.Location = new Point(left, top);
                    }
                    catch (Exception ex) { }
                }));
            }
            else
            {
                // 如果当前线程就是 UI 线程，直接设置位置
                int picWidth = pictureBox1.Width;
                int picHeight = pictureBox1.Height;
                int left = (this.Width - picWidth) / 2;
                int top = (this.Height - picHeight) / 2;
                pictureBox1.Location = new Point(left, top);
            }
        }

        /// <summary>
        /// 鼠标在图像控件上缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                try
                {
                    // 图像初次显示时候需要填充，缩放时不能使用填充，否则无法缩放
                    if (pictureBox1.Dock == DockStyle.Fill)
                        pictureBox1.Dock = DockStyle.None;

                    int x = e.Location.X;
                    int y = e.Location.Y;
                    int ow = pictureBox1.Width;
                    int oh = pictureBox1.Height;
                    int VX, VY; //因缩放产生的位移矢量
                    if (e.Delta > 0) //放大
                    {
                        //第①步
                        pictureBox1.Width += _zoomStep;
                        pictureBox1.Height += _zoomStep;
                        //第②步
                        PropertyInfo pInfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
                        BindingFlags.NonPublic);
                        Rectangle rect = (Rectangle)pInfo.GetValue(pictureBox1, null);
                        //第③步
                        pictureBox1.Width = rect.Width;
                        pictureBox1.Height = rect.Height;
                    }
                    if (e.Delta < 0) //缩小
                    {
                        // 防止一直缩成负值或过小
                        if (pictureBox1.Width - _zoomStep >= pictureBox1.MinimumSize.Width)
                        {
                            pictureBox1.Width -= _zoomStep;
                            pictureBox1.Height -= _zoomStep;
                        }
                        else
                        {
                            pictureBox1.Width -= _zoomStep;
                            pictureBox1.Height -= _zoomStep;
                            PropertyInfo pInfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
                            BindingFlags.NonPublic);
                            Rectangle rect = (Rectangle)pInfo.GetValue(pictureBox1, null);
                            pictureBox1.Width = rect.Width;
                            pictureBox1.Height = rect.Height;
                        }
                    }
                    //第④步，求因缩放产生的位移，进行补偿，实现锚点缩放的效果
                    VX = (int)((double)x * (ow - pictureBox1.Width) / ow);
                    VY = (int)((double)y * (oh - pictureBox1.Height) / oh);
                    pictureBox1.Location = new Point(pictureBox1.Location.X + VX, pictureBox1.Location.Y + VY);
                }
                catch (Exception) { }

            }
        }
        /// <summary>
        /// 鼠标左键在图像控件上按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    try
                    {
                        _srcDragLoc.X = Cursor.Position.X; //记录鼠标左键按下时位置
                        _srcDragLoc.Y = Cursor.Position.Y;
                        _isMove = true;
                        pictureBox1.Focus(); //鼠标滚轮事件(缩放时)需要picturebox有焦点
                    }
                    catch (Exception) { }
                }
            }
        }
        /// <summary>
        /// 鼠标左键拖动图像控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    pictureBox1.Focus(); //鼠标在picturebox上时才有焦点，此时可以缩放
                    if (_isMove)
                    {
                        int x, y; //新的pictureBox1.Location(x,y)
                        int moveX, moveY; //X方向，Y方向移动大小。
                        moveX = Cursor.Position.X - _srcDragLoc.X;
                        moveY = Cursor.Position.Y - _srcDragLoc.Y;
                        x = pictureBox1.Location.X + moveX;
                        y = pictureBox1.Location.Y + moveY;
                        pictureBox1.Location = new Point(x, y);
                        _srcDragLoc.X = Cursor.Position.X;
                        _srcDragLoc.Y = Cursor.Position.Y;
                    }
                }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// 鼠标左键弹起，停止拖动图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isMove = false;
                }
            }
        }
        /// <summary>
        /// 在自定义控件本身按下鼠标左键，记录拖动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YTPictrueBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _srcDragLoc.X = Cursor.Position.X; //记录鼠标左键按下时位置
                    _srcDragLoc.Y = Cursor.Position.Y;
                    _isMove = true;
                }
            }
        }

        private void YTPictrueBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isMove = false;
                }
            }
        }

        private void YTPictrueBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    this.Focus(); //鼠标不在picturebox上时焦点给别的控件，此时无法缩放
                    if (_isMove)
                    {
                        int x, y; //新的pictureBox1.Location(x,y)
                        int moveX, moveY; //X方向，Y方向移动大小。
                        moveX = Cursor.Position.X - _srcDragLoc.X;
                        moveY = Cursor.Position.Y - _srcDragLoc.Y;
                        x = pictureBox1.Location.X + moveX;
                        y = pictureBox1.Location.Y + moveY;
                        pictureBox1.Location = new Point(x, y);
                        _srcDragLoc.X = Cursor.Position.X;
                        _srcDragLoc.Y = Cursor.Position.Y;
                    }
                }
                catch (Exception) { }
            }
        }

        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Save(saveFileDialog1.FileName);
                        YTMessageBox.YTMessageBox yTMessageBox = new YTMessageBox.YTMessageBox("图像保存成功！");
                        yTMessageBox.Show();
                    }
                    else
                    {
                        MessageBoxTD.Show("图像为空！");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxTD.Show("图像保存失败！");
                    return;
                }
            }
        }

        private void 默认大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
        }

        private void 上次大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Location = _srcResizeLoc;
                pictureBox1.Size = _srcResizeSize;
            }
        }

        private void 清空图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }
    }
}
