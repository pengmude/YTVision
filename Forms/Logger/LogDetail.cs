using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Logger
{
    internal partial class LogDetail : Form
    {
        string logMsg = string.Empty;
        public LogDetail(string inputMsg)
        {
            InitializeComponent();
            logMsg = inputMsg;
        }

        private void LogDetail_Load(object sender, EventArgs e)
        {
            string[] itemList = Regex.Split(logMsg, "    ");
            if (itemList.Length == 3 || itemList.Length == 4)
            {
                string time = itemList[0];
                string msgLevel = itemList[1];
                string msgDetal = itemList[2];
                txbLogLevel.Text = msgLevel;
                if (logMsg.Contains("Info"))
                    txbLogLevel.BackColor = Color.DodgerBlue;
                else if (logMsg.Contains("Debug"))
                    txbLogLevel.BackColor = Color.Green;
                else if (logMsg.Contains("Warn"))
                    txbLogLevel.BackColor = Color.Orange;
                else if (logMsg.Contains("Exception"))
                    txbLogLevel.BackColor = Color.Red;
                else if (logMsg.Contains("Fatal"))
                    txbLogLevel.BackColor = Color.Red;
                else
                    txbLogLevel.BackColor = Color.DarkGray;
                txbLogTime.Text = time;
                richDetail.Text = msgDetal;
            }
            else
            {
                richDetail.Text = logMsg;
            }
        }
    }
}
