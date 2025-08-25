using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger;
using TDJS_Vision.Device;
using TDJS_Vision.Device.Modbus;
using TDJS_Vision.Device.PLC;
using TDJS_Vision.Forms.ProcessNew;

namespace TDJS_Vision.Forms.GlobalSignalSettings
{
    public partial class FormGlobalSignal : Form
    {
        public FormGlobalSignal()
        {
            InitializeComponent();
            Shown += FormGlobalSignal_Shown;
            dataGridView1.Leave += DataGridView1_Leave;
            dataGridView2.Leave += DataGridView2_Leave;
            FormMain.OnRunChange += FormMain_OnRunChange;
        }
        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (sender is DataGridView view && view == dataGridView1)
                {
                    // 检查点击是否发生在 deleteColumn1 列
                    if (e.ColumnIndex == dataGridView1.Columns["deleteColumn1"].Index && e.RowIndex >= 0)
                    {
                        // 获取被点击的行
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        if (row.IsNewRow) return;
                        // 删除当前行
                        dataGridView1.Rows.Remove(row);
                    }
                }
                else if (sender is DataGridView view2 && view2 == dataGridView2)
                {
                    // 检查点击是否发生在 deleteColumn1 列
                    if (e.ColumnIndex == dataGridView2.Columns["deleteColumn2"].Index && e.RowIndex >= 0)
                    {
                        // 获取被点击的行
                        DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                        if (row.IsNewRow) return;
                        // 删除当前行
                        dataGridView2.Rows.Remove(row);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"删除信号失败！{ex.Message}", true);
            }
        }
        /// <summary>
        /// 运行状态改变，给PLC发送信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void FormMain_OnRunChange(object sender, bool e)
        {
            await Task.Run(async () =>
            {
                string txt = e ? "准备" : "离线";
                try
                {
                    List<SingleGlobalSignalSettings> signals;
                    if (Solution.Instance.GlobalSignal == null || Solution.Instance.GlobalSignal.ReadySignals == null || Solution.Instance.GlobalSignal.StopSignals == null)
                        return;
                    if(e)
                    {
                        signals = Solution.Instance.GlobalSignal.ReadySignals;
                    }
                    else
                    {
                        signals = Solution.Instance.GlobalSignal.StopSignals;
                    }
                    foreach (var signal in signals)
                    {
                        if (string.IsNullOrEmpty(signal.DeviceName) || string.IsNullOrEmpty(signal.Address))
                            continue;
                        var device = Solution.Instance.AllDevices.Find(r => r.DevName == signal.DeviceName);
                        if (device == default(IDevice))
                            continue;
                        else
                        {
                            if (device is IPlc plc)
                            {
                                if(!plc.IsConnect)
                                {
                                    LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})未连接，视觉{txt}信号发送失败！", true);
                                    continue;
                                }
                                switch (signal.Type)
                                {
                                    case "布尔类型":
                                        await plc.WriteBoolAsync(signal.Address, new bool[] { signal.Value.Equals("True", StringComparison.OrdinalIgnoreCase) || signal.Value != "0" });
                                        break;
                                    case "整数类型":
                                        await plc.WriteIntAsync(signal.Address, int.Parse(signal.Value));
                                        break;
                                    case "字符串类型":
                                        await plc.WriteStringAsync(signal.Address, signal.Value);
                                        break;
                                    default:
                                        LogHelper.AddLog(MsgLevel.Exception, $"不支持的信号类型！", true);
                                        break;
                                }
                            }
                            else if (device is IModbus modbus)
                            {
                                if (!modbus.IsConnect)
                                {
                                    LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})未连接，视觉{txt}信号发送失败！", true);
                                    continue;
                                }
                                switch (signal.Type)
                                {
                                    case "布尔类型":
                                        await modbus.WriteSingleCoilAsync(ushort.Parse(signal.Address), signal.Value.Equals("True", StringComparison.OrdinalIgnoreCase) || signal.Value != "0");
                                        break;
                                    case "整数类型":
                                        await modbus.WriteSingleRegisterAsync(ushort.Parse(signal.Address), ushort.Parse(signal.Value));
                                        break;
                                    case "字符串类型":
                                        await modbus.WriteMultipleRegistersAsync(ushort.Parse(signal.Address), StringToHoldingRegisters(signal.Value));
                                        break;
                                    default:
                                        LogHelper.AddLog(MsgLevel.Exception, $"不支持的信号类型！", true);
                                        break;
                                }
                            }
                            else
                            {
                                LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})不支持全局信号功能！", true);
                                continue;
                            }
                        }
                    }
                    LogHelper.AddLog(MsgLevel.Info, $"视觉{txt}信号已发送！", true);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"视觉{txt}信号发送异常！原因:{ex.Message}", true);
                }
            });
        }

        /// <summary>
        /// 加载完方案后触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void FormNewProcessWizard_OnLoadFinished(object sender, EventArgs e)
        {
            await Task.Run(async() =>
            {
                try
                {
                    if (Solution.Instance.GlobalSignal == null || Solution.Instance.GlobalSignal.ReadySignals == null)
                        return;
                    foreach (var signal in Solution.Instance.GlobalSignal.ReadySignals)
                    {
                        if (string.IsNullOrEmpty(signal.DeviceName) || string.IsNullOrEmpty(signal.Address))
                            continue;
                        var device = Solution.Instance.AllDevices.Find(r => r.DevName == signal.DeviceName);
                        if (device == default(IDevice))
                            continue;
                        else
                        {
                            if (device is IPlc plc)
                            {
                                if (!plc.IsConnect)
                                {
                                    LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})未连接，视觉准备信号发送失败！", true);
                                    continue;
                                }
                                switch (signal.Type)
                                {
                                    case "布尔类型":
                                        await plc.WriteBoolAsync(signal.Address, signal.Value.Equals("True", StringComparison.OrdinalIgnoreCase) || signal.Value != "0");
                                        break;
                                    case "整数类型":
                                        await plc.WriteIntAsync(signal.Address, int.Parse(signal.Value));
                                        break;
                                    case "字符串类型":
                                        await plc.WriteStringAsync(signal.Address, signal.Value);
                                        break;
                                    default:
                                        LogHelper.AddLog(MsgLevel.Exception, $"不支持的信号类型！", true);
                                        break;
                                }
                            }
                            else if (device is IModbus modbus)
                            {
                                if (!modbus.IsConnect)
                                {
                                    LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})未连接，视觉准备信号发送失败！", true);
                                    continue;
                                }
                                switch (signal.Type)
                                {
                                    case "布尔类型":
                                        await modbus.WriteSingleCoilAsync(ushort.Parse(signal.Address), signal.Value.Equals("True", StringComparison.OrdinalIgnoreCase) || signal.Value != "0");
                                        break;
                                    case "整数类型":
                                        await modbus.WriteSingleRegisterAsync(ushort.Parse(signal.Address), ushort.Parse(signal.Value));
                                        break;
                                    case "字符串类型":
                                        await modbus.WriteMultipleRegistersAsync(ushort.Parse(signal.Address), StringToHoldingRegisters(signal.Value));
                                        break;
                                    default:
                                        LogHelper.AddLog(MsgLevel.Exception, $"不支持的信号类型！", true);
                                        break;
                                }
                            }
                            else
                            {
                                LogHelper.AddLog(MsgLevel.Exception, $"设备({signal.DeviceName})不支持全局信号功能！", true);
                                continue;
                            }
                        }
                    }
                    LogHelper.AddLog(MsgLevel.Info, "视觉准备信号已发送！", true);
                }
                catch (Exception ex)
                {
                    LogHelper.AddLog(MsgLevel.Exception, $"准备信号发送异常！原因:{ex.Message}", true);
                }
            });
        }
        public static ushort[] StringToHoldingRegisters(string input)
        {
            // 确保输入不为空
            if (string.IsNullOrEmpty(input))
                return new ushort[0];

            // 将字符串转换为字节数组，这里使用 ASCII 编码
            byte[] bytes = Encoding.ASCII.GetBytes(input);

            // 计算所需的寄存器数量，向上取整以确保有足够的空间存放最后一个字符（如果字符串长度为奇数）
            int registerCount = (bytes.Length + 1) / 2;

            // 创建目标数组
            ushort[] registers = new ushort[registerCount];

            for (int i = 0; i < bytes.Length; i += 2)
            {
                // 第一个字节
                byte highByte = bytes[i];
                // 如果存在第二个字节
                byte lowByte = (i + 1) < bytes.Length ? bytes[i + 1] : (byte)0;

                // 合并两个字节成为一个 16 位整数，并添加到结果数组中
                registers[i / 2] = (ushort)((highByte << 8) | lowByte);
            }

            return registers;
        }
        /// <summary>
        /// dataGridView1 离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DataGridView1_Leave(object sender, EventArgs e)
        {
            if (Solution.Instance.GlobalSignal == null)
                Solution.Instance.GlobalSignal = new GlobalSignal();
            Solution.Instance.GlobalSignal.ReadySignals = GetConfig(dataGridView1);
        }
        /// <summary>
        /// dataGridView2 离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DataGridView2_Leave(object sender, EventArgs e)
        {
            if (Solution.Instance.GlobalSignal == null)
                Solution.Instance.GlobalSignal = new GlobalSignal();
            Solution.Instance.GlobalSignal.StopSignals = GetConfig(dataGridView2);
        }

        private void FormGlobalSignal_Shown(object sender, EventArgs e)
        {
            #region 设置表格样式

            // 设置列标题的高度
            dataGridView1.ColumnHeadersHeight = 64;
            dataGridView2.ColumnHeadersHeight = 64;
            // 设置行的高度
            dataGridView1.RowTemplate.Height = 36;
            dataGridView2.RowTemplate.Height = 36;
            //等分表格列宽
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            #endregion

            #region 初始化表格数据
            try
            {
                var deviceNames = Solution.Instance.AllDevices.Select(d => d.DevName).ToList();
                deviceColumn1.DataSource = deviceNames;
                deviceColumn2.DataSource = deviceNames;
                LoadConfig(dataGridView1, Solution.Instance.GlobalSignal.ReadySignals);
                LoadConfig(dataGridView2, Solution.Instance.GlobalSignal.StopSignals);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"加载全局信号配置异常！原因:{ex.Message}", true);
            }
            #endregion
        }

        /// <summary>
        /// 加载配置到表格中
        /// </summary>
        /// <param name="infos"></param>
        public void LoadConfig(DataGridView dataGridView, List<SingleGlobalSignalSettings> infos)
        {
            dataGridView.Rows.Clear();
            if (infos == null) return;
            foreach (var info in infos)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);

                row.Cells[0].Value = info.DeviceName;
                row.Cells[1].Value = info.Address;
                row.Cells[2].Value = info.Type;
                row.Cells[3].Value = info.Value;

                dataGridView.Rows.Add(row);
            }
        }
        /// <summary>
        /// 获取表格的数据
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public List<SingleGlobalSignalSettings> GetConfig(DataGridView dataGridView)
        {
            dataGridView.EndEdit();
            var infos = new List<SingleGlobalSignalSettings>();
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;
                    var info = new SingleGlobalSignalSettings(
                        row.Cells[0].Value?.ToString() ?? string.Empty,
                        row.Cells[1].Value?.ToString() ?? string.Empty,
                        row.Cells[2].Value?.ToString() ?? string.Empty,
                        row.Cells[3].Value?.ToString() ?? string.Empty
                    );
                    infos.Add(info);
                }
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
                return null;
            }
            return infos;
        }

        private void FormGlobalSignal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Solution.Instance.GlobalSignal == null)
                Solution.Instance.GlobalSignal = new GlobalSignal();
            Solution.Instance.GlobalSignal.ReadySignals = GetConfig(dataGridView1);
            Solution.Instance.GlobalSignal.StopSignals = GetConfig(dataGridView2);
        }
    }
    /// <summary>
    /// 全局信号配置类
    /// </summary>
    public class GlobalSignal
    {
        /// <summary>
        /// 准备信号
        /// </summary>
        public List<SingleGlobalSignalSettings> ReadySignals { get; set; } = new List<SingleGlobalSignalSettings>();
        /// <summary>
        /// 停止信号
        /// </summary>
        public List<SingleGlobalSignalSettings> StopSignals { get; set; } = new List<SingleGlobalSignalSettings>();
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GlobalSignal() { }
    }

    /// <summary>
    /// 单条信号
    /// </summary>
    public class SingleGlobalSignalSettings
    {
        public string DeviceName { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public SingleGlobalSignalSettings(string devName, string address, string type, string value)
        {
            DeviceName = devName;
            Address = address;
            Type = type;
            Value = value;
        }
    }
}
