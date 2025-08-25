using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._3_Detection.TDAI;
using TDJS_Vision.Node._5_EquipmentCommunication.ModbusRead;
using TDJS_Vision.Node._5_EquipmentCommunication.PlcRead;

namespace TDJS_Vision.Node._6_LogicTool.ProcessTrigger
{
    public partial class NodeParamFormProcessTrigger : FormBase, INodeParamForm
    {
        Process _process;
        public NodeParamFormProcessTrigger(Process process)
        {
            InitializeComponent();
            Shown += NodeParamFormProcessTrigger_Shown;
            _process = process;
        }

        private void NodeParamFormProcessTrigger_Shown(object sender, EventArgs e)
        {
            #region 流程下拉框填充

            string textOk = comboBoxOKProcess.Text;
            string textNg = comboBoxNGProcess.Text;
            string text = comboBoxProcess.Text;

            comboBoxOKProcess.Items.Clear();
            comboBoxNGProcess.Items.Clear();
            comboBoxProcess.Items.Clear();
            foreach (var pro in Solution.Instance.AllProcesses)
            {
                if (pro.Group == _process.Group && pro.ProcessName != _process.ProcessName)
                {
                    comboBoxOKProcess.Items.Add(pro.ProcessName);
                    comboBoxNGProcess.Items.Add(pro.ProcessName);
                    comboBoxProcess.Items.Add(pro.ProcessName);
                }
            }
            // 选中OK流程
            if (comboBoxOKProcess.Items.Contains(textOk))
                comboBoxOKProcess.SelectedItem = textOk;
            else
                comboBoxOKProcess.SelectedIndex = -1;

            // 选中OK流程
            if (comboBoxNGProcess.Items.Contains(textNg))
                comboBoxNGProcess.SelectedItem = textNg;
            else
                comboBoxNGProcess.SelectedIndex = -1;

            // 选中直接流程
            if (comboBoxProcess.Items.Contains(text))
                comboBoxProcess.SelectedItem = text;
            else
                comboBoxProcess.SelectedIndex = -1;

            #endregion
        }

        public INodeParam Params { get; set; }

        /// <summary>
        /// 获取订阅Bool结果
        /// </summary>
        /// <returns></returns>
        public bool GetBoolValue()
        {
            try
            {
                switch (nodeSubscription1.GetNodeType()) 
                {
                    case NodeType.PLCRead:

                        #region 解析出PLC读取节点结果的bool值

                        var res = nodeSubscription1.GetValue<PlcReadResult>();
                        if(res.DataType == typeof(bool).Name)
                        {
                            return (bool)res.Data;
                        }
                        else if(res.DataType == typeof(bool[]).Name)
                        {
                            return ((bool[])res.Data)[0];
                        }
                        else if (res.DataType == typeof(int).Name)
                        {
                            return (int)res.Data != 0;
                        }
                        else if (res.DataType == typeof(int[]).Name)
                        {
                            return ((int[])res.Data)[0] != 0;
                        }
                        else
                            throw new Exception($"PLCRead结果数据类型不支持！当前数据类型：{res.DataType}");

                        #endregion

                    case NodeType.ModbusRead:

                        #region 解析出Modbus读取节点结果的bool值

                        var res2 = nodeSubscription1.GetValue<ModbusReadResult>();
                        if (res2.DataType == typeof(bool[]).Name)
                        {
                            return ((bool[])res2.Data)[0];
                        }
                        else if (res2.DataType == typeof(ushort[]).Name)
                        {
                            return ((ushort[])res2.Data)[0] != 0u;
                        }
                        else
                            throw new Exception($"ModbusRead结果数据类型不支持！当前数据类型：{res2.DataType}");

                        #endregion

                    case NodeType.AITD:

                        #region 解析出TDAI节点结果的bool值

                        var resAi = nodeSubscription1.GetValue<AlgorithmResult>();

                        // AI检测结果总OK/NG
                        if (resAi.DetectResults.All(kvp => kvp.Value.All(result => result.IsOk)))
                            return true;
                        else
                            return false;
                        #endregion

                    default:
                        throw new Exception("尚不不支持该节点的结果解析成Bool值！");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NodeParamProcessTrigger nodeParamProcessTrigger = new NodeParamProcessTrigger();
                nodeParamProcessTrigger.UseOkOrNg = checkBox1.Checked;
                nodeParamProcessTrigger.Text1 = nodeSubscription1.GetText1();
                nodeParamProcessTrigger.Text2 = nodeSubscription1.GetText2();
                nodeParamProcessTrigger.OKProcessName = comboBoxOKProcess.Text;
                nodeParamProcessTrigger.NGProcessName = comboBoxNGProcess.Text;
                nodeParamProcessTrigger.ProcessName = comboBoxProcess.Text;
                Params = nodeParamProcessTrigger;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hide();
        }

        void INodeParamForm.SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        public void SetParam2Form()
        {
            try
            {
                if (Params is NodeParamProcessTrigger param)
                {
                    checkBox1.Checked = param.UseOkOrNg;
                    nodeSubscription1.SetText(param.Text1, param.Text2);
                    // 重新获取流程
                    comboBoxOKProcess.Items.Clear();
                    comboBoxNGProcess.Items.Clear();
                    comboBoxProcess.Items.Clear();
                    foreach (var proinfo in ConfigHelper.SolConfig.ProcessInfos)
                    {
                        if (proinfo.Group == _process.Group && proinfo.ProcessName != _process.ProcessName)
                        {
                            comboBoxOKProcess.Items.Add(proinfo.ProcessName);
                            comboBoxNGProcess.Items.Add(proinfo.ProcessName);
                            comboBoxProcess.Items.Add(proinfo.ProcessName);
                        }
                    }
                    comboBoxOKProcess.SelectedItem = param.OKProcessName;
                    comboBoxNGProcess.SelectedItem = param.NGProcessName;
                    comboBoxProcess.SelectedItem = param.ProcessName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 是否订阅条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                nodeSubscription1.Enabled = true;
                labelOK.Enabled = true;
                labelNG.Enabled = true;
                comboBoxOKProcess.Enabled = true;
                comboBoxNGProcess.Enabled = true;
                buttonReset1.Enabled = true;
                buttonReset2.Enabled = true;
            }
            else
            {
                nodeSubscription1.Enabled = false;
                labelOK.Enabled = false;
                labelNG.Enabled = false;
                comboBoxOKProcess.Enabled = false;
                comboBoxNGProcess.Enabled = false;
                buttonReset1.Enabled = false;
                buttonReset2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxOKProcess.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBoxNGProcess.SelectedIndex = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBoxProcess.SelectedIndex = -1;
        }
    }
}
