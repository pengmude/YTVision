﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node
{
    internal partial class NodeBase : UserControl
    {
        #region 节点界面的操作

        private int _id = 0;
        private bool _active = true;
        private bool _selected = false;
        private string _nodeName;
        private string _notes = "无备注"; // 节点备注
        private FrmNodeRename _frmNodeRename;
        /// <summary>
        /// 节点所属流程
        /// </summary>
        public Process Process; 

        /// <summary>
        /// 节点的类别
        /// </summary>
        public NodeType NodeType;

        /// <summary>
        /// 节点改名后要刷新节点订阅控件的下拉框节点名称
        /// </summary>
        public static EventHandler<RenameResult> RefreshNodeSubControl;

        /// <summary>
        /// 因为是控件类，提供无参构造函数让设计器可以显示出来
        /// </summary>
        public NodeBase() 
        {
            InitializeComponent();
            启用ToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// 实际只使用这个有参构造函数创建控件
        /// </summary>
        /// <param name="paramForm"></param>
        public NodeBase(int nodeId, string nodeName, Process process, NodeType nodeType)
        {
            InitializeComponent();
            启用ToolStripMenuItem.Enabled = false;
            _id = nodeId;
            _nodeName = nodeName;
            label1.Text = $"{ID}.{_nodeName}";
            Process = process;
            _frmNodeRename = new FrmNodeRename(this);
            FrmNodeRename.RenameChangeEvent += RenameChangeEvent;
            NodeType = nodeType;
            toolTip1.SetToolTip(this.label3, _notes);
        }

        private void RenameChangeEvent(object sender, RenameResult e)
        {
            // 只重命名指定ID的节点
            if(e.NodeId == _id)
            {
                _nodeName = e.NodeNameNew;
                label1.Text = ID + "." + e.NodeNameNew;
                RefreshNodeSubControl?.Invoke(this, e);
            }
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
        /// 检查节点信号源Token是否取消,如果取消会抛出异常，停止运行流程
        /// </summary>
        public virtual Task CheckTokenCancel(CancellationToken token) 
        {
            try
            {
                // 检查取消请求
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }

        public virtual Task Run(CancellationToken token)
        {
            return Task.CompletedTask;
        }

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
        public static event EventHandler<NodeBase> NodeDeletedEvent;

        /// <summary>
        /// 禁用节点事件
        /// </summary>
        public static event EventHandler<bool> NodeDisableEvent;

        /// <summary>
        /// 设置备注文本
        /// </summary>
        /// <param name="text"></param>
        public void SetNotes(string text)
        {
            _notes = text;
            toolTip1.SetToolTip(this.label3, _notes);
        }

        /// <summary>
        /// 设置节点状态,主要颜色，中心颜色，是否闪烁
        /// </summary>
        /// <param name="color"></param>
        /// <param name=""></param>
        public void SetStatus(NodeStatus status, string time)
        {
            label2.Text = $"{time} ms";
            switch (status)
            {
                case NodeStatus.Unexecuted:
                    uiLight1.State = Sunny.UI.UILightState.Off;
                    label2.ForeColor = uiLight1.OffColor;
                    label2.Text = $"* ms";
                    break;
                case NodeStatus.Successful:
                    uiLight1.OnColor = Color.Green;
                    uiLight1.OnCenterColor = Color.Lime;
                    uiLight1.State = Sunny.UI.UILightState.On;
                    label2.ForeColor = uiLight1.OnCenterColor;
                    break;
                case NodeStatus.Failed:
                    uiLight1.OnColor = Color.DarkRed;
                    uiLight1.OnCenterColor = Color.Red;
                    uiLight1.State = Sunny.UI.UILightState.Blink;
                    label2.ForeColor = uiLight1.OnCenterColor;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 节点运行结果基本状态设置
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <param name="status"></param>
        public long SetRunResult(DateTime startTime, NodeStatus status)
        {
            long elapsedMi11iseconds = (long)(DateTime.Now - startTime).TotalMilliseconds;
            SetStatus(status, elapsedMi11iseconds.ToString());
            Result.RunTime = elapsedMi11iseconds;
            Result.Status = status;
            Result.RunStatusCode = status == NodeStatus.Successful ? NodeRunStatusCode.OK
                                : status == NodeStatus.Unexecuted ? NodeRunStatusCode.UNEXECUTED
                                : NodeRunStatusCode.UNKNOW_ERROR;
            return elapsedMi11iseconds;
        }

        /// <summary>
        /// 删除时触发删除事件，参数为待删除的节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 测试节点删除前代码

            //string str0 = "";
            //foreach (var node in Solution.Instance.Nodes)
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
                Solution.Instance.Nodes.Remove(this);
                Process.Nodes.Remove(this);
                NodeDeletedEvent.Invoke(this, this); 
                if (this is NodeHTAI)
                {
                    Solution.Instance.SolAiModelNum--;
                }
            }

            #region 测试节点删除后代码

            //string str2 = "";
            //foreach (var node in Solution.Instance.Nodes)
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
            NodeDisableEvent?.Invoke(this, false);
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
                tableLayoutPanel1.BackColor = SystemColors.ActiveCaption;
                禁用ToolStripMenuItem.Enabled = true;
                启用ToolStripMenuItem.Enabled = false;
            }
            else
            {
                label1.BackColor = SystemColors.AppWorkspace;
                tableLayoutPanel1.BackColor = SystemColors.AppWorkspace;
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
                {
                    label1.BackColor = Color.CornflowerBlue;
                    tableLayoutPanel1.BackColor = Color.CornflowerBlue;
                }
                else
                {
                    label1.BackColor = SystemColors.ActiveCaption;
                    tableLayoutPanel1.BackColor = SystemColors.ActiveCaption;
                }
            }
            else
            {
                if (_selected)
                {
                    label1.BackColor = SystemColors.ControlDarkDark;
                    tableLayoutPanel1.BackColor = SystemColors.ControlDarkDark;
                }
                else
                {
                    label1.BackColor = SystemColors.AppWorkspace;
                    tableLayoutPanel1.BackColor = SystemColors.AppWorkspace;
                }
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
            foreach (var node in Solution.Instance.Nodes)
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
            try
            {
                if (Active && ParamForm is Form form)
                    form.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        #endregion 定义节点界面操作-结束

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmNodeRename.ShowDialog();
        }

        private void 添加备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddNotes formAddNotes = new FormAddNotes(this);
            formAddNotes.ShowDialog();
        }

        /// <summary>
        /// 计时器用于检查节点是否被取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// 节点运行状态
    /// </summary>
    internal enum NodeStatus
    {
        /// <summary>
        /// 未运行的
        /// </summary>
        Unexecuted,
        /// <summary>
        /// 运行成功的
        /// </summary>
        Successful,
        /// <summary>
        /// 运行失败的
        /// </summary>
        Failed
    }

}
