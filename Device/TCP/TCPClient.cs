using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;
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
        public DevType DevType { get; set; } = DevType.TcpClient;
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(TcpClient).FullName;

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

        public async Task SendMessage(string message)
        {
            if (!_client.Connected)
            {
                throw new InvalidOperationException("没有连接到服务器！");
            }

            byte[] data = Encoding.UTF8.GetBytes(message);
            await _client.GetStream().WriteAsync(data, 0, data.Length);
        }

        public async Task<string> ReceiveMessage()
        {
            if (!_client.Connected)
            {
                throw new InvalidOperationException("没有连接到服务器！");
            }

            byte[] buffer = new byte[1024];
            int bytesRead = await _client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public void Disconnect()
        {
            _client.Close();
            IsConnect = false;
        }
    }
}
