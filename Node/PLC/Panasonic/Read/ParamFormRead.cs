﻿using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro.Node.PLC.Panasonic.Read
{
    internal partial class ParamFormRead : Form, INodeParamForm
    {
        /// <summary>
        /// 参数改变事件，设置完参数后触发，给节点订阅
        /// </summary>
        public event EventHandler<INodeParam> OnNodeParamChange;

        public ParamFormRead()
        {
            InitializeComponent();
            InitPLCComboBox();
        }

        public void SetNodeBelong(NodeBase node) { }

        /// <summary>
        /// 初始化PLC下拉框
        /// </summary>
        private void InitPLCComboBox()
        {
            string text1 = comboBox1.Text;

            comboBox1.Items.Clear();
            comboBox1.Items.Add("[未设置]");
            // 初始化PLC列表,只显示添加的PLC用户自定义名称
            foreach (var plc in Solution.Instance.PlcDevices)
            {
                comboBox1.Items.Add(plc.UserDefinedName);
            }
            int index1 = comboBox1.Items.IndexOf(text1);
            if (index1 == -1)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = index1;

            string text2 = comboBox2.Text;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("int");
            comboBox2.Items.Add("bool");
            comboBox2.Items.Add("string");
            int index2 = comboBox2.Items.IndexOf(text2);
            if (index2 == -1)
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.SelectedIndex = index2;
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.IsNullOrEmpty() || comboBox1.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "PLC不能为空！", true);
                MessageBox.Show("PLC不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "信号地址为空", true);
                MessageBox.Show("信号地址为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(this.comboBox2.Text == "string" && string.IsNullOrEmpty(this.textBox3.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "选择string类型未配置读取长度", true);
                MessageBox.Show("选择string类型未配置读取长度", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //查找当前选择的PLC
                IPlc plc = null;
                foreach (var plcTmp in Solution.Instance.PlcDevices)
                {
                    if(plcTmp.UserDefinedName == comboBox1.Text)
                    {
                        plc = plcTmp;
                        break;
                    }
                }

                //把设置好的参数传给PLC节点NodeRead去更新结果
                NodeParamRead nodeParamRead = new NodeParamRead();
                nodeParamRead.Plc = plc;
                nodeParamRead.Address = this.textBox1.Text;
                switch (this.comboBox2.Text)
                {
                    case "int":
                        nodeParamRead.DataType = DataType.INT;
                        break;
                    case "bool":
                        nodeParamRead.DataType = DataType.BOOL;
                        break;
                    case "string":
                        nodeParamRead.DataType = DataType.STRING;
                        break;
                    default:
                        break;
                }
                nodeParamRead.Length = ushort.Parse(this.textBox3.Text);
                OnNodeParamChange?.Invoke(this, nodeParamRead);
                Close();
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
                MessageBox.Show("保存失败,请检查参数是否有误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormRead_Shown(object sender, EventArgs e)
        {
            InitPLCComboBox();
        }
    }
}
