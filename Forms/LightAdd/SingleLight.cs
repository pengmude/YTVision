﻿using Logger;
using Sunny.UI;
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
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light;

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

        public SingleLight(LightParam parms)
        {
            InitializeComponent();
            this.label1.Text =  parms.LightName;
            Parms = parms;

            try
            {
                //创建光源设备并添加到方案中
                if (Parms.Brand == LightBrand.PPX)
                    Light = new LightPPX(Parms);
                else
                    Light = new LightRsee(Parms);
                Solution.Instance.AddDevice(Light);
                //给光源绑定参数界面
                LightParamsShowControl = new LightParamsShowControl(Parms);
                // 保存所有的实例
                SingleLights.Add(this);
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
            foreach (var item in SingleLights)
            {
                item.tableLayoutPanel1.BackColor = SystemColors.Control;
                item.label1.BackColor = SystemColors.Control;
                IsSelected = false;
            }

            // 设置当前选中的样式
            this.tableLayoutPanel1.BackColor = Color.Gray;
            this.label1.BackColor = Color.Gray;
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
                    //为了防止在给uiSwitch1.Active赋值时事件循环触发，要先取消订阅
                    this.uiSwitch1.ValueChanged -= new UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
                    MessageBox.Show(e.Message);
                    LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                    uiSwitch1.Active = false;
                    this.uiSwitch1.ValueChanged += new UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
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
                    //为了防止在给uiSwitch1.Active赋值时事件循环触发，要先取消订阅
                    this.uiSwitch1.ValueChanged -= new UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
                    MessageBox.Show(e.Message);
                    LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                    uiSwitch1.Active = true;
                    this.uiSwitch1.ValueChanged += new UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
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
            foreach (var node in Solution.Nodes)
            {
                if (node is NodeLight lightNode
                    && lightNode.ParamForm.Params is NodeParamLight paramLight
                    && Light.UserDefinedName == paramLight.Light.UserDefinedName)
                {
                    MessageBox.Show("当前方案的节点正在使用该光源，无法删除光源！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            SingleLights.Remove(this);
            SingleLightRemoveEvent?.Invoke(this, this);
        }
    }
}
