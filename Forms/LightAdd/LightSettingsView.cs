using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace YTVisionPro.Forms.LightAdd
{
    public struct SerialStructure
    {
        /// <summary>
        /// 串口号
        /// </summary>
        private string serialNumber;

        /// <summary>
        /// 波特率
        /// </summary>
        private Int32 baudrate;
        /// <summary>
        /// 数据位
        /// </summary>
        private int dataBits;
        /// <summary>
        /// 停止位
        /// </summary>
        private StopBits stopBits;
        /// <summary>
        /// 校验位
        /// </summary>
        private Parity parity;
        /// <summary>
        /// 通道值
        /// </summary>
        private byte channelValue;
        //光源名
        private string name;
        //品牌
        public string brand;

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public int Baudrate { get => baudrate; set => baudrate = value; }
        public int DataBits { get => dataBits; set => dataBits = value; }
        public StopBits StopBits { get => stopBits; set => stopBits = value; }
        public Parity Parity { get => parity; set => parity = value; }
        public byte ChannelValue { get => channelValue; set => channelValue = value; }
        public string Name { get => name; set => name = value; }
    }

    public partial class LightSettingsView : Form
    {
        public bool isSet = false;
        public SerialStructure serialStructure = new SerialStructure();
        public bool IsCancelled { get; private set; } = false;
        LightItem userControl1 = new LightItem();
        public event EventHandler<SerialStructure> Settings;

        public LightSettingsView()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.comboBox1.DataSource = new List<string>() { "磐鑫" };
            this.comboBox1.SelectedIndex = 0;

            this.comboBox3.DataSource = new List<string>() { "9600" };
            this.comboBox4.DataSource = new List<string>() { "8" };
            this.comboBox5.DataSource = new List<string>() { "1" };
            this.comboBox6.DataSource = new List<string>() { "N" };
            this.comboBox7.DataSource = new List<byte>() { 1,2,3,4 };

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;

            userControl1.NotificationSettings += UserControl1_NotificationSettings;
            SearchAnAddSerialToComboBox();
        }

        public LightSettingsView(LightItem userControl1)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.comboBox1.DataSource = new List<string>() { "磐鑫" };
            this.comboBox1.SelectedIndex = 0;

            this.comboBox3.DataSource = new List<string>() { "9600" };
            this.comboBox4.DataSource = new List<string>() { "8" };
            this.comboBox5.DataSource = new List<string>() { "1" };
            this.comboBox6.DataSource = new List<string>() { "N" };
            this.comboBox7.DataSource = new List<byte>() { 1, 2, 3, 4 };

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;

            this.userControl1 = userControl1;
            this.userControl1.NotificationSettings += UserControl1_NotificationSettings;
            SearchAnAddSerialToComboBox();
        }

        /// <summary>
        /// 触发设置界面，更新Form2UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl1_NotificationSettings(object sender, EventArgs e)
        {         
            userControl1 = (LightItem)sender;
            this.textBox2.Text = userControl1.serialStructure.Name;
            int i = 0;
            foreach (string item in comboBox2.Items)
            {
                if (item == userControl1.serialStructure.SerialNumber)
                {
                    this.comboBox2.SelectedIndex = i;
                    break;
                }
                i++;
            }
            this.comboBox3.Text = userControl1.serialStructure.Baudrate.ToString();
            this.comboBox4.Text = userControl1.serialStructure.DataBits.ToString();
            switch (userControl1.serialStructure.StopBits)
            {
                case StopBits.One:
                    this.comboBox5.Text = "1";
                    break;
            }
            switch (userControl1.serialStructure.Parity)
            {
                case Parity.None:
                    this.comboBox6.Text = "N";
                    break;
            }
            this.comboBox7.Text = userControl1.serialStructure.ChannelValue.ToString();
            isSet = true;
        }

        /// <summary>
        /// 搜索串口并添加到下拉框
        /// </summary>
        public void SearchAnAddSerialToComboBox()
        {
            // 获取当前系统所有有效的串行通信端口信息。
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");

            // 通过GetValueNames方法获取到SerialComm键中的所有值的名称。
            string[] sSubKeys = keyCom.GetValueNames();

            // 这行代码清除ComboBox控件port_cbb当前的所有项，准备填入新的串行端口列表。
            comboBox2.Items.Clear();

            /* 循环遍历刚才获取的所有值名，对于每个值名，使用GetValue方法从SerialComm键中读取相应的数据值，这里的数据值预期是一个表示端口名称的字符串						 （如"COM1"）。
             * 随后，这个端口名称被添加到port_cbb控件的项目列表中。
             */
            foreach (var sValue in sSubKeys)
            {
                string portName = (string)keyCom.GetValue(sValue);
                comboBox2.Items.Add(portName);
            }

            this.comboBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSet == false)
            {
                if (this.textBox2.Text != null && this.textBox2.Text != "")
                {
                    serialStructure.brand = this.comboBox1.Text;
                    serialStructure.Name = this.textBox2.Text;
                    serialStructure.SerialNumber = this.comboBox2.Text;
                    serialStructure.Baudrate = int.Parse(this.comboBox3.Text);
                    serialStructure.DataBits = int.Parse(this.comboBox4.Text);
                    switch (comboBox5.Text)
                    {
                        case "1":
                            serialStructure.StopBits = StopBits.One;
                            break;
                    }
                    switch (comboBox6.Text)
                    {
                        case "N":
                            serialStructure.Parity = Parity.None;
                            break;
                    }
                    serialStructure.ChannelValue = byte.Parse(comboBox7.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("请输入名称！！");
            }
            else
            {
                if (this.textBox2.Text != null && this.textBox2.Text != "")
                {
                    serialStructure.brand = this.comboBox1.Text;
                    serialStructure.Name = this.textBox2.Text;
                    serialStructure.SerialNumber = this.comboBox2.Text;
                    serialStructure.Baudrate = int.Parse(this.comboBox3.Text);
                    serialStructure.DataBits = int.Parse(this.comboBox4.Text);
                    switch (comboBox5.Text)
                    {
                        case "1":
                            serialStructure.StopBits = StopBits.One;
                            break;
                    }
                    switch (comboBox6.Text)
                    {
                        case "N":
                            serialStructure.Parity = Parity.None;
                            break;
                    }
                    serialStructure.ChannelValue = byte.Parse(comboBox7.Text);                   
                }
                else
                {
                    MessageBox.Show("请输入名称！！");
                    return;
                }                           
                //如果是设置作用，则触发设置事件
                Settings?.Invoke(this, serialStructure);
                this.Close();
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            IsCancelled = true;
            this.Close();
        }
    }
}
