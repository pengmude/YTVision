namespace YTVisionPro.Modules
{
    internal interface IModuleResult
    {
        /// <summary>
        /// 模块是否执行成功
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// 模块执行错误码
        /// </summary>
        ErrorCode ErrorCode { get; }
    }
}
