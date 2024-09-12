using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.DataShow
{
    internal class NodeParamDataShow : INodeParam
    {
        public AiResult AiResultData;
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
