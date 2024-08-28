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
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Forms.PLCAdd
{
    internal partial class FrmPLCListView : Form
    {
        FrmPLCNew _frmAdd = new FrmPLCNew();

        public FrmPLCListView()
        {
            InitializeComponent();
            _frmAdd.PLCAddEvent += FrmAdd_PLCAddEvent;
            SinglePLC.SelectedChange += SinglePLC_SelectedChange;
            SinglePLC.SinglePLCRemoveEvent += SinglePLC_SinglePLCRemoveEvent;
        }
        /// <summary>
        /// 移除PLC设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePLC_SinglePLCRemoveEvent(object sender, SinglePLC e)
        {
            //移除的是被选中的则要清除它参数显示控件
            if (e.IsSelected)
                panel1.Controls.Remove(e.SerialParamsControl);
            e.Plc.Disconnect();
            Solution.Instance.Devices.Remove(e.Plc);
            flowLayoutPanel1.Controls.Remove(e);
        }

        /// <summary>
        /// PLC选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePLC_SelectedChange(object sender, SinglePLC e)
        {
            //将选中的PLC的参数控件设置到右侧
            panel1.Controls.Clear();
            if (e.ConType == PlcConType.COM)
            {
                e.SerialParamsControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(e.SerialParamsControl);
            }
            else if (e.ConType == PlcConType.ETHERNET)
            {
                e.EthernetParamsControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(e.EthernetParamsControl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _frmAdd.ShowDialog();
        }

        private void FrmAdd_PLCAddEvent(object sender, PLCParms e)
        {
            SinglePLC singlePLC = null;
            try
            {
                singlePLC = new SinglePLC(e);
                singlePLC.Anchor = AnchorStyles.Left;
                singlePLC.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singlePLC);
            }
            catch (Exception)
            {
                SinglePLC.SinglePLCs.Remove(singlePLC);
            }
        }

        private void FrmPLCListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
