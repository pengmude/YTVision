using YTVisionPro.Hardware.Camera;
using System;
using System.Windows.Forms;
using Logger;
using System.Diagnostics;
using YTVisionPro.Forms.LightAdd;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class FrmCameraListView : Form
    {
        public static event EventHandler OnCameraListViewClosed;
        public static event EventHandler OnCameraDeserializationCompletionEvent;
        /// <summary>
        /// 设备信息弹窗
        /// </summary>
        FrmCameraInfo _infoWnd = new FrmCameraInfo();

        public FrmCameraListView()
        {
            InitializeComponent();
            FrmCameraInfo.AddCameraDevEvent += FrmCameraInfo_AddCameraDevEvent;
            SingleCamera.SelectedChange += SingleCamera_SingleCameraSelectedChanged;
            SingleCamera.SingleCameraRemoveEvent += SingleCamera_SingleCameraRemoveEvent;
            FrmLightListView.OnLightDeserializationCompletionEvent += Deserialization;
        }

        /// <summary>
        /// 反序列化相机设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender,  EventArgs e)
        {
            // 先移除旧方案的相机控件
            flowLayoutPanel1.Controls.Clear();

            // 添加新的相机
            SingleCamera singleCamera = null;
            LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【相机设备列表】=================================================", true);
            foreach (var dev in ConfigHelper.SolConfig.Devices)
            {
                if (dev is ICamera camera)
                {
                    try
                    {
                        camera.CreateDevice(); // 创建相机，必要的
                        singleCamera = new SingleCamera(camera);
                        singleCamera.Anchor = AnchorStyles.Left;
                        singleCamera.Anchor = AnchorStyles.Right;
                        flowLayoutPanel1.Controls.Add(singleCamera);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"相机（{camera.UserDefinedName}）打开失败，请检查相机状态！原因：{ex.Message}");
                        LogHelper.AddLog(MsgLevel.Exception, $"相机（{camera.UserDefinedName}）打开失败，请检查相机状态！原因：{ex.Message}", true);
                        continue;
                    }
                }
                
            }
            LogHelper.AddLog(MsgLevel.Debug, $"================================================【相机设备列表】已加载完成 ================================================", true);
            OnCameraDeserializationCompletionEvent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 移除一个相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCamera_SingleCameraRemoveEvent(object sender, SingleCamera e)
        {
            SingleCamera.SingleCameraList.Remove(e);
            panel1.Controls.Remove(e.CameraParamsShowControl);
            e.Camera.Dispose();
            //然后移除掉方案中的全局相机并释放相机内存
            Solution.Instance.AllDevices.Remove(e.Camera);
            //最后移除掉单个相机控件
            flowLayoutPanel1.Controls.Remove(e);
        }

        /// <summary>
        /// 处理相机设备添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCameraInfo_AddCameraDevEvent(object sender, CameraParam e)
        {
            SingleCamera singleCamera = null;
            try
            {
                singleCamera = new SingleCamera(e);
                singleCamera.Anchor = AnchorStyles.Left;
                singleCamera.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singleCamera);
            }
            catch (Exception ex)
            {
                SingleCamera.SingleCameraList.Remove(singleCamera);
                LogHelper.AddLog(MsgLevel.Fatal, $"添加相机失败:{ex.Message}", true);
                MessageBox.Show("添加失败！原因：" + ex.Message);
            }
        }
        
        /// <summary>aq
        /// 处理选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCamera_SingleCameraSelectedChanged(object sender, SingleCamera e)
        {
            panel1.Controls.Clear();
            e.CameraParamsShowControl.Dock = DockStyle.Fill;
            e.CameraParamsShowControl.Show();
            panel1.Controls.Add(e.CameraParamsShowControl);
        }

        /// <summary>
        /// 点击添加单个相机控件（左侧）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _infoWnd.ShowDialog();
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
                if(item.IsOpen)
                    item.SetTriggerMode(true);
            }
        }
    }
}
