using Logger;
using System;
using System.IO.Ports;
using System.Net;
using System.Windows.Forms;
using YTVisionPro.Device.Modbus;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Forms.ModbusAdd
{
    internal partial class FrmModbusNew : Form
    {
        /// <summary>
        /// Modbus添加事件
        /// </summary>
        public static event EventHandler<ModbusParam> ModbusAddEvent;

        public FrmModbusNew()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            Shown += FrmModbusNew_Shown;
        }

        private void FrmModbusNew_Shown(object sender, EventArgs e)
        {
            if(comboBoxType.SelectedIndex == 0)
            {
                textBoxDevName.Text = $"Modbus主站{i}";
            }
            else
            {
                textBoxDevName.Text = $"Modbus从站{i}";
            }
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiipTextBoxIP.Text) || string.IsNullOrEmpty(textBoxPort.Text) || string.IsNullOrEmpty(this.textBoxDevName.Text))
            {
                MessageBox.Show("参数不能为空！");
                return;
            }

            // 已添加设备冲突判断
            if (Solution.Instance.ModbusDevices.Exists(modbus => modbus.UserDefinedName == textBoxDevName.Text))
            {
                MessageBox.Show("Modbus的用户自定义名不能相同！");
                LogHelper.AddLog(MsgLevel.Warn, "Modbus的用户自定义名不能相同！", true);
                return;
            }

            ModbusParam modbusParms = new ModbusParam();

            try
            {
                modbusParms.DevType = comboBoxType.SelectedIndex == 0 ? Device.DevType.ModbusPoll : Device.DevType.ModbusSlave;
                modbusParms.IP = uiipTextBoxIP.Text;
                modbusParms.Port = int.Parse(textBoxPort.Text);
                modbusParms.DevName = textBoxDevName.Text;
                modbusParms.UserDefinedName = textBoxDevName.Text;
                ModbusAddEvent?.Invoke(this, modbusParms);
                if (modbusParms.DevType == Device.DevType.ModbusPoll)
                    ++i;
                if(modbusParms.DevType == Device.DevType.ModbusSlave)
                    ++j;
                Hide();
            }
            catch(NotImplementedException ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, ex.Message, true);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加Modbus时参数设置错误！", true);
                MessageBox.Show("请检查Modbus参数是否有误！");
            }
        }

        int i = 1, j = 1;   // 设备计数
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxType.Text)
            {
                case "作为主站":
                    uiipTextBoxIP.Enabled = true;
                    textBoxDevName.Text = $"Modbus主站{i}";
                    break;
                case "作为从站":
                    uiipTextBoxIP.Enabled = false;
                    textBoxDevName.Text = $"Modbus从站{j}";
                    break;
                default:
                    break;
            }
        }
    }
}
