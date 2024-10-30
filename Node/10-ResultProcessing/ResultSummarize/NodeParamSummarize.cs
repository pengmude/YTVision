using Newtonsoft.Json;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ResultProcessing.ResultSummarize
{
    internal class NodeParamSummarize : INodeParam
    {
        /// <summary>
        /// AI检测结果数据
        /// </summary>
        [JsonIgnore]
        public ResultViewData AiResult;
        /// <summary>
        /// 传统算法检测结果数据
        /// </summary>
        [JsonIgnore]
        public ResultViewData NonAiResult;
        // 反序列化还原界面参数用
        public string AiText1;
        public string AiText2;
        public string NonAiText1;
        public string NonAiText2;
    }
}
