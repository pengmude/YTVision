using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTVisionPro.Forms.ResultView;
using YTVisionPro.Node.AI.HTAI;

namespace YTVisionPro.Node.Tool.ResultSummarize
{
    internal class NodeParamSummarize : INodeParam
    {
        public AiResult AiResult;

        public AiResult DetectResult;
    }
}
