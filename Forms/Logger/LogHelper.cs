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

        #region ��־�ȼ�����־���ݽṹ��
        struct LevelAndInfo
        {
            public MsgLevel Level { get; set; }
            public string Info { get; set; }
        }
        #endregion

        #region ������־�ṹ��
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
                    // ��ʾ���ض���־�ȼ���
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
                    // ͬʱ��ʾ��ȫ����־��
                    listBoxAll.Items.Add(levelAndInfo.Info);
                    listBoxAll.SelectedIndex = listBoxAll.Items.Count - 1;
                    // ��ʾ1000�����Զ����UI��־
                    if (listBoxAll.Items.Count > 1000)
                    {
                        listBoxAll.Items.Clear();
                    }
                    Application.DoEvents();
                });
            }
            else
            {
                // ��ʾ���ض���־�ȼ���
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
                // ͬʱ��ʾ��ȫ����־��
                listBoxAll.Items.Add(levelAndInfo.Info);
                listBoxAll.SelectedIndex = listBoxAll.Items.Count - 1;
                // ��ʾ1000�����Զ����UI��־
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
        /// ���һ����־
        /// </summary>
        /// <param name="level">��־�ȼ�</param>
        /// <param name="logInfo">��־����</param>
        /// <param name="isDisplay">��־д���ļ���ͬʱ�Ƿ���ʾ�ڴ��ڣ�true��Ҫ�������־�ؼ���false����д����־�ļ�</param>
        /// <param name="filePath">��ӡ����־�Ĵ����ļ���</param>
        /// <param name="memberName">��־���ĸ�������ӡ</param>
        /// <param name="lineNumber">��־�ڴ����ļ�����һ��</param>
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
            // д�뱾���ļ�Log
            WriteLog(singleLog);
            // ��ʾ�������Log
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
