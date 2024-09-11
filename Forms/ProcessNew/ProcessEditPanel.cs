using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Node;
using YTVisionPro.Node.AI.HTAI;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.Camera.HiK.WaitHardTrigger;
using YTVisionPro.Node.Camera.HiK.WaitSoftTrigger;
using YTVisionPro.Node.ImageRead;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.PLC.Panasonic.HTDeepResultSend;
using YTVisionPro.Node.PLC.Panasonic.Read;
using YTVisionPro.Node.Tool.ImageSave;
using YTVisionPro.Node.Tool.SleepTool;
using static YTVisionPro.Node.NodeComboBox;

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

        public ProcessEditPanel(string processName = "")
        {
            InitializeComponent();
            _process = new Process(processName);
            Solution.Instance.AddProcess(_process);
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

                #region 根据text创建对应类型的节点

                DragData data = (DragData)e.Data.GetData(DragDataFormat);

                NodeBase node = null;
                switch (data.NodeType)
                {
                    case NodeType.LightSourceControl:
                        node = new NodeLight(data.Text, _process);
                        break;
                    case NodeType.CameraShot:
                        node = new NodeCamera(data.Text, _process);
                        break;
                    case NodeType.LocalPicture:
                        node = new NodeImageRead(data.Text, _process);
                        break;
                    case NodeType.PLCRead:
                        node = new NodePlcRead(data.Text, _process);
                        break;
                    case NodeType.PLCWrite:
                        break;
                    case NodeType.PLCHTAIResultSend:
                        node = new NodeHTAISendSignal(data.Text, _process);
                        break;
                    case NodeType.AIHT:
                        node = new NodeHTAI(data.Text, _process);
                        break;
                    case NodeType.ImageSave:
                        node = new NodeImageSave(data.Text, _process);
                        break;
                    case NodeType.SleepTool:
                        node = new SleepTool(data.Text, _process);
                        break;
                    case NodeType.WaitSoftTrigger:
                        node = new NodeWaitSoftTrigger(data.Text, _process);
                        break;
                    case NodeType.WaitHardTrigger:
                        node = new NodeWaitHardTrigger(data.Text, _process);
                        break;
                    default:
                        break;
                }
                if(node == null)
                {
                    MessageBox.Show("当前节点类型创建失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogHelper.AddLog(MsgLevel.Exception, "当前节点类型创建失败！", true);
                    return;
                }
                node.Size = new Size(this.Size.Width - 5, 42);
                node.Dock = DockStyle.Top;
                NodeBase.NodeDeletedEvent += NewNode_NodeDeletedEvent;
                _stack.Push(node);
                Solution.Nodes.Add(node);
                _process.AddNode(node);
                UpdateNode();

                #endregion

                //更新节点数量到界面
                label1.Text = $"节点数:{_stack.Count}";
            }
        }

        /// <summary>
        /// 节点删除事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void NewNode_NodeDeletedEvent(object sender, int e)
        {
            //使用Stack<Node> 来临时存储控件，因为不能在迭代Stack时修改它
            Stack<NodeBase> tmp = new Stack<NodeBase>(_stack);
            // 清空原栈
            _stack.Clear();
            foreach (NodeBase node in tmp)
            {
                // 如果控件的Name与目标控件不同，再压入栈中
                if (node.ID != e)
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
