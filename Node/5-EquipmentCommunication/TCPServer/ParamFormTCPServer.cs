using Logger;
using Sunny.UI;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using TDJS_Vision.Device.TCP;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._5_EquipmentCommunication.TcpServer
{
    public partial class ParamFormTCPServer : FormBase, INodeParamForm
    {
        public INodeParam Params { get; set; }
        public delegate Task AsyncEventHandler<T>(object sender, T e);
        public event AsyncEventHandler<EventArgs> RunHandler;

        public ParamFormTCPServer()
        {
            InitializeComponent();
            InitServerComboBox();
            comboBoxServer.SelectedIndex = 0;
            Shown += ParamFormRead_Shown;
        }

        public void SetNodeBelong(NodeBase node) 
        {
            nodeSubscription1.Init(node);
        }
        /// <summary>
        /// 获取订阅的条件布尔值
        /// </summary>
        /// <returns></returns>
        public bool GetCondition()
        {
            return nodeSubscription1.GetValue<bool>();
        }

        /// <summary>
        /// 窗口每次显示时都要刷新下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamFormRead_Shown(object sender, EventArgs e)
        {
            InitServerComboBox();
        }

        private void InitServerComboBox()
        {
            string text1 = comboBoxServer.Text;

            comboBoxServer.Items.Clear();
            comboBoxServer.Items.Add("[未设置]");
            // 初始化TCP服务器列表
            foreach (var server in Solution.Instance.TcpDevices)
            {
                // 排除TCP客户端设备
                if (server.DevType == Device.DevType.TcpClient)
                    continue;
                comboBoxServer.Items.Add(server.UserDefinedName);
            }
            if(text1.IsNullOrEmpty())
                return;
            int index1 = comboBoxServer.Items.IndexOf(text1);
            comboBoxServer.SelectedIndex = index1 == -1 ? 0 : index1;
        }

        /// <summary>
        /// 服务器选择改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitClientIPComboBox();
        }

        /// <summary>
        /// 初始化服务器已连接的客户端ip下拉框
        /// </summary>
        private void InitClientIPComboBox()
        {
            if(comboBoxServer.Text == "[未设置]" || comboBoxServer.Text.IsNullOrEmpty())
                return;
            comboBoxClientIp.Items.Clear();
            // 找到当前选中的服务器
            foreach (var server in Solution.Instance.TcpDevices)
            {
                if (server is TCPServer tCPServer && tCPServer.UserDefinedName == comboBoxServer.Text)
                {
                    //填充连接到服务器上的客户端IP
                    foreach (var pair in tCPServer.Ip2TcpClientDic)
                    {
                        comboBoxClientIp.Items.Add(pair.Key);
                    }
                }
            }
            if (comboBoxClientIp.Items.Count > 0)
                comboBoxClientIp.SelectedIndex = 0;
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
            if (comboBoxClientIp.Text.IsNullOrEmpty() || comboBoxClientIp.Text == "[未设置]")
            {
                LogHelper.AddLog(MsgLevel.Exception, "未选择TCP设备！", true);
                MessageBoxTD.Show("未选择TCP设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if ((tabControl1.SelectedIndex == 0 && textBoxNoConditionContent.Text.IsNullOrEmpty()) ||
                (tabControl1.SelectedIndex == 1 && (textBoxResponseTrue.Text.IsNullOrEmpty() || textBoxResponseFalse.Text.IsNullOrEmpty())))
            {
                LogHelper.AddLog(MsgLevel.Exception, "发送内容不能为空！", true);
                MessageBoxTD.Show("发送内容不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //查找当前选择的Modbus
            ITcpDevice tcpDev = null;
            foreach (var dev in Solution.Instance.TcpDevices)
            {
                if (dev.UserDefinedName == comboBoxServer.Text)
                {
                    tcpDev = dev;
                    break;
                }
            }

            NodeParamTCPServer nodeParamTcpServer = new NodeParamTCPServer();
            nodeParamTcpServer.Sever = tcpDev;
            nodeParamTcpServer.SeverName = tcpDev.UserDefinedName;
            nodeParamTcpServer.ClientIP = comboBoxClientIp.Text;
            nodeParamTcpServer.NeedsCondition = tabControl1.SelectedIndex == 1 ? true : false;
            nodeParamTcpServer.NoConditionContent = textBoxNoConditionContent.Text;
            nodeParamTcpServer.Text1 = nodeSubscription1.GetText1();
            nodeParamTcpServer.Text2 = nodeSubscription1.GetText2();
            nodeParamTcpServer.ResponseContentTrue = textBoxResponseTrue.Text;
            nodeParamTcpServer.ResponseContentFalse = textBoxResponseFalse.Text;
            Params = nodeParamTcpServer;

            return true;
        }

        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        public void SetParam2Form()
        {
            if (Params is NodeParamTCPServer param)
            {
                int index1 = comboBoxServer.Items.IndexOf(param.SeverName);
                comboBoxServer.SelectedIndex = index1 == -1 ? 0 : index1;
                // 反序列化后方案中的TCP设备对象才是完整的，需要赋值给用到TCP的节点参数中
                foreach (var dev in Solution.Instance.TcpDevices)
                {
                    if(dev.UserDefinedName == param.SeverName)
                    {
                        param.Sever = dev;
                        break;
                    }
                }
                textBoxNoConditionContent.Text = param.NoConditionContent;
                tabControl1.SelectedIndex = param.NeedsCondition ? 1 : 0;
                nodeSubscription1.SetText(param.Text1, param.Text2);
                textBoxResponseTrue.Text = param.ResponseContentTrue;
                textBoxResponseFalse.Text = param.ResponseContentFalse;
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if(SaveParams())
                RunHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
