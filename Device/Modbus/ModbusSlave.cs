using Modbus.Device;
using System;
using System.Net.Sockets;
using System.Net;
using Modbus.Data;
using Logger;
using Newtonsoft.Json;

namespace YTVisionPro.Device.Modbus
{
    internal class ModbusSlave : IModbus
    {
        private TcpListener _server;
        public ModbusParam ModbusParam { get; set; }
        public string DevName { get; set; }
        public string UserDefinedName { get; set; }
        public DevType DevType { get; set; } = DevType.ModbusSlave;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(ModbusSlave).FullName;

        public bool IsConnect { get; set; }

        public event EventHandler<bool> ConnectStatusEvent;

        private ModbusTcpSlave _slave;

        #region 反序列化使用

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public ModbusSlave() { }

        public void CreateDevice()
        {
            try
            {
                // 初始化设备
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion  

        public ModbusSlave(ModbusParam modbusParam)
        {
            ModbusParam = modbusParam;
            DevName = modbusParam.DevName;
            UserDefinedName = modbusParam.UserDefinedName;
        }

        /// <summary>
        /// 从站启动
        /// </summary>
        public void Connect()
        {
            try
            {
                // 创建TCP通信对象以及初始化TCP从站
                _server = new TcpListener(IPAddress.Any, ModbusParam.Port);
                _slave = ModbusTcpSlave.CreateTcp(ModbusParam.ID, _server);
                //创建寄存器存储对象
                _slave.DataStore = DataStoreFactory.CreateDefaultDataStore(10, 10, 10, 10);
                //Modbus命令写入DataStore时发生
                _slave.DataStore.DataStoreWrittenTo += DataStore_DataStoreWrittenTo;
                //当Modbus从站收到请求
                _slave.ModbusSlaveRequestReceived += Slave_ModbusSlaveRequestReceived;
                //当Modbus从站写入完成
                _slave.WriteComplete += Slave_WriteComplete;
                _server.Start();
                IsConnect = true;
                ConnectStatusEvent?.Invoke(this, true);
                _slave.Listen();
            }
            catch (Exception ex)
            {
                IsConnect = false;
                ConnectStatusEvent?.Invoke(this, false);
                LogHelper.AddLog(MsgLevel.Exception, $"从站（{DevName}）启动失败: {ex.Message}", true);
                throw;
            }
        }

        /// <summary>
        /// 从站停止
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if(_slave != null && _server != null)
                {
                    _slave.Dispose();
                    _server.Stop();
                }
                IsConnect = false;
                ConnectStatusEvent?.Invoke(this, false);
                LogHelper.AddLog(MsgLevel.Info, $"从站（{DevName}）已停止运行", true);
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 从站开始写入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataStore_DataStoreWrittenTo(object sender, DataStoreEventArgs e)
        {
            try
            {
                switch (e.ModbusDataType)
                {
                    case ModbusDataType.Coil:
                        ModbusDataCollection<bool> discretes = _slave.DataStore.CoilDiscretes;
                        LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(discretes), true);
                        break;
                    case ModbusDataType.HoldingRegister:
                        ModbusDataCollection<ushort> holdingRegisters = _slave.DataStore.HoldingRegisters;
                        LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(holdingRegisters), true);
                        break;
                    default:
                        LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据: " + JsonConvert.SerializeObject(e.Data), true);
                        break;

                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
            }

        }

        /// <summary>
        /// 从站接收到的请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slave_ModbusSlaveRequestReceived(object sender, ModbusSlaveRequestEventArgs e)
        {
            try
            {
                LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})接收请求数据: " + JsonConvert.SerializeObject(e.Message), true);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
            }
        }

        /// <summary>
        /// 从站写入数据完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slave_WriteComplete(object sender, ModbusSlaveRequestEventArgs e)
        {
            try
            {
                LogHelper.AddLog(MsgLevel.Info, $"从站({UserDefinedName})写入数据完成: " + JsonConvert.SerializeObject(e.Message), true);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, ex.Message, true);
            }
        }

    }
}

