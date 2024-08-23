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
        public static int CurWindowsNum = 0;

        /// <summary>
        /// 图像窗口隐藏事件
        /// </summary>
        public event EventHandler HideChangedEvent;

        public FrmImageViewer()
        {
            InitializeComponent();
            InitWindows();
            InitDockPanel();
            CanvasSet.WindowNumChangeEvent += WindowNumChangeEvent;
            CanvasSet.SaveDockPanelEvent += SaveDockPanelEventHandler;
        }

        private void WindowNumChangeEvent(object sender, int e)
        {
            SetWindowNum(e);
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
            for (int i = 0; i < 1; i++)
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
            if (persistString == typeof(FrmSingleImage).ToString())
            {
                return _frmSingleImages[CurWindowsNum++];
            }
            return null;
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
        /// 设置对应数量的画布窗口
        /// </summary>
        /// <param name="num">窗口数</param>
        public void SetWindowNum(int num)
        {
            if (CurWindowsNum > num)
            {
                for (int i = 0; i < CurWindowsNum - num; i++)
                {
                    FrmSingleImage frmSingleImage = _frmSingleImages[CurWindowsNum - 1 - i];

                    frmSingleImage.Hide();

                }
            }

            for (int i = 0; i < num; i++)
            {
                _frmSingleImages[i].Show(dockPanel1, DockState.Document);
            }

            CurWindowsNum = num;

            if (num == 2)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[1].Show(dockPanel1, DockState.Document);
                _frmSingleImages[1].DockHandler.DockPanel.DockRightPortion = 0.5;
            }

            if (num == 3)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.Document);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;

                _frmSingleImages[1].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[1].DockHandler.DockPanel.DockRightPortion = 0.5;
                _frmSingleImages[1].DockHandler.DockPanel.DockTopPortion = 0.5;

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.5;
                _frmSingleImages[2].DockHandler.DockPanel.DockBottomPortion = 0.5;

            }

            if (num == 4)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[0].DockHandler.DockPanel.DockTopPortion = 0.5;


                _frmSingleImages[2].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[2].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[2].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[1].Show(dockPanel1, DockState.DockRight);

                _frmSingleImages[3].Show(dockPanel1, DockState.Document);

                _frmSingleImages[1].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Top, 0);

            }

            if (num == 5)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.6666666;

                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[2].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Left, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
            }

            if (num == 6)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.333333;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.333333;

                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[5].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
            }

            if (num == 7)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[3].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[3].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[3].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[4].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[4].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[4].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[5].Show(dockPanel1, DockState.Document);

                _frmSingleImages[6].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[6].DockHandler.DockPanel.DockRightPortion = 0.25;
                _frmSingleImages[6].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[1].DockTo(_frmSingleImages[5].DockHandler.Pane, DockStyle.Top, 0);
                _frmSingleImages[3].DockTo(_frmSingleImages[4].DockHandler.Pane, DockStyle.Left, 0);

            }

            if (num == 8)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[1].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[1].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[2].Show(dockPanel1, DockState.Document);

                _frmSingleImages[3].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[3].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[4].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[4].DockHandler.DockPanel.DockLeftPortion = 0.25;
                _frmSingleImages[4].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[5].Show(dockPanel1, DockState.Document);

                _frmSingleImages[6].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[6].DockHandler.DockPanel.DockRightPortion = 0.25;
                _frmSingleImages[6].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[7].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[7].DockHandler.DockPanel.DockRightPortion = 0.25;
                _frmSingleImages[7].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[1].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Left, 0);
                _frmSingleImages[5].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[6].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);

            }

            if (num == 9)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.333333;
                _frmSingleImages[0].DockHandler.DockPanel.DockBottomPortion = 0.333333;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.333333;
                _frmSingleImages[2].DockHandler.DockPanel.DockBottomPortion = 0.333333;

                _frmSingleImages[3].Show(_frmSingleImages[0].Pane, DockAlignment.Bottom, 0.6666);

                _frmSingleImages[4].Show(_frmSingleImages[1].Pane, DockAlignment.Bottom, 0.6666);

                _frmSingleImages[5].Show(_frmSingleImages[2].Pane, DockAlignment.Bottom, 0.6666);

                _frmSingleImages[6].Show(_frmSingleImages[3].Pane, DockAlignment.Bottom, 0.5);

                _frmSingleImages[7].Show(_frmSingleImages[4].Pane, DockAlignment.Bottom, 0.5);

                _frmSingleImages[8].Show(_frmSingleImages[5].Pane, DockAlignment.Bottom, 0.5);

            }

            if (num == 10)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.2;

                _frmSingleImages[5].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[5].DockHandler.DockPanel.DockLeftPortion = 0.2;

                _frmSingleImages[2].Show(dockPanel1, DockState.Document);

                _frmSingleImages[4].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Right, 0);
                _frmSingleImages[3].DockTo(_frmSingleImages[4].DockHandler.Pane, DockStyle.Left, 0);
                _frmSingleImages[2].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Right, 0);
                _frmSingleImages[6].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[7].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[8].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[9].DockTo(_frmSingleImages[4].DockHandler.Pane, DockStyle.Bottom, 0);
            }

            if (num == 11)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[0].DockHandler.DockPanel.DockBottomPortion = 0.25;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[4].Show(_frmSingleImages[0].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[5].Show(_frmSingleImages[1].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[6].Show(_frmSingleImages[2].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[7].Show(_frmSingleImages[3].Pane, DockAlignment.Bottom, 0.6666);

                _frmSingleImages[8].Show(_frmSingleImages[4].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[9].Show(_frmSingleImages[5].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[10].Show(_frmSingleImages[6].Pane, DockAlignment.Bottom, 0.5);

                _frmSingleImages[3].DockTo(_frmSingleImages[4].DockHandler.Pane, DockStyle.Left, 0);
                _frmSingleImages[7].DockTo(_frmSingleImages[8].DockHandler.Pane, DockStyle.Left, 0);
            }

            if (num == 12)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[1].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[1].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[2].Show(dockPanel1, DockState.Document);

                _frmSingleImages[3].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[3].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[1].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Left, 0);

                _frmSingleImages[4].Show(_frmSingleImages[0].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[5].Show(_frmSingleImages[1].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[6].Show(_frmSingleImages[2].Pane, DockAlignment.Bottom, 0.6666);
                _frmSingleImages[7].Show(_frmSingleImages[3].Pane, DockAlignment.Bottom, 0.6666);

                _frmSingleImages[8].Show(_frmSingleImages[4].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[9].Show(_frmSingleImages[5].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[10].Show(_frmSingleImages[6].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[11].Show(_frmSingleImages[7].Pane, DockAlignment.Bottom, 0.5);

            }

            if (num == 13)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[0].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[5].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Fill, 0);
                _frmSingleImages[6].DockTo(_frmSingleImages[5].DockHandler.Pane, DockStyle.Right, 0);
                _frmSingleImages[7].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[8].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[9].DockTo(_frmSingleImages[5].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[10].DockTo(_frmSingleImages[6].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[11].DockTo(_frmSingleImages[7].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[12].DockTo(_frmSingleImages[8].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[3].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
            }

            if (num == 14)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[0].DockHandler.DockPanel.DockBottomPortion = 0.5;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[6].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[7].DockTo(_frmSingleImages[6].DockHandler.Pane, DockStyle.Right, 0);
                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[8].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[9].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[10].DockTo(_frmSingleImages[6].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[11].DockTo(_frmSingleImages[7].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[12].DockTo(_frmSingleImages[8].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[13].DockTo(_frmSingleImages[9].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[5].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
            }

            if (num == 15)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.5;
                _frmSingleImages[0].DockHandler.DockPanel.DockBottomPortion = 0.25;

                _frmSingleImages[1].Show(dockPanel1, DockState.Document);

                _frmSingleImages[2].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[2].DockHandler.DockPanel.DockRightPortion = 0.25;


                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[7].DockTo(_frmSingleImages[4].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[3].DockTo(_frmSingleImages[0].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[4].DockTo(_frmSingleImages[3].DockHandler.Pane, DockStyle.Right, 0);
                _frmSingleImages[8].DockTo(_frmSingleImages[7].DockHandler.Pane, DockStyle.Right, 0);

                _frmSingleImages[5].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[6].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[9].DockTo(_frmSingleImages[5].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[10].DockTo(_frmSingleImages[6].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[5].DockTo(_frmSingleImages[1].DockHandler.Pane, DockStyle.Bottom, 0);
                _frmSingleImages[6].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Bottom, 0);

                _frmSingleImages[11].Show(_frmSingleImages[7].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[12].Show(_frmSingleImages[8].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[13].Show(_frmSingleImages[9].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[14].Show(_frmSingleImages[10].Pane, DockAlignment.Bottom, 0.5);
            }

            if (num == 16)
            {
                _frmSingleImages[0].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[0].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[1].Show(dockPanel1, DockState.DockLeft);
                _frmSingleImages[1].DockHandler.DockPanel.DockLeftPortion = 0.25;

                _frmSingleImages[2].Show(dockPanel1, DockState.Document);

                _frmSingleImages[3].Show(dockPanel1, DockState.DockRight);
                _frmSingleImages[3].DockHandler.DockPanel.DockRightPortion = 0.25;

                _frmSingleImages[1].DockTo(_frmSingleImages[2].DockHandler.Pane, DockStyle.Left, 0);

                _frmSingleImages[4].Show(_frmSingleImages[0].Pane, DockAlignment.Bottom, 0.75);
                _frmSingleImages[5].Show(_frmSingleImages[1].Pane, DockAlignment.Bottom, 0.75);
                _frmSingleImages[6].Show(_frmSingleImages[2].Pane, DockAlignment.Bottom, 0.75);
                _frmSingleImages[7].Show(_frmSingleImages[3].Pane, DockAlignment.Bottom, 0.75);

                _frmSingleImages[8].Show(_frmSingleImages[4].Pane, DockAlignment.Bottom, 0.65);
                _frmSingleImages[9].Show(_frmSingleImages[5].Pane, DockAlignment.Bottom, 0.65);
                _frmSingleImages[10].Show(_frmSingleImages[6].Pane, DockAlignment.Bottom, 0.65);
                _frmSingleImages[11].Show(_frmSingleImages[7].Pane, DockAlignment.Bottom, 0.65);

                _frmSingleImages[12].Show(_frmSingleImages[8].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[13].Show(_frmSingleImages[9].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[14].Show(_frmSingleImages[10].Pane, DockAlignment.Bottom, 0.5);
                _frmSingleImages[15].Show(_frmSingleImages[11].Pane, DockAlignment.Bottom, 0.5);
            }
        }

        private void FrmImageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;  // 取消关闭事件，防止窗口关闭
            this.Hide(); // 隐藏窗口
            HideChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
