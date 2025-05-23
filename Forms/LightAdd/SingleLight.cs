﻿using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Device;
using YTVisionPro.Device.Light;
using YTVisionPro.Node._5_EquipmentCommunication.LightOpen;

namespace YTVisionPro.Forms.LightAdd
{
    /// <summary>
    /// 单个光源控件
    /// </summary>
    internal partial class SingleLight : UserControl
    {
        /// <summary>
        /// 光源对象
        /// </summary>
        public ILight Light;
        /// <summary>
        /// 光源参数
        /// </summary>
        public LightParam Parms = new LightParam();
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected = false;
        /// <summary>
        /// 光源名称
        /// </summary>
        public string LightName { get => label1.Text; set => label1.Text = value; }
        /// <summary>
        /// 当前类实例选中改变事件
        /// </summary>
        public static event EventHandler<SingleLight> SelectedChange;
        /// <summary>
        /// 移除当前实例
        /// </summary>
        public static event EventHandler<SingleLight> SingleLightRemoveEvent;
        /// <summary>
        /// 光源参数显示控件
        /// </summary>
        public LightParamsShowControl LightParamsShowControl;
        /// <summary>
        /// 保存所有的当前类实例
        /// </summary>
        public static List<SingleLight> SingleLights = new List<SingleLight>();

        /// <summary>
        /// 反序列化用
        /// </summary>
        /// <param name="light"></param>
        public SingleLight(ILight light)
        {
            InitializeComponent();
            if (light.IsOpen)
            {
                try
                {
                    light.Connenct();
                    light.ConnectStatusEvent += Light_ConnectStatusEvent;
                    light.TurnOn(light.Brightness);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"光源（{light.UserDefinedName}）打开失败，请检查光源状态！原因：{ex.Message}", true);
                }
            }
            this.label1.Text = light.UserDefinedName;
            Light = light;
            Parms = light.LightParam;
            Solution.Instance.AllDevices.Add(Light);
            //给光源绑定参数界面
            LightParamsShowControl = new LightParamsShowControl(Parms);
            // 保存所有的实例
            SingleLights.Add(this);
        }

        private void Light_ConnectStatusEvent(object sender, bool e)
        {
            uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
            uiSwitch1.Active = e;
            uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
        }

        public SingleLight(LightParam parms)
        {
            InitializeComponent();
            this.label1.Text =  parms.LightName;
            Parms = parms;

            try
            {
                //创建光源设备并添加到方案中
                if (Parms.Brand == DeviceBrand.PPX)
                    Light = new LightPPX(Parms);
                else if (Parms.Brand == DeviceBrand.Rsee)
                    Light = new LightRsee(Parms);
                else
                    throw new Exception("暂时不支持的光源品牌！");
                Solution.Instance.AllDevices.Add(Light);
                //给光源绑定参数界面
                LightParamsShowControl = new LightParamsShowControl(Parms);
                // 保存所有的实例
                SingleLights.Add(this);
            }
            catch (Exception ex)
            {
                throw ex;
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
            foreach (var item in SingleLights)
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
        /// 连接光源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if (value)
            {
                try
                {
                    Light.TurnOn(255);
                    LogHelper.AddLog(MsgLevel.Info, $"{Parms.LightName}(串口号：{Parms.Port}，通道：{Parms.Channel})已打开！", true);
                }
                catch (Exception e)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Parms.LightName}(串口号：{Parms.Port}，通道：{Parms.Channel})打开失败！", true);
                }
            }
            else
            {
                try
                {
                    Light.TurnOff();
                    LogHelper.AddLog(MsgLevel.Info, $"{Parms.LightName}(串口号：{Parms.Port}，通道：{Parms.Channel})已关闭！", true);
                }
                catch (Exception e)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"{Parms.LightName}(串口号：{Parms.Port}，通道：{Parms.Channel})无法关闭！", true);
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
                if (node is NodeLight lightNode
                    && lightNode.ParamForm.Params is NodeParamLight paramLight
                    && Light.UserDefinedName == paramLight.Light.UserDefinedName)
                {
                    MessageBox.Show("当前方案的节点正在使用该光源，无法删除光源！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if(IsSelected)
                SingleLightRemoveEvent?.Invoke(this, this);
        }
    }
}
