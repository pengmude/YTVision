using System;
using System.Collections.Generic;
using System.Drawing;
using YTVisionPro.Forms.ResultView;

namespace YTVisionPro.Node.AI.HTAI
{
    internal class NodeResultHTAI : INodeResult
    {
        public bool Success { get; set ; }
        public long RunTime { get; set; }
        NodeRunStatusCode INodeResult.RunStatusCode { get; set; }
        /// <summary>
        /// AI检测结果数据
        /// </summary>
        public AiResult ResultData { get; set; }
        /// <summary>
        /// 渲染图
        /// </summary>
        public Bitmap RenderImage { get; set; }
    }

    internal class AiResult
    {
        public bool IsAllOk;
        public List<SingleResultViewData> DeepStudyResult = new List<SingleResultViewData>();
    }
}
