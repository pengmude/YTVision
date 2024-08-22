using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;

namespace YTVisionPro.Forms.LightAdd
{
    internal partial class LightParamsShowControl : UserControl
    {
        public LightParamsShowControl(LightParam parms)
        {
            InitializeComponent();
            this.labelCom.Text = parms.Port;
            this.labelBaudRate.Text = parms.BaudRate.ToString();
            this.labelDataBits.Text = parms.DataBits.ToString();
            this.labelStopBits.Text = parms.StopBits == System.IO.Ports.StopBits.One ? "1" : parms.StopBits == System.IO.Ports.StopBits.OnePointFive ? "1.5" : "2";
            this.labelPairty.Text = parms.Parity == System.IO.Ports.Parity.None ? "无" : parms.Parity == System.IO.Ports.Parity.Odd ? "奇" : "偶";
            this.labelChanel.Text = parms.Channel.ToString();
        }
    }
}
