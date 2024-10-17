using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.Modbus;

namespace YTVisionPro.Forms.ModbusAdd
{
    internal partial class ModbusParamsControl : UserControl
    {
        public ModbusParamsControl(ModbusParam param)
        {
            InitializeComponent();
            uiLabelIP.Text = param.IP;
            uiLabelPort.Text = param.Port.ToString();
        }
    }
}
