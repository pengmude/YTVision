using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp;

namespace TDJS_Vision.Forms.ShapeDraw
{
    public partial class ImageROIEditControl : UserControl
    {
        private ROIManager roiManager;
        private ROI currentROI; // 当前正在绘制的 ROI
        private Status CurStatus = Status.ReadyMove; // 当前状态
        private System.Drawing.Point startPoint; // 绘制开始点
        private ROIType selectedROIType = ROIType.Rectangle; // 选中要绘制的 ROI 类型
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
        }
        /// <summary>
        /// 获取所有ROI截取的图像
        /// </summary>
        /// <returns>List of Bitmap</returns>
        public List<Mat> GetROIImages()
        {
            return roiManager.GetROIImages();
        }

        /// <summary>
        /// 获取所有ROI对象
        /// </summary>
        /// <returns>List of ROI</returns>
        public List<ROI> GetROIs()
        {
            return new List<ROI>(roiManager.ROIs);
        }

        /// <summary>
        /// 获取所有ROI对应的矩形区域（相对于图像）
        /// </summary>
        /// <returns>List of Rectangle</returns>
        public List<Rect> GetImageROIRects()
        {
            // 没绘制ROI默认返回全图区域
            if (roiManager.ROIs.Count == 0)
                return new List<Rect>() { new Rect(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height) };
            List<Rect> rects = new List<Rect>();
            foreach (var roi in roiManager.ROIs)
            {
                rects.Add(roi.GetROIRect(pictureBox1));
            }
            return rects;
        }

