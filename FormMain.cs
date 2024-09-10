using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Forms;
using YTVisionPro.Forms.Helper;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Forms.PLCMonitor;
using YTVisionPro.Forms.ProcessNew;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Forms.ResultView;

namespace YTVisionPro
{
    internal partial class FormMain : Form
    {
        /// <summary>
        /// 光源添加窗口
        /// </summary>
        Forms.LightAdd.FrmLightListView FrmLightAdd = new Forms.LightAdd.FrmLightListView();
        /// <summary>
        /// 相机添加窗口
        /// </summary>
        Forms.CameraAdd.FrmCameraListView FrmCameraAdd = new Forms.CameraAdd.FrmCameraListView();
        /// <summary>
        /// PLC添加窗口
        /// </summary>
        Forms.PLCAdd.FrmPLCListView FrmPLCAdd= new Forms.PLCAdd.FrmPLCListView();
        /// <summary>
        /// PLC信号监听设置
        /// </summary>
        FrmSignalMonitor FrmSignalMonitor = new FrmSignalMonitor();
        /// <summary>
        /// 图像显示栏
        /// </summary>
        static FrmImageViewer FrmImgeDlg = new FrmImageViewer();
        /// <summary>
        /// 结果数据显示栏
        /// </summary>
        static FrmResultView FrmResultDlg = new FrmResultView();
        /// <summary>
        /// 日志栏
        /// </summary>
        static FrmLogger FrmLoggerDlg = new FrmLogger();
        /// <summary>
        /// 流程创建窗口
        /// </summary>
        static FormNewProcessWizard FrmNewProcessWizard = new FormNewProcessWizard();
        /// <summary>
        /// 图像数量设置窗口
        /// </summary>
        static CanvasSet canvasSet = new CanvasSet();
        /// <summary>
        /// 联系我们
        /// </summary>
        static ContactUsFormForm contactUsFormForm = new ContactUsFormForm();
        /// <summary>
        /// 关于YTViisionPro
        /// </summary>
        static FrmAbout frmAbout = new FrmAbout();
        /// <summary>
        /// 用户登录界面
        /// </summary>
        static FrmLogin frmLogin = new FrmLogin();
        /// <summary>
        /// 窗口布局配置
        /// </summary>
        private readonly string DockPanelConfig = Application.StartupPath + "\\DockPanel.config";
        /// <summary>
        /// 反序列化DockContent代理
        /// </summary>
        private DeserializeDockContent DeserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        /// <summary>
        /// 系统当前的运行模式
        /// </summary>
        private RunMode CurRunMode { get; set; }
        /// <summary>
        /// 系统是否正在运行
        /// </summary>
        private bool IsRunning { get; set; }

        public FormMain()
        {
            InitializeComponent();

            #region 测试代码
            //var lights = new List<IDevice>() { new LightPPX("光源1", "COM1"), new LightPPX("光源2", "COM2"), new LightPPX("光源3", "COM3") };
            //var camers = new List<IDevice>() { new CameraHik("相机1"), new CameraHik("相机2"), new CameraHik("相机3") };
            //var plcs = new List<IDevice>() { new PlcPanasonic("PLC1"), new PlcPanasonic("PLC2"), new PlcPanasonic("PLC3") };
            //Solution.Instance.AddDeviceList(lights);
            //Solution.Instance.AddDeviceList(camers);
            //Solution.Instance.AddDeviceList(plcs);
            #endregion
        }
        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            InitDockPanel();
            CameraHik.InitSDK();

            运行日志ToolStripMenuItem.Checked = FrmLoggerDlg.Visible;
            检测结果ToolStripMenuItem.Checked = FrmResultDlg.Visible;
            图像显示ToolStripMenuItem.Checked = FrmImgeDlg.Visible;
            默认视图ToolStripMenuItem.Checked = FrmLoggerDlg.Visible && FrmResultDlg.Visible && FrmImgeDlg.Visible ? true : false;

            FrmImgeDlg.HideChangedEvent += HideChangedEvent;
            FrmResultDlg.HideChangedEvent += HideChangedEvent;
            FrmLoggerDlg.HideChangedEvent += HideChangedEvent;
        }

