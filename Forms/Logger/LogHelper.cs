using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Logger
{
    public partial class LogHelper : UserControl
    {
        static readonly string LogDictory = Environment.CurrentDirectory + @"\Logs\";
        static event EventHandler<LevelAndInfo> LogAddEvent;
        private static readonly object _logFileLocker = new object();
        private static bool OnlyLogException = false; // 是否仅记录异常日志

        #region 日志等级和日志内容结构体
        struct LevelAndInfo
        {
            public MsgLevel Level { get; set; }
            public string Info { get; set; }
        }
        #endregion

        #region 单条日志结构体
        public struct SingleLog
        {
            public string LogTime;
            public string LogLevel;
            public string LogInfo;
            public string LogFile;
            public string LogFunc;
            public string LogLine;
            public string Separator;
            public string Ex;
            public void MakeLog(string time, string level, string info, string file, string func, string line, string sep = "    ", string ex = "")
            {
                LogTime = time;
                LogLevel = level;
                LogInfo = info;
                LogFile = file;
                LogFunc = func;
                LogLine = line;
                Separator = sep;
                Ex = ex;
            }
            public string GetString()
            {
                if (Separator == string.Empty)
                    Separator = "    ";
                return LogTime + Separator + LogLevel + Separator + LogInfo + Separator + LogFile + Separator + LogFunc + Separator + LogLine + Separator + Ex;
            }
        }
        #endregion

        public LogHelper()
        {
            InitializeComponent();
            LogAddEvent += LogHelper_LogAddEvent;
        }

        public void AdjustListBoxWidth(ListBox listBox)
        {
            if (listBox.Items.Count == 0) return;

            // 获取 Graphics 对象用于测量字符串宽度
            using (Graphics g = listBox.CreateGraphics())
            {
                int maxWidth = 0;

                // 遍历所有项，找到最长的一项
                foreach (var item in listBox.Items)
                {
                    string text = item.ToString();
                    // 使用 MeasureString 测量字符串宽度
                    SizeF size = g.MeasureString(text, listBox.Font);
                    if (size.Width > maxWidth)
                    {
                        maxWidth = (int)size.Width;
                    }
                }

                // 设置 HorizontalExtent 以便显示水平滚动条
                listBox.HorizontalExtent = maxWidth;
            }
        }

        private void LogHelper_LogAddEvent(object sender, LevelAndInfo levelAndInfo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    // 显示到特定日志等级栏
                    switch (levelAndInfo.Level)
                    {
                        case MsgLevel.Debug:
                            ControlListBox(levelAndInfo.Info, listBoxDebug);
                            break;
                        case MsgLevel.Info:
                            ControlListBox(levelAndInfo.Info, listBoxInfo);
                            break;
                        case MsgLevel.Warn:
                            ControlListBox(levelAndInfo.Info, listBoxWarn);
                            break;
                        case MsgLevel.Exception:
                            ControlListBox(levelAndInfo.Info, listBoxExpection);
                            break;
                        case MsgLevel.Fatal:
                            ControlListBox(levelAndInfo.Info, listBoxFatal);
                            break;
                        default:
                            break;
                    }
                    // 同时显示到全部日志栏
                    listBoxAll.Items.Add(levelAndInfo.Info);
                    listBoxAll.SelectedIndex = listBoxAll.Items.Count - 1;
                    listBoxAll.SelectedIndex = -1;    //是否取消选中行
                    // 显示1000条后自动清除UI日志
                    if (listBoxAll.Items.Count > 1000)
                    {
                        listBoxAll.Items.Clear();
                    }
                    Application.DoEvents();
                });
            }
            else
            {
                // 显示到特定日志等级栏
                switch (levelAndInfo.Level)
                {
                    case MsgLevel.Debug:
                        ControlListBox(levelAndInfo.Info, listBoxDebug);
                        break;
                    case MsgLevel.Info:
                        ControlListBox(levelAndInfo.Info, listBoxInfo);
                        break;
                    case MsgLevel.Warn:
                        ControlListBox(levelAndInfo.Info, listBoxWarn);
                        break;
                    case MsgLevel.Exception:
                        ControlListBox(levelAndInfo.Info, listBoxExpection);
                        break;
                    case MsgLevel.Fatal:
                        ControlListBox(levelAndInfo.Info, listBoxFatal);
                        break;
                    default:
                        break;
                }
                // 同时显示到全部日志栏
                listBoxAll.Items.Add(levelAndInfo.Info);
                listBoxAll.SelectedIndex = listBoxAll.Items.Count - 1;
                listBoxAll.SelectedIndex = -1;    //是否取消选中行
                // 显示1000条后自动清除UI日志
                if (listBoxAll.Items.Count > 1000)
                {
                    listBoxAll.Items.Clear();
                }
                Application.DoEvents();
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            else
            {
                ListBox seelctBox = sender as ListBox;
                string logText = seelctBox.Items[e.Index].ToString();
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                if (logText.Contains("Info"))
                {
                    mybsh = Brushes.DodgerBlue;
                }
                else if (logText.Contains("Debug"))
                {
                    mybsh = Brushes.Green;
                }
                else if (logText.Contains("Warn"))
                {
                    mybsh = Brushes.Orange;
                }
                else if (logText.Contains("Exception"))
                {
                    mybsh = Brushes.Red;
                }
                else if (logText.Contains("Fatal"))
                {
                    mybsh = Brushes.Red;
                }
                else
                    mybsh = Brushes.DarkGray;

                // 判断是否是选中项
                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                // 设置背景颜色
                if (isSelected)
                {
                    e.Graphics.FillRectangle(Brushes.MediumTurquoise, e.Bounds); // 选中时背景颜色
                    mybsh = Brushes.White; // 选中时文字颜色
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds); // 默认背景色
                }

                e.DrawFocusRectangle();
                e.Graphics.DrawString(seelctBox.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
            }
        }

        /// <summary>
        /// 添加一条日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="logInfo">日志内容</param>
        /// <param name="isDisplay">日志写入文件的同时是否显示在窗口，true需要界面带日志控件，false仅仅写入日志文件</param>
        /// <param name="filePath">打印出日志的代码文件名</param>
        /// <param name="memberName">日志在哪个函数打印</param>
        /// <param name="lineNumber">日志在代码文件的哪一行</param>
        public static void AddLog(
            MsgLevel level,
            string logInfo,
            bool isDisplay = false,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0
            )
        {
            if (OnlyLogException && (level == MsgLevel.Info || level == MsgLevel.Debug))
                return; // 如果仅记录异常日志，且当前日志不是异常，则不记录
            SingleLog singleLog = new SingleLog();
            singleLog.MakeLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), $"{level}", logInfo, filePath, memberName, $"{lineNumber}");
            // 写入本地文件Log
            lock (_logFileLocker)
            {
                WriteLog(singleLog);
            }
            // 显示到界面的Log
            if (isDisplay)
            {
                string logToShow = singleLog.LogTime + singleLog.Separator + singleLog.LogLevel + singleLog.Separator + singleLog.LogInfo;
                LevelAndInfo levelAndInfo = new LevelAndInfo
                {
                    Level = level,
                    Info = logToShow
                };
                LogAddEvent?.Invoke(null, levelAndInfo);
            }
        }

        private void ControlListBox(string logToShow, ListBox myListBox)
        {
            myListBox.Items.Add(logToShow);
            myListBox.SelectedIndex = myListBox.Items.Count - 1;
            if (myListBox.Items.Count > 300)
            {
                myListBox.Items.Clear();
            }
        }

        private static void WriteLog(SingleLog singleLog)
        {
            string msg = singleLog.GetString();
            if (!Directory.Exists(LogDictory))
            {
                Directory.CreateDirectory(LogDictory);
            }
            string runningLogFileName = LogDictory + DateTime.Now.ToString("yyyyMMdd") + ".log";
            StreamWriter mySW = new StreamWriter(runningLogFileName, true);
            mySW.WriteLine(msg);
            mySW.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        listBoxAll.Items.Clear();
                        break;
                    case 1:
                        listBoxInfo.Items.Clear();
                        break;
                    case 2:
                        listBoxDebug.Items.Clear();
                        break;
                    case 3:
                        listBoxWarn.Items.Clear();
                        break;
                    case 4:
                        listBoxExpection.Items.Clear();
                        break;
                    case 5:
                        listBoxFatal.Items.Clear();
                        break;
                    default:
                        break;
                }

            }
            catch
            {
            }

        }

        private void 打开日志目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 使用 Process.Start 运行文件管理器打开日志文件夹
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = LogDictory,
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }
        /// <summary>
        /// 仅记录异常日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 仅记录异常日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            仅记录异常日志ToolStripMenuItem.Checked = !仅记录异常日志ToolStripMenuItem.Checked;
            OnlyLogException = 仅记录异常日志ToolStripMenuItem.Checked ? true : false;
        }
    }
}
