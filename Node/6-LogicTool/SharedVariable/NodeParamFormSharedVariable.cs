using Sunny.UI;
using System;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._6_LogicTool.SharedVariable
{
    public partial class NodeParamFormSharedVariable : FormBase, INodeParamForm
    {
        // 前一个变量名称
        private string _preVariable = null;

        public NodeParamFormSharedVariable()
        {
            InitializeComponent();
            Shown += NodeParamFormSharedVariable_Shown;
            comboBoxWhichOne.SelectedIndex = 0;
        }

        private void NodeParamFormSharedVariable_Shown(object sender, EventArgs e)
        {
            // 保留原有设置刷新
            string text1 = comboBoxVars.Text;
            comboBoxVars.Items.Clear();
            comboBoxVars.Items.Add("[未选择]");
            foreach (var name in Solution.Instance.SharedVariable.GetNames())
            {
                comboBoxVars.Items.Add(name);
            }
            int index1 = comboBoxVars.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxVars.SelectedIndex = 0;
            else
                comboBoxVars.SelectedIndex = index1;
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 获取订阅的要写入共享变量的值
        /// </summary>
        /// <returns></returns>
        public object GetSubValue()
        {
            try
            {
                return nodeSubscription1.GetValue<object>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamSharedVariable param = new NodeParamSharedVariable();
                param.IsRead = radioButton1.Checked;
                if (comboBoxVars.Text.IsNullOrEmpty())
                    throw new Exception("读取的变量名称不能为空！");
                param.ReadName = comboBoxVars.Text;
                param.Flag = checkBox1.Checked;
                param.Index = comboBoxWhichOne.SelectedIndex;
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.WriteName = textBox1.Text;

                // 要在读该变量之前写入默认值，要不然获取不到该变量
                if (!param.IsRead)
                {
                    Solution.Instance.SharedVariable.SetValue(textBox1.Text, new SharedVarValue());
                }

                Params = param;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        /// <summary>
        /// 订阅节点初始化
        /// </summary>
        /// <param name="node"></param>
        void INodeParamForm.SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamSharedVariable param)
            {
                radioButton1.Checked = param.IsRead;
                radioButton2.Checked = !param.IsRead;

                comboBoxVars.Items.Add(param.ReadName);
                comboBoxVars.SelectedItem = param.ReadName;

                checkBox1.Checked = param.Flag;
                comboBoxWhichOne.SelectedIndex = param.Index;
                nodeSubscription1.SetText(param.Text1, param.Text2);
                textBox1.Text = param.WriteName;
                Solution.Instance.SharedVariable.SetValue(param.WriteName, new SharedVarValue());
            }
        }

        /// <summary>
        /// 选择读取还是写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanelRead.Enabled = radioButton1.Checked;
            tableLayoutPanelWrite.Enabled = radioButton2.Checked;
            tabControl1.SelectedIndex = radioButton1.Checked ? 0 : 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxWhichOne.Enabled = checkBox1.Checked;
        }
    }
}
