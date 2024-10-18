using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Device;
using YTVisionPro.Device.TCP;

namespace YTVisionPro.Forms.TCPAdd
{
    /// <summary>
    /// 单个TCP
    /// </summary>
    internal partial class SingleTcp : UserControl
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
        /// TCP参数显示控件
        /// </summary>
        public TcpParamsControl TcpParamsControl;

        /// <summary>
        /// 反序列化用
        /// </summary>
        /// <param name="modbus"></param>
        public SingleTcp(ITcpDevice dev)
        {
            InitializeComponent();
            if (dev.IsConnect)
            {
                try
                {
                    dev.ConnectStatusEvent += TCP_ConnectStatusEvent;
                    dev.Connect();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            this.label1.Text = dev.UserDefinedName;
            TcpParamsControl = new TcpParamsControl(dev.TcpParam);
            TcpDevice = dev;
            Parms = dev.TcpParam;
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
                TcpParamsControl = new TcpParamsControl(param);
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
            uiSwitch1.Active = e;
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
