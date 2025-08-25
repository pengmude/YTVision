using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TDJS_Vision.Node._3_Detection.TDAI;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageDraw
{
    public class NodeParamImageDraw : INodeParam
    {
        [JsonIgnore]
        public AlgorithmResult AiResultData;
        public string Text1;
        public string Text2;
        public string Text3;
        public string Text4;
        public int FontSize;
        public int LineWidth;
        [JsonConverter(typeof(StringEnumConverter))]
        public TextLoc TextLoc;
    }

    public class DatashowData
    {
        public string TabPageName;
        public AlgorithmResult AiResultData;
        public DatashowData(string nodename, AlgorithmResult airesult)
        {
            TabPageName = nodename;
            AiResultData = airesult;
        }
    }
    /// <summary>
    /// 文字绘制位置
    /// </summary>
    public enum TextLoc
    {
        LeftUp,//左上
        RightDown,//右下
    }
}
