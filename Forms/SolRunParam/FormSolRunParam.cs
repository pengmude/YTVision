using System;
using System.IO.Ports;
using System.Windows.Forms;
using Logger;
using TDJS_Vision.Device;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.SolRunParam
{
    public partial class FormSolRunParam : FormBase
    {
        private IDevice curDevice = null;
        public FormSolRunParam()
        {
            InitializeComponent();
            comboBoxCoilValue.SelectedIndex = 1;
        }
        /// <summary>
        /// 窗体显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSolRunParam_Shown(object sender, System.EventArgs e)
        {
            #region 创建选项卡

            RemoveAllExceptSpecifiedPage(tabControl1, "Modbus通信");//保留“Modbus通信”选项卡,删除所有
            foreach (var process in Solution.Instance.AllProcesses)
            {
                

                SolRunParamControl solRunParam = new SolRunParamControl();
                TabPage page = new TabPage();
                page.Text = process.ProcessName;
                page.Controls.Add(solRunParam);
                tabControl1.Controls.Add(page);
                try
                {
                    solRunParam.Init(process);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Fatal, $"“{process.ProcessName}”加载设置参数失败！原因：{ex.Message}", true);
                }
            }

            #endregion

            #region 显示Modbus通信参数

            if(Solution.Instance.ModbusDevices.Count > 0)
            {
                foreach (var modbus in Solution.Instance.ModbusDevices)
                {
                    switch (modbus.DevType)
                    {
                        case DevType.ModbusRTUPoll:
                            curDevice = modbus;
                            ShowModbusRTUParams();
                            break;
                        case DevType.ModbusTcpPoll:
                            curDevice = modbus;
                            ShowModbusTCPParams();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                // 当没有modbus通信设备时，隐藏Modbus通信参数面板
                tableLayoutPanel2.Visible = false; 
            }

            #endregion
        }
        /// <summary>
        /// 显示modbusRTU参数
        /// </summary>
        private void ShowModbusRTUParams()
        {
            // 隐藏TCP的参数控件
            tableLayoutPanelTCP.Visible = false;
            tableLayoutPanelRTU.Visible = true;
            tableLayoutPanelRTU.Dock = DockStyle.Fill;
            tableLayoutPanel2.Visible = true;

            // 串口号获取
            comboBoxCom.Items.Clear();
            foreach (var com in SerialPort.GetPortNames())
            {
                comboBoxCom.Items.Add(com);
            }
            var dev = curDevice as ModbusRTUPoll;

            int index1 = comboBoxCom.Items.IndexOf(((ModbusRTUParam)dev.ModbusParam).PortName);
            comboBoxCom.SelectedIndex = index1 == -1 ? -1 : index1;

            // 波特率
            int index2 = comboBoxBaute.Items.IndexOf(((ModbusRTUParam)dev.ModbusParam).BaudRate.ToString());
            comboBoxBaute.SelectedIndex = index2 == -1 ? -1 : index2;

            // 数据位
            int index3 = comboBoxDataBit.Items.IndexOf(((ModbusRTUParam)dev.ModbusParam).DataBits.ToString());
            comboBoxDataBit.SelectedIndex = index3 == -1 ? -1 : index3;

            // 停止位
            int index4 = comboBoxStopBit.Items.IndexOf(((ModbusRTUParam)dev.ModbusParam).StopBits == StopBits.One ? "1"
                : ((ModbusRTUParam)dev.ModbusParam).StopBits == StopBits.OnePointFive? "1.5" : "2");
            comboBoxStopBit.SelectedIndex = index4 == -1 ? -1 : index4;

            // 校验位
            int index5 = comboBoxParity.Items.IndexOf(((ModbusRTUParam)dev.ModbusParam).Parity == Parity.Odd ? "奇"
               : ((ModbusRTUParam)dev.ModbusParam).Parity == Parity.Even ? "偶": "无");
            comboBoxParity.SelectedIndex = index5 == -1 ? -1 : index5;
        }
        /// <summary>
        /// 显示Modbus TCP参数
        /// </summary>
        private void ShowModbusTCPParams()
        {
            // 隐藏TCP的参数控件
            tableLayoutPanelTCP.Visible = true;
            tableLayoutPanelTCP.Dock = DockStyle.Fill;
            tableLayoutPanelRTU.Visible = false;
            tableLayoutPanel2.Visible = true;

            var dev = curDevice as ModbusTcpPoll;
            var param = dev.ModbusParam as ModbusTcpParam;
            uiipTextBoxIP.Text = param.IP;
            textBoxPort.Text = param.Port.ToString();
            textBoxHoldTime.Text = param.HoldTime.ToString();
        }
        /// <summary>
        /// 除了保留指定的选项卡外，删除所有选项卡
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="pageNameToKeep"></param>
        private void RemoveAllExceptSpecifiedPage(TabControl tabControl, string pageNameToKeep)
        {
            // 由于在遍历集合时不能直接修改该集合（如添加或移除项），我们需要使用逆序遍历或者创建一个新的列表来存储需要移除的项。
            // 这里我们选择直接在循环中操作，但使用了倒序遍历以避免索引问题。
            for (int i = tabControl.TabPages.Count - 1; i >= 0; i--)
            {
                if (tabControl.TabPages[i].Text != pageNameToKeep)
                {
                    tabControl.TabPages.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// 连接Modbus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Solution.Instance.ModbusDevices.Count == 0)
            {
                MessageBoxTD.Show("设备尚未添加！");
                return;
            }
            if (string.IsNullOrEmpty(uiipTextBoxIP.Text) || string.IsNullOrEmpty(textBoxPort.Text))
            {
                MessageBoxTD.Show("参数不能为空！");
                return;
            }
            try
            {
                // 连接Modbus
                switch (curDevice.DevType)
                {
                    case DevType.ModbusRTUPoll:
                        var rtuPoll = curDevice as ModbusRTUPoll;
                        var paramRtuPoll = rtuPoll.ModbusParam as ModbusRTUParam;
                        rtuPoll.Disconnect();
                        paramRtuPoll.PortName = comboBoxCom.Text;
                        paramRtuPoll.BaudRate = int.Parse(comboBoxBaute.Text);
                        paramRtuPoll.DataBits = int.Parse(comboBoxDataBit.Text);
                        paramRtuPoll.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBoxStopBit.Text);
                        paramRtuPoll.Parity = comboBoxParity.Text == "奇" ? Parity.Odd : comboBoxParity.Text == "偶" ? Parity.Even : Parity.None;

                        rtuPoll.Connect();
                        curDevice = rtuPoll;
                        labelConnectInfo1.Text = $"设备连接成功连接！";
                        labelConnectInfo1.ForeColor = System.Drawing.Color.Green;
                        break;
                    case DevType.ModbusTcpPoll:
                        var tcpPoll = curDevice as ModbusTcpPoll;
                        var paramTcpPoll = tcpPoll.ModbusParam as ModbusTcpParam;
                        tcpPoll.Disconnect();
                        paramTcpPoll.IP = uiipTextBoxIP.Text;
                        paramTcpPoll.Port = int.Parse(textBoxPort.Text);
                        paramTcpPoll.HoldTime = ushort.Parse(textBoxHoldTime.Text);

                        tcpPoll.Connect();
                        curDevice = tcpPoll;
                        labelConnectInfo2.Text = $"设备连接成功连接！";
                        labelConnectInfo2.ForeColor = System.Drawing.Color.Green;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                if(curDevice.DevType == DevType.ModbusRTUPoll)
                {
                    labelConnectInfo1.Text = $"设备连接失败！原因：{ex.Message}";
                    labelConnectInfo1.ForeColor = System.Drawing.Color.Red;
                }
                else if (curDevice.DevType == DevType.ModbusTcpPoll)
                {
                    labelConnectInfo2.Text = $"设备连接失败！原因：{ex.Message}";
                    labelConnectInfo2.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
        }
        /// <summary>
        /// 测试读取线圈值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (Solution.Instance.ModbusDevices.Count == 0)
            {
                MessageBoxTD.Show("设备尚未添加！");
                return;
            }
            try
            {

                switch (curDevice.DevType)
                {
                    case DevType.ModbusRTUPoll:
                        var rtuPoll = curDevice as ModbusRTUPoll;
                        if (rtuPoll == null || !rtuPoll.IsConnect) throw new Exception("Modbus设备已失效或未打开");
                        var valueRtu = rtuPoll.ReadCoils(ushort.Parse(textBoxAddress.Text), 1);
                        labelTestInfo.Text = $"读取线圈值成功！值为：{valueRtu[0]}";
                        labelTestInfo.ForeColor = System.Drawing.Color.Green;
                        break;
                    case DevType.ModbusTcpPoll:
                        var tcpPoll = curDevice as ModbusTcpPoll;
                        if (tcpPoll == null || !tcpPoll.IsConnect) throw new Exception("Modbus设备已失效或未打开");
                        var valueTcp = tcpPoll.ReadCoils(ushort.Parse(textBoxAddress.Text), 1);
                        labelTestInfo.Text = $"读取线圈值成功！值为：{valueTcp[0]}";
                        labelTestInfo.ForeColor = System.Drawing.Color.Green;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                labelTestInfo.Text = $"读取线圈值失败！原因：{ex.Message}";
                labelTestInfo.ForeColor = System.Drawing.Color.Red;
            }
        }
        /// <summary>
        /// 测试写入线圈值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWrite_Click(object sender, EventArgs e)
        {
            if (Solution.Instance.ModbusDevices.Count == 0)
            {
                MessageBoxTD.Show("设备尚未添加！");
                return;
            }
            try
            {
                switch (curDevice.DevType)
                {
                    case DevType.ModbusRTUPoll:
                        var rtuPoll = curDevice as ModbusRTUPoll;
                        if (rtuPoll == null || !rtuPoll.IsConnect) throw new Exception("Modbus设备已失效或未打开");
                        rtuPoll.WriteSingleCoil(ushort.Parse(textBoxAddress.Text), comboBoxCoilValue.Text == "1");
                        labelTestInfo.Text = $"写入线圈值成功！";
                        labelTestInfo.ForeColor = System.Drawing.Color.Green;
                        break;
                    case DevType.ModbusTcpPoll:
                        var tcpPoll = curDevice as ModbusTcpPoll;
                        if (tcpPoll == null || !tcpPoll.IsConnect) throw new Exception("Modbus设备已失效或未打开");
                        tcpPoll.WriteSingleCoil(ushort.Parse(textBoxAddress.Text), comboBoxCoilValue.Text == "1");
                        labelTestInfo.Text = $"写入线圈值成功！";
                        labelTestInfo.ForeColor = System.Drawing.Color.Green;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                labelTestInfo.Text = $"写入线圈值失败！原因：{ex.Message}";
                labelTestInfo.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
