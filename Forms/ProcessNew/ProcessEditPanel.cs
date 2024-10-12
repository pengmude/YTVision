using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Node;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.ImageSrc.Shot;
using YTVisionPro.Node.PLC.WaitSoftTrigger;
using YTVisionPro.Node.ImageSrc.ImageRead;
using YTVisionPro.Node.LightControl;
using YTVisionPro.Node.ResultProcessing.HTDeepResultSend;
using YTVisionPro.Node.PLC.PanasonicRead;
using YTVisionPro.Node.PLC.PanasonicWirte;
using YTVisionPro.Node.ResultProcessing.DataShow;
using YTVisionPro.Node.ResultProcessing.ImageSave;
using YTVisionPro.Node.ResultProcessing.ResultSummarize;
using YTVisionPro.Node.ProcessControl.SleepTool;
using static YTVisionPro.Forms.ProcessNew.FormNewProcessWizard;

namespace YTVisionPro.Forms.ProcessNew
{
    internal partial class ProcessEditPanel : UserControl
    {
        /// <summary>
        /// 绑定的流程
        /// </summary>
        private Process _process { get; set; }

        /// <summary>
        /// 所有的节点控件
        /// </summary>
        private Stack<NodeBase> _stack = new Stack<NodeBase>();

        /// <summary>
        /// 选中的节点
        /// </summary>
        public Button SelectedNode { get; set; } = null;

