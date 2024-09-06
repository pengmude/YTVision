using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Forms.PLCMonitor
{
    public partial class FrmSignalMonitor : Form
    {
        //Control.ControlCollection collection;
        public FrmSignalMonitor()
        {
            InitializeComponent();
            //collection = new Control.ControlCollection(this);
            InitTabPages();
            FrmPLCNew.PLCAddEvent += FrmAdd_PLCAddEvent;
            SinglePLC.SinglePLCRemoveEvent += SinglePLC_SinglePLCRemoveEvent;
        }

        private void SinglePLC_SinglePLCRemoveEvent(object sender, SinglePLC e)
        {
            TabPage tabPageRemove = null;
            foreach (TabPage tab in tabControl1.Controls)
            {
                if(tab.Text == e.Plc.UserDefinedName)
                {
                    tabPageRemove = tab;
                    break;
                }
            }
            if(tabPageRemove != null)
                tabControl1.Controls.Remove(tabPageRemove);
        }

        private void FrmAdd_PLCAddEvent(object sender, PLCParms e)
        {
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                if(plc.UserDefinedName == e.UserDefinedName)
                {
                    TabPage tabPage = new TabPage(e.UserDefinedName);
                    tabPage.BackColor = Color.White;
                    var con = new SignalConfig();
                    con.SetPlc(plc);
                    con.Dock = DockStyle.Fill;
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.ColumnCount = 1;
                    tableLayoutPanel.RowCount = 1;
                    tableLayoutPanel.Controls.Add(con, 0, 0);
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(tableLayoutPanel);
                    tabControl1.Controls.Add(tabPage);
                    //collection.Add(tabPage);
                    break;
                }
            }
        }

        private void InitTabPages()
        {
            tabControl1.Controls.Clear();
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                TabPage tabPage = new TabPage(plc.UserDefinedName);
                tabPage.BackColor = Color.White;
                var con = new SignalConfig();
                con.SetPlc(plc);
                con.Dock = DockStyle.Fill;
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.ColumnCount = 1;
                tableLayoutPanel.RowCount = 1;
                tableLayoutPanel.Controls.Add(con, 0, 0);
                tableLayoutPanel.Dock = DockStyle.Fill;
                tabPage.Controls.Add(tableLayoutPanel);
                tabControl1.Controls.Add(tabPage);
                //collection.Add(tabPage);
            }
        }
    }
}
