using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;

namespace Test_light_controller
{
    public partial class Form1 : Form
    {
        public List<UserControl1> UserControls = new List<UserControl1>();
        public UserControl1 selectedUserControl;
        public static event EventHandler<int> ValueC;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;
            numericUpDown1.KeyDown += NumericUpDown1_KeyDown;
        }

        /// <summary>
        /// NumericUpDown1改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.trackBar1.Value = (int)this.numericUpDown1.Value;
        }

        /// <summary>
        /// NumericUpDown1确认触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //设置亮度
                selectedUserControl?.light.SetValue((int)this.numericUpDown1.Value);
                //更新数据与界面
                foreach (var item in UserControls)
                {
                    if (item == selectedUserControl)
                    {
                        item.light.Brightness = (int)this.numericUpDown1.Value;
                        ValueC?.Invoke(this, (int)this.numericUpDown1.Value);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 添加光源按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //打开Form2窗口进行设置
            Form2 form2 = new Form2();
            form2.FormClosed += Form2_FormClosed;
            form2.ShowDialog();
        }

        /// <summary>
        /// Form2关闭时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 form2 = sender as Form2;
            if (form2 != null && form2.IsCancelled)
            {
                return;
            }

            //foreach循环判断添加的用户控件是否有重复
            foreach (UserControl1 item in UserControls)
            {
                if (form2.serialStructure.SerialNumber == item.serialStructure.SerialNumber && form2.serialStructure.ChannelValue == item.serialStructure.ChannelValue)
                {
                    MessageBox.Show("重复！");
                    return;
                }
            }

            //创建用户控件
            UserControl1 myControl = new UserControl1(form2.serialStructure);
            myControl.Click += MyControl_Click;
            myControl.Serialportstatuschange += MyControl_Serialportstatuschange;
            myControl.light.PortName = form2.serialStructure.SerialNumber;
            myControl.Delect += MyControl_Delect;

            //foreach循环，如果添加的用户控件在列表中有相同的串口号，则代表添加的用户控件的_serialPort与列表的某个相同
            foreach (UserControl1 item in UserControls)
            {
                if (item.serialStructure.SerialNumber == myControl.serialStructure.SerialNumber)
                {
                    FieldInfo fieldInfo = typeof(LightPPX).GetField("_serialPort", BindingFlags.NonPublic | BindingFlags.Instance);
                    SerialPort value = (SerialPort)fieldInfo.GetValue(item.light);
                    fieldInfo.SetValue(myControl.light, value);
                    //如果列表的isSerialPortOpen为true，则添加的用户控件的isSerialPortOpen也为true
                    if (item.isSerialPortOpen == true)
                    {
                        myControl.isSerialPortOpen = true;
                        myControl.Text = "关闭串口";
                    }
                }
            }
            //添加的用户控件也添加到列表中
            UserControls.Add(myControl);

            this.panel1.Controls.Add(myControl);
            //设置添加的用户控件的位置
            panel1.Controls.SetChildIndex(myControl, 0); 
        }

        /// <summary>
        /// 删除用户控件时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyControl_Delect(object sender, EventArgs e)
        {
            UserControl1 userControl1 = (UserControl1)sender;
            //for循环遍历，删除特定的用户控件
            for (int i = 0; i < UserControls.Count; i++)
            {
                var item = UserControls[i];
                if (item.serialStructure.SerialNumber == userControl1.serialStructure.SerialNumber && item.serialStructure.ChannelValue == userControl1.serialStructure.ChannelValue)
                {
                    UserControls.RemoveAt(i);
                    panel1.Controls.Remove(userControl1);
                    break;
                }
            }
        }

        /// <summary>
        /// 选中用户控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyControl_Click(object sender, bool e)
        {
            this.selectedUserControl = (UserControl1)sender;
            //更新UI显示
            this.label8.Text = selectedUserControl.serialStructure.name;
            this.label4.Text = selectedUserControl.serialStructure.ChannelValue.ToString();
            //如果e为false表示该用户控件没有打开串口，不需要把isSerialPortOpen设置为true
            if (e == false)
            {
                return;
            }
            foreach (var item in UserControls)
            {
                if (this.selectedUserControl.serialStructure.SerialNumber == item.serialStructure.SerialNumber)
                {
                    item.isSerialPortOpen = true;
                }
            }
            UpdateMainFormUI();  //更新UI界面
            SetActiveControl(this.selectedUserControl); //改变控件颜色
            this.label8.Text = selectedUserControl.serialStructure.name;
        }

        /// <summary>
        /// 关闭串口时触发，设置UI初始值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyControl_Serialportstatuschange(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = 0;
            this.trackBar1.Value = 0;
            foreach (var item in UserControls)
            {
                if (this.selectedUserControl.serialStructure.SerialNumber == item.serialStructure.SerialNumber)
                {
                    item.isSerialPortOpen = false;
                }
            }
        }

        /// <summary>
        /// 改变控件颜色
        /// </summary>
        /// <param name="activeControl"></param>
        public void SetActiveControl(UserControl1 activeControl)
        {
            foreach (var control in UserControls)
            {
                if (control == activeControl)
                {
                    control.BackColor = Color.FromArgb(211, 211, 211);
                    //更新UI界面
                    UpdateMainFormUI(); 
                }
                else
                {
                    control.BackColor = Color.AliceBlue;
                }
            }
        }

        /// <summary>
        /// 更新UI界面
        /// </summary>
        private void UpdateMainFormUI()
        {
            if (selectedUserControl != null)
            {
                if (selectedUserControl.isSerialPortOpen)
                {
                    this.numericUpDown1.Value = selectedUserControl.light.Brightness;
                    this.trackBar1.Value = (int)this.numericUpDown1.Value;
                }
                else
                {
                    this.numericUpDown1.Value = 0;
                    this.trackBar1.Value = 0;
                }
                this.label4.Text = selectedUserControl.light.Sn;
            }
        }

        /// <summary>
        /// 滑块修改时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = (int)this.trackBar1.Value;
            selectedUserControl?.light.SetValue((int)this.trackBar1.Value);
            foreach (var item in UserControls)
            {
                if (item == selectedUserControl)
                {
                    item.light.Brightness = this.trackBar1.Value;
                    ValueC?.Invoke(this, this.trackBar1.Value);
                    return;
                }
            }
        }
    }
}
