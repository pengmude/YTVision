using Logger;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Device.TCP
{
    /// <summary>
    /// TCP服务器
    /// </summary>
    internal class TCPServer : ITcpDevice
    {
        private TcpListener _server;
        public TcpParam TcpParam { get; set; }
        public string DevName { get; set; }
        public string UserDefinedName { get; set; }
        public DevType DevType { get; set; } = DevType.TcpServer;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(TCPServer).FullName;

        public bool IsConnect { get; set; }

        public event EventHandler<bool> ConnectStatusEvent;

        #region 反序列化使用

        /// <summary>
        /// 指定反序列化的构造函数
        /// </summary>
        [JsonConstructor]
        public TCPServer() { }

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

        public TCPServer(TcpParam tcpParam)
        {
            TcpParam = tcpParam;
            DevName = tcpParam.DevName;
            UserDefinedName = tcpParam.UserDefinedName;
        }

        /// <summary>
        /// 服务器启动
        /// </summary>
        public void Connect()
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, TcpParam.Port);
                _server.Start();
                IsConnect = true;
                ConnectStatusEvent?.Invoke(this, true);
                Task.Run(() => ListenForClients());
            }
            catch (Exception)
            {
                IsConnect = false;
                ConnectStatusEvent?.Invoke(this, false);
                throw;
            }
        }
        /// <summary>
        /// 服务器停止
        /// </summary>
        public void Disconnect()
        {
            _server.Stop();
            IsConnect = false;
            ConnectStatusEvent?.Invoke(this, false);
        }

        private async void ListenForClients()
        {
            try
            {
                while (IsConnect)
                {
                    var client = await _server.AcceptTcpClientAsync();
                    _ = HandleClient(client); // 使用_忽略返回的任务
                }
            }
            catch (ObjectDisposedException ex)
            {
                LogHelper.AddLog(MsgLevel.Warn, $"服务器（{DevName}）已停止运行！", true);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    LogHelper.AddLog(MsgLevel.Info, $"Tcp服务器（{DevName}）接收到请求数据: " + data, true);

                    // Echo back the received data
                    byte[] msg = Encoding.UTF8.GetBytes(data);
                    await stream.WriteAsync(msg, 0, msg.Length);
                }
            }
        }
    }
}


