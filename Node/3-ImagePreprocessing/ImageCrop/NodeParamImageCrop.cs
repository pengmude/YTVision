using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YTVisionPro.Node.ImageROIEditControl;

namespace YTVisionPro.Node.ImagePreprocessing.ImageCrop
{
    internal class NodeParamImageCrop : INodeParam
    {
        [JsonIgnore]
        /// <summary>
        /// 裁剪后的图像
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// 订阅的节点名称（反序列化用）
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// 订阅的节点结果属性名（反序列化用）
        /// </summary>
        public string Text2 { get; set; }

        /// <summary>
        /// ROI（反序列化用）
        /// </summary>
        [JsonConverter(typeof(PolyConverter))]
        public ROI ROI { get; set; }
    }
}
