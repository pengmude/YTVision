using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.ResultProcessing.DataShow
{
    internal class NodeParamDataShow : INodeParam
    {
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
