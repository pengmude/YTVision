using Logger;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Node;


namespace YTVisionPro.Forms.PLCMonitor
{ 
    internal class PlcMonitor
    {
        public ConcurrentDictionary<string, MonitorTask> Tasks = new ConcurrentDictionary<string, MonitorTask>();
        private readonly object _lock = new object();
        private CancellationTokenSource _mainCancellationTokenSource = new CancellationTokenSource();
        public event EventHandler<string> SendMessageEvent;
        private IPlc _plc = null;

        /// <summary>
        /// 轮询时间ms
        /// </summary>
        public int PollTime { get; set; } = 180;

        public void SetPlc(IPlc plc)
        {
            _plc = plc;
        }

        /// <summary>
        /// 添加一条监听任务，通常以寄存器类型区分
        /// </summary>
        /// <param name="RegisterType"></param>
        /// <param name="addressRange"></param>
        public void AddMonitorTask(string RegisterType, AddressRange addressRange)
        {
            MonitorTask task = new MonitorTask
            {
                AddressRange = addressRange,
                CancellationTokenSource = new CancellationTokenSource()
            };

            Tasks.TryAdd(RegisterType, task);
        }

        /// <summary>
        /// 启动监听
        /// </summary>
        public void Start()
        {
            Reset();
            foreach (var pair in Tasks)
            {
                Task.Run(() =>
                {
                    MonitorTaskAsync(pair.Key, pair.Value, _mainCancellationTokenSource.Token);
                });
            }
        }

        private void Reset()
        {
            foreach (var pair in Tasks)
            {
                pair.Value.CancellationTokenSource = new CancellationTokenSource();
            }
            _mainCancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// 停止所有监听
        /// </summary>
        public void StopAllMonitoring()
        {
            foreach (var task in Tasks.Values)
            {
                task.CancellationTokenSource.Cancel();
            }
            _mainCancellationTokenSource.Cancel();
        }

        /// <summary>
        /// 单条监听任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="mainCancellationToken"></param>
        /// <returns></returns>
        private void MonitorTaskAsync(string RegisterType, MonitorTask task, CancellationToken mainCancellationToken)
        {
            if(_plc == null)
            {
                MessageBox.Show("PLC监听对象参数为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(!_plc.IsOpen())
            {
                _plc.Connect();
                //MessageBox.Show("PLC尚未连接！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }

            var cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(mainCancellationToken, task.CancellationTokenSource.Token).Token;

            try
            {
                ulong i = 0, j = 0;
                while (!cancellationToken.IsCancellationRequested)
                {

                    long time = 0;
                    lock (_lock)
                    {
                        string address = task.AddressRange.Prefix + task.AddressRange.StartAddress.ToString();
                        ushort length = (ushort)task.AddressRange.Length;
                        Stopwatch stopwatch = new Stopwatch();
                        var content = _plc.ReadPLCData(address, Hardware.PLC.DataType.Bytes, length);
                        //_plc.WritePLCData(address, true);
                        time = GetRunTime(startTime);
                    }

                    //不同的寄存器
                    if(RegisterType == "D")
                    {

                        lock (_lock)
                            LogHelper.AddLog(MsgLevel.Info, $"PLC监听[D类型]第[{++i}]次......读取耗时：{time}", true);
                    }
                    else if (RegisterType == "R")
                    {

                        lock (_lock)
                            LogHelper.AddLog(MsgLevel.Info, $"PLC监听[R类型]第[{++j}]次......读取耗时：{time}", true);
                    }
                    // 处理结果，例如记录日志或更新UI
                    //SendMessageEvent?.Invoke(this, $"数据读取结果：{result.Address}: {result.Value}");

                    // 轮询等待时间
                    Thread.Sleep(PollTime);
                }
            }
            catch (TaskCanceledException)
            {
                // 任务被取消
            }
            finally
            {
                // 清理资源
            }
        }
        private long GetRunTime(DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            return (long)elapsed.TotalMilliseconds;
        }
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        Bool,
        Int32,
        Float,
        String
    }

    /// <summary>
    /// 连续监听的地址
    /// </summary>
    public class AddressRange
    {
        private int _startAdress;
        private int _endAdress;
        private int _length;

        public string Prefix {  get; set; }
        public int StartAddress { get => _startAdress; set { _startAdress = value; _length = _endAdress - _startAdress + 1; } }
        public int EndAddress { get => _endAdress; set { _endAdress = value; _length = _endAdress - _startAdress + 1; } }
        public int Length { get => _length; }
    }

    /// <summary>
    /// 监听任务
    /// </summary>
    public class MonitorTask
    {
        public AddressRange AddressRange {  get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }

    /// <summary>
    /// 监听结果
    /// </summary>
    public class MonitorResult
    {
        public string Address { get; set; }
        public DataType DataType { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}