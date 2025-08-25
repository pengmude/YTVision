using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace Logger
{
    public class LoggerClass
    {
        /// <summary>
        /// Log队列
        /// </summary>
        public static Queue<LogInfo> logQueue { get; set; } = new Queue<LogInfo>() { };

        public static void WriteLog(string info, bool ShowMsgBox = false)
        {
            logQueue.Enqueue(new LogInfo { message = info, ex = null, logLevel = MsgLevel.Info });
            if (ShowMsgBox)
            {
                MessageBoxTD.Show(info);
            }
        }
        public static void WriteLog(string info, MsgLevel msgLevel, bool ShowMsgBox = false)
        {
            logQueue.Enqueue(new LogInfo { message = info, ex = null, logLevel = msgLevel });
            if (ShowMsgBox)
            {
                MessageBoxTD.Show(info);
            }
        }
        public static void WriteLog(string info, MsgLevel msgLevel, Exception ex, bool ShowMsgBox = false)
        {
            logQueue.Enqueue(new LogInfo { message = info, ex = ex, logLevel = msgLevel });
            if (ShowMsgBox)
            {
                MessageBoxTD.Show(info);
            }
        }
        public static void WriteLog(string info, Exception ex, bool ShowMsgBox = false)
        {
            logQueue.Enqueue(new LogInfo { message = info, ex = ex, logLevel = MsgLevel.Exception });
            if (ShowMsgBox)
            {
                MessageBoxTD.Show(info);
            }
        }
    }

    public class LogInfo
    {
        public string message { get; set; }
        public MsgLevel logLevel { get; set; }
        public Exception ex { get; set; }
    }

    public enum MsgLevel
    {
        /// <summary>
        /// 0.调试信息输出
        /// </summary>
        Debug = 0,
        /// <summary>
        /// 1.业务信息记录
        /// </summary>
        Info = 1,
        /// <summary>
        /// 2.警告提醒（捕获的业务异常）
        /// </summary>
        Warn = 2,
        /// <summary>
        /// 3.发生了异常（捕获的系统异常）
        /// </summary>
        Exception = 3,
        /// <summary>
        /// 4.发生致命异常（未被捕获的异常|捕获的业务逻辑异常）
        /// </summary>
        Fatal = 4
    }
}
