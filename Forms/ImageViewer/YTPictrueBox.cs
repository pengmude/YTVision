using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ImageViewer
{
    internal partial class YTPictrueBox : UserControl
    {
        Point _srcDragLoc = new Point();                        // 图像拖动前鼠标按下的位置
        bool _isMove = false;                                       // 是否拖拽移动
        int _zoomStep = 100;                                        // 缩放步长
        Point _srcResizeLoc = new Point();                                // 保存缩放前的位置
        Size _srcResizeSize = new Size();                                 // 保存缩放前的大小
        Image _srcImg = null;                                       // 原图
        Image _renderImg = null;                                    // 渲染图
        Image _curImg = null;                                       // 当前图
        DisplayImageType _showType = DisplayImageType.RENDERIMG;    // 当前显示的是渲染图还是原图


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
        /// 原图
        /// </summary>
        public Image SrcImage 
        {
            get => _srcImg;
            set
            {
                _srcImg = value;
                // 没有渲染图就显示原图
                if(_renderImg == null)
                {
                    DisplayImageType = DisplayImageType.SRCIMG;
                    this.渲染图ToolStripMenuItem.Checked = false;
                    this.原图ToolStripMenuItem.Checked = true;
                }
            }
        }
        /// <summary>
        /// 渲染图
        /// </summary>
        public Image RenderImage
        {
            get => _renderImg;
            set
            {
                _renderImg = value;
                // 设置了渲染图就显示渲染图
                DisplayImageType = DisplayImageType.RENDERIMG;
                this.渲染图ToolStripMenuItem.Checked = true;
                this.原图ToolStripMenuItem.Checked = false;
            }
        }
        /// <summary>
        /// 当前窗口显示的图像类型（原图/渲染图）
        /// </summary>
        public DisplayImageType DisplayImageType
        {
            get => _showType;
            set
            {
                _showType = value;
                SetDisplayImageType(_showType);
            }
        }
        /// <summary>
        /// 点击显示原图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 原图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayImageType = DisplayImageType.SRCIMG;
            this.渲染图ToolStripMenuItem.Checked = false;
            this.原图ToolStripMenuItem.Checked = true;
        }
        /// <summary>
        /// 点击显示渲染图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 渲染图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayImageType = DisplayImageType.RENDERIMG;
            this.渲染图ToolStripMenuItem.Checked = true;
            this.原图ToolStripMenuItem.Checked = false;
        }
        /// <summary>
        /// 设置图像居中显示
        /// </summary>
        private void SetLocationCenter()
        {
            // 设置 PictureBox 的位置和大小，使其居中
            int picWidth = pictureBox1.Width;
            int picHeight = pictureBox1.Height;
            int left = (this.Width - picWidth) / 2;
            int top = (this.Height - picHeight) / 2;
            pictureBox1.Location = new Point(left, top);
        }
        /// <summary>
        /// 设置当前显示的图像类型（原图还是渲染图）
        /// </summary>
        /// <param name="displayImageType"></param>
        private void SetDisplayImageType(DisplayImageType displayImageType)
        {
            if (displayImageType == DisplayImageType.SRCIMG)
                _curImg = _srcImg;
            if(displayImageType == DisplayImageType.RENDERIMG)
                _curImg = _renderImg;
            pictureBox1.Image = _curImg;
            SetLocationCenter();
            // 保存初始位置和大小，用于还原
            _srcResizeLoc = pictureBox1.Location;
            _srcResizeSize = pictureBox1.Size;
        }
        /// <summary>
        /// 鼠标在图像控件上缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_curImg != null)
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
        }
        /// <summary>
        /// 鼠标左键在图像控件上按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(_curImg != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _srcDragLoc.X = Cursor.Position.X; //记录鼠标左键按下时位置
                    _srcDragLoc.Y = Cursor.Position.Y;
                    _isMove = true;
                    pictureBox1.Focus(); //鼠标滚轮事件(缩放时)需要picturebox有焦点
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
            if (_curImg != null)
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
        }
        /// <summary>
        /// 鼠标左键弹起，停止拖动图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_curImg != null)
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
            if (_curImg != null)
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
            if (_curImg != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isMove = false;
                }
            }
        }

        private void YTPictrueBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_curImg != null)
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
        }

        private void 还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_curImg != null)
            {
                pictureBox1.Location = _srcResizeLoc;
                pictureBox1.Size = _srcResizeSize;
            }
        }

        private void 保存渲染图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                    YTMessageBox.YTMessageBox yTMessageBox = new YTMessageBox.YTMessageBox("保存图像成功！");
                    yTMessageBox.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存图像失败！");
                }
            }
        }
    }

    public enum DisplayImageType
    {
        SRCIMG,
        RENDERIMG
    }
}
