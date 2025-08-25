using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TDJS_Vision.Node;
using static TDJS_Vision.Forms.ProcessNew.FormNewProcessWizard;
using TDJS_Vision.Node._3_Detection.FindLine;
using TDJS_Vision.Node._3_Detection.FindCircle;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._2_ImagePreprocessing.ImageRotate;
using TDJS_Vision.Node._5_EquipmentCommunication.LightOpen;
using TDJS_Vision.Node._2_ImagePreprocessing.ImageCrop;
using TDJS_Vision.Node._5_EquipmentCommunication.PlcRead;
using TDJS_Vision.Node._5_EquipmentCommunication.PLCSoftTrigger;
using TDJS_Vision.Node._5_EquipmentCommunication.TcpClient;
using TDJS_Vision.Node._5_EquipmentCommunication.TcpServer;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusWrite;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusSoftTrigger;
using TDJS_Vision.Node._5_EquipmentCommunication.AIResultSend;
using TDJS_Vision.Node._6_LogicTool.SleepTool;
using TDJS_Vision.Node._7_ResultProcessing.ImageSave;
using TDJS_Vision.Node._7_ResultProcessing.DataShow;
using TDJS_Vision.Node._7_ResultProcessing.ResultSummarize;
using TDJS_Vision.Node._2_ImagePreprocessing.ImageSplit;
using TDJS_Vision.Node._3_Detection.QRScan;
using TDJS_Vision.Node._3_Detection.MatchTemplate;
using TDJS_Vision.Node._7_ResultProcessing.ImageDelete;
using TDJS_Vision.Node._6_LogicTool.SharedVariable;
using TDJS_Vision.Node._7_ResultProcessing.GenerateExcelSpreadsheet;
using TDJS_Vision.Node._3_Detection.TDAI;
using TDJS_Vision.Node._7_ResultProcessing.ImageDraw;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._6_LogicTool.ConditionRun;
using TDJS_Vision.Node._5_EquipmentCommunication.PlcWirte;
using TDJS_Vision.Node._6_LogicTool.ProcessTrigger;
using TDJS_Vision.Node._6_LogicTool.ProcessSignal;
using static OpenCvSharp.ML.DTrees;
using TDJS_Vision.Node._6_LogicTool.If;
using TDJS_Vision.Node._6_LogicTool.Else;
using TDJS_Vision.Node._6_LogicTool.EndIf;
using TDJS_Vision.Node._3_Detection.BatteryEar;
using TDJS_Vision.Node._6_LogicTool.CSharpScript;

namespace TDJS_Vision.Forms.ProcessNew
{
    public partial class ProcessEditPanel : UserControl
    {
        /// <summary>
        /// 绑定的流程
        /// </summary>
        private Process _process { get; set; }

        /// <summary>
        /// 流程优先级设置窗口
        /// </summary>
        private FormSetProcessLv _processLvSet { get; set; }

        private FormProcessGroupSetting formProcessGroupSetting { get; set; }

