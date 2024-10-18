using Basler.Pylon;
using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Forms.CameraAdd;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Device.Light;
using YTVisionPro.Device.PLC;

namespace YTVisionPro.Forms.PLCAdd
{
    internal partial class FrmPLCListView : Form
    {
        /// <summary>
        /// 添加PLC时通过快捷键保存方案的事件
        /// </summary>
        public event EventHandler OnShotKeySavePressed;
        /// <summary>
        /// 添加PLC窗口
        /// </summary>
        FrmPLCNew _frmAdd = new FrmPLCNew();
        /// <summary>
        /// PLC反序列化完成事件
        /// </summary>
        public static event EventHandler<bool> OnPLCDeserializationCompletionEvent;
        public FrmPLCListView()
        {
            InitializeComponent();
            FrmPLCNew.PLCAddEvent += FrmAdd_PLCAddEvent;
            SinglePLC.SelectedChange += SinglePLC_SelectedChange;
            SinglePLC.SinglePLCRemoveEvent += SinglePLC_SinglePLCRemoveEvent;
            FrmCameraListView.OnCameraDeserializationCompletionEvent += Deserialization;
            this.KeyPreview = true;
        }

        /// <summary>
        /// 反序列化PLC设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender, bool e)
        {
            // 先移除旧方案的PLC控件
            flowLayoutPanel1.Controls.Clear();

            // 添加新的PLC
            SinglePLC singlePLC = null;
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【PLC设备列表】=================================================", true);
            foreach (var dev in ConfigHelper.SolConfig.Devices)
            {
                if (dev is IPlc plc)
                {
                    try
                    {
                        plc.CreateDevice(); // 创建PLC，必要的
                        singlePLC = new SinglePLC(plc);
                        singlePLC.Anchor = AnchorStyles.Left;
                        singlePLC.Anchor = AnchorStyles.Right;
                        flowLayoutPanel1.Controls.Add(singlePLC);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"PLC（{plc.UserDefinedName}）连接失败，请检查PLC状态！原因：{ex.Message}");
                        continue;
                    }
                }
            }
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================【PLC设备列表】已加载完成 ================================================", true);
            OnPLCDeserializationCompletionEvent?.Invoke(this, e);
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
        /// 移除PLC设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePLC_SinglePLCRemoveEvent(object sender, SinglePLC e)
        {
            SinglePLC.SinglePLCs.Remove(e);
            panel1.Controls.Remove(e.SerialParamsControl);
            e.Plc.Disconnect();
            Solution.Instance.AllDevices.Remove(e.Plc);
            flowLayoutPanel1.Controls.Remove(e);
            LogHelper.AddLog(MsgLevel.Info, $"PLC设备（{e.Plc.DevName}）已成功移除！", true);
        }

        /// <summary>
        /// PLC选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePLC_SelectedChange(object sender, SinglePLC e)
        {
            //将选中的PLC的参数控件设置到右侧
            panel1.Controls.Clear();
            if (e.ConType == PlcConType.COM)
            {
                e.SerialParamsControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(e.SerialParamsControl);
            }
            else if (e.ConType == PlcConType.ETHERNET)
            {
                e.EthernetParamsControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(e.EthernetParamsControl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _frmAdd.ShowDialog();
        }

        private void FrmAdd_PLCAddEvent(object sender, PLCParms e)
        {
            SinglePLC singlePLC = null;
            try
            {
                singlePLC = new SinglePLC(e);
                singlePLC.Anchor = AnchorStyles.Left;
                singlePLC.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singlePLC);
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
