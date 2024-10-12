using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.PanasonicRead
{
    internal partial class ParamFormPlcRead : Form, INodeParamForm
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
            string text1 = comboBox1.Text;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("[未设置]");
            // 初始化PLC列表,只显示添加的PLC用户自定义名称
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBox1.Items.Add(plc.UserDefinedName);
            }
            int index1 = comboBox1.Items.IndexOf(text1);
            if (index1 == -1)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = index1;

            string text2 = comboBox2.Text;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("整数类型");
            comboBox2.Items.Add("布尔类型");
            comboBox2.Items.Add("字符串类型");
            int index2 = comboBox2.Items.IndexOf(text2);
            if (index2 == -1)
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.SelectedIndex = index2;
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.IsNullOrEmpty() || comboBox1.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "PLC不能为空！", true);
                MessageBox.Show("PLC不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "信号地址为空", true);
                MessageBox.Show("信号地址为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(this.comboBox2.Text == "字符串类型" && string.IsNullOrEmpty(this.textBox3.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "读取字符串类型需要设置读取长度", true);
                MessageBox.Show("读取字符串类型需要设置读取长度", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //查找当前选择的PLC
            IPlc plc = null;
            foreach (var plcTmp in Solution.Instance.PlcDevices)
            {
                if(plcTmp.UserDefinedName == comboBox1.Text)
                {
                    plc = plcTmp;
                    break;
                }
            }

            //把设置好的参数传给PLC节点NodePlcDataRead去更新结果
            NodeParamPlcRead nodeParamRead = new NodeParamPlcRead();
            nodeParamRead.Plc = plc;
            nodeParamRead.Address = this.textBox1.Text;
            switch (this.comboBox2.Text)
            {
                case "整数类型":
                    nodeParamRead.DataType = DataType.INT;
                    break;
                case "布尔类型":
                    nodeParamRead.DataType = DataType.BOOL;
                    break;
                case "字符串类型":
                    nodeParamRead.DataType = DataType.STRING;
                    try
                    {
                        nodeParamRead.Length = ushort.Parse(this.textBox3.Text);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
                        MessageBox.Show("保存失败,请检查参数是否有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                default:
                    break;
            }
            Params = nodeParamRead;
            Hide();
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
            textBox3.Enabled = comboBox2.Text == "字符串类型" ? true : false;
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamPlcRead param)
            {
                int index1 = comboBox1.Items.IndexOf(param.Plc.UserDefinedName);
                comboBox1.SelectedIndex = index1;
                // 反序列化后方案中的PLC设备对象才是完整的，需要赋值给用到PLC的节点参数中
                foreach (var plc in Solution.Instance.PlcDevices)
                {
                    if(plc.UserDefinedName == param.Plc.UserDefinedName)
                    {
                        param.Plc = plc;
                        break;
                    }
                }
                textBox1.Text = param.Address;
                switch (param.DataType)
                {
                    case DataType.INT:
                        comboBox2.SelectedIndex = 0;
                        break;
                    case DataType.BOOL:
                        comboBox2.SelectedIndex = 1;
                        break;
                    case DataType.STRING:
                        comboBox2.SelectedIndex = 2;
                        textBox3.Text = param.Length.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
