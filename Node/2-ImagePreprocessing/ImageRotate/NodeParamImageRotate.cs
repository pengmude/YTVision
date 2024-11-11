namespace YTVisionPro.Node._2_ImagePreprocessing.ImageRotate
{
    internal class NodeParamImageRotate : INodeParam
    {
        /// <summary>
        /// 订阅的节点名称（反序列化用）
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// 订阅的节点结果属性名（反序列化用）
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// 图像旋转的角度
        /// </summary>
        public float Angle { get; set; }
    }
}
