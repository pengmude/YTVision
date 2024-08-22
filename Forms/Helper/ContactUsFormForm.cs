﻿using System.Diagnostics;
using System.Windows.Forms;

namespace YTVisionPro.Forms.Helper
{
    internal partial class ContactUsFormForm : Form
    {
        public ContactUsFormForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 点击进入云田视觉公司官网
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start http://www.gdytv.com/") { CreateNoWindow = true });
        }
    }
}
