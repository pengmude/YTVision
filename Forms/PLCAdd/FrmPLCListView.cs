using Logger;
using System;
using System.Linq;
using System.Windows.Forms;
using TDJS_Vision.Forms.CameraAdd;
using TDJS_Vision.Device.PLC;

namespace TDJS_Vision.Forms.PLCAdd
{
    public partial class FrmPLCListView : FormBase
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
            try
            {
                // 先移除旧方案的PLC控件
                flowLayoutPanel1.Controls.Clear();
                // 添加新的PLC
                SinglePLC singlePLC = null;
                if (e)
                    LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【PLC设备列表】=================================================", true);
                foreach (var dev in ConfigHelper.SolConfig.Devices)
                {
                    if (dev is IPlc plc)
                    {
                        plc.CreateDevice(); // 创建PLC，必要的
                        singlePLC = new SinglePLC(plc);
                        singlePLC.Anchor = AnchorStyles.Left;
                        singlePLC.Anchor = AnchorStyles.Right;
                        flowLayoutPanel1.Controls.Add(singlePLC);
                        if (e)
                            LogHelper.AddLog(MsgLevel.Info, $"PLC设备【{dev.DevName}】已加载！", true);
                    }
                }
                if (e)
                    LogHelper.AddLog(MsgLevel.Debug, $"================================================【PLC设备列表】已加载完成 ================================================", true);

            }
            catch (Exception) { }
            finally
            {
                OnPLCDeserializationCompletionEvent?.Invoke(this, e);
            }
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
            panel1.Controls.Clear();
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
                var control = new SerialParamsControl(e.Plc.PLCParms);
                control.Dock = DockStyle.Fill;
                panel1.Controls.Add(control);
            }
            else if (e.ConType == PlcConType.ETHERNET)
            {
                var control = new EthernetParamsControl(e.Plc.PLCParms);
                control.Dock = DockStyle.Fill;
                panel1.Controls.Add(control);
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
