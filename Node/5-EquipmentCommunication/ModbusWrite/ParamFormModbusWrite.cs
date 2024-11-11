using Logger;
using Sunny.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Node._5_EquipmentCommunication.ModbusRead;

namespace YTVisionPro.Node._5_EquipmentCommunication.ModbusWrite
{
    internal partial class ParamFormModbusWrite : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }

        // 在 ParamFormModbusRead 类中定义一个新的委托类型
        public delegate Task AsyncEventHandler<T>(object sender, T e);

        // 定义事件
        public event AsyncEventHandler<EventArgs> RunHandler;

        public ParamFormModbusWrite()
        {
            InitializeComponent();
            InitModbusComboBox();
            comboBoxType.SelectedIndex = 0;
        }

        public void SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 获取订阅的bool值
        /// </summary>
        public string GetSubValue()
        {
            try
            {
                return nodeSubscription1.GetValue<bool>() ? "1" : "0";
            }
            catch (Exception)
            {
                throw;
            }
        }

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
                // 从站作为服务器不能发起请求
                if (modbus.ModbusParam.DevType == Device.DevType.ModbusSlave)
                    continue;
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
            SaveParam();
            Hide();
        }

        private void SaveParam()
        {
            if (comboBoxModbusDev.Text.IsNullOrEmpty() || comboBoxModbusDev.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "Modbus不能为空！", true);
                MessageBox.Show("Modbus不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ushort adress;
            try
            {
                adress = ushort.Parse(this.textBoxAddress.Text);
            }
            catch (Exception)
            {
                LogHelper.AddLog(MsgLevel.Exception, "无效的起始地址", true);
                MessageBox.Show("无效的起始地址", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //查找当前选择的Modbus
            IModbus modbus = null;
            foreach (var dev in Solution.Instance.ModbusDevices)
            {
                if (dev.UserDefinedName == comboBoxModbusDev.Text)
                {
                    modbus = dev;
                    break;
                }
            }

            NodeParamModbusWrite nodeParamWrite = new NodeParamModbusWrite();
            nodeParamWrite.Device = modbus;
            nodeParamWrite.DeviceName = modbus.UserDefinedName;
            nodeParamWrite.StartAddress = adress;
            switch (this.comboBoxType.Text)
            {
                case "线圈":
                    nodeParamWrite.DataType = RegistersType.Coils;
                    break;
                case "保持寄存器":
                    nodeParamWrite.DataType = RegistersType.HoldingRegisters;
                    break;
                default:
                    break;
            }
            nodeParamWrite.IsAsync = checkBox1.Checked;
            if (radioButton2.Checked)
            {
                nodeParamWrite.IsSubscribed = false;
                nodeParamWrite.Data = textBoxData.Text;
            }else
            {
                nodeParamWrite.IsSubscribed = true;
                nodeParamWrite.Text1 = nodeSubscription1.GetText1();
                nodeParamWrite.Text2 = nodeSubscription1.GetText2();
            }
            Params = nodeParamWrite;
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
            if (Params is NodeParamModbusWrite param)
            {
                int index1 = comboBoxModbusDev.Items.IndexOf(param.DeviceName);
                comboBoxModbusDev.SelectedIndex = index1 == -1 ? 0 : index1;
                // 反序列化后方案中的Modbus设备对象才是完整的，需要赋值给用到Modbus的节点参数中
                foreach (var dev in Solution.Instance.ModbusDevices)
                {
                    if(dev.UserDefinedName == param.DeviceName)
                    {
                        param.Device = dev;
                        break;
                    }
                }
                textBoxAddress.Text = param.StartAddress.ToString();
                switch (param.DataType)
                {
                    case RegistersType.Coils:
                        comboBoxType.SelectedIndex = 0;
                        break;
                    case RegistersType.DiscreteInput:
                        comboBoxType.SelectedIndex = 1;
                        break;
                    case RegistersType.InputRegisters:
                        comboBoxType.SelectedIndex = 2;
                        break;
                    case RegistersType.HoldingRegisters:
                        comboBoxType.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
                checkBox1.Checked = param.IsAsync;
                if (param.IsSubscribed)
                {
                    radioButton1.Checked = true;
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                }
                else
                {
                    radioButton2.Checked = true;
                    textBoxData.Text = param.Data.ToString();
                }
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            SaveParam();
            RunHandler?.Invoke(this, EventArgs.Empty);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
                tabControl1.SelectedIndex = 0;
            else
                tabControl1.SelectedIndex = 1;
        }
    }
}
