using Logger;
using System;
using System.IO.Ports;
using System.Windows.Forms;
using TDJS_Vision.Device;
using TDJS_Vision.Device.Light;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.LightAdd
{
    public partial class FrmLightNew : FormBase
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
            comboBoxBrand.SelectedIndex = 0;
            // 通信方式
            comboBoxConnectType.SelectedIndex = 0;
            // 光源通道
            comboBoxChannel.SelectedIndex = 0;

            // 锐视光源种类
            comboBoxRseeType.SelectedIndex = 0;

        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBoxLightName.Text))
            {
                LightParam lightParam = new LightParam();
                try
                {
                    lightParam.Brand = this.comboBoxBrand.Text == "磐鑫" ? DeviceBrand.PPX : this.comboBoxBrand.Text == "锐视" ? DeviceBrand.Rsee: DeviceBrand.Unknow;
                    lightParam.RseeDeviceType = comboBoxRseeType.SelectedIndex == 0 ? RseeDeviceType.PM_D : RseeDeviceType.P_MDPS_24W75;
                    lightParam.Channel = byte.Parse(comboBoxChannel.Text);
                    lightParam.LightName = this.textBoxLightName.Text;
                    lightParam.Value = 255;

                    lightParam.Port = this.comboBox2.Text;
                    lightParam.BaudRate = int.Parse(this.comboBox3.Text);
                    lightParam.DataBits = int.Parse(this.comboBox4.Text);
                    lightParam.StopBits = comboBox5.Text == "1" ? StopBits.One : comboBox5.Text == "1.5" ? StopBits.OnePointFive : StopBits.Two;
                    lightParam.Parity = comboBox6.Text == "奇" ? Parity.Odd : comboBox6.Text == "偶" ? Parity.Even : Parity.None;

                    // 已添加设备冲突判断
                    foreach (var light in Solution.Instance.LightDevices)
                    {
                        if (light.LightParam.Port == lightParam.Port && light.LightParam.Channel == lightParam.Channel)
                        {
                            MessageBoxTD.Show("对应串口和通道的光源已存在！");
                            LogHelper.AddLog(MsgLevel.Info, "对应串口和通道的光源已存在！", true);
                            return;
                        }
                        if(light.UserDefinedName == lightParam.LightName)
                        {
                            MessageBoxTD.Show("该光源名称已存在！");
                            LogHelper.AddLog(MsgLevel.Info, "该光源名称已存在！", true);
                            return;
                        }
                    }

                    LightAddEvent?.Invoke(this, lightParam);
                    this.Hide();
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Warn, "添加光源时参数设置错误！\n" + ex, true);
                    MessageBoxTD.Show("请检查参数是否有误！");
                }
            }
            else
            {
                MessageBoxTD.Show("无效的光源名称！");
            }
        }

        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrand.SelectedIndex == 0)
            {
                comboBoxRseeType.Enabled = false;
                textBoxLightName.Text = "磐鑫光源";
            }
            else
            {
                comboBoxRseeType.Enabled = true;
                textBoxLightName.Text = "锐视光源";
            }

        }
    }
}
