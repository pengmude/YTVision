using System.Windows.Forms;
using System.IO.Ports;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Forms.PLCAdd
{
    /// <summary>
    /// PLC串口参数显示控件
    /// </summary>
    public partial class SerialParamsControl : UserControl
    {
        public SerialParamsControl(PLCParms parms)
        {
            InitializeComponent();
            // 初始化下拉框参数
            InitControlParams(parms);
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        private void InitControlParams(PLCParms parms)
        {
            this.labelPort.Text = parms.SerialParms.PortName;
            this.labelBaudRate.Text = parms.SerialParms.BaudRate.ToString();
            this.labelDataBits.Text = parms.SerialParms.DataBits.ToString();
            this.labelStopBits.Text = parms.SerialParms.StopBits == StopBits.One ? "1" : (parms.SerialParms.StopBits == StopBits.OnePointFive ? "1.5" : "2");
            this.labelParity.Text = parms.SerialParms.Parity == Parity.None ? "无" : (parms.SerialParms.Parity == Parity.Odd ? "奇" : "偶");
        }
    }
}
