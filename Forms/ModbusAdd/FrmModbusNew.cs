using Logger;
using System;
using System.IO.Ports;
using TDJS_Vision.Device.Light;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.ModbusAdd
{
    public partial class FrmModbusNew : FormBase
    {
        // 对应类型设备计数
        private static int _countRtuPoll = 0;
        private static int _countTcpPoll = 0;
        private static int _countTcpSlave = 0;
        /// <summary>
        /// Modbus添加事件
        /// </summary>
        public static event EventHandler<IModbusParam> ModbusAddEvent;

        public FrmModbusNew()
        {
            InitializeComponent();
            InitPortComboBox();
        }

        /// <summary>
        /// 搜索串口并添加到下拉框
        /// </summary>
        public void InitPortComboBox()
        {
            // 串口号获取
            comboBoxCom.Items.Clear();
            foreach (var com in SerialPort.GetPortNames())
            {
                comboBoxCom.Items.Add(com);
            }
            if (comboBoxCom.Items.Count > 0)
                this.comboBoxCom.SelectedIndex = 0;

            // 波特率
            comboBoxBaute.SelectedIndex = 1;
            // 数据位
            comboBoxDataBit.SelectedIndex = 3;
            // 停止位
            comboBoxStopBit.SelectedIndex = 0;
            // 校验位
            comboBoxParity.SelectedIndex = 0;

        }

        /// <summary>
        /// 点击创建RTU主站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRTUPoll_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBoxCom.Text) || string.IsNullOrEmpty(comboBoxBaute.Text) 
                || string.IsNullOrEmpty(comboBoxDataBit.Text) || string.IsNullOrEmpty(comboBoxStopBit.Text)
                || string.IsNullOrEmpty(comboBoxParity.Text) || string.IsNullOrEmpty(textBoxRTUDevName.Text))
            {
                MessageBoxTD.Show("参数不能为空！");
                return;
            }
            // 已添加设备冲突判断
            if (Solution.Instance.ModbusDevices.Exists(modbus => modbus.UserDefinedName == textBoxRTUDevName.Text))
            {
                MessageBoxTD.Show("Modbus设备名称重复！");
                LogHelper.AddLog(MsgLevel.Warn, "Modbus设备名称重复！", true);
                return;
            }

            try
            {
                ModbusRTUParam modbusRTUParms = new ModbusRTUParam
                {
                    DevType = Device.DevType.ModbusRTUPoll,
                    PortName = comboBoxCom.Text,
                    BaudRate = int.Parse(comboBoxBaute.Text),
                    DataBits = int.Parse(comboBoxDataBit.Text),
                    StopBits = comboBoxStopBit.Text == "1" ? StopBits.One : comboBoxStopBit.Text == "1.5" ? StopBits.OnePointFive : StopBits.Two,
                    Parity = comboBoxParity.Text == "奇" ? Parity.Odd : comboBoxParity.Text == "偶" ? Parity.Even : Parity.None,
                    DevName = textBoxRTUDevName.Text,
                    UserDefinedName = textBoxRTUDevName.Text
                };
                ModbusAddEvent?.Invoke(this, modbusRTUParms);
                ++_countRtuPoll;
                textBoxRTUDevName.Text = $"ModbusRTU主站{_countRtuPoll + 1}";
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加Modbus时参数设置错误！", true);
                MessageBoxTD.Show("请检查Modbus参数是否有误！");
            }
        }
        /// <summary>
        /// 点击创建TCP主站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTcpPoll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiipTextBoxTcpPollIP.Text) || string.IsNullOrEmpty(textBoxTcpPollPort.Text)
                || string.IsNullOrEmpty(textBoxTcpPollDevName.Text))
            {
                MessageBoxTD.Show("参数不能为空！");
                return;
            }
            // 已添加设备冲突判断
            if (Solution.Instance.ModbusDevices.Exists(modbus => modbus.UserDefinedName == textBoxTcpPollDevName.Text))
            {
                MessageBoxTD.Show("Modbus设备名称重复！");
                LogHelper.AddLog(MsgLevel.Warn, "Modbus设备名称重复！", true);
                return;
            }

            try
            {
                ModbusTcpParam modbusTcpParms = new ModbusTcpParam
                {
                    DevType = Device.DevType.ModbusTcpPoll,
                    IP = uiipTextBoxTcpPollIP.Text,
                    Port = int.Parse(textBoxTcpPollPort.Text),
                    DevName = textBoxTcpPollDevName.Text,
                    UserDefinedName = textBoxTcpPollDevName.Text
                };
                ModbusAddEvent?.Invoke(this, modbusTcpParms);
                ++_countTcpPoll;
                textBoxTcpPollDevName.Text = $"ModbusTcp主站{_countTcpPoll + 1}";
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加Modbus时参数设置错误！", true);
                MessageBoxTD.Show("请检查Modbus参数是否有误！");
            }
        }
        /// <summary>
        /// 点击创建TCP从站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTcpSlave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTcpSlaveID.Text) || string.IsNullOrEmpty(textBoxTcpSlavePort.Text) || string.IsNullOrEmpty(textBoxTcpSlaveDevName.Text))
            {
                MessageBoxTD.Show("参数不能为空！");
                return;
            }

            // 已添加设备冲突判断
            if (Solution.Instance.ModbusDevices.Exists(modbus => modbus.UserDefinedName == textBoxTcpSlaveDevName.Text))
            {
                MessageBoxTD.Show("Modbus设备名称重复！");
                LogHelper.AddLog(MsgLevel.Warn, "Modbus设备名称重复！", true);
                return;
            }

            try
            {
                ModbusTcpParam modbusTcpParms = new ModbusTcpParam
                {
                    DevType = Device.DevType.ModbusTcpSlave,
                    //IP = uiipTextBoxTcpPollIP.Text,
                    Port = int.Parse(textBoxTcpSlavePort.Text),
                    ID = byte.Parse(textBoxTcpSlaveID.Text),
                    DevName = textBoxTcpSlaveDevName.Text,
                    UserDefinedName = textBoxTcpSlaveDevName.Text
                };
                ModbusAddEvent?.Invoke(this, modbusTcpParms);
                ++_countTcpSlave;
                textBoxTcpSlaveDevName.Text = $"ModbusTcp从站{_countTcpSlave + 1}";
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加Modbus时参数设置错误！", true);
                MessageBoxTD.Show("请检查Modbus参数是否有误！");
            }
        }
    }
}
