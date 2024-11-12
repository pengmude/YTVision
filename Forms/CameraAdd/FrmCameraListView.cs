using YTVisionPro.Device.Camera;
using System;
using System.Windows.Forms;
using Logger;
using System.Diagnostics;
using YTVisionPro.Forms.LightAdd;
using System.Linq;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class FrmCameraListView : Form
    {
        /// <summary>
        /// 添加相机时通过快捷键保存方案的事件
        /// </summary>
        public event EventHandler OnShotKeySavePressed;
        /// <summary>
        /// 相机管理窗口关闭事件
        /// </summary>
        public static event EventHandler OnCameraListViewClosed;
        /// <summary>
        /// 反序列化完成相机后开始反序列化PLC
        /// </summary>
        public static event EventHandler<bool> OnCameraDeserializationCompletionEvent;
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
            this.KeyPreview = true;
        }

        /// <summary>
        /// 反序列化相机设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender,  bool e)
        {
            // 先移除旧方案的相机控件
            flowLayoutPanel1.Controls.Clear();

            // 没有对应类型设备跳过加载
            if (ConfigHelper.SolConfig.Devices.Count(device => device is ICamera) == 0)
            {
                OnCameraDeserializationCompletionEvent?.Invoke(this, e);
                return;
            }
            // 添加新的相机
            SingleCamera singleCamera = null;
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【相机设备列表】=================================================", true);
            foreach (var dev in ConfigHelper.SolConfig.Devices)
            {
                if (dev is ICamera camera)
                {
                    camera.CreateDevice(); // 创建相机，必要的
                    singleCamera = new SingleCamera(camera);
                    singleCamera.Anchor = AnchorStyles.Left;
                    singleCamera.Anchor = AnchorStyles.Right;
                    flowLayoutPanel1.Controls.Add(singleCamera);
                    if (e)
                        LogHelper.AddLog(MsgLevel.Info, $"相机设备【{dev.DevName}】已加载！", true);
                }
                
            }
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================【相机设备列表】已加载完成 ================================================", true);

            OnCameraDeserializationCompletionEvent?.Invoke(this, e);
        }

        /// <summary>
        /// 按下保存快捷键
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control && e.KeyCode == Keys.S)
            {
                // 触发保存方案事件
                OnShotKeySavePressed?.Invoke(this, EventArgs.Empty);
            }
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
            LogHelper.AddLog(MsgLevel.Info, $"相机设备（{e.Camera.DevName}）已成功移除！", true);
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
            try 
            {
                foreach (var item in Solution.Instance.CameraDevices)
                {
                    if (item.IsOpen)
                        item.SetTriggerMode(true);
                }
            } 
            catch(Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);    
            }
        }
    }
}
