using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.TCP;

namespace YTVisionPro.Forms.TCPAdd
{
    internal partial class TcpParamsControl : UserControl
    {
        public TcpParamsControl(TcpParam param)
        {
            InitializeComponent();
            uiLabelIP.Text = param.IP;
            uiLabelPort.Text = param.Port.ToString();
        }
    }
}
