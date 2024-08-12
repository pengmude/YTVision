using System;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    public partial class NodeBase : UserControl
    {
        public NodeBase()
        {
            InitializeComponent();
            启用ToolStripMenuItem.Enabled = false;
        }

        #region 节点界面的操作

        private int _id = 0;
        private bool _active = true;
        private bool _selected = false;

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
        public string NodeName { get { return label1.Text; } set { label1.Text = value; } }

        /// <summary>
        /// 节点所属的流程
        /// </summary>
        public Process Process { get; }

        /// <summary>
        /// 删除节点事件
        /// </summary>
        public event EventHandler<int> NodeDeletedEvent;

        /// <summary>
        /// 删除时触发删除事件，参数为待删除的节点ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                label1.BackColor = SystemColors.GradientActiveCaption;
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
                    label1.BackColor = SystemColors.GradientActiveCaption;
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
            ShowSettingsWindow();
        }

        public virtual INodeParamForm ParamForm { get; set; }

        protected virtual void ShowSettingsWindow()
        {

        }

        #endregion 定义节点界面操作-结束
    }
}
