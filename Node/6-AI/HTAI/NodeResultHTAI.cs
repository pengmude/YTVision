using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using YTVisionPro.Forms.ResultView;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeResultHTAI : INodeResult
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeStatus Status { get; set; }
        public long RunTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NodeRunStatusCode RunStatusCode { get; set; }
        /// <summary>
        /// AI检测结果数据
        /// </summary>
        public ResultViewData ResultData { get; set; }
        /// <summary>
        /// 渲染图
        /// </summary>
        public Bitmap RenderImage { get; set; }
    }

    /// <summary>
    /// AI/传统算法检出结果
    /// </summary>
    internal class ResultViewData
    {
        public bool IsAllOk { get { return SingleRowDataList.TrueForAll(sr => sr.IsOk); } }
        public List<SingleResultViewData> SingleRowDataList = new List<SingleResultViewData>();
    }
}
