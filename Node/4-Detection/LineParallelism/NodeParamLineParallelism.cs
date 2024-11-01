using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTVisionPro.Node._4_Detection.DetectionLineParallelism
{
    internal class NodeParamLineParallelism : INodeParam
    {
        [JsonIgnore]
        /// <summary>
        /// 两直线端点集合
        /// </summary>
        public List<Point[]> LinePoint = new List<Point[]>();

        /// <summary>
        /// 订阅的节点名称（反序列化用）
        /// </summary>
        public string Text1 { get; set; }

        /// <summary>
        /// 订阅的节点结果属性名（反序列化用）
        /// </summary>
        public string Text2 { get; set; }
    }
}
