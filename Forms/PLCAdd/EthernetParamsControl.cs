using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.PLC;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YTVisionPro.Forms.PLCAdd
{
    /// <summary>
    /// PLC网口参数显示界面
    /// </summary>
    internal partial class EthernetParamsControl : UserControl
    {
        private string _plcName;
        public EthernetParamsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存PLC网口连接参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                if (plc.PLCParms.UserDefinedName == _plcName)
                {
                    PLCParms pLCParms = new PLCParms();
                    pLCParms.UserDefinedName = _plcName;
                    pLCParms.PlcConType = PlcConType.ETHERNET;
                    pLCParms.EthernetParms.IP = textBox1.Text;
                    pLCParms.EthernetParms.Port = int.Parse(textBox2.Text);

                    plc.PLCParms = pLCParms;
                }
            }
            MessageBox.Show("保存设置成功！");
        }

        /// <summary>
        /// 更新PLC网口连接参数到控件
        /// </summary>
        /// <param name="pLCParms"></param>
        public void UpdatePlcParams(PLCParms pLCParms)
        {
            _plcName = pLCParms.UserDefinedName;
            this.textBox1.Text = pLCParms.EthernetParms.IP;
            this.textBox2.Text = pLCParms.EthernetParms.Port.ToString();
        }

        /// <summary>
        /// 控件状态
        /// </summary>
        /// <param name="openstatus"></param>
        public void ControlStatus(bool openstatus)
        {
            textBox1.Enabled = openstatus;
            textBox2.Enabled = openstatus;
            button1.Enabled = openstatus;
        }
    }
}
