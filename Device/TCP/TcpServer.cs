using Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public DevType DevType { get; set; } = DevType.TcpServer;
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceBrand Brand { get; set; } = DeviceBrand.Unknow;
        public string ClassName { get; set; } = typeof(TCPServer).FullName;
        [JsonIgnore]
        public Dictionary<string, TcpClient> Ip2TcpClientDic = new Dictionary<string, TcpClient>();
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
                LogHelper.AddLog(MsgLevel.Exception, $"{ex.Message}", true);
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
                LogHelper.AddLog(MsgLevel.Info, $"服务器（{DevName}）已启动！", true);
                IsConnect = true;
                ConnectStatusEvent?.Invoke(this, true);
                Task.Run(() => ListenForClients());
            }
            catch (Exception ex)
            {
                IsConnect = false;
                ConnectStatusEvent?.Invoke(this, false);
                LogHelper.AddLog(MsgLevel.Warn, $"服务器（{DevName}）已停止运行！原因：{ex.Message}", true);
                throw;
            }
        }
        /// <summary>
        /// 服务器停止
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (_server != null)
                    _server.Stop();
                IsConnect = false;
                ConnectStatusEvent?.Invoke(this, false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void ListenForClients()
        {
            try
            {
                while (IsConnect)
                {
                    var client = await _server.AcceptTcpClientAsync();
                    Ip2TcpClientDic.Add(GetRemoteIP(client), client);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        /// <summary>
        /// 响应结果给客户端（发送结果）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessge2Client(TcpClient client, string message)
        {
            if(client == null) throw new ArgumentNullException("客户端对象为空异常！");
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;
            // 返回结果给客户端
            byte[] msg = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(msg, 0, msg.Length);
            LogHelper.AddLog(MsgLevel.Info, $"服务器（{UserDefinedName}）[响应]-->“{message}”", true);
        }

        public string GetRemoteIP(TcpClient cln)
        {
            string ip = cln.Client.RemoteEndPoint.ToString().Split(':')[0];
            return ip;
        }

        public int GetRemotePort(TcpClient cln)
        {
            string temp = cln.Client.RemoteEndPoint.ToString().Split(':')[1];
            int port = Convert.ToInt32(temp);
            return port;
        }
    }
}


