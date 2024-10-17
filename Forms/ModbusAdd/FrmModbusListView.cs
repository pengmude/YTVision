using Logger;
using System;
using System.Windows.Forms;
using YTVisionPro.Forms.CameraAdd;
using YTVisionPro.Forms.PLCAdd;
using YTVisionPro.Hardware.Modbus;

namespace YTVisionPro.Forms.ModbusAdd
{
    internal partial class FrmModbusListView : Form
    {
        /// <summary>
        /// 添加Modbus时通过快捷键保存方案的事件
        /// </summary>
        public event EventHandler OnShotKeySavePressed;
        /// <summary>
        /// 添加Modbus窗口
        /// </summary>
        FrmModbusNew _frmAdd = new FrmModbusNew();
        /// <summary>
        /// Modbus反序列化完成事件
        /// </summary>
        public static event EventHandler<bool> OnModbusDeserializationCompletionEvent;
        public FrmModbusListView()
        {
            InitializeComponent();
            FrmModbusNew.ModbusAddEvent += FrmAdd_ModbusAddEvent;
            SingleModbus.SelectedChange += SingleModbus_SelectedChange;
            SingleModbus.SingleModbusRemoveEvent += SingleModbus_SinglePLCRemoveEvent;
            FrmPLCListView.OnPLCDeserializationCompletionEvent += Deserialization;
            this.KeyPreview = true;
        }

        /// <summary>
        /// 反序列化Modbus设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deserialization(object sender, bool e)
        {
            // 先移除旧方案的Modbus控件
            flowLayoutPanel1.Controls.Clear();

            // 添加新的Modbus
            SingleModbus singlePLC = null;
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================= 正在加载【Modbus设备列表】=================================================", true);
            foreach (var dev in ConfigHelper.SolConfig.Devices)
            {
                if (dev is ModbusDevice modbus)
                {
                    try
                    {
                        modbus.CreateDevice(); // 创建modbus，必要的
                        singlePLC = new SingleModbus(modbus);
                        singlePLC.Anchor = AnchorStyles.Left;
                        singlePLC.Anchor = AnchorStyles.Right;
                        flowLayoutPanel1.Controls.Add(singlePLC);
                        if (e)
                            LogHelper.AddLog(MsgLevel.Info, $"Modbus设备【{modbus.DevName}】已成功加载！", true);
                    }
                    catch (Exception ex)
                    {
                        if (e)
                            LogHelper.AddLog(MsgLevel.Exception, $"Modbus设备【{modbus.DevName}】连接失败，请检查Modbus状态！原因：{ex.Message}", true);
                        continue;
                    }
                }
            }
            if(e)
                LogHelper.AddLog(MsgLevel.Debug, $"================================================【Modbus设备列表】已加载完成 ================================================", true);
            OnModbusDeserializationCompletionEvent?.Invoke(this, e);
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
        /// 移除Modbus设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleModbus_SinglePLCRemoveEvent(object sender, SingleModbus e)
        {
            SingleModbus.SingleModbuss.Remove(e);
            panel1.Controls.Remove(e.ModbusParamsControl);
            e.ModbusDevice.DisConnect();
            Solution.Instance.AllDevices.Remove(e.ModbusDevice);
            flowLayoutPanel1.Controls.Remove(e);
        }

        /// <summary>
        /// Modbus选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleModbus_SelectedChange(object sender, SingleModbus e)
        {
            //将选中的Modbus的参数控件设置到右侧
            panel1.Controls.Clear();
            e.ModbusParamsControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(e.ModbusParamsControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _frmAdd.ShowDialog();
        }

        private void FrmAdd_ModbusAddEvent(object sender, ModbusParam e)
        {
            SingleModbus singleModbus = null;
            try
            {
                singleModbus = new SingleModbus(e);
                singleModbus.Anchor = AnchorStyles.Left;
                singleModbus.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singleModbus);
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
