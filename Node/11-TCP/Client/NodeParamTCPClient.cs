using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.TCP;

namespace YTVisionPro.Node.TCP.Client
{
    internal class NodeParamTCPClient : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public ITcpDevice Device { get; set; }
        /// <summary>
        /// 是否等待服务器响应
        /// </summary>
        public bool IsWaitingForResponse {  get; set; }
        /// <summary>
        /// 是否需要条件发起请求
        /// </summary>
        public bool NeedsCondition { get; set; }
        /// <summary>
        /// 不需要条件时发送的内容
        /// </summary>
        public string NoConditionContent { get; set; }
        /// <summary>
        /// 订阅节点的文本1
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅节点的文本2
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 条件为true发送的内容
        /// </summary>
        public string SendContentTrue { get; set; }
        /// <summary>
        /// 条件为false发送的内容
        /// </summary>
        public string SendContentFalse { get; set; }
    }
}
