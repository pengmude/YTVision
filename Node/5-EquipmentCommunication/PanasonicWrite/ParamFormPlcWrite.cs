using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.PanasonicWirte
{
    internal partial class ParamFormPlcWrite : Form, INodeParamForm
    {
        public ParamFormPlcWrite()
        {
            InitializeComponent();
            InitPLCComboBox();
        }

        public INodeParam Params { get; set; }

        public void SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }

        public object GetSubResult()
        {
            try
            {
                switch (this.comboBoxDataType.Text)
                {
                    case "整数类型":
                        return nodeSubscription1.GetValue<int>();
                    case "布尔类型":
                        return nodeSubscription1.GetValue<bool>();
                    case "字符串类型":
                        return nodeSubscription1.GetValue<string>();
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 初始化PLC下拉框
        /// </summary>
        private void InitPLCComboBox()
        {
            string text1 = comboBoxPLCList.Text;

            comboBoxPLCList.Items.Clear();
            comboBoxPLCList.Items.Add("[未设置]");
            // 初始化PLC列表,只显示添加的PLC用户自定义名称
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBoxPLCList.Items.Add(plc.UserDefinedName);
            }
            int index1 = comboBoxPLCList.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxPLCList.SelectedIndex = 0;
            else
                comboBoxPLCList.SelectedIndex = index1;

            string text2 = comboBoxDataType.Text;
            comboBoxDataType.Items.Clear();
            comboBoxDataType.Items.Add("整数类型");
            comboBoxDataType.Items.Add("布尔类型");
            comboBoxDataType.Items.Add("字符串类型");
            int index2 = comboBoxDataType.Items.IndexOf(text2);
            if (index2 == -1)
                comboBoxDataType.SelectedIndex = 0;
            else
                comboBoxDataType.SelectedIndex = index2;

            string text3 = comboBoxBoolValue.Text;
            int index3 = comboBoxBoolValue.Items.IndexOf(text3);
            if (index3 == -1)
                comboBoxBoolValue.SelectedIndex = 0;
            else
                comboBoxBoolValue.SelectedIndex = index3;

        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxPLCList.Text.IsNullOrEmpty() || comboBoxPLCList.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "PLC不能为空！", true);
                MessageBox.Show("PLC不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.textBoxAddress.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "信号地址为空", true);
                MessageBox.Show("信号地址为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.comboBoxDataType.Text == "字符串类型" && string.IsNullOrEmpty(this.textBoxCustomData.Text) && this.radioButton1.Checked)
            {
                LogHelper.AddLog(MsgLevel.Exception, "写入字符串类型需要设置值", true);
                MessageBox.Show("写入字符串类型需要设置值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //查找当前选择的PLC
            IPlc plc = null;
            foreach (var plcTmp in Solution.Instance.PlcDevices)
            {
                if (plcTmp.UserDefinedName == comboBoxPLCList.Text)
                {
                    plc = plcTmp;
                    break;
                }
            }

            NodeParamPlcWrite nodeParamPlcWrite = new NodeParamPlcWrite();
            nodeParamPlcWrite.Plc = plc;
            nodeParamPlcWrite.PlcName = plc.UserDefinedName;
            nodeParamPlcWrite.Address = this.textBoxAddress.Text;
            switch(this.comboBoxDataType.Text)
            {
                case "整数类型":
                    nodeParamPlcWrite.DataType = DataType.INT;
                    if(radioButton1.Checked)
                    {
                        nodeParamPlcWrite.Value = int.Parse(this.textBoxCustomData.Text);
                    }
                    break;
                case "布尔类型":
                    nodeParamPlcWrite.DataType = DataType.BOOL;
                    if(radioButton1.Checked)
                    {
                        nodeParamPlcWrite.Value = this.comboBoxBoolValue.SelectedIndex == 0 ? true : false;
                    }
                    break;
                case "字符串类型":
                    nodeParamPlcWrite.DataType = DataType.STRING;
                    if (radioButton1.Checked)
                    {
                        nodeParamPlcWrite.Value = this.textBoxCustomData.Text;
                    }
                    break;
                default:
                    break;
            }
            if(radioButton1.Checked)
            {
                nodeParamPlcWrite.IsSubscribed = false;
            }
            else
            {
                nodeParamPlcWrite.IsSubscribed = true;
                nodeParamPlcWrite.Text1 = nodeSubscription1.GetText1();
                nodeParamPlcWrite.Text2 = nodeSubscription1.GetText2();
            }
            Params = nodeParamPlcWrite;
            Hide();
        }

        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormPlcWrite_Shown(object sender, EventArgs e)
        {
            InitPLCComboBox();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCustomData.Enabled = (comboBoxDataType.Text == "字符串类型" || comboBoxDataType.Text  == "整数类型" )? true : false;
            comboBoxBoolValue.Enabled = comboBoxDataType.Text == "布尔类型"? true : false;
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamPlcWrite param)
            {
                int index1 = comboBoxPLCList.Items.IndexOf(param.PlcName);
                comboBoxPLCList.SelectedIndex = index1 == -1 ? 0 : index1;
                // 反序列化后方案中的PLC设备对象才是完整的，需要赋值给用到PLC的节点参数中
                foreach (var plc in Solution.Instance.PlcDevices)
                {
                    if (plc.UserDefinedName == param.PlcName)
                    {
                        param.Plc = plc;
                        break;
                    }
                }
                textBoxAddress.Text = param.Address;
                switch (param.DataType)
                {
                    case DataType.INT:
                        comboBoxDataType.SelectedIndex = 0;
                        if(!param.IsSubscribed)
                        {
                            textBoxCustomData.Text = param.Value.ToString();
                        }
                        break;
                    case DataType.BOOL:
                        comboBoxDataType.SelectedIndex = 1;
                        if (!param.IsSubscribed)
                        {
                            comboBoxBoolValue.SelectedIndex = (bool)param.Value == true ? 0 : 1;
                        }
                        break;
                    case DataType.STRING:
                        comboBoxDataType.SelectedIndex = 2;
                        if (!param.IsSubscribed)
                        {
                            textBoxCustomData.Text = param.Value.ToString();
                        }
                        break;
                    default:
                        break;
                }
                if(param.IsSubscribed)
                {
                    radioButton2.Checked = true;
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                }
                else
                {
                    radioButton1.Checked = true;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                tabControl1.SelectedIndex = 0;
                radioButton2.Checked = false;
            }
            else
            {
                tabControl1.SelectedIndex = 1;
                radioButton2.Checked = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
        }
    }
}
