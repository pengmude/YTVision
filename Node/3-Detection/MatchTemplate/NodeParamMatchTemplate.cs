namespace YTVisionPro.Node._3_Detection.MatchTemplate
{
    internal class NodeParamMatchTemplate : INodeParam
    {
        /// <summary>
        /// 模版图片文件名称
        /// </summary>
        public string TemplateFileName {  get; set; }
        /// <summary>
        /// 订阅节点的名称
        /// </summary>
        public string Text1 { get; set; }
        /// <summary>
        /// 订阅节点的属性
        /// </summary>
        public string Text2 { get; set; }
        /// <summary>
        /// 图像缩小倍数
        /// </summary>
        public float Scale { get; set; }
        /// <summary>
        /// 匹配最小得分
        /// </summary>
        public float MinScore { get; set; }
    }
}
