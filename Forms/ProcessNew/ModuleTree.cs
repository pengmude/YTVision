using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using gCursorLib;
using YTVisionPro.Properties;
using System.IO;
using static YTVisionPro.Node.NodeComboBox;
using System.Reflection.Emit;
using YTVisionPro.Node;

namespace YTVisionPro.Forms.ProcessNew
{
    /// <summary>
    /// 所有工具的展示
    /// </summary>
    internal partial class ModTree : UserControl
    {
        private Bitmap gBuff;
        private Icon iconDefult;
        private TreeView _treeView;
        private bool ref_enabled = true;
        public event TreeViewEventHandler AfterSelect;
        Dictionary<string, TreeNodeInfo> m_PluginInfoDic;
        private int ViewItemCount
        {
            get
            {
                return (int)Math.Ceiling((double)(((double)base.Height) / ((double)this.ItemHeight)));
            }
        }
        public TreeNode SelectedNode
        {
            get
            {
                if (this._treeView != null)
                {
                    return this._treeView.SelectedNode;
                }
                return null;
            }
            set
            {
                if (this._treeView != null)
                {
                    this._treeView.SelectedNode = value;
                }
            }
        }
        public TreeNodeCollection Nodes
        {
            get
            {
                if (this._treeView != null)
                {
                    return this._treeView.Nodes;
                }
                return null;
            }
        }
        public int ItemHeight { get; } = 0x18;
        /// <summary>
        /// 获取或设置每个子树节点级别的缩进距离。
        /// </summary>
        public int Indent
        {
            get
            {
                if (this._treeView != null)
                {
                    return this._treeView.Indent;
                }
                return 0;
            }
            set
            {
                if (this._treeView != null)
                {
                    this._treeView.Indent = value;
                }
            }
        }
        public ModTree()
        {
            InitializeComponent();
            iconDefult = Resources.光源工具;
            this._treeView = new TreeView();
            EnsureVisible();
            this.AllowDrop = true; //拖拽允许
        }

        public static Icon ConvertBitmapToIcon(Bitmap bitmap)
        {
            // 使用Bitmap对象创建一个Icon对象
            using (MemoryStream ms = new MemoryStream())
            {
                // 将Bitmap保存到内存流中
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;  // 重置流的位置

                // 从内存流中读取Icon
                return new Icon(ms);
            }
        }