        /// <summary>
        /// 流程编辑面板构造函数
        /// </summary>
        /// <param name="processName"></param>
        public ProcessEditPanel(string processName, bool showInfo = true, ProcessConfig processConfig = null)
        {
            InitializeComponent();
            _process = new Process(processName);
            Solution.Instance.AddProcess(_process);

            // 反序列化需要执行以下逻辑
            if(processConfig != null)
            {
                _stack.Clear();
                label1.Text = $"节点数:0";
                if(showInfo)
                    LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载流程（{_process.ProcessName}）=================================================", true);
                // 阻塞UI去创建流程
                CreateProcess(processConfig, showInfo);
                uiSwitchEnable.Active = processConfig.Enable;
                if(showInfo)
                    LogHelper.AddLog(MsgLevel.Debug, $"================================================ 流程（{_process.ProcessName}）已加载完成 ================================================", true);
            }
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeEditPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DragDataFormat))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 拖拽放下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeEditPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DragDataFormat))
            {
                DragData data = (DragData)e.Data.GetData(DragDataFormat);
                try
                {
                    CreateNode(data.NodeType, data.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
                }
            }
        }

        /// <summary>
        /// 用于反序列化创建流程中的所有节点
        /// </summary>
        /// <param name="processConfig"></param>
        private delegate void SetParamDelegate();
        private void CreateProcess(ProcessConfig processConfig, bool showInfo)
        {
            foreach (var nodeInfo in processConfig.NodeInfos)
            {
                NodeBase nodeBase = null;
                try
                {
                    // 1.还原节点
                    nodeBase = CreateNode(nodeInfo.NodeType, nodeInfo.NodeName, nodeInfo.ID);
                    nodeBase.Selected = nodeInfo.Selected;
                    nodeBase.Active = nodeInfo.Active;
                    // 2.还原节点的参数
                    nodeBase.ParamForm.Params = nodeInfo.NodeParam;
                    // 3.节点参数到参数设置界面
                    nodeBase.ParamForm.SetParam2Form();
                    if(showInfo)
                        LogHelper.AddLog(MsgLevel.Info, $"=> 节点（{nodeInfo.ID}.{nodeInfo.NodeName}）已加载", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if(showInfo)
                        LogHelper.AddLog(MsgLevel.Exception, $"=> 节点（{nodeInfo.ID}.{nodeInfo.NodeName}）加载失败！原因：{ex.Message}", true);
                    continue;
                }
            }
        }

        /// <summary>
        /// 创建一个对应派生类型的节点，id参数只有反序列化需要传入
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="nodeName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private NodeBase CreateNode(NodeType nodeType, string nodeName, int id = -1)
        {

            NodeBase node = null;

            // 反序列化节点时使用原来的id
            // 而正常创建节点使用新id
            int nodeId = id;
            if (id == -1)
                nodeId = ++Solution.Instance.NodeCount;

            #region 根据text创建对应类型的节点

            switch (nodeType)
            {
                case NodeType.LightSourceControl:
                    node = new NodeLight(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.CameraShot:
                    node = new NodeShot(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.LocalPicture:
                    node = new NodeImageRead(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.PLCRead:
                    node = new NodePlcRead(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.PLCWrite:
                    node = new NodePlcWrite(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.PLCHTAIResultSend:
                    node = new NodeHTAISendSignal(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.AIHT:
                    node = new NodeHTAI(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageSave:
                    node = new NodeImageSave(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.SleepTool:
                    node = new NodeSleepTool(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.WaitSoftTrigger:
                    node = new NodeWaitSoftTrigger(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.DetectResultShow:
                    node = new NodeDataShow(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.Summarize:
                    node = new NodeSummarize(nodeId, nodeName, _process, nodeType);
                    break;
                default:
                    break;
            }
            if (node == null)
            {
                --Solution.Instance.NodeCount;
                throw new Exception("当前节点类型创建失败！");
            }
            node.Size = new Size(this.Size.Width - 5, 42);
            node.Dock = DockStyle.Top;
            NodeBase.NodeDeletedEvent += NewNode_NodeDeletedEvent;
            _stack.Push(node);
            Solution.Instance.Nodes.Add(node);
            _process.AddNode(node);
            UpdateNode();

            #endregion

            //更新节点数量到界面
            label1.Text = $"节点数:{_stack.Count}";

            return node;
        }

        /// <summary>
        /// 节点删除事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewNode_NodeDeletedEvent(object sender, NodeBase e)
        {
            //使用Stack<Node> 来临时存储控件，因为不能在迭代Stack时修改它
            Stack<NodeBase> tmp = new Stack<NodeBase>(_stack);
            // 清空原栈
            _stack.Clear();
            foreach (NodeBase node in tmp)
            {
                // 如果控件的Name与目标控件不同，再压入栈中
                if (node.ID != e.ID)
                {
                    _stack.Push(node);
                }
            }

            UpdateNode();

            //更新节点数量到界面
            label1.Text = $"节点数:{_stack.Count}";
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void UpdateNode()
        {
            this.panel1.Controls.Clear();
            foreach (var item in _stack)
            {
                this.panel1.Controls.Add(item);
            }
        }

        /// <summary>
        /// 点击运行流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            //运行流程
            try
            {
                buttonRun.Enabled = false;
                uiSwitchEnable.Enabled = false;
                if (Solution.Instance.CancellationToken.IsCancellationRequested)
                    Solution.Instance.ResetTokenSource();
                await _process.Run(false, Solution.Instance.CancellationToken);

                SetProcessRunStatus(_process.RunTime, true);

            }
            catch (Exception)
            {
                SetProcessRunStatus(_process.RunTime, false);
            }

            buttonRun.Enabled = true;
            uiSwitchEnable.Enabled = true;
        }

        /// <summary>
        /// 设置界面的运行状态
        /// </summary>
        /// <param name="ok"></param>
        private void SetProcessRunStatus(long time, bool ok)
        {
            if (ok)
            {
                uiLedBulb1.Color = Color.LawnGreen;
            }
            else
            {
                uiLedBulb1.Color = Color.Red;
            }
            label2.Text = $"耗时:{time} ms";
        }

        /// <summary>
        /// 设置流程状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            _process.Enable = value;
            if (value)
                LogHelper.AddLog(MsgLevel.Info, $"{_process.ProcessName}启用", true);
            else
                LogHelper.AddLog(MsgLevel.Info, $"{_process.ProcessName}禁用", true);
        }
    }
}
