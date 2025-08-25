using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Device.TCP;

namespace TDJS_Vision.Forms.TCPAdd
{
    public partial class TcpParamsControl : UserControl
    {
        public TcpParamsControl(TcpParam param)
        {
            InitializeComponent();
            if (param.DevType == Device.DevType.TcpServer)
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