        /// <summary>
        /// 添工具列表
        /// </summary>
        /// <param name="pluginsInfoDic"></param>
        public void Init(Dictionary<string, TreeNodeInfo> pluginsInfoDic)
        {
            m_PluginInfoDic = pluginsInfoDic;
            _treeView.Nodes.Add("光源设备", "光源设备", 0);
            _treeView.Nodes.Add("图像采集", "图像采集");
            _treeView.Nodes.Add("PLC通讯", "PLC通讯");
            _treeView.Nodes.Add("几何测量", "几何测量");
            _treeView.Nodes.Add("逻辑工具", "逻辑工具");
            foreach (TreeNodeInfo info in pluginsInfoDic.Values)
            {
                // 获取或创建根节点
                TreeNode rootNode = _treeView.Nodes[info.RootNodeName];
                if (rootNode == null)
                {
                    rootNode.SelectedImageIndex = rootNode.ImageIndex;
                    rootNode = _treeView.Nodes.Add(info.RootNodeName, info.RootNodeName);
                }
                // 创建子节点并设置 Tag 属性
                TreeNode childNode = new TreeNode(info.Name)
                {
                    Tag = info.NodeType,
                    //ImageIndex = imageList1.Images[]
                };
                // 在根节点下添加子节点
                rootNode.Nodes.Add(childNode);
            }
            this.Refresh();
#if DEBUG
            //_treeView.ExpandAll();//ToDo;调试模式下展开所欲
#endif
        }
        private bool GetContainState(Control ctrl, Rectangle rect, Point pt)
        {
            Point p = pt;
            Point point2 = ctrl.PointToClient(p);
            return rect.Contains(point2);
        }
        private Point GetPoint(IntPtr _xy)
        {
            uint num = (IntPtr.Size == 8) ? ((uint)_xy.ToInt64()) : ((uint)_xy.ToInt32());
            int x = (short)num;
            return new Point(x, (short)(num >> 0x10));
        }
        private bool GetClientContainState(Control ctrl, Point pt)
        {
            Rectangle clientRectangle = ctrl.ClientRectangle;
            return this.GetContainState(ctrl, clientRectangle, pt);
        }
        /// <summary>
        /// 绘制node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="g"></param>
        /// <param name="top"></param>
        /// <param name="nest"></param>
        /// <param name="NodeCnt"></param>
        /// <param name="WLineCnt"></param>
        private void PaintItem(TreeNode node, Graphics g, ref int top, int nest, int NodeCnt, ref int WLineCnt)
        {
            int x = ((nest * this.ItemHeight) + (this.ItemHeight / 2)) - 6;
            int num2 = (x + (this.ItemHeight / 2)) + 6;
            int num3 = (num2 + this.ItemHeight) + 3;
            int num4 = num3 + 2;
            int height = (int)g.MeasureString(node.Text, this.Font).Height;
            int num6 = top + (this.ItemHeight / 2);
            Pen pen = new Pen(Color.Gray)
            {
                DashStyle = DashStyle.Dash
            };
            //画树结构虚线 横线---
            g.DrawLine(pen, x + 6, num6, num2, num6);
            //画模块图标
            if (this.iconDefult != null)
            {
                Icon image = iconDefult;//this.ImageList.Images[node.ImageIndex];
                if(node.Level == 0)
                    image = Resources.图像采集工具;
                if (node.Level == 1)//模块结构才画工具图标
                {
                    image = m_PluginInfoDic[node.Text].IconImage;
                }
                if (image != null)
                {
                    g.DrawIcon(image, new Rectangle(num2, top, this.ItemHeight, this.ItemHeight));
                }
            }
            //画树结构文字 如果选中就添加蓝色底色
            if (node.Equals(this._treeView.SelectedNode))
            {
                g.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), new Rectangle(num3, top, base.Width, this.ItemHeight));
                g.DrawString(" " + node.Text, this.Font, new SolidBrush(Color.White), (float)num4, (float)(num6 - (height / 2)));
            }
            else
            {
                g.DrawString(" " + node.Text, this.Font, new SolidBrush(Color.Black), (float)num4, (float)(num6 - (height / 2)));
            }
            pen.DashStyle = DashStyle.Solid;
            if (node.Nodes.Count > 0)
            {
                WLineCnt++;
                //绘制折叠框白色填充
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(x, num6 - 6, 12, 12));
                pen.Color = Color.Black;
                //绘制折叠框黑色边框
                g.DrawRectangle(pen, new Rectangle(x, num6 - 6, 12, 12));
                if (!node.IsExpanded)
                {
                    pen.DashStyle = DashStyle.Solid;
                    int num7 = x + 6;
                    g.DrawLine(pen, num7 - 3, num6, num7 + 3, num6);
                    g.DrawLine(pen, num7, num6 - 3, num7, num6 + 3);
                    pen.Color = Color.Gray;
                    if (node.Index != (NodeCnt - 1))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        g.DrawLine(pen, (int)(x + 6), (int)(num6 + 6), (int)(x + 6), (int)(num6 + this.ItemHeight));
                    }
                    top += this.ItemHeight;
                }
                else
                {
                    pen.DashStyle = DashStyle.Solid;
                    g.DrawLine(pen, (x + 6) - 3, num6, (x + 6) + 3, num6);
                    pen.Color = Color.Gray;
                    if (NodeCnt > WLineCnt)
                    {
                        pen.DashStyle = DashStyle.Dash;
                        g.DrawLine(pen, (int)(x + 6), (int)(num6 + 6), (int)(x + 6), (int)(num6 + (this.ItemHeight * (node.Nodes.Count + 1))));
                    }
                    top += this.ItemHeight;
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        this.PaintItem(node2, g, ref top, nest + 1, NodeCnt, ref WLineCnt);
                    }
                }
            }
            else
            {
                pen.DashStyle = DashStyle.Dash;
                if (node.Index == 0)
                {
                    g.DrawLine(pen, x + 6, top, x + 6, num6);
                }
                else
                {
                    g.DrawLine(pen, x + 6, num6 - this.ItemHeight, x + 6, num6);
                }
                top += this.ItemHeight;
            }
        }
        public void EnsureVisible()
        {
            if (this._treeView != null)
            {
                int itemPos = 0;
                itemPos = -this.VscrollBar.Value;
                int selectedItemTop = 0;
                selectedItemTop = this.GetSelectedItemTop(ref itemPos, this._treeView.Nodes);
                int num3 = this.VscrollBar.Value;
                if (selectedItemTop < 0)
                {
                    if (num3 < base.Height)
                    {
                        num3 = 0;
                    }
                    else
                    {
                        num3 = ((num3 + selectedItemTop) - (this.VscrollBar.Value - base.Height)) + selectedItemTop;
                        if (num3 < 0)
                        {
                            num3 = this.VscrollBar.Value - base.Height;
                        }
                    }
                }
                else if ((selectedItemTop + this.ItemHeight) > base.Height)
                {
                    num3 += (selectedItemTop + (this.ItemHeight * 3)) - base.Height;
                }
                else if ((selectedItemTop + this.ItemHeight) == base.Height)
                {
                    num3 += this.ItemHeight;
                }
                num3 = Math.Max(0, Math.Min(this.VscrollBar.Maximum - this.VscrollBar.LargeChange, num3));
                num3 -= num3 % this.ItemHeight;
                this.VscrollBar.Value = num3;
            }
        }
        private int GetSelectedItemTop(ref int ItemPos, TreeNodeCollection Nodes)
        {
            int selectedItemTop = 0;
            foreach (TreeNode node in Nodes)
            {
                if (node.Equals(this._treeView.SelectedNode))
                {
                    return ItemPos;
                }
                ItemPos += this.ItemHeight;
                if ((node.Nodes.Count > 0) & node.IsExpanded)
                {
                    selectedItemTop = this.GetSelectedItemTop(ref ItemPos, node.Nodes);
                }
                if (selectedItemTop != 0)
                {
                    return selectedItemTop;
                }
            }
            return selectedItemTop;
        }
        private TreeNode GetNodeFromPosition(ref int PItem, TreeNodeCollection Nodes, MouseEventArgs e)
        {
            TreeNode node = null;
            foreach (TreeNode node2 in Nodes)
            {
                if ((e.Y >= PItem) && (e.Y < (PItem + this.ItemHeight)))
                {
                    return node2;
                }
                PItem += this.ItemHeight;
                if ((node2.Nodes.Count > 0) & node2.IsExpanded)
                {
                    node = this.GetNodeFromPosition(ref PItem, node2.Nodes, e);
                }
                if (node != null)
                {
                    return node;
                }
            }
            return node;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.gBuff != null)
            {
                this.gBuff.Dispose();
            }
            this.gBuff = new Bitmap(Math.Max(base.Width, 1), Math.Max(base.Height, 1));
            this.VscrollBar.SmallChange = this.ItemHeight;
            this.VscrollBar.LargeChange = Math.Max((this.ViewItemCount - 1), this.ItemHeight);
            this.Refresh();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (((base.Visible) && (this.VscrollBar.Visible)))
            {
                if (e.Delta > 0)
                {
                    if (this.VscrollBar.Value - this.ItemHeight > this.VscrollBar.Minimum)
                    {
                        this.VscrollBar.Value -= this.ItemHeight;
                    }
                }
                if (e.Delta < 0)
                {
                    if (this.VscrollBar.Value + this.ItemHeight < this.VscrollBar.Maximum)
                    {
                        this.VscrollBar.Value += this.ItemHeight;
                    }
                }
            }
            base.OnMouseWheel(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                int num;
                int top = num = -this.VscrollBar.Value/*+10*/;
                if (this.gBuff != null)
                {
                    using (Graphics graphics = Graphics.FromImage(this.gBuff))
                    {
                        //EasyVision.Common.Log4Net.Log.Debug($"流程树开始刷新");
                        //填充背景色
                        graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, base.Width, base.Height));
                        int count = this.Nodes.Count;
                        int wLineCnt = 0;
                        //绘制节点
                        foreach (TreeNode node in this.Nodes)
                        {
                            this.PaintItem(node, graphics, ref top, 0, count, ref wLineCnt);
                        }
                        if (this.ref_enabled)
                        {
                            //graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, base.Width, base.Height));
                            e.Graphics.DrawImage(this.gBuff, 0, 0);
                        }
                        graphics.Dispose();
                    }
                    if ((top + this.VscrollBar.Value) < this.VscrollBar.Height)
                    {
                        this.VscrollBar.Value = 0;
                        this.VscrollBar.Visible = false;
                    }
                    else
                    {
                        this.VscrollBar.Visible = true;
                    }
                    this.VscrollBar.Maximum = Math.Max((top - num) - ((top - num) % this.ItemHeight), 0);
                    base.OnPaint(e);
                }
            }
            catch (OutOfMemoryException exception)
            {
                //SharedClass.ExceptionOutOfMemory(exception);
            }
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            TreeNode selectedNode = this.SelectedNode;
            this._treeView.Focus();
            int pItem = -this.VscrollBar.Value;
            TreeNode node = this.GetNodeFromPosition(ref pItem, this._treeView.Nodes, e);
            if (node != null)
            {
                this._treeView.SelectedNode = node;
                TreeNode parent = node.Parent;
                int num2 = this.ItemHeight;
                while (parent != null)
                {
                    num2 += this.ItemHeight;
                    parent = parent.Parent;
                }
                if ((this._treeView.SelectedNode.Nodes.Count > 0) && ((num2 > e.X) || (selectedNode == node)))
                {
                    node.Toggle();
                }
                if (this.AfterSelect != null)
                {
                    this.AfterSelect(this, new TreeViewEventArgs(node, TreeViewAction.ByMouse));
                }
            }
            this.Refresh();
            base.OnMouseDown(e);
        }
        private void VscrollBar_ValueChanged(object sender, EventArgs e)
        {
            base.SuspendLayout();
            int num = this.VscrollBar.Value;
            num -= num % this.ItemHeight;
            this.VscrollBar.Value = num;
            base.ResumeLayout(false);
            base.Invalidate();
        }
        bool m_MouseDown = false;
        private void ModelBoxtUI_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseDown = true;
            if (_treeView.SelectedNode != null && m_PluginInfoDic.ContainsKey(_treeView.SelectedNode.Text) &&
                 m_PluginInfoDic[_treeView.SelectedNode.Text].IconImage != null)
            {
                gCursor1.gText = _treeView.SelectedNode.Text;
                gCursor1.gEffect = gCursor.eEffect.Move;
                gCursor1.gImage = m_PluginInfoDic[_treeView.SelectedNode.Text].IconImage.ToBitmap();
                gCursor1.gType = gCursor.eType.Both;
                gCursor1.MakeCursor();
            }
        }
        private void ModelBoxtUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (_treeView.SelectedNode != null && m_MouseDown && _treeView.SelectedNode.Level == 1)
            {
                DataObject dragData = new DataObject(DragDataFormat, new DragData(_treeView.SelectedNode.Text, (NodeType)_treeView.SelectedNode.Tag));
                DoDragDrop(dragData, DragDropEffects.Move);
            }
        }
        private void ModelBoxtUI_MouseLeave(object sender, EventArgs e)
        {
            m_MouseDown = false;
        }
        private void ModelBoxtUI_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseDown = false;
        }
        private void ModTree_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            Cursor.Current = gCursor1.gCursor;
        }
    }


    internal class TreeNodeInfo
    {
        /// <summary>节点名称</summary>
        public string Name { get; set; }

        /// <summary>根节点</summary>
        public string RootNodeName { get; set; }

        /// <summary>节点类型</summary>
        public NodeType NodeType { get; set; }

        /// <summary>图标索引</summary>
        public int IconIndex { get; set; }
    }

    internal class TreeNodeIcon
    {
        
    }
}
