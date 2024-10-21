using Modbus.Device;
using System;
using System.Net.Sockets;
using System.Net;
using Modbus.Data;
using System.Windows.Forms;
using Logger;
using Newtonsoft.Json;
using static System.Windows.Forms.AxHost;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YTVisionPro.Device.Modbus
{
    internal class ModbusSlave : IModbus
    {
        private TcpListener listener;
        private ModbusTcpSlave slave;
        private List<IAsyncResult> asyncResults = new List<IAsyncResult>();

        public string DevName { get; set; }
        public string UserDefinedName { get; set; }
        public ModbusParam ModbusParam { get; set; }
        public bool IsConnect { get; set; } = false;
        public DevType DevType { get; set; } = DevType.ModbusSlave;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(ModbusSlave).FullName;

        public event EventHandler<bool> ConnectStatusEvent;

        #region 反序列化专用函数

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public ModbusSlave() { }

        public void CreateDevice()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        public ModbusSlave(ModbusParam param)
        {
            ModbusParam = param;
            DevName = param.DevName;
            UserDefinedName = param.UserDefinedName;
        }

        public void Connect()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, ModbusParam.Port);
                listener.Start();

                slave = ModbusTcpSlave.CreateTcp(ModbusParam.ID, listener/*, 5000*/);
                //创建寄存器存储对象
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore(100, 100, 100, 100);
                //Modbus命令写入DataStore时发生
                slave.DataStore.DataStoreWrittenTo += DataStore_DataStoreWrittenTo;
                //当Modbus从站收到请求
                slave.ModbusSlaveRequestReceived += Slave_ModbusSlaveRequestReceived;
                //当Modbus从站写入完成
                slave.WriteComplete += Slave_WriteComplete;
                slave.Listen();
                IsConnect = true;
                ConnectStatusEvent?.Invoke(this, true);

                // 启动异步操作
                //Task.Run(() =>
                //{
                //    StartListeningAsync();
                //});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                if (!IsConnect) return;
                IsConnect = false;
                listener.Stop();
                slave.Dispose();
                slave = null;
                ConnectStatusEvent?.Invoke(this, false);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, $"断开异常: {ex.Message}", true);
            }
        }

        /// <summary>
        /// 从站开始写入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataStore_DataStoreWrittenTo(object sender, DataStoreEventArgs e)
        {
            switch (e.ModbusDataType)
            {
                case ModbusDataType.Coil:
                    ModbusDataCollection<bool> discretes = slave.DataStore.CoilDiscretes;
                    LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(discretes), true);
                    break;
                case ModbusDataType.HoldingRegister:
                    ModbusDataCollection<ushort> holdingRegisters = slave.DataStore.HoldingRegisters;
                    LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(holdingRegisters), true);
                    break;
                default:
                    LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(e.Data), true);
                    break;

            }

        }

        /// <summary>
        /// 从站接收到的请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slave_ModbusSlaveRequestReceived(object sender, ModbusSlaveRequestEventArgs e)
        {
            LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})接收请求数据: " + JsonConvert.SerializeObject(e.Message), true);
        }

        /// <summary>
        /// 从站写入数据完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slave_WriteComplete(object sender, ModbusSlaveRequestEventArgs e)
        {
            LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据完成: " + JsonConvert.SerializeObject(e.Message), true);
        }
    }
}
