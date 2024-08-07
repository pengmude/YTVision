using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tets_ResizePictrueBox
{
    public partial class YTMessageBox : Form
    {
        private Timer _closeTimer;
        public YTMessageBox(string message, int interval = 1800)
        {
            InitializeComponent();

            this.label1.Text = message;
            // 初始化计时器
            _closeTimer = new Timer();
            _closeTimer.Interval = interval; 
            _closeTimer.Tick += CloseTimer_Tick;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _closeTimer.Start();
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            // 当计时器触发时，关闭窗体
            _closeTimer.Stop(); // 停止计时器
            _closeTimer.Dispose(); // 清理计时器资源
            this.Close(); // 关闭窗体
        }
    }
}
