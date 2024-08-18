﻿using Logger;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Forms.PLCAdd
{
    public partial class FrmPLCNew : Form
    {
        /// <summary>
        /// PLC添加事件
        /// </summary>
        public event EventHandler<PLCParms> PLCAddEvent;

        public FrmPLCNew()
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
                PLCParms pLCParms = new PLCParms();

                try
                {
                    if (comboBox1.Text == "松下" && comboBox8.Text == "串口")
                    {
                        pLCParms.EthernetParms = new EthernetParms();

                        pLCParms.UserDefinedName = textBox2.Text;
                        pLCParms.PlcConType = PlcConType.COM;

                        pLCParms.SerialParms.PortName = comboBox2.Text;
                        pLCParms.SerialParms.BaudRate = int.Parse(comboBox3.Text);
                        pLCParms.SerialParms.DataBits = int.Parse(comboBox4.Text);
                        pLCParms.SerialParms.StopBits = comboBox5.SelectedIndex == 0 ? StopBits.One : (comboBox5.SelectedIndex == 1 ? StopBits.OnePointFive : StopBits.Two);
                        pLCParms.SerialParms.Parity = comboBox6.SelectedIndex == 0 ? Parity.None : (comboBox6.SelectedIndex == 1 ? Parity.Odd : Parity.Even);

                        // 已添加设备冲突判断
                        foreach (var plc in Solution.Instance.PlcDevices)
                        {
                            try
                            {
                                if (plc.PLCParms.SerialParms.PortName == comboBox2.Text)
                                {
                                    MessageBox.Show("对应串口的PLC已存在！");
                                    LogHelper.AddLog(MsgLevel.Info, "对应串口的PLC已存在！", true);
                                    return;
                                }
                                if (plc.UserDefinedName == pLCParms.UserDefinedName)
                                {
                                    MessageBox.Show("该光源名称已存在！");
                                    LogHelper.AddLog(MsgLevel.Info, "该光源名称已存在！", true);
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }

                        PLCAddEvent?.Invoke(this, pLCParms);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Warn, "添加PLC时参数设置错误！\n" + ex, true);
                    MessageBox.Show("请检查参数是否有误！");
                }
            }
            else
            {
                MessageBox.Show("请添加PLC设备名称！");
            }
        }
    }
}