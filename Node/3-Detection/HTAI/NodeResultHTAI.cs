using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using YTVisionPro.Forms.ResultView;
using System.ComponentModel;

namespace YTVisionPro.Node._3_Detection.HTAI
{
    internal class NodeResultHTAI : INodeResult
    {
        [DisplayName("运行状态")]
        public NodeStatus Status { get; set; }

        [DisplayName("节点耗时")]
        public long RunTime { get; set; }

        [DisplayName("运行状态码")]
        public NodeRunStatusCode RunStatusCode { get; set; }

        [DisplayName("算法结果")]
        public ResultViewData ResultData { get; set; }

        [DisplayName("渲染图")]
        public Bitmap RenderImage { get; set; }

        [DisplayName("是否全部OK")]
        public bool IsAllOk { get; set; }
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
