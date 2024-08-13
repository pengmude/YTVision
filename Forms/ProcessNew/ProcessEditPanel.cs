using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node;
using YTVisionPro.Node.Light;
using YTVisionPro.Node.NodeDemo;
using YTVisionPro.Node.NodeDemoLight;

namespace YTVisionPro.Forms.ProcessNew
{
    public partial class ProcessEditPanel : UserControl
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


        public ProcessEditPanel()
        {
            InitializeComponent();
            _process = new Process();
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeEditPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
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
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);

                #region TODO:根据text创建对应类型的节点，并且赋予节点对应类型的参数设置窗口（给ParamForm赋值）

                LightBrand lightBrand = new LightBrand();
                if (text.Contains("磐鑫"))
                {
                    lightBrand = LightBrand.PPX;
                    text = $"{Solution.NodeCount + 1}{text}";

                    NodeDemoLight node = new NodeDemoLight(text);
                    node.Size = new Size(this.Size.Width - 5, 42);
                    node.Dock = DockStyle.Top;
                    node.NodeDeletedEvent += NewNode_NodeDeletedEvent;
                    node.ParamForm = new ParamFormLight(lightBrand);
                    node.ParamForm.OnNodeParamChange += node.ParamForm_OnNodeParamChange;
                    _stack.Push(node);
                    Solution.Nodes.Add(node);
                    UpdateNode();
                }
                else if (text.Contains("锐视"))
                {
                    lightBrand = LightBrand.RSEE;
                    text = $"{Solution.NodeCount + 1}{text}";

                    NodeDemoLight node = new NodeDemoLight(text);
                    node.Size = new Size(this.Size.Width - 5, 42);
                    node.Dock = DockStyle.Top;
                    node.NodeDeletedEvent += NewNode_NodeDeletedEvent;
                    node.ParamForm = new ParamFormLight(lightBrand);
                    _stack.Push(node);
                    Solution.Nodes.Add(node);
                    UpdateNode();
                }
                //text = $"{Solution.NodeCount + 1}{text}";

                //NodeDemo node = new NodeDemo(text); 
                //node.Size = new Size(this.Size.Width - 5, 42);
                //node.Dock = DockStyle.Top;
                //node.NodeDeletedEvent += NewNode_NodeDeletedEvent;
                //node.ParamForm = new NodeParamSetDemo();

                #endregion

                //_stack.Push(node);
                //Solution.Nodes.Add(node);
                //UpdateNode();
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
        }

        /// <summary>
        /// 刷新控件
        /// </summary>
        private void UpdateNode()
        {
            this.Controls.Clear();
            foreach (var item in _stack)
            {
                this.Controls.Add(item);
            }
        }
    }
}
