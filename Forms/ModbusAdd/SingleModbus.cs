using Basler.Pylon;
using Logger;
using Microsoft.VisualBasic.Devices;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YTVisionPro.Forms.LightAdd;
using YTVisionPro.Device;
using YTVisionPro.Device.Light;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.PLC;
using Modbus.Device;

namespace YTVisionPro.Forms.ModbusAdd
{
    /// <summary>
    /// 单个Modbus
    /// </summary>
    internal partial class SingleModbus : UserControl
    {
        /// <summary>
        /// Modbus设备
        /// </summary>
        public IModbus ModbusDevice = null;
        /// <summary>
        /// 设备参数
        /// </summary>
        public ModbusParam Parms = new ModbusParam();
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected;
        /// <summary>
        /// 选中改变事件
        /// </summary>
        public static event EventHandler<SingleModbus> SelectedChange;
        /// <summary>
        /// 移除事件
        /// </summary>
        public static event EventHandler<SingleModbus> SingleModbusRemoveEvent;
        /// <summary>
        /// 保存所有的当前类实例
        /// </summary>
        public static List<SingleModbus> SingleModbuss = new List<SingleModbus>();
        /// <summary>
        /// Modbus参数显示控件
        /// </summary>
        public ModbusParamsControl ModbusParamsControl;

        /// <summary>
        /// 反序列化用
        /// </summary>
        /// <param name="modbus"></param>
        public SingleModbus(IModbus dev)
        {
            InitializeComponent();
            if (dev.IsConnect)
            {
                try
                {
                    dev.ConnectStatusEvent += Modbus_ConnectStatusEvent;
                    dev.Connect();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            this.label1.Text = dev.UserDefinedName;
            ModbusParamsControl = new ModbusParamsControl(dev.ModbusParam);
            ModbusDevice = dev;
            Parms = dev.ModbusParam;
            Solution.Instance.AllDevices.Add(dev);
            SingleModbuss.Add(this);
        }

        public SingleModbus(ModbusParam param)
        {
            InitializeComponent();
            this.label1.Text = param.UserDefinedName;
            Parms = param;
            try
            {
                ModbusParamsControl = new ModbusParamsControl(param);
                switch (param.DevType)
                {
                    case DevType.ModbusPoll:
                        ModbusDevice = new ModbusPoll(param);
                        break;
                    case DevType.ModbusSlave:
                        // TODO:实现Modbus从站
                        throw new NotImplementedException("未实现ModbusSlave类型");
                        break;
                    default:
                        break;
                }
                Solution.Instance.AllDevices.Add(ModbusDevice);
                SingleModbuss.Add(this);
                ModbusDevice.ConnectStatusEvent += Modbus_ConnectStatusEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 订阅Modbus连接状态
        /// </summary>
        private void Modbus_ConnectStatusEvent(object sender, bool e)
        {
            uiSwitch1.Active = e;
        }

        /// <summary>
        /// 点击选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleModbusInfo_MouseClick(object sender, MouseEventArgs e)
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
            foreach (var item in SingleModbuss)
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
        /// 连接Modbus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            try
            {
                if (value)
                {
                    ModbusDevice.Connect();
                    LogHelper.AddLog(MsgLevel.Info, $"Modbus设备【{ModbusDevice.DevName}】打开", true);
                }
                else
                {
                    if (ModbusDevice.IsConnect)
                    {
                        ModbusDevice.Disconnect();
                        LogHelper.AddLog(MsgLevel.Info, $"Modbus设备【{ModbusDevice.DevName}】关闭", true);
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
                SingleModbusRemoveEvent?.Invoke(this, this);
        }
    }
}
