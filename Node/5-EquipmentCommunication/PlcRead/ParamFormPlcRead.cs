using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using TDJS_Vision.Device.PLC;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._5_EquipmentCommunication.PlcRead
{
    public partial class ParamFormPlcRead : FormBase, INodeParamForm
    {
        public INodeParam Params { get; set; }
        
        public ParamFormPlcRead()
        {
            InitializeComponent();
            InitPLCComboBox();
        }

        public void SetNodeBelong(NodeBase node) { }

        /// <summary>
        /// 初始化PLC下拉框
        /// </summary>
        private void InitPLCComboBox()
        {
            string text1 = comboBoxPlcList.Text;

            comboBoxPlcList.Items.Clear();
            comboBoxPlcList.Items.Add("[未设置]");
            // 初始化PLC列表,只显示添加的PLC用户自定义名称
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBoxPlcList.Items.Add(plc.UserDefinedName);
            }
            int index1 = comboBoxPlcList.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxPlcList.SelectedIndex = 0;
            else
                comboBoxPlcList.SelectedIndex = index1;

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
            try
            {

                if (comboBoxPlcList.Text.IsNullOrEmpty() || comboBoxPlcList.Text == "[未设置]")
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
                if(this.comboBoxDataType.Text == "字符串类型" && string.IsNullOrEmpty(this.textBoxStringLength.Text))
                {
                    LogHelper.AddLog(MsgLevel.Exception, "读取字符串类型需要设置读取长度", true);
                    MessageBoxTD.Show("读取字符串类型需要设置读取长度", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //查找当前选择的PLC
                IPlc plc = null;
                foreach (var plcTmp in Solution.Instance.PlcDevices)
                {
                    if(plcTmp.UserDefinedName == comboBoxPlcList.Text)
                    {
                        plc = plcTmp;
                        break;
                    }
                }

                //把设置好的参数传给PLC节点NodePlcDataRead去更新结果
                NodeParamPlcRead nodeParamRead = new NodeParamPlcRead();
                nodeParamRead.Plc = plc;
                nodeParamRead.PlcName = plc.UserDefinedName;
                nodeParamRead.Address = this.textBoxAddress.Text;
                switch (this.comboBoxDataType.Text)
                {
                    case "布尔":
                        nodeParamRead.DataType = typeof(bool).Name;
                        break;
                    case "整数":
                        nodeParamRead.DataType = typeof(int).Name;
                        break;
                    case "浮点":
                        nodeParamRead.DataType = typeof(float).Name;
                        break;
                    case "字符串":
                        nodeParamRead.DataType = typeof(string).Name;
                        nodeParamRead.Length = ushort.Parse(this.textBoxStringLength.Text);
                        break;
                    default:
                        break;
                }
                Params = nodeParamRead;
                Hide();

            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"保存失败,请检查参数是否有误！原因：{ex.Message}", true);
                MessageBoxTD.Show($"保存失败,请检查参数是否有误！原因：{ex.Message}", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormRead_Shown(object sender, EventArgs e)
        {
            InitPLCComboBox();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 只有字符串/字符串数组需要设置长度
            textBoxStringLength.Enabled = comboBoxDataType.Text.Contains("字符串") ? true : false;
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamPlcRead param)
            {
                int index1 = comboBoxPlcList.Items.IndexOf(param.PlcName);
                comboBoxPlcList.SelectedIndex = index1;
                // 反序列化后方案中的PLC设备对象才是完整的，需要赋值给用到PLC的节点参数中
                foreach (var plc in Solution.Instance.PlcDevices)
                {
                    if(plc.UserDefinedName == param.PlcName)
                    {
                        param.Plc = plc;
                        break;
                    }
                }
                try
                {
                    textBoxAddress.Text = param.Address;
                    if (param.DataType == typeof(bool).Name)
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
                    else
                        throw new Exception("不支持的类型");
                }
                catch (Exception) { }
            }
        }
    }
}
