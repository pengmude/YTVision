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
                uiLabelIP.Text = "【接受任意IP】";
            else 
                uiLabelIP.Text = param.IP;
            uiLabelPort.Text = param.Port.ToString();
        }
    }
}
