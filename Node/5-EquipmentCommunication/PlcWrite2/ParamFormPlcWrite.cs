using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using TDJS_Vision.Device.PLC;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PanasonicWirte
{
    internal partial class ParamFormPlcWrite : FormBase, INodeParamForm
    {
        public ParamFormPlcWrite()
        {
            InitializeComponent();
            InitPLCComboBox();
        }

        public INodeParam Params { get; set; }

        public void SetNodeBelong(NodeBase node) { }

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
            comboBoxDataType.Items.Add("布尔");
            comboBoxDataType.Items.Add("整数");
            comboBoxDataType.Items.Add("浮点");
            comboBoxDataType.Items.Add("字符串");
            int index2 = comboBoxDataType.Items.IndexOf(text2);
            if (index2 == -1)
                comboBoxDataType.SelectedIndex = 0;
            else
                comboBoxDataType.SelectedIndex = index2;
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
                MessageBoxTD.Show("PLC不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.textBoxAddress.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "信号地址为空", true);
                MessageBoxTD.Show("信号地址为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.textBoxValue.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "写入的值不能为空！", true);
                MessageBoxTD.Show("写入的值不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            nodeParamPlcWrite.Value = textBoxValue.Text;
            switch (this.comboBoxDataType.Text)
            {
                case "布尔":
                    nodeParamPlcWrite.DataType = typeof(bool).Name;
                    break;
                case "整数":
                    nodeParamPlcWrite.DataType = typeof(int).Name;
                    break;
                case "浮点":
                    nodeParamPlcWrite.DataType = typeof(float).Name;
                    break;
                case "字符串":
                    nodeParamPlcWrite.DataType = typeof(string).Name;
                    break;
                default:
                    break;
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
                textBoxValue.Text = param.Value;
                if(param.DataType == typeof(bool).Name)
                {
                    comboBoxDataType.SelectedIndex = 0;
                }
                else if (param.DataType == typeof(int).Name)
                {
                    comboBoxDataType.SelectedIndex = 1;
                }
                else if (param.DataType == typeof(float).Name)
                {
                    comboBoxDataType.SelectedIndex = 2;
                }
                else if (param.DataType == typeof(string).Name)
                {
                    comboBoxDataType.SelectedIndex = 3;
                }
            }
        }
    }
}
