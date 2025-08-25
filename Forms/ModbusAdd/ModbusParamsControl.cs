using System.Windows.Forms;
using Sunny.UI;
using TDJS_Vision.Device.Modbus;

namespace TDJS_Vision.Forms.ModbusAdd
{
    public partial class ModbusParamsControl : UserControl
    {
        public ModbusParamsControl(IModbusParam param)
        {
            InitializeComponent();

            switch (param.DevType)
            {
                case Device.DevType.ModbusRTUPoll:
                    var paramSerialPoll = param as ModbusRTUParam;
                    labelCom.Text = paramSerialPoll.PortName;
                    labelBaudRate.Text = paramSerialPoll.BaudRate.ToString();
                    labelDataBits.Text = paramSerialPoll.DataBits.ToString();
                    labelPairty.Text = paramSerialPoll.Parity.ToString();
                    labelStopBits.Text = paramSerialPoll.StopBits.ToString();
                    SetVisible2();
                    break;
                case Device.DevType.ModbusRTUSlave:
                    SetVisible2();
                    break;
                case Device.DevType.ModbusTcpPoll:
                    var paramTcpPoll = param as ModbusTcpParam;
                    uiLabelIP.Text = paramTcpPoll.IP;
                    uiLabelPort.Text = paramTcpPoll.Port.ToString();
                    SetVisible1();
                    break;
                case Device.DevType.ModbusTcpSlave:
                    var paramTcpSlave = param as ModbusTcpParam;
                    uiLabel1.Text = "监听的IP";
                    uiLabelIP.Text = "[监听所有可用的IP]";
                    uiLabelPort.Text = paramTcpSlave.Port.ToString();
                    SetVisible1();
                    break;
            }
        }

        private void SetVisible1()
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel2.Visible = false;
        }
        private void SetVisible2()
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Visible = true;
        }
    }
}
