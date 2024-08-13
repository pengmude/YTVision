using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using YTVisionPro;
using YTVisionPro.Hardware.Light;

namespace Test_light_controller
{
    public partial class UserControl1 : UserControl
    {
        public bool isSerialPortOpen = false;
        public LightPPX light = new LightPPX();
        public SerialStructure serialStructure = new SerialStructure();
        public event EventHandler<bool> Click;
        public event EventHandler Serialportstatuschange;
        public event EventHandler NotificationSettings;
        public event EventHandler Delect;
        public static Dictionary<string, UserControl1> openWith = new Dictionary<string, UserControl1>();

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                this.button1.Text = text;
            }
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(SerialStructure serialStructure)
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;

            this.label3.Text = serialStructure.ChannelValue.ToString();
            this.label4.Text = serialStructure.Name;
            this.serialStructure = serialStructure;
            this.light.Id = serialStructure.ChannelValue.ToString();
            this.MouseDown += UserControl1_Click;
            this.MouseDoubleClick += UserControl1_MouseDoubleClick;
            this.BackColor = Color.AliceBlue;
            this.label7.Text = serialStructure.SerialNumber.ToString();
            Form1.ValueC += Form1_ValueC;
        }

        /// <summary>
        /// 同步修改光源值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ValueC(object sender, int e)
        {
            Form1 form1 = (Form1)sender;
            if (form1.selectedUserControl == this)
            {
                this.label6.Text = e.ToString();
            }
        }

        /// <summary>
        /// 鼠标双击触发,打开设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (button1.Text == "关闭串口" || isSerialPortOpen == true)
            {
                MessageBox.Show("请关闭串口再修改");
                return;
            }
            Form2 form2 = new Form2(this);
            form2.Settings += Form2_Settings;
            //更新Form2UI
            NotificationSettings?.Invoke(this, e);
            form2.ShowDialog();
        }

        /// <summary>
        /// 设置(把Form2的设置更新到用户控件中)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Settings(object sender, SerialStructure e)
        {
            Form1 parentForm = (Form1)this.FindForm();
            foreach (var item in parentForm.UserControls)
            {
                if (item.serialStructure.SerialNumber == e.SerialNumber && item.serialStructure.ChannelValue == e.ChannelValue)
                {
                    MessageBox.Show("重复的！");
                    return;
                }
            }
            this.label3.Text = e.ChannelValue.ToString();
            this.label4.Text = e.Name;
            this.serialStructure = e;
            this.light.SerialStructure = e;
            this.light.Id = e.ChannelValue.ToString();
            this.label7.Text = e.SerialNumber.ToString();

            Click?.Invoke(this, isSerialPortOpen);   
        }

        /// <summary>
        /// 打开串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.IsSelected())
            {
                MessageBox.Show("请先选中控件");
                return;
            }
            if (isSerialPortOpen == false)  // 如果当前串口设备是关闭状态
            {
                OpenSerialPort();
            }
            else
            {
                CloseSerialPort();
            }          
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        private void OpenSerialPort()
        {
            try
            {
                light.Connenct(serialStructure.SerialNumber, serialStructure.Baudrate, serialStructure.DataBits, serialStructure.StopBits, serialStructure.Parity);
                light.Brightness = light.ReadValue();
                this.label6.Text = light.Brightness.ToString();
                isSerialPortOpen = true;

                Click?.Invoke(this, isSerialPortOpen);
                button1.Text = "关闭串口";
                Form1 parentForm = (Form1)this.FindForm();
                foreach (UserControl1 item in parentForm.UserControls)
                {
                    if (item.serialStructure.SerialNumber == this.serialStructure.SerialNumber)
                    {
                        item.button1.Text = "关闭串口";
                        item.isSerialPortOpen = true;
                        FieldInfo fieldInfo = typeof(LightPPX).GetField("isOpen", BindingFlags.NonPublic | BindingFlags.Instance);
                        fieldInfo.SetValue(item.light, true);
                    }
                }
                FieldInfo fieldInfo2 = typeof(LightPPX).GetField("isOpen", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo2.SetValue(light, true);
                if (openWith.ContainsKey(this.serialStructure.SerialNumber))
                    return;
                openWith.Add(this.serialStructure.SerialNumber,this);
            }
            catch
            {
                MessageBox.Show("打开串口失败，请检查串口", "错误");
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void CloseSerialPort()
        {
            try
            {
                if (this.ParentForm is Form1 form1)
                {
                    bool canClosePort = true;

                    string sp = this.serialStructure.SerialNumber.ToString();

                    if (this.serialStructure.SerialNumber == openWith[sp].serialStructure.SerialNumber)
                    {
                        Form1 parentForm = (Form1)this.FindForm();
                        foreach (UserControl1 item in parentForm.UserControls)
                        {
                            if (item.serialStructure.SerialNumber == this.serialStructure.SerialNumber)
                            {
                                item.button1.Text = "打开串口";
                                item.isSerialPortOpen = false;
                                FieldInfo fieldInfo = typeof(LightPPX).GetField("isOpen", BindingFlags.NonPublic | BindingFlags.Instance);
                                fieldInfo.SetValue(item.light, false);
                            }
                        }

                        light.Disconnect();
                        isSerialPortOpen = false;
                        Serialportstatuschange?.Invoke(this, EventArgs.Empty);
                        button1.Text = "打开串口";

                        // 从 openWith 中移除对应的串口记录
                        openWith.Remove(sp);
                    }
                }
            }
            catch
            {
                MessageBox.Show("关闭串口失败，请检查串口", "错误");
            }
        }

        /// <summary>
        /// 鼠标单击触发，实现选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl1_Click(object sender, EventArgs e)
        {
            Form1 parentForm = (Form1)this.FindForm();
            foreach (UserControl1 item in parentForm.UserControls)
            {

                if (item.serialStructure.SerialNumber == this.serialStructure.SerialNumber && item.serialStructure.ChannelValue != this.serialStructure.ChannelValue && item.light.IsOpen == true)
                {
                    light.Brightness = this.light.ReadValue();
                    this.label6.Text = light.Brightness.ToString();
                    Click?.Invoke(this, isSerialPortOpen);
                    parentForm.SetActiveControl(this);
                    return;
                }
                if (item.serialStructure.SerialNumber == this.serialStructure.SerialNumber && item.serialStructure.ChannelValue == this.serialStructure.ChannelValue && item.light.IsOpen == true)
                {
                    if (isSerialPortOpen == true)
                    {
                        light.Brightness = this.light.ReadValue();
                        this.label6.Text = light.Brightness.ToString();
                        Click?.Invoke(this, isSerialPortOpen);
                        return;
                    }
                }

            }
            Click?.Invoke(this, isSerialPortOpen);
            parentForm.SetActiveControl(this);
        }

        /// <summary>
        /// 判断是否选中该控件
        /// </summary>
        /// <returns></returns>
        public bool IsSelected()
        {
            //假如颜色为灰色代表选中该控件
            return this.BackColor == Color.FromArgb(211, 211, 211);
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button1.Text == "关闭串口" || isSerialPortOpen == true)
            {
                MessageBox.Show("请关闭串口再删除");
                return;
            }
            Delect?.Invoke(this,EventArgs.Empty);
            Solution.Instance.RemoveDevice(this.light);
            LogHelper.AddLog(MsgLevel.Info, "删除了光源：  "+this.light.DevName + "   串口号：" + this.light.SerialStructure.SerialNumber + "   通道数" + this.light.SerialStructure.SerialNumber, true);
            foreach (LightPPX item in Solution.Instance.Devices)
            {
                Console.WriteLine("删除一个光源后，现在存在的光源：" + "光源名:" + item.DevName + "串口号：" + item.SerialStructure.SerialNumber + "通道数" + item.SerialStructure.SerialNumber);
            }
        }

        /// <summary>
        /// 关闭光源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            light.TurnOff();
            light.Brightness = 0;
            Click?.Invoke(this, isSerialPortOpen);
        }
    }
}
