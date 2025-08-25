using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TDJS_Vision.Forms.LogoView
{
    public partial class FrmLogo : DockContent
    {
        /// <summary>
        /// 图像窗口隐藏事件
        /// </summary>
        public event EventHandler HideChangedEvent;

        public FrmLogo()
        {
            InitializeComponent();
            try
            {
                var logo = Bitmap.FromFile(Application.StartupPath + "\\Logo.png");
                pictureBox1.Image = logo;
            }
            catch (Exception) { }
        }

        private void FrmResultView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;  // 取消关闭事件，防止窗口关闭
            this.Hide(); // 隐藏窗口
            HideChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
