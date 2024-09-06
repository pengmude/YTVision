
namespace YTVisionPro.Forms.ResultView
{
    internal class SingleResultViewData
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
    }

}
