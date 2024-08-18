﻿using Logger;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Forms.LightAdd
{
    public partial class FrmLightNew : Form
    {
        /// <summary>
        /// 光源添加事件
        /// </summary>
        public event EventHandler<LightParam> LightAddEvent;

        public FrmLightNew()
        {
            InitializeComponent();
            InitPortComboBox();
        }

        /// <summary>
        /// 搜索串口并添加到下拉框
        /// </summary>
        public void InitPortComboBox()
        {
            // 串口号获取
            comboBox2.Items.Clear();
            foreach (var com in SerialPort.GetPortNames())
            {
                comboBox2.Items.Add(com);
            }
            if (comboBox2.Items.Count > 0)
                this.comboBox2.SelectedIndex = 0;

            // 波特率
            comboBox3.SelectedIndex = 1;
            // 数据位
            comboBox4.SelectedIndex = 3;
            // 停止位
            comboBox5.SelectedIndex = 0;
            // 校验位
            comboBox6.SelectedIndex = 0;
            // 品牌
            comboBox1.SelectedIndex = 0;
            // 通信方式
            comboBox8.SelectedIndex = 0;
            // 光源通道
            comboBox7.SelectedIndex = 0;

        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox2.Text))
            {
                LightParam lightParam = new LightParam();
                try
                {
                    lightParam.Brand = this.comboBox1.Text == "磐鑫" ? LightBrand.PPX : this.comboBox1.Text == "锐视" ? LightBrand.RSEE : LightBrand.UNKNOW;
                    lightParam.Channel = byte.Parse(comboBox7.Text);
                    lightParam.LightName = this.textBox2.Text;

                    lightParam.Port = this.comboBox2.Text;
                    lightParam.BaudRate = int.Parse(this.comboBox3.Text);
                    lightParam.DataBits = int.Parse(this.comboBox4.Text);
                    lightParam.StopBits = comboBox5.Text == "1" ? StopBits.One : StopBits.Two;
                    lightParam.Parity = comboBox6.Text == "奇" ? Parity.Odd : comboBox6.Text == "偶" ? Parity.Even : Parity.None;

                    // 已添加设备冲突判断
                    foreach (var light in Solution.Instance.LightDevices)
                    {
                        if (light.LightParam.Port == lightParam.Port && light.LightParam.Channel == lightParam.Channel)
                        {
                            MessageBox.Show("对应串口和通道的光源已存在！");
                            LogHelper.AddLog(MsgLevel.Info, "对应串口和通道的光源已存在！", true);
                            return;
                        }
                        if(light.UserDefinedName == lightParam.LightName)
                        {
                            MessageBox.Show("该光源名称已存在！");
                            LogHelper.AddLog(MsgLevel.Info, "该光源名称已存在！", true);
                            return;
                        }
                    }

                    LightAddEvent?.Invoke(this, lightParam);
                    this.Close();
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Warn, "添加光源时参数设置错误！\n" + ex, true);
                    MessageBox.Show("请检查参数是否有误！");
                }
            }
            else
            {
                MessageBox.Show("请添加光源名称！");
            }
        }
    }
}