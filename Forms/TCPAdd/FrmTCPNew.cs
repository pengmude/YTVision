using Logger;
using System;
using System.Windows.Forms;
using TDJS_Vision.Device.TCP;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.TCPAdd
{
    public partial class FrmTCPNew : FormBase
    {
        /// <summary>
        /// TCP添加事件
        /// </summary>
        public static event EventHandler<TcpParam> TCPAddEvent;

        public FrmTCPNew()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            Shown += FrmTCPNew_Shown;
        }

        private void FrmTCPNew_Shown(object sender, EventArgs e)
        {
            if(comboBoxType.SelectedIndex == 0)
            {
                textBoxDevName.Text = $"TCP服务器{i}";
            }
            else
            {
                textBoxDevName.Text = $"TCP客户端{j}";
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
                MessageBoxTD.Show("参数不能为空！");
                return;
            }

            // 已添加设备冲突判断
            if (Solution.Instance.TcpDevices.Exists(modbus => modbus.UserDefinedName == textBoxDevName.Text))
            {
                MessageBoxTD.Show("TCP的用户自定义名不能相同！");
                LogHelper.AddLog(MsgLevel.Warn, "TCP的用户自定义名不能相同！", true);
                return;
            }

            TcpParam tcpParms = new TcpParam();

            try
            {
                tcpParms.DevType = comboBoxType.SelectedIndex == 0 ? Device.DevType.TcpServer : Device.DevType.TcpClient;
                tcpParms.IP = uiipTextBoxIP.Text;
                tcpParms.Port = int.Parse(textBoxPort.Text);
                tcpParms.DevName = textBoxDevName.Text;
                tcpParms.UserDefinedName = textBoxDevName.Text;
                TCPAddEvent?.Invoke(this, tcpParms);
                if(tcpParms.DevType == Device.DevType.TcpServer)
                    ++i;
                if (tcpParms.DevType == Device.DevType.TcpClient)
                    ++j;
                Hide();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, "添加TCP时参数设置错误！\n" + ex, true);
                MessageBoxTD.Show("请检查TCP参数是否有误！");
            }
        }
        static int i = 1, j = 1;   // 服务器客户端计数
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxType.Text == "服务器")
            {
                textBoxDevName.Text = $"TCP服务器{i}";
                uiipTextBoxIP.Enabled = false;
            }
            else if(comboBoxType.Text == "客户端")
            {
                textBoxDevName.Text = $"TCP客户端{j}";
                uiipTextBoxIP.Enabled = true;
            }
        }
    }
}
