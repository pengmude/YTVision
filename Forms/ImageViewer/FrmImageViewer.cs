using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace YTVisionPro.Forms.ImageViewer
{
    internal partial class FrmImageViewer : DockContent
    {
        /// <summary>
        /// 所有的图像窗口
        /// </summary>
        private static List<FrmSingleImage> _frmSingleImages = new List<FrmSingleImage>();

        /// <summary>
        /// 窗口布局配置
        /// </summary>
        private readonly string DockPanelConfig = Application.StartupPath + "\\DockPanelImageWindows.config";

        /// <summary>
        /// 反序列化DockContent代理
        /// </summary>
        private DeserializeDockContent DeserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        /// <summary>
        /// 当前显示的图像窗口数量
        /// </summary>
        public int CurWindowsNum = 0;

        public FrmImageViewer()
        {
            InitializeComponent();
            InitWindows();
            SetWindowNum(4);
            InitDockPanel();
            FormMain.SaveDockPanelEvent += SaveDockPanelEventHandler;
        }

        /// <summary>
        /// 加载窗口布局
        /// </summary>
        private void InitDockPanel()
        {
            try
            {
                if (File.Exists(DockPanelConfig))
                {
                    // 如果存在，则从配置文件加载布局
                    this.dockPanel1.LoadFromXml(DockPanelConfig, DeserializeDockContent);
                }
                else
                {
                    LoadDefaultDockPanel();
                }
            }
            catch (Exception ex)
            {
                LoadDefaultDockPanel();
            }
        }
        /// <summary>
        /// 默认窗口布局
        /// </summary>
        private void LoadDefaultDockPanel()
        {
            for (int i = 0; i < 6; i++)
            {
                _frmSingleImages[i].Show(dockPanel1, DockState.Document);
            }
        }

        // 处理主窗口触发的保存布局事件
        private void SaveDockPanelEventHandler(object sender, EventArgs e)
        {
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }

        static int count = 0;
        /// <summary>
        /// 配置委托函数
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private static IDockContent GetContentFromPersistString(string persistString)
        {
            if(count == 4)
                return null;
            return _frmSingleImages[count]; ;
        }

        /// <summary>
        /// 初始化所有的窗口列表
        /// </summary>
        private void InitWindows()
        {
            for (int i = 0; i < 16; i++)
            {
                FrmSingleImage imageWin = new FrmSingleImage();
                _frmSingleImages.Add(imageWin);
            }
        }

        /// <summary>
        /// 设置要显示的窗口数量
        /// </summary>
        public void SetWindowNum(int num)
        {
            if(num < 1 || num > 16) { MessageBox.Show("窗口数量最多设置0-16！"); return; }

            for (int i = 0; i < num; i++)
            {
                _frmSingleImages[i].Show(dockPanel1, DockState.Document);
            }
            CurWindowsNum = num;
        }

        private void 窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWindowNum(2);
        }

        private void 窗口ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetWindowNum(4);
        }
    }
}
