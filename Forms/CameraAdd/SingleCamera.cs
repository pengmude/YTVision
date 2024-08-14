using YTVisionPro.Hardware.Camera;
using Sunny.UI;
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
    public partial class SingleCamera : UserControl
    {
        private static int cnt = 1;
        public CameraParamsControl CameraParamsControl;
        private static SingleCamera preSingleCamera;
        public static event EventHandler SingleCameraSelectedChanged;

        public SingleCamera()
        {
            InitializeComponent();
            this.label1.Text = "相机" + $"{cnt++}";
            CameraParamsControl = new CameraParamsControl();
        }

        /// <summary>
        /// 改变控件背景颜色和状态
        /// </summary>
        /// <param name="flag"></param>
        public void SetSelectedStatus(bool flag)
        {
            if (flag)
            {
                this.tableLayoutPanel1.BackColor = Color.Gray;
                this.label1.BackColor = Color.Gray;
            }
            else
            {
                this.tableLayoutPanel1.BackColor = SystemColors.Control;
                this.label1.BackColor = SystemColors.Control;
            }
        }

        private void SingleCamera_MouseClick(object sender, EventArgs e)
        {
            if(preSingleCamera == null)
            {
                SetSelectedStatus(true);
            }
            else
            {
                preSingleCamera.SetSelectedStatus(false);
                SetSelectedStatus(true);
                preSingleCamera = this;
            }
            SingleCameraSelectedChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
