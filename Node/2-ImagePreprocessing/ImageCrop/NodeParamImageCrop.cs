using Newtonsoft.Json;
using System.Collections.Generic;
using TDJS_Vision.Forms.ShapeDraw;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._2_ImagePreprocessing.ImageCrop
{
    public class NodeParamImageCrop : INodeParam
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
        /// ROIs（反序列化用）
        /// </summary>
        [JsonConverter(typeof(ROIListConverter<ROI>))]
        public List<ROI> ROIs { get; set; }
        
    }
}
