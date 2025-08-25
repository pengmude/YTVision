using System;
using System.Windows.Forms;
using TDJS_Vision.Device.Camera;

namespace TDJS_Vision.Forms.CameraAdd
{
    public partial class CameraParamsShowControl : UserControl
    {
        CameraHik cameraHik;
        public CameraParamsShowControl(ICamera camera)
        {
            InitializeComponent();
            cameraHik = camera as CameraHik;
            cameraHik.ConnectStatusEvent += Cam_ConnectStatusEvent;
            SetInfo(cameraHik);
        }

        private void Cam_ConnectStatusEvent(object sender, bool e)
        {
            if (e)
            {
                if (this.InvokeRequired)
                {
                    // 使用 BeginInvoke 异步地执行设置位置的操作
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            SetInfo(cameraHik);
                        }
                        catch (Exception ex) { }
                    }));
                }
                else
                {
                    SetInfo(cameraHik);
                }
            }
        }

        private void SetInfo(CameraHik cam)
        {
            labelName.Text = cam?.DevName.Split('(')[0].Trim() ?? "未获取到设备名";
            labelSN.Text = cam?.SN ?? "未获取到相机SN";
            labelIP.Text = cam?.IP ?? "未获取到相机IP";
            var src = cam?.TriggerSource ?? TriggerSource.Auto;
            labelTriggerType.Text = "未获取到触发源";
            switch (src)
            {
                case TriggerSource.Auto:
                    labelTriggerType.Text = "自动触发";
                    break;
                case TriggerSource.SOFT:
                    labelTriggerType.Text = "软件触发";
                    break;
                case TriggerSource.LINE0:
                case TriggerSource.LINE1:
                case TriggerSource.LINE2:
                case TriggerSource.LINE3:
                case TriggerSource.LINE4:
                    labelTriggerType.Text = $"外部触发({src})";
                    break;
            }
        }
    }
}
