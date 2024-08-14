using YTVisionPro.Hardware.Camera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Forms.CameraAdd
{
    public partial class FrmCameraListView : Form
    {
        public FrmCameraListView()
        {
            InitializeComponent();
            SingleCamera.SingleCameraSelectedChanged += SingleCamera_SingleCameraSelectedChanged;
        }

        private void SingleCamera_SingleCameraSelectedChanged(object sender, EventArgs e)
        {
            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is SingleCamera cameraInfo)
                {
                    panel1.Controls.Clear();
                    cameraInfo.CameraParamsControl.Dock = DockStyle.Fill;
                    cameraInfo.CameraParamsControl.Show();
                    panel1.Controls.Add(cameraInfo.CameraParamsControl);
                }
            }
        }

        /// <summary>
        /// 点击添加单个相机控件（左侧）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SingleCamera singleCamera = new SingleCamera();
            singleCamera.Anchor = AnchorStyles.Left;
            singleCamera.Anchor = AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(singleCamera);
            singleCamera.Show();
            singleCamera.CameraParamsControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(singleCamera.CameraParamsControl);
        }

        /// <summary>
        /// 窗口关闭关掉所有相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCameraListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in Solution.Instance.CameraDevices)
            {
                item.Close();
            }
        }
    }
}
