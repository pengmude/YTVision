using OpenCvSharp;
using System.Collections.Generic;

namespace YTVisionPro.Node._2_ImagePreprocessing.ImageSplit
{
    internal class NodeParamImageSplit : INodeParam
    {
        /// <summary>
        /// 订阅的节点名称（反序列化用）
        /// </summary>
        public string Text1;

        /// <summary>
        /// 订阅的节点结果属性名（反序列化用）
        /// </summary>
        public string Text2;

        /// <summary>
        /// 图像分割行数
        /// </summary>
        public int Rows;

        /// <summary>
        /// 图像分割列数
        /// </summary>
        public int Cols;
        
    }
}
