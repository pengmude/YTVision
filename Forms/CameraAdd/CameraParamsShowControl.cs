﻿using System;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Hardware.Camera;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class CameraParamsShowControl : UserControl
    {
        ICamera _camera = null;
        public CameraParamsShowControl(ICamera camera)
        {
            InitializeComponent();
            if(camera is CameraHik hik)
            {
                hik.PublishImageEvent += Camera_PublishImageEvent;
            }else if(camera is CameraBasler basler)
            {
                basler.PublishImageEvent += Camera_PublishImageEvent;
            }
            //camera.PublishImageEvent += Camera_PublishImageEvent;
            _camera = camera;
            FrmCameraListView.OnCameraListViewClosed += FrmCameraListView_OnCameraListViewClosed;
        }

        private void FrmCameraListView_OnCameraListViewClosed(object sender, EventArgs e)
        {
            _camera.PublishImageEvent -= Camera_PublishImageEvent;
        }

        private void Camera_PublishImageEvent(object sender, Bitmap e)
        {
            ytPictrueBox1.Image = e;
        }

    }
}
