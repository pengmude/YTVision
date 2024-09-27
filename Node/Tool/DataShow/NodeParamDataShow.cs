using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.DataShow
{
    internal class NodeParamDataShow : INodeParam
    {
        public AiResult AiResultData;
        public string Text1;
        public string Text2;
    }

    internal class DatashowData
    {
        public string TabPageName;
        public AiResult AiResultData;
        public DatashowData(string nodename, AiResult airesult)
        {
            TabPageName = nodename;
            AiResultData = airesult;
        }
    }
}
