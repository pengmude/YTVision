﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YTVisionPro.Device.Modbus;
using YTVisionPro.Device.TCP;

namespace YTVisionPro.Node.TCP.Server
{
    internal class NodeParamTCPServer : INodeParam
    {
        [JsonConverter(typeof(PolyConverter))]
        public ITcpDevice Sever { get; set; }
        /// <summary>
        /// 响应的客户端
        /// </summary>
        public string ClientIP { get; set; }
        /// <summary>
        /// 是否需要条件响应客户端
        /// </summary>
        public bool NeedsCondition { get; set; }
        /// <summary>
        /// 不需要条件时响应给客户端的内容
        /// </summary>
        public string NoConditionContent { get; set; }
        /// <summary>
        /// 订阅节点的文本1
        /// </summary>
        public string Text1 {  get; set; }
        /// <summary>
        /// 订阅节点的文本2
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 条件为true响应给客户端的内容
        /// </summary>
        public string ResponseContentTrue { get; set; }
        /// <summary>
        /// 条件为false响应给客户端的内容
        /// </summary>
        public string ResponseContentFalse { get; set; }
    }
}