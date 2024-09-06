using Logger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace YTVisionPro.Node
{
    /// <summary>
    /// 
    /// 【作者】: pengmude
    /// 【类名】: NodeSubscription
    /// 【创建时间】: 2024年8月22日
    /// 【描述】: 这是一个自定义控件类，用于订阅某个流程Process中的节点的运行结果。
    /// 【使用教程】：界面拖出本控件到节点参数设置界面后，还需要在节点参数设置界面类中的SetNodeBelong中
    /// 初始化,如"nodeSubscription1.Init(node)"；最后需要在节点类的构造函数设置“节点参数设置窗口”
    /// 所属的节点，如HTAI节点类：ParamForm.SetNodeBelong(this)，这样即可订阅到之前节点结果。
    ///【注意事项】: 截止当前为止节点仅仅支持在当前流程订阅当前节点之前的节点结果。
    ///
    /// </summary>
    internal partial class NodeSubscription : UserControl
    {
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

        public void Init(NodeBase node)
        {
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
            if(_node == null) return;
            _selectedNode = null;
            comboBox1.Items.Clear();
            foreach (var node in _node.Process.Nodes)
            {
                if(node.ID < _node.ID)
                {
                    comboBox1.Items.Add($"{node.ID}.{node.NodeName}");
                    if(_selectedNode == null)
                        _selectedNode = node;
                }
            }
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 节点ID下拉框选中改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var node in _node.Process.Nodes)
            {
                if($"{node.ID}.{node.NodeName}" == comboBox1.Text)
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
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }
        /// <summary>
        /// 获取节点对应结果的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            try
            {
                PropertyInfo propertyInfo = _selectedNode.Result.GetType().GetProperty(comboBox2.Text);
                if (propertyInfo != null && propertyInfo.CanRead && propertyInfo.PropertyType == typeof(T))
                {
                    return (T)propertyInfo.GetValue(_selectedNode.Result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({_selectedNode.NodeName})获取订阅的{comboBox2.Text}值失败!原因：{ex.Message}");
                throw ex;
            }
            return default(T);
        }
    }
}
