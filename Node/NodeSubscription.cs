using Logger;
using Sunny.UI;
using System;
using System.ComponentModel;
using System.Linq;
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
            NodeBase.RefreshNodeSubControl += RenameChangeEvent;
        }

        /// <summary>
        /// 只刷新一次
        /// </summary>
        private void RenameChangeEvent(object sender, RenameResult e)
        {
            // 只有订阅控件所在节点和重命名的节点同属于一条流程，且前者ID大于后者ID时，才需要前者更新下拉框节点名称
            if (_node.Process.Nodes.Exists(node => node.ID == e.NodeId) && _node.ID > e.NodeId)
            {
                UpdateComboBoxItem($"{e.NodeId}.{e.NodeNameOld}", $"{e.NodeId}.{e.NodeNameNew}");
            }
        }

        public void UpdateComboBoxItem(string oldText, string newText)
        {
            // 检查 comboBox1.Items 是否存在 oldText
            int index = comboBox1.Items.IndexOf(oldText);

            if (index != -1)
            {
                // 存在 oldText，更新为 newText
                comboBox1.Items[index] = newText;

                // 检查当前选中的文本是否等于 oldText
                if (comboBox1.SelectedItem?.ToString() == oldText)
                {
                    // 更新选中的项
                    comboBox1.SelectedItem = newText;
                }
            }
            else
            {
                // 不存在 oldText，输出提示信息
                LogHelper.AddLog(MsgLevel.Warn, $"重命名节点名称时，节点“{_node.NodeName}”的订阅控件找不到该节点原名称“{oldText}”！", true);
            }
        }

        public void Init(NodeBase node)
        {
            _node = node;
            InitNodeIdList(); //把节点id小于当前节点id的节点的节点加入到下拉框中
            NodeBase.NodeDeletedEvent += NodeBase_NodeDeletedEvent;
        }

        /// <summary>
        /// 订阅处理每当有节点删除的逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeBase_NodeDeletedEvent(object sender, NodeBase e)
        {
            // 移除的节点是当前实例的订阅节点，要置空
            if(_selectedNode != null && e.ID == _selectedNode.ID)
                _selectedNode = null;
            // 更新节点列表
            comboBox1.Items.Remove($"{e.ID}.{e.NodeName}");
            if(comboBox1.Items.Count == 0)
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "";
            }
        }

        /// <summary>
        /// 设置节点Id下拉框
        /// </summary>
        /// <param name="ids"></param>
        private void InitNodeIdList()
        {
            if(_node == null) return;
            comboBox1.Items.Clear();
            foreach (var node in _node.Process.Nodes)
            {
                if (node.ID < _node.ID)
                {
                    comboBox1.Items.Add($"{node.ID}.{node.NodeName}");
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
                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
                if (displayNameAttribute != null)
                {
                    comboBox2.Items.Add(displayNameAttribute.DisplayName);
                }
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
            if(comboBox2.Text.IsNullOrEmpty())
                throw new Exception($"节点无法获取订阅的值!");

            var propertyInfo = _selectedNode.Result.GetType().GetProperties().ToList().Find(item => item.DisplayName() == comboBox2.Text);
            if (propertyInfo != null && propertyInfo.CanRead && propertyInfo.PropertyType == typeof(T))
            {
                return (T)propertyInfo.GetValue(_selectedNode.Result);
            }
            else
            {
                throw new Exception($"节点({_selectedNode.ID}.{_selectedNode.NodeName})获取订阅的{comboBox2.Text}值失败!");
            }
        }

        public string GetText1()
        {
            return comboBox1.Text;
        }

        public string GetText2()
        {
            return comboBox2.Text;
        }

        public void HideText2()
        {
            label2.Visible = false;
            comboBox2.Visible = false;
        }

        //反序列化使用
        public void SetText(string text1, string text2)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if(text1 == comboBox1.Items[i].ToString())
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0;i < comboBox2.Items.Count; i++)
            {
                if (text2 == comboBox2.Items[i].ToString())
                {
                    comboBox2.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
