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

namespace YTVisionPro.Forms.ImageViewer
{
    internal partial class FrmSingleImage : DockContent
    {
        public static int i = 0;
        public FrmSingleImage()
        {
            InitializeComponent();
            this.Text = $"图像窗口{i++}";
        }
    }
}
