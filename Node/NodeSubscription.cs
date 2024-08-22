using Logger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

/*
 * ******************************************************************************************************
 * 
 * 类名: NodeSubscription
 * 描述: 这是一个自定义控件类，用于订阅某个流程Process中的节点的运行结果。
 * 作者: pengmude
 * 创建时间: 2024年8月22日
 * 
 * 注意: 截止当前为止节点仅仅支持在当前流程订阅当前节点之前的节点结果。
 * 
 * ******************************************************************************************************
 */

namespace YTVisionPro.Node
{
    internal partial class NodeSubscription : UserControl
    {
        Process _process;
        /// <summary>
        /// 所属节点
        /// </summary>
        NodeBase _node = null;
        /// <summary>
        /// 选择的节点
        /// </summary>
        NodeBase _selectedNode = null;



        public NodeSubscription()
        {
            InitializeComponent();
        }

        public void Init(Process process, NodeBase node)
        {
            _process = process;
            _node = node;
            InitNodeIdList();
            NodeBase.NodeDeletedEvent += NodeBase_NodeDeletedEvent;
        }

        /// <summary>
        /// 订阅处理每当有节点删除的逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeBase_NodeDeletedEvent(object sender, int e)
        {
            InitNodeIdList();
        }

        /// <summary>
        /// 设置节点Id下拉框
        /// </summary>
        /// <param name="ids"></param>
        private void InitNodeIdList()
        {
            _selectedNode = null;
            comboBox1.Items.Clear();
            foreach (var node in _process.Nodes)
            {
                if(node.ID < _node.ID)
                {
                    comboBox1.Items.Add(node.ID);
                    if(_selectedNode == null)
                        _selectedNode = node;
                }
            }
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox1.SelectedIndex == 0) InitProperties(_selectedNode);
        }

        /// <summary>
        /// 节点ID下拉框选中改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var node in _process.Nodes)
            {
                if(node.ID == _node.ID)
                {
                    _selectedNode = node;
                    InitProperties(node);
                }
            }
        }

        /// <summary>
        /// 初始化节点的属性到下拉框中
        /// </summary>
        /// <param name="nodeBase"></param>
        private void InitProperties(NodeBase nodeBase)
        {
            comboBox2.Items.Clear();
            Type nodeResult = nodeBase.Result.GetType();
            var properties = nodeResult.GetProperties();

            foreach (var property in properties) //遍历属性
            {
                comboBox2.Items.Add(property.Name);
            }
        }

        public T GetValue<T>()
        {
            PropertyInfo propertyInfo = _selectedNode.Result.GetType().GetProperty(comboBox2.Text);
            if (propertyInfo != null && propertyInfo.CanRead && propertyInfo.PropertyType == typeof(T))
            {
                return (T)propertyInfo.GetValue(_selectedNode.Result);
            }
            LogHelper.AddLog(MsgLevel.Fatal, $"{_selectedNode.Result.GetType().Name}类型找不到属性{comboBox2.Text}!");
            return (T)_selectedNode.Result;
        }
    }
}
