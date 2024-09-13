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
using YTVisionPro.Node.Tool.ImageShow;

namespace YTVisionPro.Forms.ImageViewer
{
    internal partial class FrmSingleImage : DockContent
    {
        public static int i = 0;
        public FrmSingleImage()
        {
            InitializeComponent();
            this.Text = $"图像窗口{++i}";
            NodeImageShow.ImageShowChanged += NodeImageShow_ImageShowChanged;
        }

        private void NodeImageShow_ImageShowChanged(object sender, Paramsa e)
        {
            if(Text == e.Winname)
                ytPictrueBox1.SrcImage = e.Bitmap;
        }

        private void FrmSingleImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmImageViewer.CurWindowsNum--;
            e.Cancel = true;
            Hide();
        }
    }
}
