using System;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Device.Camera;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class CameraParamsShowControl : UserControl
    {
        ICamera _camera = null;
        public CameraParamsShowControl(ICamera camera)
        {
            InitializeComponent();
            camera.PublishImageEvent += Camera_PublishImageEvent;
            _camera = camera;
        }

        private void Camera_PublishImageEvent(object sender, Bitmap e)
        {
            ytPictrueBox1.Image = e;
        }

    }
}
