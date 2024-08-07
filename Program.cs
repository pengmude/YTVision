using System;
using System.Windows.Forms;

namespace YTVisionPro
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            //Application.Run(new YTVisionPro.Forms.ImageViewer.FrmImgeView());
            //Application.Run(new FormSystemSettings());
            //Application.Run(new Form4());
            //Application.Run(new FrmImgeView());

            //Application.Run(new FormNewProcessWizard());
        }
    }
}
