using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logger
{
    internal partial class LogHelper : UserControl
    {
        static readonly string LogDictory = Environment.CurrentDirectory + @"\Logs\";
        static event EventHandler<LevelAndInfo> LogAddEvent;

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
            Load += LogHelper_Load;
        }

        private void LogHelper_Load(object sender, EventArgs e)
        {
            LogAddEvent += LogHelper_LogAddEvent;
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
            SingleLog singleLog = new SingleLog();
            singleLog.MakeLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), $"{level}", logInfo, filePath, memberName, $"{lineNumber}");
            // 写入本地文件Log
            WriteLog(singleLog);
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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ListBox mySelectBox = sender as ListBox;
            if (mySelectBox.SelectedItem != null)
            {
                string selectInfo = mySelectBox.SelectedItem.ToString();
                LogDetail myLogDetail = new LogDetail(selectInfo);
                myLogDetail.Show();
            }

        }
    }
}
