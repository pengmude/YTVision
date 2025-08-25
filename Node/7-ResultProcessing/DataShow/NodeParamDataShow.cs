using System.Collections.Generic;
using Newtonsoft.Json;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.DataShow
{
    public class NodeParamDataShow : INodeParam
    {
        public string Text1;
        public string Text2;
    }

    public class DataShowData
    {
        public string TabPageName;
        public AlgorithmResult AiResultData;
        public DataShowData(string nodename, AlgorithmResult airesult)
        {
            TabPageName = nodename;
            AiResultData = airesult;
        }
    }
}
