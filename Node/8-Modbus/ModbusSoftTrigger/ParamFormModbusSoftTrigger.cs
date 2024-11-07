using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Modbus;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using YTVisionPro.Node.PLC.WaitSoftTrigger;

namespace YTVisionPro.Node.Modbus.ModbusSoftTrigger
{
    internal partial class ParamFormModbusSoftTrigger : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }

        public ParamFormModbusSoftTrigger()
        {
            InitializeComponent();
            InitModBusComboBox();
        }

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormModbusSoftTrigger_Load(object sender, EventArgs e)
        {
            InitModBusComboBox();
        }

        /// <summary>
        /// 初始化modbus下拉框
        /// </summary>
        private void InitModBusComboBox()
        {
            string text1 = comboBoxModBusList.Text;

            comboBoxModBusList.Items.Clear();
            comboBoxModBusList.Items.Add("[未设置]");
            // 初始化ModBus列表,只显示添加的ModBus用户自定义名称
            foreach (var mod in Solution.Instance.ModbusDevices)
            {
                comboBoxModBusList.Items.Add(mod.UserDefinedName);
            }
            int index1 = comboBoxModBusList.Items.IndexOf(text1);
            if (index1 == -1)
                comboBoxModBusList.SelectedIndex = 0;
            else
                comboBoxModBusList.SelectedIndex = index1;
        }
        public void SetNodeBelong(NodeBase node){}
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxModBusList.Text.IsNullOrEmpty() || comboBoxModBusList.Text == "[未设置]")
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

            //查找当前选择的modbus
            IModbus modBus = null;
            foreach (var modbusTmp in Solution.Instance.ModbusDevices)
            {
                if (modbusTmp.UserDefinedName == comboBoxModBusList.Text)
                {
                    modBus = modbusTmp;
                    break;
                }
            }

            NodeParamModbusSoftTrigger param = new NodeParamModbusSoftTrigger();
            param.modbus = modBus;
            param.ModBusName = comboBoxModBusList.Text;
            param.Address = this.textBoxAddress.Text;
            Params = param;
            Hide();
        }

        /// <summary>
        /// 反序列化代码
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamModbusSoftTrigger param)
            {
                int index = comboBoxModBusList.Items.IndexOf(param.ModBusName);
                comboBoxModBusList.SelectedIndex = index == -1 ? 0 : index;
                textBoxAddress.Text = param.Address;
                foreach (var modbus in Solution.Instance.ModbusDevices)
                {
                    if (modbus.UserDefinedName == param.ModBusName)
                    {
                        param.modbus = modbus;
                        break;
                    }
                }
            }
        }

        
    }
}
