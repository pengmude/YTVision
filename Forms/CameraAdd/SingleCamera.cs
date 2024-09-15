﻿using Basler.Pylon;
using Logger;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node.Camera.HiK;
using YTVisionPro.Node.Light;

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
        public Hardware.Camera.ICamera Camera;
        /// <summary>
        /// 相机参数
        /// </summary>
        public CameraParam Params;
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

        public SingleCamera(CameraParam parms)
        {
            InitializeComponent();

            this.label1.Text =  parms.UserDefineName;
            Params = parms;

            try
            {
                //创建相机设备并添加到方案中
                if (parms.Brand == CameraBrand.HiKVision)
                {
                    Camera = new CameraHik(parms.DevInfo.Hik, parms.UserDefineName);
                }
                else if (parms.Brand == CameraBrand.Basler)
                {
                    Camera = new CameraBasler(parms.DevInfo.Basler, parms.UserDefineName);
                }
                Solution.Instance.AddDevice(Camera);

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
                    LogHelper.AddLog(MsgLevel.Info, $"{Params.UserDefineName}已打开！", true);
                }
                catch (Exception e)
                {
                    //为了防止在给uiSwitch1.Active赋值时事件循环触发，要先取消订阅
                    this.uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
                    MessageBox.Show(e.Message);
                    LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                    uiSwitch1.Active = false;
                    this.uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Params.UserDefineName}打开失败！", true);
                }
            }
            else
            {
                try
                {
                    Camera.Close();
                    LogHelper.AddLog(MsgLevel.Info, $"{Params.UserDefineName}已关闭！", true);
                }
                catch (Exception e)
                {
                    //为了防止在给uiSwitch1.Active赋值时事件循环触发，要先取消订阅
                    this.uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
                    MessageBox.Show(e.Message);
                    LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                    uiSwitch1.Active = true;
                    this.uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Params.UserDefineName}关闭失败！", true);
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
            foreach (var node in Solution.Nodes)
            {
                if (node is NodeCamera cameraNode
                    && cameraNode.ParamForm.Params is NodeParamCamera paramCamera
                    && Camera.UserDefinedName == paramCamera.Camera.UserDefinedName)
                {
                    MessageBox.Show("当前方案的节点正在使用该相机，无法删除相机！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            SingleCameraList.Remove(this);
            SingleCameraRemoveEvent?.Invoke(this, this);
        }
    }
}
