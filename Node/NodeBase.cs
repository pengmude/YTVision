using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Xml.Linq;

namespace YTVisionPro.Node
{
    internal partial class NodeBase : UserControl
    {
        #region 节点界面的操作

        private int _id = 0;
        private bool _active = true;
        private bool _selected = false;
        private string _nodeName;
        private FrmNodeRename _frmNodeRename;
        /// <summary>
        /// 节点所属流程
        /// </summary>
        public Process Process; 



        /// <summary>
        /// 因为是控件类，提供无参构造函数让设计器可以显示出来
        /// </summary>
        public NodeBase() 
        {
            InitializeComponent();
            启用ToolStripMenuItem.Enabled = false;
            _id = ++Solution.NodeCount;
        }

        /// <summary>
        /// 实际只使用这个有参构造函数创建控件
        /// </summary>
        /// <param name="paramForm"></param>
        public NodeBase(string nodeName, Process process)
        {
            InitializeComponent();
            启用ToolStripMenuItem.Enabled = false;
            _id = ++Solution.NodeCount;
            _nodeName = nodeName;
            label1.Text = $"{ID}.{_nodeName}";
            Process = process;
            _frmNodeRename = new FrmNodeRename(this);
            _frmNodeRename.RenameChangeEvent += RenameChangeEvent;
        }

        private void RenameChangeEvent(object sender, string e)
        {
            _nodeName = e;
            label1.Text = ID + "." + e;
        }

        /// <summary>
        /// 节点参数设置界面
        /// </summary>
        public INodeParamForm ParamForm { get; set; }
        /// <summary>
        /// 节点运行结果
        /// </summary>
        public INodeResult Result { get; protected set; }
        /// <summary>
        /// 节点运行，虚函数需要重写
        /// </summary>
        public virtual void Run() { }
        /// <summary>
        /// 节点id
        /// </summary>
        public int ID { get { return _id; } private set { _id = value; } }

        /// <summary>
        /// 节点是否启用
        /// </summary>
        public bool Active { get { return _active; } set => SetActive(value); }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get { return _selected; } set => SetSelected(value); }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get => _nodeName; set => _nodeName = value; }

        /// <summary>
        /// 删除节点事件
        /// </summary>
        public static event EventHandler<int> NodeDeletedEvent;

        /// <summary>
        /// 删除时触发删除事件，参数为待删除的节点ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 测试节点删除前代码

            //string str0 = "";
            //foreach (var node in Solution.Nodes)
            //{
            //    str0 += node.NodeName + "\n";
            //}
            //MessageBox.Show("删除前方案节点：" + str0);

            //string str1 = "";
            //foreach (var node in Process.Nodes)
            //{
            //    str1 += node.NodeName + "\n";
            //}
            //MessageBox.Show("删除前流程节点：" + str1);

            #endregion

            if (Selected)
            {
                Solution.Nodes.Remove(this);
                Process.Nodes.Remove(this);
                NodeDeletedEvent.Invoke(this, ID);
            }

            #region 测试节点删除后代码

            //string str2 = "";
            //foreach (var node in Solution.Nodes)
            //{
            //    str2 += node.NodeName + "\n";
            //}
            //MessageBox.Show("删除后方案节点：" + str2);

            //string str3 = "";
            //foreach (var node in Process.Nodes)
            //{
            //    str3 += node.NodeName + "\n";
            //}
            //MessageBox.Show("删除后流程节点：" + str3);

            #endregion
        }

        /// <summary>
        /// 设置节点为启用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 启用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActive(true);
        }

        /// <summary>
        /// 设置节点为禁用状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 禁用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActive(false);
        }

        /// <summary>
        /// 设置启用/禁用的呈现样式
        /// </summary>
        /// <param name="active"></param>
        private void SetActive(bool active)
        {
            _active = active;
            if (_active)
            {
                label1.BackColor = SystemColors.ActiveCaption;
                禁用ToolStripMenuItem.Enabled = true;
                启用ToolStripMenuItem.Enabled = false;
            }
            else
            {
                label1.BackColor = SystemColors.AppWorkspace;
                禁用ToolStripMenuItem.Enabled = false;
                启用ToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// 设置选中状态以及样式
        /// </summary>
        /// <param name="selected"></param>
        private void SetSelected(bool selected)
        {
            // 设置当前实例为选中
            _selected = selected;
            if (_active)
            {
                if (_selected)
                    label1.BackColor = Color.CornflowerBlue;
                else
                    label1.BackColor = SystemColors.ActiveCaption; 
            }
            else
            {
                if (_selected)
                    label1.BackColor = SystemColors.ControlDarkDark;
                else
                    label1.BackColor = SystemColors.AppWorkspace;
            }
        }

        /// <summary>
        /// 设置节点选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            //清空全部选中状态
            foreach (var node in Solution.Nodes)
            {
                node.Selected = false;
            }
            SetSelected(!_selected);
        }

        /// <summary>
        /// 鼠标双击，打开设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Active && ParamForm is Form form)
                form.ShowDialog();
        }

        #endregion 定义节点界面操作-结束

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmNodeRename.ShowDialog();
        }
    }
}
