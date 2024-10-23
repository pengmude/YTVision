using Logger;
using Sunny.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.TCP;
using YTVisionPro.Node.TCP.Client;

namespace YTVisionPro.Node.TCPClient
{
    internal partial class ParamFormTCPClient : Form, INodeParamForm
    {
        public INodeParam Params { get; set; }
        public delegate Task AsyncEventHandler<T>(object sender, T e);
        public event AsyncEventHandler<EventArgs> RunHandler;

        public ParamFormTCPClient()
        {
            InitializeComponent();
            InitClientComboBox();
        }

        public void SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }
        /// <summary>
        /// 获取订阅的条件布尔值
        /// </summary>
        public bool GetCondition()
        {
            return nodeSubscription1.GetValue<bool>();
        }

        /// <summary>
        /// 提供节点运行后设置结果
        /// </summary>
        /// <param name="res"></param>
        public void SetResult(string res)
        {
            textBoxResponseData.Text = res;
        }

        /// <summary>
        /// 初始化客户端下拉框
        /// </summary>
        private void InitClientComboBox()
        {
            string text1 = comboBoxTCPDev.Text;

            comboBoxTCPDev.Items.Clear();
            comboBoxTCPDev.Items.Add("[未设置]");
            // 初始化TCP客户端列表
            foreach (var tcp in Solution.Instance.TcpDevices)
            {
                // 排除TCP服务器设备
                if(tcp.DevType == Device.DevType.TcpServer)
                    continue;
                comboBoxTCPDev.Items.Add(tcp.UserDefinedName);
            }
            int index1 = comboBoxTCPDev.Items.IndexOf(text1);
            comboBoxTCPDev.SelectedIndex = index1 == -1 ? 0 : index1;
        }


        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormRead_Shown(object sender, EventArgs e)
        {
            InitClientComboBox();
        }

        /// <summary>
        /// 点击保存当前参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SaveParams();
            Hide();
        }

        private bool SaveParams()
        {
            if (comboBoxTCPDev.Text.IsNullOrEmpty() || comboBoxTCPDev.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "未选择TCP设备！", true);
                MessageBox.Show("未选择TCP设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if ((tabControl1.SelectedIndex == 0 && textBoxNoConditionContent.Text.IsNullOrEmpty()) ||
                (tabControl1.SelectedIndex == 1 && (textBoxResponseTrue.Text.IsNullOrEmpty() || textBoxResponseFalse.Text.IsNullOrEmpty())))
            {
                LogHelper.AddLog(MsgLevel.Exception, "发送内容不能为空！", true);
                MessageBox.Show("发送内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //查找当前选择的Modbus
            ITcpDevice tcpDev = null;
            foreach (var dev in Solution.Instance.TcpDevices)
            {
                if (dev.UserDefinedName == comboBoxTCPDev.Text)
                {
                    tcpDev = dev;
                    break;
                }
            }

            NodeParamTCPClient nodeParamTcpClient = new NodeParamTCPClient();
            nodeParamTcpClient.Device = tcpDev;
            nodeParamTcpClient.IsWaitingForResponse = checkBox1.Checked;
            nodeParamTcpClient.NoConditionContent = textBoxNoConditionContent.Text;
            nodeParamTcpClient.NeedsCondition = tabControl1.SelectedIndex == 1 ? true : false;
            nodeParamTcpClient.Text1 = nodeSubscription1.GetText1();
            nodeParamTcpClient.Text2 = nodeSubscription1.GetText2();
            nodeParamTcpClient.SendContentTrue = textBoxResponseTrue.Text;
            nodeParamTcpClient.SendContentFalse = textBoxResponseFalse.Text;
            Params = nodeParamTcpClient;

            return true;
        }

        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamTCPClient param)
            {
                int index1 = comboBoxTCPDev.Items.IndexOf(param.Device.UserDefinedName);
                comboBoxTCPDev.SelectedIndex = index1 == -1 ? 0 : index1;
                // 反序列化后方案中的TCP设备对象才是完整的，需要赋值给用到TCP的节点参数中
                foreach (var dev in Solution.Instance.TcpDevices)
                {
                    if(dev.UserDefinedName == param.Device.UserDefinedName)
                    {
                        param.Device = dev;
                        break;
                    }
                }
                checkBox1.Checked = param.IsWaitingForResponse;
                textBoxNoConditionContent.Text = param.NoConditionContent;
                tabControl1.SelectedIndex = param.NeedsCondition ? 1 : 0;
                nodeSubscription1.SetText(param.Text1, param.Text2);
                textBoxResponseTrue.Text = param.SendContentTrue;
                textBoxResponseFalse.Text = param.SendContentFalse;
                textBoxResponseData.Text = "";
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if(SaveParams())
                RunHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
