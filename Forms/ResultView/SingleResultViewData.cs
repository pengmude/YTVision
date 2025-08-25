
namespace TDJS_Vision.Forms.ResultView
{
    public class SingleResultViewData
    {
        /// <summary>
        /// 节点名称（AI）
        /// </summary>
        public string NodeName = string.Empty;
        /// <summary>
        /// 类别名称（AI）
        /// </summary>
        public string ClassName = string.Empty;
        /// <summary>
        /// 检测项名称（传统算法）
        /// </summary>
        public string DetectName = string.Empty;
        /// <summary>
        /// 检测结果（共用）
        /// </summary>
        public string DetectResult;
        /// <summary>
        /// OK或NG标记
        /// </summary>
        public bool IsOk;

        public SingleResultViewData() { }

        public SingleResultViewData(string nodeName, string className, string detectName, string detectResult, bool isOk) 
        {
            NodeName = nodeName;
            ClassName = className;
            DetectName = detectName;
            DetectResult = detectResult;
            IsOk = isOk;
        }
    }

}