        /// <summary>
        /// 设置多个ROI
        /// </summary>
        /// <param name="rois">要设置的ROI集合</param>
        public void SetROIs(List<ROI> rois)
        {
            if (rois == null)
                throw new ArgumentNullException(nameof(rois));

            roiManager.ROIs.Clear();
            rois.ForEach(roi => roiManager.AddROI(roi));
            currentROI = rois.Find(r => r.IsSelected);
            // 立即强制重绘，让ROI为实际区域
            this.PerformLayout();
            this.Parent?.Refresh();
        }
        /// <summary>
        /// 移除上一个ROI
        /// </summary>
        public void RemovePrevious()
        {
            roiManager.RemovePrevious();
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
            if (CurStatus == Status.ReadyDraw || CurStatus == Status.EndDraw)
            {
                switch (selectedROIType)
                {
                    case ROIType.Rectangle:
                        currentROI = new RectangleROI(new RectangleF(startPoint, new System.Drawing.Size()), 0);
                        break;
                    case ROIType.Circle:
                        currentROI = new CircleROI(new RectangleF(startPoint, new System.Drawing.Size()), 0);
                        break;
                }
                CurStatus = Status.StartDraw;
                return;
            }

            // 处于移动ROI的状态
            if (CurStatus == Status.ReadyMove || CurStatus == Status.EndMove || CurStatus == Status.EndEdit)
            {
                // 判断按下位置是ROI还是ROI的控制点矩形
                ROISelectionState = ROIManager.GetClickedROIItem(roiManager, e.Location, pictureBox1, ref selectedROI);
                switch (ROISelectionState)
                {
                    case ROISelectionState.None:
                        break;
                    case ROISelectionState.ROIOnly:
                        CurStatus = Status.StartMove;
                        break;
                    case ROISelectionState.TopLeft:
                    case ROISelectionState.TopRight:
                    case ROISelectionState.BottomLeft:
                    case ROISelectionState.BottomRight:
                        CurStatus = Status.StartEdit;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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

            // 如果是移动/编辑状态
            if (CurStatus == Status.StartMove && ROISelectionState == ROISelectionState.ROIOnly)
            {
                selectedROI.Move(new PointF(e.X - startPoint.X, e.Y - startPoint.Y));
                startPoint = e.Location;
                pictureBox1.Invalidate();
                return;
            }

            if (CurStatus == Status.StartEdit)
            {
                switch (ROISelectionState)
                {
                    case ROISelectionState.None:
                    case ROISelectionState.ROIOnly:
                        break;

                    case ROISelectionState.TopLeft:
                        // 左上角：左上点移动，右下固定
                        selectedROI.UpdateRect(new RectangleF(
                            e.X, e.Y,
                            selectedROI.Rectangle.Right - e.X,
                            selectedROI.Rectangle.Bottom - e.Y));
                        break;

                    case ROISelectionState.TopRight:
                        // 右上角：右上点移动，左下固定
                        selectedROI.UpdateRect(new RectangleF(
                            selectedROI.Rectangle.Left, e.Y,
                            e.X - selectedROI.Rectangle.Left,
                            selectedROI.Rectangle.Bottom - e.Y));
                        break;

                    case ROISelectionState.BottomLeft:
                        // 左下角：左下点移动，右上固定
                        selectedROI.UpdateRect(new RectangleF(
                            e.X, selectedROI.Rectangle.Top,
                            selectedROI.Rectangle.Right - e.X,
                            e.Y - selectedROI.Rectangle.Top));
                        break;

                    case ROISelectionState.BottomRight:
                        // 右下角：右下点移动，左上固定
                        selectedROI.UpdateRect(new RectangleF(
                            selectedROI.Rectangle.Left, selectedROI.Rectangle.Top,
                            e.X - selectedROI.Rectangle.Left,
                            e.Y - selectedROI.Rectangle.Top));
                        break;

                    default:
                        break;
                }
                startPoint = e.Location;
                pictureBox1.Invalidate();
                return;
            }

            // 如果是开始绘制状态
            if (CurStatus == Status.StartDraw)
            {
                var endPoint = e.Location;
                var rectF = new RectangleF(Math.Min(startPoint.X, endPoint.X),
                                Math.Min(startPoint.Y, endPoint.Y),
                                Math.Abs(endPoint.X - startPoint.X),
                                Math.Abs(endPoint.Y - startPoint.Y));
                if (currentROI != null)
                    currentROI.UpdateRect(rectF);
                pictureBox1.Invalidate(); // 重绘 PictureBox
                return;
            }
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // 如果在绘制状态
            if (CurStatus == Status.StartDraw)
            {
                if(currentROI.Rectangle.Width < 10 || currentROI.Rectangle.Height < 10)
                    return; // 忽略掉鼠标误点生成的小ROI
                roiManager.AddROI(currentROI);
                selectedROI = currentROI;
                CurStatus = Status.EndDraw;
            }else if(CurStatus == Status.StartEdit)
            {
                CurStatus = Status.EndEdit;
            }
            else if (CurStatus == Status.StartMove)
            {
                CurStatus = Status.EndMove;
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

            if (CurStatus == Status.StartDraw && currentROI != null)
            {
                pen.Color = Color.Orange;
                currentROI.Draw(e.Graphics, pen); // 使用橘色表示正在绘制的 ROI
            }
        }

        private void 清除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearROI();
        }

        private void 矩形ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            矩形ToolStripMenuItem1.Checked = !矩形ToolStripMenuItem1.Checked;
            圆形ToolStripMenuItem1.Checked = !圆形ToolStripMenuItem1.Checked;
            selectedROIType = ROIType.Rectangle;
        }

        private void 圆形ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            圆形ToolStripMenuItem1.Checked = !圆形ToolStripMenuItem1.Checked;
            矩形ToolStripMenuItem1.Checked = !矩形ToolStripMenuItem1.Checked;
            selectedROIType = ROIType.Circle;
        }

        private void 移除上一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemovePrevious();
            currentROI = null;
        }

        private void 绘制状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            绘制状态ToolStripMenuItem.Checked = !绘制状态ToolStripMenuItem.Checked;
            if (绘制状态ToolStripMenuItem.Checked)
            {
                selectedROIType = 矩形ToolStripMenuItem1.Checked ? ROIType.Rectangle : ROIType.Circle;
                CurStatus = Status.ReadyDraw;
            }
            else
            {
                CurStatus = Status.ReadyMove;
            }
        }
    }

    public enum Status 
    {
        /// <summary>
        /// 设置为绘制状态，等待绘制
        /// </summary>
        ReadyDraw,
        /// <summary>
        /// 设置为绘制状态，开始绘制
        /// </summary>
        StartDraw,
        /// <summary>
        /// 设置为绘制状态，结束绘制
        /// </summary>
        EndDraw,
        /// <summary>
        /// 准备移动状态
        /// </summary>
        ReadyMove,
        /// <summary>
        /// 开始移动
        /// </summary>
        StartMove,
        /// <summary>
        /// 结束移动
        /// </summary>
        EndMove,
        /// <summary>
        /// 开始编辑
        /// </summary>
        StartEdit,
        /// <summary>
        /// 结束编辑
        /// </summary>
        EndEdit,
    }

}
