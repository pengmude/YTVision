using Sunny.UI.Win32;
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
        public CameraParamsShowControl(ICamera camera)
        {
            InitializeComponent();
            camera.PublishImageEvent += Camera_PublishImageEvent;
        }

        private void Camera_PublishImageEvent(object sender, Bitmap e)
        {
            ytPictrueBox1.SrcImage = e;
        }

    }
}
