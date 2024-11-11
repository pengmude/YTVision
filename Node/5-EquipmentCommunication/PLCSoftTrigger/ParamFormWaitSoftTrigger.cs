using Logger;
using Sunny.UI;
using System;
using System.Windows.Forms;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Node._5_EquipmentCommunication.PLCSoftTrigger
{
    internal partial class ParamFormWaitSoftTrigger : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }
        
        public ParamFormWaitSoftTrigger()
        {
            InitializeComponent();
            Shown += ParamFormWaitSoftTrigger_Shown;
            InitPLCComboBox();
        }

        private void ParamFormWaitSoftTrigger_Shown(object sender, EventArgs e)
        {
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
                MessageBox.Show("PLC不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                LogHelper.AddLog(MsgLevel.Exception, "信号地址为空", true);
                MessageBox.Show("信号地址为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


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

            NodeParamWaitSoftTrigger param = new NodeParamWaitSoftTrigger();
            param.Plc = plc;
            param.PlcName = comboBox1.Text;
            param.Address = this.textBox1.Text;
            Params = param;
            Hide();
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
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamWaitSoftTrigger param)
            {
                int index = comboBox1.Items.IndexOf(param.PlcName);
                comboBox1.SelectedIndex = index == -1 ? 0 : index;
                textBox1.Text = param.Address;
                foreach (var plc in Solution.Instance.PlcDevices)
                {
                    if(plc.UserDefinedName == param.PlcName)
                    {
                        param.Plc = plc;
                        break;
                    }
                }
            }
        }
    }
}
