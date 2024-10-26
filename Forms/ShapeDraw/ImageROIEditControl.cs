using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ShapeDraw
{
    public partial class ImageROIEditControl : UserControl
    {
        private ROIManager roiManager;
        private ROI currentROI; // 当前正在绘制的 ROI
        private bool isDrawing = false; // 是否正在绘制
        private bool isDraging = false; // 是否正在拖拽
        private Point startPoint; // 绘制开始点
        private ROIType selectedROIType; // 选中要绘制的 ROI 类型
        private ROI selectedROI = null; // 当前选中的ROI
        private ROISelectionState ROISelectionState = ROISelectionState.None; // 记录选中ROI的哪个部位

        public ImageROIEditControl()
        {
            InitializeComponent();
            roiManager = new ROIManager(pictureBox1);
        }

        /// <summary>
        /// 设置图像
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetImage(Bitmap bitmap)
        {
            pictureBox1.Image = bitmap;
        }

        /// <summary>
        /// 清除ROI
        /// </summary>
        public void ClearROI() 
        {
            roiManager.ROIs.Clear();
            pictureBox1.Invalidate();
        }

        /// <summary>
        /// 设置要绘制的ROI种类
        /// </summary>
        /// <param name="type"></param>
        public void SetROIType2Draw(ROIType type) 
        {
            selectedROIType = type;
            SetContextMenu(type);
        }

        private void SetContextMenu(ROIType type)
        {
            switch (type)
            {
                case ROIType.None:
                    清除全部ToolStripMenuItem.Visible = true;
                    矩形ToolStripMenuItem1.Visible = false;
                    圆形ToolStripMenuItem1.Visible = false;
                    直线卡尺ToolStripMenuItem.Visible = false;
                    圆卡尺ToolStripMenuItem.Visible = false;
                    break;
                case ROIType.Rectangle:
                case ROIType.Circle:
                    清除全部ToolStripMenuItem.Visible = true;
                    矩形ToolStripMenuItem1.Visible = true;
                    圆形ToolStripMenuItem1.Visible = true;
                    直线卡尺ToolStripMenuItem.Visible = false;
                    圆卡尺ToolStripMenuItem.Visible = false;
                    break;
                case ROIType.CaliperCircle:
                case ROIType.CaliperRect:
                    清除全部ToolStripMenuItem.Visible = true;
                    矩形ToolStripMenuItem1.Visible = false;
                    圆形ToolStripMenuItem1.Visible = false;
                    直线卡尺ToolStripMenuItem.Visible = true;
                    圆卡尺ToolStripMenuItem.Visible = true;
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 获取ROI截取的图像
        /// </summary>
        /// <returns></returns>
        public Bitmap GetROIImages()
        {
            var rois = roiManager.GetROIImages();
            if (rois.Count != 0)
                return rois.First();
            return null;
        }

        /// <summary>
        /// 默认只获取一个ROI
        /// </summary>
        /// <returns></returns>
        public ROI GetROI() 
        {
            if(roiManager.ROIs.Count > 0)
                return roiManager.ROIs.First();
            else return null;
        }

        public Rectangle GetImageROIRect()
        {
            if(roiManager.ROIs.Count == 0)
                throw new Exception("ROI为空！");
            return roiManager.ROIs[0].GetROIRect(pictureBox1);
        }

        /// <summary>
        /// 设置ROI
        /// </summary>
        /// <param name="image"></param>
        public void SetROI(ROI roi)
        {
            roiManager.ROIs.Clear();
            roiManager.AddROI(roi);
            pictureBox1.Invalidate();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { return; }
            startPoint = e.Location;
            // 处于绘制ROI的状态
            if (selectedROIType != ROIType.None)
            {
                ClearROI();
                switch (selectedROIType)
                {
                    case ROIType.Rectangle:
                        currentROI = new RectangleROI(new RectangleF(startPoint, new Size()), 0);
                        break;
                    case ROIType.Circle:
                        currentROI = new CircleROI(new RectangleF(startPoint, new Size()), 0);
                        break;
                }
                if (currentROI == null)
                    isDrawing = false;
                else
                    isDrawing = true;
                isDraging = false;
            }
            else
            {
                isDrawing = false;
                isDraging = true;

                // 判断按下位置是ROI还是ROI的控制点矩形
                ROISelectionState = ROIManager.GetClickedROIItem(roiManager, e.Location, pictureBox1, ref selectedROI);
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // 如果正在绘制ROI
            if (isDrawing)
            {
                var endPoint = e.Location;
                var rectF = new RectangleF( Math.Min(startPoint.X, endPoint.X),
                                Math.Min(startPoint.Y, endPoint.Y),
                                Math.Abs(endPoint.X - startPoint.X),
                                Math.Abs(endPoint.Y - startPoint.Y));

                currentROI.UpdateRect(rectF);

                pictureBox1.Invalidate(); // 重绘 PictureBox
                return;
            }
            // 如果正在拖拽ROI,或者拖拽ROI上的控制点矩形
            else
            {
                // 更新鼠标光标
                if (selectedROI != null)
                {
                    // 鼠标选中一个ROI并且移动时，设置在不同控制点上显示的鼠标光标
                    var rects = selectedROI.GetControlPointRects();
                    int flag = 0;
                    for (int i = 0; i < rects.Length; ++i)
                    {
                        if (rects[i].Contains(e.Location))
                        {
                            switch (i)
                            {
                                case 0: // 左上
                                    Cursor = Cursors.SizeNWSE;
                                    break;
                                case 1: // 右上
                                    Cursor = Cursors.SizeNESW;
                                    break;
                                case 2: // 右下
                                    Cursor = Cursors.SizeNWSE;
                                    break;
                                case 3: // 左下
                                    Cursor = Cursors.SizeNESW;
                                    break;
                                case 4: // 中心
                                    break;
                            }
                            flag++;
                        }
                    }
                    if (flag == 0)
                        Cursor = Cursors.Default;
                }

                if (isDraging)
                {
                    switch (ROISelectionState)
                    {
                        case ROISelectionState.None:
                            break;
                        case ROISelectionState.ROIOnly:
                            selectedROI.Move(new PointF(e.X - startPoint.X, e.Y - startPoint.Y));
                            break;
                        case ROISelectionState.TopLeft:
                            selectedROI.UpdateRect(new RectangleF(e.X, e.Y, selectedROI.Rectangle.Right - e.X, selectedROI.Rectangle.Bottom - e.Y));
                            break;
                        case ROISelectionState.TopRight:
                            selectedROI.UpdateRect(new RectangleF(selectedROI.Rectangle.Left, e.Y, e.X - selectedROI.Rectangle.Left, selectedROI.Rectangle.Bottom - e.Y));
                            //selectedROI.Rectangle = new RectangleF(selectedROI.Rectangle.Left, e.Y, e.X - selectedROI.Rectangle.Left, selectedROI.Rectangle.Bottom - e.Y);
                            break;
                        case ROISelectionState.BottomRight:
                            selectedROI.UpdateRect(new RectangleF(selectedROI.Rectangle.Left, selectedROI.Rectangle.Top, e.X - selectedROI.Rectangle.Left, e.Y - selectedROI.Rectangle.Top));
                            //selectedROI.Rectangle = new RectangleF(selectedROI.Rectangle.Left, selectedROI.Rectangle.Top, e.X - selectedROI.Rectangle.Left, e.Y - selectedROI.Rectangle.Top);
                            break;
                        case ROISelectionState.BottomLeft:
                            selectedROI.UpdateRect(new RectangleF(e.X, selectedROI.Rectangle.Top, selectedROI.Rectangle.Right - e.X, e.Y - selectedROI.Rectangle.Top));
                            //selectedROI.Rectangle = new RectangleF(e.X, selectedROI.Rectangle.Top, selectedROI.Rectangle.Right - e.X, e.Y - selectedROI.Rectangle.Top);
                            break;
                        default:
                            break;
                    }
                    #region 实时更新鼠标位置

                    // 更新鼠标位置
                    //mousePosition = e.Location;
                    // 请求重绘 PictureBox
                    //pictureBox1.Invalidate();

                    #endregion

                    startPoint = e.Location;
                    pictureBox1.Invalidate();
                }
            }
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // 如果在是绘制ROI状态，鼠标弹起应重置状态
            if (isDrawing)
            {
                isDrawing = false;
                roiManager.AddROI(currentROI);
                selectedROI = currentROI;
                selectedROIType = ROIType.None;
            }
            else
            {
                isDraging = false;
                ROISelectionState = ROISelectionState.None;
            }
        }

        /// <summary>
        /// pictureBox重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Green, 2);
            roiManager.DrawROIs(e.Graphics, pen);

            if (isDrawing && currentROI != null)
            {
                pen.Color = Color.Red;
                currentROI.Draw(e.Graphics, pen); // 使用不同的颜色表示正在绘制的 ROI
            }
        }

        private void 清除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearROI();
        }

        private void 矩形ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectedROIType = ROIType.Rectangle;
        }

        private void 圆形ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            selectedROIType = ROIType.Circle;
        }

        private void 直线卡尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedROIType = ROIType.CaliperRect;
        }

        private void 圆卡尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedROIType = ROIType.CaliperCircle;
        }
    }
}
