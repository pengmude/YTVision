using System.Windows.Forms;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Forms.PLCAdd
{
    /// <summary>
    /// PLC网口参数显示界面
    /// </summary>
    public partial class EthernetParamsControl : UserControl
    {
        public EthernetParamsControl(PLCParms pLCParms)
        {
            InitializeComponent();
            UpdatePlcParams(pLCParms);
        }

        /// <summary>
        /// 更新PLC网口连接参数到控件
        /// </summary>
        /// <param name="pLCParms"></param>
        private void UpdatePlcParams(PLCParms pLCParms)
        {
            this.labelIP.Text = pLCParms.EthernetParms.IP;
            this.labelPort.Text = pLCParms.EthernetParms.Port.ToString();
        }
    }
}
