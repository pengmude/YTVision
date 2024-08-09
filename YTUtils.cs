using System.Diagnostics;
using System.Reflection;

namespace YTVisionPro
{
    internal class YTUtils
    {
    }


    /// <summary>
    /// 运行模式
    /// 在线自动模式（需要与相机、光源、PLC等设备通信，让“读取采集信号->开始采集->检测识别->发送结果”流程自动循环执行）
    /// 在线点检模式（需要与相机、光源、PLC等设备通信，让“读取采集信号->开始采集->检测识别->发送结果”流程通过用户在界面点击后单次执行）
    /// 离线自动模式（不需要连接相机、光源、PLC等设备，让“读取本地图片->检测识别”流程自动执行）
    /// 离线点检模式（不需要连接相机、光源、PLC等设备，让“读取本地图片->检测识别”流程自动执行）
    /// </summary>
    enum RunMode
    {
        ONLINE_AUTO,    // 在线自动
        ONLINE_JOG,     // 在线点检
        OFFLINE_AUTO,   // 离线自动
        OFFLINE_JOG     // 离线点检
    }

    /// <summary>
    /// 错误码定义
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 无错误
        /// </summary>
        NONE = 0x0000,

        /// <summary>
        /// 未知的错误
        /// </summary>
        UNKNOWN_ERROR = 0x0001,

        /// <summary>
        /// 默认的错误
        /// </summary>
        DEFAULT_ERROR = 0x0002,

        /// <summary>
        /// 文件不存在
        /// </summary>
        FILE_NOT_EXISTS = 0x0003,

        /// <summary>
        /// 无法正确解析方案文件格式
        /// </summary>
        INCORRECT_SOL_FORMAT = 0x0004,

        /// <summary>
        /// 方案加载失败
        /// </summary>
        LOAD_SOL_FAIL = 0x0005,


        #region 模块的错误码



        #endregion
    }

    public struct ProcessResult
    {
        public bool IsSuccess;

    }
}


namespace VersionInfo
{
    class VersionInfo
    {
        public static string GetExeVer()
        {
            // 获取当前程序集的信息
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取程序集的版本信息
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            // 输出版本信息
            //MessageBox.Show($"Assembly Informational Version: {assembly.GetName().Version}\nFile Version: {fvi.FileVersion}\n" +
            //    $"Product Version: {fvi.ProductVersion}");

            return assembly.GetName().Version.ToString();
        }
    }
}