        /// <summary>
        /// 所有的节点控件
        /// </summary>
        private Stack<NodeBase> _stack = new Stack<NodeBase>();
        /// <summary>
        /// 流程编辑面板构造函数
        /// </summary>
        /// <param name="processName"></param>
        public ProcessEditPanel(string processName, bool showInfo = true, ProcessConfig processConfig = null)
        {
            InitializeComponent();
            _process = new Process(processName);
            _processLvSet = new FormSetProcessLv(_process);
            formProcessGroupSetting = new FormProcessGroupSetting(_process);
            Process.UpdateRunStatus += RunStatusChange;
            Solution.Instance.UpdateRunStatus += RunStatusChange;
            Solution.Instance.AddProcess(_process);

            // 反序列化需要执行以下逻辑
            if(processConfig != null)
            {
                _process.ShowLog = processConfig.ShowLog;
                _process.IsPassiveTriggered = processConfig.IsPassiveTriggered;
                foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
                {
                    if ("是否输出日志ToolStripMenuItem" == item.Name)
                    {
                        item.Checked = processConfig.ShowLog;
                    }
                    if ("是否为触发流程ToolStripMenuItem" == item.Name)
                    {
                        item.Checked = processConfig.IsPassiveTriggered;
                    }
                }
                _process.RunLv = processConfig.Level;
                _process.Group = processConfig.Group;
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

        private void RunStatusChange(object sender, ProcessRunResult e)
        {
            if(_process.ProcessName == e.ProcessName)
            {
                label2.Text = $"耗时:{_process.RunTime} ms";
                buttonRun.Enabled = !e.IsRunning;
                uiSwitchEnable.Enabled = !e.IsRunning;
                uiLedBulb1.Color = e.IsRunning ? Color.DarkGray : e.IsSuccess ? Color.LawnGreen : Color.Red;
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
                    MessageBoxTD.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
                }
            }
        }
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
                    if (nodeInfo.NodeParam != null)
                    {
                        // 2.还原节点的参数
                        nodeBase.ParamForm.Params = nodeInfo.NodeParam;
                        // 3.节点参数到参数设置界面
                        nodeBase.ParamForm.SetParam2Form();
                    }
                    if (showInfo)
                        LogHelper.AddLog(MsgLevel.Info, $"=> 节点（{nodeInfo.ID}.{nodeInfo.NodeName}）已加载", true);
                }
                catch (Exception ex)
                {
                    MessageBoxTD.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (id == -1) //表示不是反序列化，正常创建对象
                nodeId = ++Solution.Instance.NodeCount;

            #region 根据text创建对应类型的节点

            switch (nodeType)
            {
                case NodeType.LightSourceControl:
                    node = new NodeLight(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.PLCRead:
                    node = new NodePlcRead(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.PLCWrite:
                    node = new NodePlcWrite(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.AITD:
                    node = new NodeTDAI(nodeId, nodeName, _process, nodeType);
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
                case NodeType.LineFind:
                    node = new NodeFIndLine(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.CircleFind:
                    node = new NodeFIndCircle(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageCrop:
                    node = new NodeImageCrop(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageShow:
                    node = new NodeImageShow(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ModbusRead:
                    node = new NodeModbusRead(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ModbusWrite:
                    node = new NodeModbusWrite(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.TCPClientRequest:
                    node = new NodeTCPClient(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.TCPServerResponse:
                    node = new NodeTCPServer(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageRotate:
                    node = new NodeImageRotate(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ModbusSoftTrigger:
                    node = new NodeModbusSoftTrigger(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.AIResultSend:
                    node = new NodeSignalSend(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.CameraIO:
                    node = new NodeCameraIO(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageSource:
                    node = new NodeImageSource(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageSplit:
                    node = new NodeImageSplit(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.QRScan:
                    node = new NodeQRScan(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.MatchTemplate:
                    node = new NodeMatchTemplate(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ImageFileDelete:
                    node = new NodeImageDelete(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.SharedVariable:
                    node = new NodeSharedVariable(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.GenerateExcel:
                    node = new NodeGenerateExcel(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.DrawAIResult:
                    node = new NodeImageDraw(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ConditionRun:
                    node = new NodeConditionRun(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ProcessTrigger:
                    node = new NodeProcessTrigger(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ProcessSignal:
                    node = new NodeProcessSignal(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.If:
                    node = new NodeIf(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.Else:
                    node = new NodeElse(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.EndIf:
                    node = new NodeEndIf(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.BatteryEar:
                    node = new NodeBatteryEar(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.CSharpScript:
                    node = new NodeCSharpScript(nodeId, nodeName, _process, nodeType);
                    break;
                default:
                    break;
                    //工具栏图标颜色：蓝 #1296db 紫 #56227a
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
        /// 点击清理状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClean_Click(object sender, EventArgs e)
        {
            foreach (var node in _process.Nodes)
            {
                node.SetStatus(NodeStatus.Unexecuted, "*");
            }
        }

        /// <summary>
        /// 点击运行流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 重置运行取消令牌
                Solution.Instance.ResetTokenSource();
                _process.IsHandRun = true;
                await _process.Run(false);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 点击停止流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            Solution.Instance.CancelToken();
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

        private void 设置流程优先级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _processLvSet.ShowDialog();
        }

        private void 设置流程组别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formProcessGroupSetting.ShowDialog();
        }

        private void 流程重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProcessRename formProcessRename = new FormProcessRename();
            formProcessRename.SetProcess(_process);
            formProcessRename.ShowDialog();
        }
        /// <summary>
        /// 是否输出日志菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 是否输出日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            是否输出日志ToolStripMenuItem.Checked = !是否输出日志ToolStripMenuItem.Checked;
            _process.ShowLog = 是否输出日志ToolStripMenuItem.Checked;
        }

        private void 是否为触发流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            是否为触发流程ToolStripMenuItem.Checked = !是否为触发流程ToolStripMenuItem.Checked;
            _process.IsPassiveTriggered = 是否为触发流程ToolStripMenuItem.Checked;
        }

        /// <summary>
        /// 显示上下文菜单
        /// </summary>
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(Cursor.Position);
        }
    }
}
