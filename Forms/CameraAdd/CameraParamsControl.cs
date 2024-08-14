using YTVisionPro.Hardware.Camera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Threading;

namespace YTVisionPro.Forms.CameraAdd
{
    public partial class CameraParamsControl : UserControl
    {
        /// <summary>
        /// 当前相机
        /// </summary>
        CameraHik _curCamera = new CameraHik();
        /// <summary>
        /// 相机名称列表
        /// </summary>
        List<string> Cameralist = new List<string>();
        /// <summary>
        /// 相机设备信息列表
        /// </summary>
        List<MyCamera.MV_CC_DEVICE_INFO> _deviceInfoList;

        public CameraParamsControl()
        {
            InitializeComponent();
            SearchCamera();
        }

        /// <summary>
        /// 搜索相机
        /// </summary>
        private void SearchCamera()
        {
            Cameralist.Clear();
            _deviceInfoList = CameraHik.FindCamera();
            //获取相机名称 
            for (int i = 0; i < _deviceInfoList.Count; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = _deviceInfoList[i];
                //网口相机
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    string camName = gigeInfo.chModelName + "(" + gigeInfo.chSerialNumber + ")";
                    Cameralist.Add(camName);
                }
                //usb接口相机
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    string camName = usbInfo.chModelName + "(" + usbInfo.chSerialNumber + ")";
                    Cameralist.Add(camName);
                }
            }

            this.comboBox1.Items.Clear();
            foreach (var item in Cameralist)
            {
                this.comboBox1.Items.Add(item);
            }

            if (Cameralist.Count != 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 点击搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SearchCamera();
        }

        /// <summary>
        /// 下拉列表选择当前相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _curCamera.DeviceInfo = _deviceInfoList[this.comboBox1.SelectedIndex];
            //_curCamera.DevName = this.comboBox1.Text;
        }

        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if(value)
            {
                _curCamera.Open();
                _curCamera.SetTriggerMode(false);
                _curCamera.SetExposureTime(int.Parse(this.textBox1.Text));
                _curCamera.SetGain(int.Parse(this.textBox2.Text));
                _curCamera.CameraGrabEvent += _curCamera_CameraGrabEvent;
                _curCamera.StartGrabbing();


                //CamParams camParams = new CamParams();
                //camParams.OpenStatus = true;
                //_curCamera.camParams = camParams;
            }
            else
            {
                _curCamera.Close();
            }
        }

        /// <summary>
        /// 显示相机图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _curCamera_CameraGrabEvent(object sender, Bitmap e)
        {
            pictureBox1.Image = e;
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            _curCamera.SetExposureTime(int.Parse(this.textBox1.Text));
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            _curCamera.SetGain(int.Parse(this.textBox2.Text));
        }

        /// <summary>
        /// 设置软触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _curCamera.SetTriggerMode(true);
                _curCamera.SetTriggerSource(TriggerSource.SOFT);
                radioButton2.Checked = false;
                button1.Enabled = true;
            }
        }

        /// <summary>
        /// 设置硬触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                _curCamera.SetTriggerMode(true);
                _curCamera.SetTriggerSource(TriggerSource.LINE0);
                radioButton1.Checked = false;
                button1.Enabled = false;
            }
        }

        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _curCamera.GrabOne();
        }
    }
}
