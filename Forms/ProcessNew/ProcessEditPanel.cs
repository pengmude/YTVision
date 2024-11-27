using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Node;
using static YTVisionPro.Forms.ProcessNew.FormNewProcessWizard;
using YTVisionPro.Node._3_Detection.FindLine;
using YTVisionPro.Node._3_Detection.FindCircle;
using YTVisionPro.Node._1_Acquisition.ImageSource;
using YTVisionPro.Node._2_ImagePreprocessing.ImageRotate;
using YTVisionPro.Node._4_Measurement.ParallelLines;
using YTVisionPro.Node._5_EquipmentCommunication.LightOpen;
using YTVisionPro.Node._3_Detection.HTAI;
using YTVisionPro.Node._2_ImagePreprocessing.ImageCrop;
using YTVisionPro.Node._5_EquipmentCommunication.PanasonicRead;
using YTVisionPro.Node._5_EquipmentCommunication.PanasonicWirte;
using YTVisionPro.Node._5_EquipmentCommunication.PLCSoftTrigger;
using YTVisionPro.Node._5_EquipmentCommunication.TcpClient;
using YTVisionPro.Node._5_EquipmentCommunication.SendResultByPLC;
using YTVisionPro.Node._5_EquipmentCommunication.TcpServer;
using YTVisionPro.Node._5_EquipmentCommunication.ModbusRead;
using YTVisionPro.Node._5_EquipmentCommunication.ModbusWrite;
using YTVisionPro.Node._5_EquipmentCommunication.ModbusSoftTrigger;
using YTVisionPro.Node._5_EquipmentCommunication.AIResultSendByModbus;
using YTVisionPro.Node._6_LogicTool.SleepTool;
using YTVisionPro.Node._7_ResultProcessing.ImageSave;
using YTVisionPro.Node._7_ResultProcessing.DataShow;
using YTVisionPro.Node._7_ResultProcessing.ResultSummarize;
using YTVisionPro.Node._2_ImagePreprocessing.ImageSplit;
using YTVisionPro.Node._3_Detection.QRScan;
using YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement;
using YTVisionPro.Node._3_Detection.MatchTemplate;
using YTVisionPro.Node._7_ResultProcessing.ImageDelete;
using YTVisionPro.Node._6_LogicTool.SharedVariable;
using System.Diagnostics;
using YTVisionPro.Node._7_ResultProcessing.GenerateExcelSpreadsheet;

namespace YTVisionPro.Forms.ProcessNew
{
    internal partial class ProcessEditPanel : UserControl
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
                _process.RunLv = processConfig.Level;
                _process.processGroup = processConfig.Group;
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
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                case NodeType.SendResultByPLC:
                    node = new NodeSendResultByPLC(nodeId, nodeName, _process, nodeType);
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
                case NodeType.LineParallelism:
                    node = new NodeParallelLines(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.ModbusSoftTrigger:
                    node = new NodeModbusSoftTrigger(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.AIResultSendByModbus:
                    node = new NodeSignalSendByModbus(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.CameraIO:
                    node = new NodeCameraIO(nodeId, nodeName, _process, nodeType);
                    break;
                case NodeType.InjectionHole:
                    node = new NodeInjectionHole(nodeId, nodeName, _process, nodeType);
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
            try
            {
                // 重置运行取消令牌
                Solution.Instance.ResetTokenSource();
                await _process.Run(false);
            }
            catch (Exception) { }
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
    }
}
