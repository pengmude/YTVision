//#define STATIC

using Logger;
using System;
using System.Windows.Forms;


namespace YTVisionPro
{
    internal partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {

#if STATIC
            LogHelper.AddLog(MsgLevel.Info, "记录了一条Info日志");
#else
            LogHelper.AddLog(MsgLevel.Info, "记录了一条Info日志", true);
#endif
        }

        private void button3_Click(object sender, EventArgs e)
        {
#if STATIC
            LogHelper.AddLog(MsgLevel.Debug, "记录了一条Debug日志");
#else
            LogHelper.AddLog(MsgLevel.Debug, "记录了一条Debug日志", true);
#endif

        }

        private void button4_Click(object sender, EventArgs e)
        {
#if STATIC
            LogHelper.AddLog(MsgLevel.Warn, "记录了一条Warn日志");
#else
            LogHelper.AddLog(MsgLevel.Warn, "记录了一条Warn日志", true);
#endif
        }

        private void button5_Click(object sender, EventArgs e)
        {
#if STATIC
            LogHelper.AddLog(MsgLevel.Exception, "记录了一条Exception日志");
#else
            LogHelper.AddLog(MsgLevel.Exception, "记录了一条Exception日志", true);
#endif
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
#if STATIC
            LogHelper.AddLog(MsgLevel.Fatal, "记录了一条Fatal日志");
#else
            LogHelper.AddLog(MsgLevel.Fatal, "记录了一条Fatal日志", true);
#endif
        }
    }
}
