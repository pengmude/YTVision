using System;
using WeifenLuo.WinFormsUI.Docking;

namespace YTVisionPro.Forms
{
    internal partial class FrmResultView : DockContent
    {
        /// <summary>
        /// 图像窗口隐藏事件
        /// </summary>
        public event EventHandler HideChangedEvent;

        public FrmResultView()
        {
            InitializeComponent();
        }

        private void FrmResultView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;  // 取消关闭事件，防止窗口关闭
            this.Hide(); // 隐藏窗口
            HideChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
