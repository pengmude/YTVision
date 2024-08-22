﻿using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.NodeLight.PPX;
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

                #region TODO:根据text创建对应类型的节点，并且赋予节点对应类型的参数设置窗口（给ParamForm赋值）

                DragData data = (DragData)e.Data.GetData(DragDataFormat);

                NodeBase node = null;
                switch (data.NodeType)
                {
                    case NodeType.LightPPX:
                        node = new NodeLight($"{Solution.NodeCount + 1}{data.Text}", _process, LightBrand.PPX);
                        break;
                    case NodeType.LightRsee:
                        node = new NodeLight($"{Solution.NodeCount + 1}{data.Text}", _process, LightBrand.RSEE);
                        break;
                    case NodeType.Camera:
                        node = new NodeCamera($"{Solution.NodeCount + 1}{data.Text}", _process);
                        break;
                    case NodeType.PLCRead:
                        break;
                    case NodeType.PLCWrite:
                        break;
                    case NodeType.AIHT:
                        break;
                    default:
                        break;
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
                else
                {
                    Solution.Nodes.Remove(node);
                    //_process.Nodes.Remove(node);
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
        private void button1_Click(object sender, EventArgs e)
        {
            //运行流程
            try
            {
                _process.Run();

                SetRunStatus(_process.RunTime, true);

            }
            catch (Exception)
            {
                SetRunStatus(_process.RunTime, false);
            }
        }

        /// <summary>
        /// 设置界面的运行状态
        /// </summary>
        /// <param name="ok"></param>
        private void SetRunStatus(long time, bool ok)
        {
            if (ok)
            {
                uiLedBulb1.Color = Color.LawnGreen;
            }
            else
            {
                uiLedBulb1.Color = Color.Red;
            }
            label2.Text = $"耗时:{time}ms";
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
                LogHelper.AddLog(MsgLevel.Info, $"{_process.ProcessName}禁用用", true);
        }
    }
}
