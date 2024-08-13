using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            AddDev();
        }

        /// <summary>
        /// 测试设备添加
        /// </summary>
        private void AddDev()
        {
            //var lights = new List<IDevice>() { new LightPPX("光源1"), new LightPPX("光源2"), new LightPPX("光源3") };
            //var camers = new List<IDevice>() { new CameraHik("相机1"), new CameraHik("相机2"), new CameraHik("相机3") };
            //var plcs = new List<IDevice>() { new PlcPanasonic("PLC1"), new PlcPanasonic("PLC2"), new PlcPanasonic("PLC3") };
            //Solution.Instance.AddDeviceList(lights);
            //Solution.Instance.AddDeviceList(camers);
            //Solution.Instance.AddDeviceList(plcs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if(openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    ytPictrueBox1.Image = new Bitmap(openFileDialog1.FileName);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
