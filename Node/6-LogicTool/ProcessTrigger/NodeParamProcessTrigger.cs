
using Newtonsoft.Json;

namespace TDJS_Vision.Node._6_LogicTool.ProcessTrigger
{
    public class NodeParamProcessTrigger : INodeParam
    {
        public bool UseOkOrNg { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string OKProcessName { get; set; }
        public string NGProcessName { get; set; }
        public string ProcessName { get; set; }
    }
}
