using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.Modbus;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.Modbus.Read
{
    internal partial class ParamFormModbusRead : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }
        
        public ParamFormModbusRead()
        {
            InitializeComponent();
            InitModbusComboBox();
            comboBoxModbusDev.SelectedIndex = 0;
        }

        public void SetNodeBelong(NodeBase node) { }

        /// <summary>
        /// 初始化Modbus下拉框
        /// </summary>
        private void InitModbusComboBox()
        {
            string text1 = comboBoxModbusDev.Text;

            comboBoxModbusDev.Items.Clear();
            comboBoxModbusDev.Items.Add("[未设置]");
            // 初始化Modbus列表,只显示添加的Modbus用户自定义名称
            foreach (var modbus in Solution.Instance.ModbusDevices)
            {
                comboBoxModbusDev.Items.Add(modbus.UserDefinedName);
            }
            int index1 = comboBoxModbusDev.Items.IndexOf(text1);
            comboBoxModbusDev.SelectedIndex = index1 == -1 ? 0 : index1;
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxModbusDev.Text.IsNullOrEmpty() || comboBoxModbusDev.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "Modbus不能为空！", true);
                MessageBox.Show("Modbus不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ushort adress, length;
            try
            {
                adress = ushort.Parse(this.textBoxAddress.Text);
                length = ushort.Parse(this.textBoxLength.Text);
                if(length == 0)
                    throw new Exception("无效的起始地址或读取个数");
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Exception, "无效的起始地址或读取个数", true);
                MessageBox.Show("无效的起始地址或读取个数", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //查找当前选择的Modbus
            ModbusDevice modbus = null;
            foreach (var dev in Solution.Instance.ModbusDevices)
            {
                if(dev.UserDefinedName == comboBoxModbusDev.Text)
                {
                    modbus = dev;
                    break;
                }
            }

            NodeParamModbusRead nodeParamRead = new NodeParamModbusRead();
            nodeParamRead.Device = modbus;
            nodeParamRead.StartAddress = adress;
            switch (this.comboBox2.Text)
            {
                case "线圈":
                    nodeParamRead.DataType = RegistersType.Coils;
                    break;
                case "离散量输入":
                    nodeParamRead.DataType = RegistersType.DiscreteInput;
                    break;
                case "输入寄存器":
                    nodeParamRead.DataType = RegistersType.InputRegisters;
                    break;
                case "保持寄存器":
                    nodeParamRead.DataType = RegistersType.HoldingRegisters;
                    break;
                default:
                    break;
            }
            nodeParamRead.Count = length;
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
            InitModbusComboBox();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamModbusRead param)
            {
                int index1 = comboBoxModbusDev.Items.IndexOf(param.Device.UserDefinedName);
                comboBoxModbusDev.SelectedIndex = index1 == -1 ? 0 : index1;
                // 反序列化后方案中的Modbus设备对象才是完整的，需要赋值给用到Modbus的节点参数中
                foreach (var dev in Solution.Instance.ModbusDevices)
                {
                    if(dev.UserDefinedName == param.Device.UserDefinedName)
                    {
                        param.Device = dev;
                        break;
                    }
                }
                textBoxAddress.Text = param.StartAddress.ToString();
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        comboBox2.SelectedIndex = 0;
                        break;
                    case RegistersType.DiscreteInput:
                        comboBox2.SelectedIndex = 1;
                        break;
                    case RegistersType.InputRegisters:
                        comboBox2.SelectedIndex = 2;
                        break;
                    case RegistersType.HoldingRegisters:
                        comboBox2.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
                textBoxLength.Text = param.Count.ToString();
            }
        }
    }
}
