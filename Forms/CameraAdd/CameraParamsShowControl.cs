using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.Camera;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class CameraParamsShowControl : UserControl
    {
        public CameraParamsShowControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 给控件设置要显示的图片
        /// </summary>
        /// <param name="bitmap"></param>
        public void SetImage(Bitmap bitmap)
        {
            ytPictrueBox1.SrcImage = bitmap;
        }
    }
}
