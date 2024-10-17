using Logger;
using System;
using System.IO.Ports;
using System.Net;
using System.Windows.Forms;
using YTVisionPro.Hardware.Modbus;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using YTVisionPro.Hardware.PLC;

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
                modbusParms.IP = uiipTextBoxIP.Text;
                modbusParms.Port = int.Parse(textBoxPort.Text);
                modbusParms.DevName = textBoxDevName.Text;
                modbusParms.UserDefinedName = textBoxDevName.Text;
                ModbusAddEvent?.Invoke(this, modbusParms);
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加Modbus时参数设置错误！\n" + ex, true);
                MessageBox.Show("请检查Modbus参数是否有误！");
            }
        }
    }
}
