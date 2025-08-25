using System;
using System.Reflection;
using System.Windows.Forms;
using Logger;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision
{

    internal static class Program
    {
        public static FormMain MainForm { get; private set; } = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // HslCommunication通信库授权
            if (!HslCommunication.Authorization.SetAuthorizationCode("d8868ab9-4494-4056-98c6-b669e2434e25"))
            {
                MessageBoxTD.Show("HslCommunication通信库授权失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string solutionPath = null;

            if (args.Length > 0)
            {
                solutionPath = args[0];
                if (solutionPath.EndsWith(".Sol", StringComparison.OrdinalIgnoreCase))
                {
                    MainForm = new FormMain(solutionPath);
                    // 路径合法，传递给主窗体
                    Application.Run(MainForm);
                    return;
                }
            }
            MainForm = new FormMain(solutionPath);
            Application.Run(MainForm);
        }
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

            return assembly.GetName().Version.ToString();
        }
    }
}
