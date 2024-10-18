using Logger;
using System;
using System.Windows.Forms;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Device.TCP;

namespace YTVisionPro.Forms.TCPAdd
{
    internal partial class FrmTCPListView : Form
    {
        /// <summary>
        /// 添加TCP时通过快捷键保存方案的事件
        /// </summary>
        public event EventHandler OnShotKeySavePressed;
        /// <summary>
        /// 添加TCP窗口
        /// </summary>
        FrmTCPNew _frmAdd = new FrmTCPNew();
        /// <summary>
        /// TCP反序列化完成事件
        /// </summary>
        public static event EventHandler<bool> OnTCPDeserializationCompletionEvent;
        public FrmTCPListView()
        {
            InitializeComponent();
            FrmTCPNew.TCPAddEvent += FrmAdd_TCPAddEvent;
            SingleTcp.SelectedChange += SingleTCP_SelectedChange;
            SingleTcp.SingleTCPRemoveEvent += SingleTCP_SinglePLCRemoveEvent;
            FrmPLCListView.OnPLCDeserializationCompletionEvent += Deserialization;
            this.KeyPreview = true;
        }

        /// <summary>
        /// 反序列化TCP设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender, bool e)
        {
            // 先移除旧方案的TCP控件
            flowLayoutPanel1.Controls.Clear();

            // 添加新的TCP
            SingleTcp singlePLC = null;
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【TCP设备列表】=================================================", true);
            foreach (var dev in ConfigHelper.SolConfig.Devices)
            {
                if (dev is ITcpDevice modbus)
                {
                    try
                    {
                        modbus.CreateDevice(); // 创建modbus，必要的
                        singlePLC = new SingleTcp(modbus);
                        singlePLC.Anchor = AnchorStyles.Left;
                        singlePLC.Anchor = AnchorStyles.Right;
                        flowLayoutPanel1.Controls.Add(singlePLC);
                        if (e)
                            LogHelper.AddLog(MsgLevel.Info, $"TCP设备【{modbus.DevName}】已成功加载！", true);
                    }
                    catch (Exception ex)
                    {
                        if (e)
                            LogHelper.AddLog(MsgLevel.Exception, $"TCP设备【{modbus.DevName}】连接失败，请检查TCP状态！原因：{ex.Message}", true);
                        continue;
                    }
                }
            }
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================【TCP设备列表】已加载完成 ================================================", true);
            OnTCPDeserializationCompletionEvent?.Invoke(this, e);
        }

        /// <summary>
        /// 按下保存快捷键
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control && e.KeyCode == Keys.S)
            {
                // 触发保存方案事件
                OnShotKeySavePressed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 移除TCP设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleTCP_SinglePLCRemoveEvent(object sender, SingleTcp e)
        {
            SingleTcp.SingleTCPs.Remove(e);
            panel1.Controls.Remove(e.TcpParamsControl);
            e.TcpDevice.Disconnect();
            Solution.Instance.AllDevices.Remove(e.TcpDevice);
            flowLayoutPanel1.Controls.Remove(e);
            LogHelper.AddLog(MsgLevel.Info, $"Tcp设备（{e.TcpDevice.DevName}）已成功移除！", true);
        }

        /// <summary>
        /// TCP选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleTCP_SelectedChange(object sender, SingleTcp e)
        {
            //将选中的TCP的参数控件设置到右侧
            panel1.Controls.Clear();
            e.TcpParamsControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(e.TcpParamsControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _frmAdd.ShowDialog();
        }

        private void FrmAdd_TCPAddEvent(object sender, TcpParam e)
        {
            SingleTcp singleTCP = null;
            try
            {
                singleTCP = new SingleTcp(e);
                singleTCP.Anchor = AnchorStyles.Left;
                singleTCP.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singleTCP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmPLCListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