        /// <summary>
        /// 主窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsRunning)
            {
                e.Cancel = true;
                MessageBox.Show("请先停止当前任务再关闭！");
                return;
            }
            CameraHik.Finalize();
            this.dockPanel1.SaveAsXml(DockPanelConfig);
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
            FrmImgeDlg.Show(dockPanel1, DockState.Document);
            FrmResultDlg.Show(dockPanel1, DockState.DockRight);
            FrmLoggerDlg.Show(dockPanel1, DockState.DockBottom);
            图像显示ToolStripMenuItem.Checked = true;
            检测结果ToolStripMenuItem.Checked = true;
            运行日志ToolStripMenuItem.Checked = true;
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }
        /// <summary>
        /// 配置委托函数
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private static IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(Forms.ImageViewer.FrmImageViewer).ToString())
                return FrmImgeDlg;

            else if (persistString == typeof(FrmResultView).ToString())
                return FrmResultDlg;

            else if (persistString == typeof(FrmLogger).ToString())
                return FrmLoggerDlg;

            else
                return null;
        }
        /// <summary>
        /// 点击工具栏的工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton tsbt = sender as ToolStripButton;
            switch (tsbt.Text)
            {
                case "方案设置":
                    方案设置ToolStripMenuItem_Click(null, null);
                    break;
                case "新建方案":
                    新建方案ToolStripMenuItem_Click(null, null);
                    break;
                case "打开方案":
                    打开方案ToolStripMenuItem_Click(null, null);
                    break;
                case "保存方案":
                    保存方案ToolStripMenuItem_Click(null, null);
                    break;
                case "流程管理":
                    流程管理ToolStripMenuItem_Click(null, null);
                    break;
                case "光源管理":
                    光源管理ToolStripMenuItem_Click(null, null);
                    break;
                case "相机管理":
                    相机管理ToolStripMenuItem_Click(null, null);
                    break;
                case "PLC管理":
                    PLC管理ToolStripMenuItem_Click(null, null);
                    break;
                case "信号监听":
                    信号监听ToolStripMenuItem_Click(null, null);
                    break;
                case "用户登录":
                    用户登录ToolStripMenuItem_Click(null, null);
                    break;
                case "循环运行":
                    循环运行ToolStripMenuItem_Click(null, null);
                    break;
                case "单次运行":
                    单次运行ToolStripMenuItem_Click(null, null);
                    break;
                case "停止运行":
                    停止运行ToolStripMenuItem_Click(null, null);
                    break;
            }
        }

        private void 流程管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmNewProcessWizard.ShowDialog();
        }

        private void 停止运行ToolStripMenuItem_Click(object value1, object value2)
        {
            if(MessageBox.Show("确定停止当前方案运行？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            Solution.Instance.Stop();
            SetRunStatus(false);
        }

        private async void 循环运行ToolStripMenuItem_Click(object value1, object value2)
        {
            SetRunStatus(true);
            await Solution.Instance.Run(true);
            SetRunStatus(false);
        }

        private async void 单次运行ToolStripMenuItem_Click(object value1, object value2)
        {
            SetRunStatus(true);
            await Solution.Instance.Run(false);
            SetRunStatus(false);
        }

        private void SetRunStatus(bool isRunning)
        {
            tsbt_SolRunOnce.Enabled = !isRunning;
            tsbt_SolRunLoop.Enabled = !isRunning;
            tsbt_SolRunStop.Enabled = isRunning;
        }

        private void 用户登录ToolStripMenuItem_Click(object value1, object value2)
        {
            frmLogin.ShowDialog();
        }

        private void PLC管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmPLCAdd.ShowDialog();
        }

        private void 信号监听ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmSignalMonitor.ShowDialog();
        }

        private void 相机管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmCameraAdd.ShowDialog();
        }

        private void 光源管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmLightAdd.ShowDialog();
        }
        private void 保存方案ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("保存方案");
        }

        private void 打开方案ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("打开方案");
        }

        /// <summary>
        /// 新建方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否保存当前方案的修改？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // TODO:保存方案
                MessageBox.Show("方案已保存！");
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Create(saveFileDialog1.FileName).Close();
            }
        }

        private void 方案设置ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("方案设置");
        }

        private void 联系我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contactUsFormForm.ShowDialog();
        }

        /// <summary>
        /// 图像画布设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 画布设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvasSet.ShowDialog();
        }

        private void 关于YTVisionProToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout.ShowDialog();
        }

        private void 默认视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDefaultDockPanel();
        }

        private void 检测图像视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmImgeDlg.Visible)
            {
                FrmImgeDlg.Hide(); // 如果窗口可见，隐藏它
                图像显示ToolStripMenuItem.Checked = false;
                默认视图ToolStripMenuItem.Checked = false;
            }
            else
            {
                FrmImgeDlg.Show(); // 如果窗口隐藏，显示它
                图像显示ToolStripMenuItem.Checked = true;
                默认视图ToolStripMenuItem.Checked = FrmLoggerDlg.Visible && FrmResultDlg.Visible && FrmImgeDlg.Visible ? true : false;
            }
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }

        private void 检测结果视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmResultDlg.Visible)
            {
                FrmResultDlg.Hide(); // 如果窗口可见，隐藏它
                检测结果ToolStripMenuItem.Checked = false;
                默认视图ToolStripMenuItem.Checked = false;
            }
            else
            {
                FrmResultDlg.Show(); // 如果窗口隐藏，显示它
                检测结果ToolStripMenuItem.Checked = true;
                默认视图ToolStripMenuItem.Checked = FrmLoggerDlg.Visible && FrmResultDlg.Visible && FrmImgeDlg.Visible ? true : false;
            }
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }

        private void 运行日志视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmLoggerDlg.Visible)
            {
                FrmLoggerDlg.Hide(); // 如果窗口可见，隐藏它
                运行日志ToolStripMenuItem.Checked = false;
                默认视图ToolStripMenuItem.Checked = false;
            }
            else
            {
                FrmLoggerDlg.Show(); // 如果窗口隐藏，显示它
                运行日志ToolStripMenuItem.Checked = true;
                默认视图ToolStripMenuItem.Checked = FrmLoggerDlg.Visible && FrmResultDlg.Visible && FrmImgeDlg.Visible ? true : false;
            }
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }

        private void HideChangedEvent(object sender, EventArgs e)
        {
            FrmImageViewer frmImageViewer = sender as FrmImageViewer;
            FrmResultView frmResultView = sender as FrmResultView;
            FrmLogger frmLogger = sender as FrmLogger;
            if (frmImageViewer != null)
                图像显示ToolStripMenuItem.Checked = frmImageViewer.Visible;
            else if (frmResultView != null)
                检测结果ToolStripMenuItem.Checked = frmResultView.Visible;
            else if (frmLogger != null)
                运行日志ToolStripMenuItem.Checked = frmLogger.Visible;
        }
    }


}
