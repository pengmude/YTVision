using Sunny.UI;
using System;
using System.Windows.Forms;

namespace YTVisionPro.Node._6_LogicTool.SharedVariable
{
    internal partial class NodeParamFormSharedVariable : Form, INodeParamForm
    {
        public NodeParamFormSharedVariable()
        {
            InitializeComponent();
            Shown += NodeParamFormSharedVariable_Shown;
            comboBoxTypes.SelectedIndex = 0;
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
                switch (comboBoxTypes.Text)
                {
                    case "任意类型":
                        param.Type = SharedVarTypeEnum.AllType;
                        break;
                    case "布尔":
                        param.Type = SharedVarTypeEnum.Bool;
                        break;
                    case "整型":
                        param.Type = SharedVarTypeEnum.Int;
                        break;
                    case "字符串":
                        param.Type = SharedVarTypeEnum.String;
                        break;
                    case "单精度浮点":
                        param.Type = SharedVarTypeEnum.Float;
                        break;
                    case "双精度浮点":
                        param.Type = SharedVarTypeEnum.Double;
                        break;
                    case "Bitmap图像":
                        param.Type = SharedVarTypeEnum.Bitmap;
                        break;
                    case "Bitmap图像数组":
                        param.Type = SharedVarTypeEnum.BitmapArr;
                        break;
                    case "算法结果":
                        param.Type = SharedVarTypeEnum.ResultViewData;
                        break;
                    default: 
                        throw new ArgumentException($"未知的类型: {comboBoxTypes.Text}");
                }
                param.Index = comboBoxWhichOne.SelectedIndex;
                param.Text1 = nodeSubscription1.GetText1();
                param.Text2 = nodeSubscription1.GetText2();
                param.WriteName = textBox1.Text;
                // 共享变量要提前写入默认值，要不然其他地方添加读取共享变量时获取不到该变量的名称
                Solution.Instance.SharedVariable.SetValue(textBox1.Text, default(object));
                Params = param;
            }
            catch (Exception)
            {
                MessageBox.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if(Params is NodeParamSharedVariable param)
            {
                radioButton1.Checked = param.IsRead;
                radioButton2.Checked = !param.IsRead;
                comboBoxVars.Text = param.ReadName;
                checkBox1.Checked = param.Flag;
                switch (param.Type)
                {
                    case SharedVarTypeEnum.AllType:
                        comboBoxTypes.SelectedIndex = 0;
                        break;
                    case SharedVarTypeEnum.Bool:
                        comboBoxTypes.SelectedIndex = 1;
                        break;
                    case SharedVarTypeEnum.Int:
                        comboBoxTypes.SelectedIndex = 2;
                        break;
                    case SharedVarTypeEnum.String:
                        comboBoxTypes.SelectedIndex = 3;
                        break;
                    case SharedVarTypeEnum.Float:
                        comboBoxTypes.SelectedIndex = 4;
                        break;
                    case SharedVarTypeEnum.Double:
                        comboBoxTypes.SelectedIndex = 5;
                        break;
                    case SharedVarTypeEnum.Bitmap:
                        comboBoxTypes.SelectedIndex = 6;
                        break;
                    case SharedVarTypeEnum.ResultViewData:
                        comboBoxTypes.SelectedIndex = 7;
                        break;
                    default:
                        break;
                }
                comboBoxWhichOne.SelectedIndex = param.Index;
                nodeSubscription1.SetText(param.Text1, param.Text2);
                textBox1.Text = param.WriteName;
                Solution.Instance.SharedVariable.SetValue(param.WriteName, default(object));
            }
        }

        /// <summary>
        /// 选择读取还是写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = radioButton1.Checked;
            panel3.Enabled = radioButton2.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxWhichOne.Enabled = checkBox1.Checked;
        }
    }
}
