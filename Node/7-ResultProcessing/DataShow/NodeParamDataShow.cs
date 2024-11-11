using Newtonsoft.Json;
using YTVisionPro.Node._3_Detection.HTAI;

namespace YTVisionPro.Node._7_ResultProcessing.DataShow
{
    internal class NodeParamDataShow : INodeParam
    {
        [JsonIgnore]
        public ResultViewData AiResultData;
        public string Text1;
        public string Text2;
    }

    internal class DatashowData
    {
        public string TabPageName;
        public ResultViewData AiResultData;
        public DatashowData(string nodename, ResultViewData airesult)
        {
            TabPageName = nodename;
            AiResultData = airesult;
        }
    }
}
