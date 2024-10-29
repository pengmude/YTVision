using Basler.Pylon;
using Logger;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Camera;
using YTVisionPro.Node.ImageSrc.Shot;

namespace YTVisionPro.Forms.CameraAdd
{
    /// <summary>
    /// 单个相机控件
    /// </summary>
    internal partial class SingleCamera : UserControl
    {
        /// <summary>
        /// 相机对象
        /// </summary>
        public Device.Camera.ICamera Camera;
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected = false;
        /// <summary>
        /// 相机名称
        /// </summary>
        public string CameraName { get => label1.Text; set => label1.Text = value; }
        /// <summary>
        /// 当前类实例选中改变事件
        /// </summary>
        public static event EventHandler<SingleCamera> SelectedChange;
        /// <summary>
        /// 移除当前实例
        /// </summary>
        public static event EventHandler<SingleCamera> SingleCameraRemoveEvent;
        /// <summary>
        /// 相机参数显示控件
        /// </summary>
        public CameraParamsShowControl CameraParamsShowControl;
        /// <summary>
        /// 保存所有的当前类实例
        /// </summary>
        public static List<SingleCamera> SingleCameraList = new List<SingleCamera>();

        /// <summary>
        /// 反序列化用
        /// </summary>
        /// <param name="camera"></param>
        public SingleCamera(Device.Camera.ICamera camera)
        {
            InitializeComponent();
            Camera = camera;
            Camera.ConnectStatusEvent += Camera_ConnectStatusEvent;
            if (camera.IsOpen)
            {
                try
                {
                    Camera.Open();
                    Camera.SetTriggerMode(true);
                    Camera.StartGrabbing();
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"相机（{camera.UserDefinedName}）打开失败，请检查相机状态！原因：{ex.Message}", true);
                }
            }
            this.label1.Text = camera.UserDefinedName;
            Solution.Instance.AllDevices.Add(Camera);
            //绑定图像显示控件界面
            CameraParamsShowControl = new CameraParamsShowControl(Camera);
            // 保存所有的实例
            SingleCameraList.Add(this);
        }

        private void Camera_ConnectStatusEvent(object sender, bool e)
        {
            uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
            uiSwitch1.Active = e;
            uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
        }

        public SingleCamera(CameraParam parms)
        {
            InitializeComponent();

            this.label1.Text =  parms.UserDefinedName;

            try
            {
                Camera = new CameraHik(parms.DevInfo.cameraInfo, parms.UserDefinedName);
                Camera.ConnectStatusEvent += Camera_ConnectStatusEvent;
                Solution.Instance.AllDevices.Add(Camera);

                //绑定图像显示控件界面
                CameraParamsShowControl = new CameraParamsShowControl(Camera);
                // 保存所有的实例
                SingleCameraList.Add(this);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 点击选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePLCInfo_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelected();
            SelectedChange?.Invoke(this, this);
        }

        /// <summary>
        /// 设置控件选中状态
        /// </summary>
        /// <param name="flag"></param>
        private void SetSelected()
        {
            //先清除所有选中状态
            foreach (var item in SingleCameraList)
            {
                item.tableLayoutPanel1.BackColor = Color.LightSteelBlue;
                item.label1.BackColor = Color.LightSteelBlue;
                IsSelected = false;
            }
            
            // 设置当前选中的样式
            this.tableLayoutPanel1.BackColor = Color.CornflowerBlue;
            this.label1.BackColor = Color.CornflowerBlue;
            IsSelected = true;
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private async void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if (value)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        Camera.Open();
                        Camera.SetTriggerMode(false);
                        Camera.StartGrabbing();
                    });
                    LogHelper.AddLog(MsgLevel.Info, $"{Camera.UserDefinedName}已打开！", true);
                }
                catch (Exception e)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Camera.UserDefinedName}打开失败！原因：{e.Message}", true);
                }
            }
            else
            {
                try
                {
                    Camera.Close();
                    LogHelper.AddLog(MsgLevel.Info, $"{Camera.UserDefinedName}已关闭！", true);
                }
                catch (Exception e)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Camera.UserDefinedName}关闭失败！", true);
                }
            }
            
        }

        /// <summary>
        /// 右击移除当前实例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 移除设备需要判断当前是否有节点使用该设备
            foreach (var node in Solution.Instance.Nodes)
            {
                if (node is NodeShot cameraNode
                    && cameraNode.ParamForm.Params is NodeParamShot paramCamera
                    && Camera.UserDefinedName == paramCamera.Camera.UserDefinedName)
                {
                    MessageBox.Show("当前方案的节点正在使用该相机，无法删除相机！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (IsSelected)
                SingleCameraRemoveEvent?.Invoke(this, this);
        }
    }
}
