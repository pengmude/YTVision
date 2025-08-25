using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TDJS_Vision.Device;
using TDJS_Vision.Device.TCP;

namespace TDJS_Vision.Forms.TCPAdd
{
    /// <summary>
    /// 单个TCP
    /// </summary>
    public partial class SingleTcp : UserControl
    {
        /// <summary>
        /// Tcp设备
        /// </summary>
        public ITcpDevice TcpDevice = null;
        /// <summary>
        /// 设备参数
        /// </summary>
        public TcpParam Parms = new TcpParam();
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected;
        /// <summary>
        /// 选中改变事件
        /// </summary>
        public static event EventHandler<SingleTcp> SelectedChange;
        /// <summary>
        /// 移除事件
        /// </summary>
        public static event EventHandler<SingleTcp> SingleTCPRemoveEvent;
        /// <summary>
        /// 保存所有的当前类实例
        /// </summary>
        public static List<SingleTcp> SingleTCPs = new List<SingleTcp>();

        /// <summary>
        /// 反序列化用
        /// </summary>
        /// <param name="modbus"></param>
        public SingleTcp(ITcpDevice dev)
        {
            InitializeComponent();
            TcpDevice = dev;
            Parms = dev.TcpParam;
            //保存配置时是连接的，现在要还原连接状态
            if (dev.IsConnect)
            {
                try
                {
                    dev.Connect();
                    uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
                    uiSwitch1.Active = dev.IsConnect;
                    uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"TCP设备【{dev.DevName}】连接失败，请检查TCP状态！原因：{ex.Message}", true);
                }
            }
            dev.ConnectStatusEvent += TCP_ConnectStatusEvent;
            this.label1.Text = dev.UserDefinedName;
            Solution.Instance.AllDevices.Add(dev);
            SingleTCPs.Add(this);
        }

        public SingleTcp(TcpParam param)
        {
            InitializeComponent();
            this.label1.Text = param.UserDefinedName;
            Parms = param;
            try
            {
                switch (param.DevType)
                {
                    case DevType.TcpServer:
                        TcpDevice = new TCPServer(param);
                        break;
                    case DevType.TcpClient:
                        TcpDevice = new TCPClient(param);
                        break;
                    default:
                        break;
                }
                Solution.Instance.AllDevices.Add(TcpDevice);
                SingleTCPs.Add(this);
                TcpDevice.ConnectStatusEvent += TCP_ConnectStatusEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 订阅TCP连接状态
        /// </summary>
        private void TCP_ConnectStatusEvent(object sender, bool e)
        {
            uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
            uiSwitch1.Active = e;
            uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;
        }

        /// <summary>
        /// 点击选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleTCPInfo_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelected();
            SelectedChange?.Invoke(this, this);
        }

        /// <summary>
        /// 设置控件选中状态
        /// </summary>
        /// <param name="flag"></param>
        private void SetSelected()
        {
            //先清除所有选中状态
            foreach (var item in SingleTCPs)
            {
                item.tableLayoutPanel1.BackColor = Color.LightSteelBlue;
                item.label1.BackColor = Color.LightSteelBlue;
                IsSelected = false;
            }

            // 设置当前选中的样式
            this.tableLayoutPanel1.BackColor = Color.CornflowerBlue;
            this.label1.BackColor = Color.CornflowerBlue;
            IsSelected = true;
        }

        /// <summary>
        /// 改变控件背景颜色和状态
        /// </summary>
        /// <param name="flag"></param>
        public void SetSelectedStatus(bool flag)
        {
            if (flag)
            {
                this.tableLayoutPanel1.BackColor = Color.LightSteelBlue;
                this.label1.BackColor = Color.LightSteelBlue;
            }
            else
            {
                this.tableLayoutPanel1.BackColor = Color.CornflowerBlue;
                this.label1.BackColor = Color.CornflowerBlue;
            }
            IsSelected = flag;
        }

        /// <summary>
        /// 连接TCP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            try
            {
                if (value)
                {
                    TcpDevice.Connect();
                    LogHelper.AddLog(MsgLevel.Info, $"TCP设备【{TcpDevice.DevName}】打开", true);
                }
                else
                {
                    if (TcpDevice.IsConnect)
                    {
                        TcpDevice.Disconnect();
                        LogHelper.AddLog(MsgLevel.Info, $"TCP设备【{TcpDevice.DevName}】关闭", true);
                    }
                }
            }
            catch(Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                uiSwitch1.ValueChanged -= uiSwitch1_ValueChanged;
                uiSwitch1.Active = TcpDevice.IsConnect;
                uiSwitch1.ValueChanged += uiSwitch1_ValueChanged;

            }
        }

        /// <summary>
        /// 右击移除当前设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(IsSelected)
                SingleTCPRemoveEvent?.Invoke(this, this);
        }
    }
}
