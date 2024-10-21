using System.Windows.Forms;
using YTVisionPro.Device.Modbus;

namespace YTVisionPro.Forms.ModbusAdd
{
    internal partial class ModbusParamsControl : UserControl
    {
        public ModbusParamsControl(ModbusParam param)
        {
            InitializeComponent();
            if (param.DevType == Device.DevType.ModbusSlave)
            {
                uiLabel1.Text = "监听的IP";
                uiLabelIP.Text = "[监听所有可用的IP]";
            }
            else 
                uiLabelIP.Text = param.IP;
            uiLabelPort.Text = param.Port.ToString();
        }
    }
}
