using Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YTVisionPro.Device.TCP
{
    /// <summary>
    /// TCP客户端
    /// </summary>
    internal class TCPClient : ITcpDevice
    {
        private TcpClient _client;
        public bool IsConnect {  get; set; }
        public TcpParam TcpParam { get; set; }
        public string DevName { get; set; }
        public string UserDefinedName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.TcpClient;
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(TCPClient).FullName;

        public event EventHandler<bool> ConnectStatusEvent;

        #region 反序列化使用

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public TCPClient() { }

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

        public TCPClient(TcpParam tcpParam)
        {
            TcpParam = tcpParam;
            DevName = tcpParam.DevName;
            UserDefinedName = tcpParam.UserDefinedName;
        }
        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect()
        {
            try
            {
                if (TcpParam == null) throw new Exception($"TCP客户端{DevName}连接到服务器的参数为null");
                _client = new TcpClient(TcpParam.IP, TcpParam.Port);
                if (_client.Connected)
                {
                    IsConnect = true;
                    LogHelper.AddLog(MsgLevel.Info, $"客户端（{DevName}）已连接！", true);
                    ConnectStatusEvent?.Invoke(this, true);
                }
                else
                {
                    IsConnect = false;
                    ConnectStatusEvent?.Invoke(this, false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Disconnect()
        {
            _client.Close();
            IsConnect = false;
            ConnectStatusEvent?.Invoke(this, false);
        }
        /// <summary>
        /// 客户端发送数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task SendMessage(string message)
        {
            if (!_client.Connected)
            {
                throw new InvalidOperationException("没有连接到服务器！");
            }
            LogHelper.AddLog(MsgLevel.Info, $"客户端（{UserDefinedName}）[发送]-->“{message}”", true);
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _client.GetStream().WriteAsync(data, 0, data.Length);
        }
        /// <summary>
        /// 客户端接收服务器的响应数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> ReceiveMessage()
        {
            if (!_client.Connected)
            {
                throw new InvalidOperationException("没有连接到服务器！");
            }

            byte[] buffer = new byte[1024];
            NetworkStream stream = _client.GetStream();

            // 创建一个取消令牌源，设置5秒超时
            using (var cts = new CancellationTokenSource(5000))
            {
                try
                {
                    // 创建一个读取任务
                    var readTask = stream.ReadAsync(buffer, 0, buffer.Length, cts.Token);

                    // 等待读取任务完成或超时
                    var completedTask = await Task.WhenAny(readTask, Task.Delay(5000, cts.Token));

                    if (completedTask == readTask)
                    {
                        // 读取任务完成
                        int bytesRead = await readTask; // 获取读取结果

                        if (bytesRead > 0)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            LogHelper.AddLog(MsgLevel.Info, $"客户端（{UserDefinedName}）[接收]-->“{message}”", true);
                            return message;
                        }
                        else
                        {
                            // 如果没有读取到任何数据，返回空字符串或处理这种情况
                            return "null";
                        }
                    }
                    else
                    {
                        // 超时发生
                        cts.Cancel(); // 取消读取任务
                        LogHelper.AddLog(MsgLevel.Warn, $"客户端（{UserDefinedName}）读取超时", true);
                        return "null"; // 或者返回一个自定义的超时消息
                    }
                }
                catch (OperationCanceledException)
                {
                    // 读取任务被取消
                    LogHelper.AddLog(MsgLevel.Warn, $"客户端（{UserDefinedName}）读取超时", true);
                    return "null"; // 或者返回一个自定义的超时消息
                }
                catch (IOException ex)
                {
                    // 处理其他可能的IO异常
                    LogHelper.AddLog(MsgLevel.Exception, $"客户端（{UserDefinedName}）读取错误: {ex.Message}", true);
                    throw;
                }
                catch (Exception ex)
                {
                    // 处理其他可能的异常
                    LogHelper.AddLog(MsgLevel.Exception, $"客户端（{UserDefinedName}）读取错误: {ex.Message}", true);
                    throw;
                }
            }
        }
    }
}
