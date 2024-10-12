using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Forms;
using YTVisionPro.Forms.Helper;
using YTVisionPro.Forms.ImageViewer;
using YTVisionPro.Forms.ProcessNew;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;
using YTVisionPro.Forms.ResultView;
using YTVisionPro.Node.AI.HTAI;
using System.Threading.Tasks;
using Sunny.UI;
using YTVisionPro.Properties;
using HslCommunication;
using YTVisionPro.Forms.SystemSetting;

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
        /// 软件操作锁定
        /// </summary>
        static FrmOperatorLocker frmLocker = new FrmOperatorLocker();
        /// <summary>
        /// 系统设置窗口
        /// </summary>
        private FrmSystemSetting frmSystemSetting = new FrmSystemSetting();
        /// <summary>
        /// 窗口布局配置
        /// </summary>
        private readonly string DockPanelConfig = Application.StartupPath + "\\DockPanel.config";
        /// <summary>
        /// 反序列化DockContent代理
        /// </summary>
        private DeserializeDockContent DeserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // 初始化主窗口布局
            InitDockPanel();

            // 设置主窗口标题
            Text = $"鹰眼智检系统 V{Solution.Instance.SolVersion}";

            // 初始化海康相机SDK
            CameraHik.InitSDK();

            // 工具菜单状态同步
            运行日志ToolStripMenuItem.Checked = FrmLoggerDlg.Visible;
            检测结果ToolStripMenuItem.Checked = FrmResultDlg.Visible;
            图像显示ToolStripMenuItem.Checked = FrmImgeDlg.Visible;
            默认视图ToolStripMenuItem.Checked = FrmLoggerDlg.Visible && FrmResultDlg.Visible && FrmImgeDlg.Visible ? true : false;

            // 布局同步更新事件
            FrmImgeDlg.HideChangedEvent += HideChangedEvent;
            FrmResultDlg.HideChangedEvent += HideChangedEvent;
            FrmLoggerDlg.HideChangedEvent += HideChangedEvent;

            // 界面锁事件
            frmLocker.OperatorLockerChanged += FrmOperatorLocker_OperatorLockerChanged;

            // 保存快捷键事件处理
            FrmNewProcessWizard.OnShotKeySavePressed += OnShotKeySavePressed;
            FrmLightAdd.OnShotKeySavePressed += OnShotKeySavePressed;
            FrmCameraAdd.OnShotKeySavePressed += OnShotKeySavePressed;
            FrmPLCAdd.OnShotKeySavePressed += OnShotKeySavePressed;

            // 加载默认方案
            AutoLoadSolution();
        }

        // 软件启动是否加载指定方案
        private void AutoLoadSolution()
        {
            if (Settings.Default.IsAutoLoad)
            {
                try
                {
                    Solution.Instance.Load(Settings.Default.SolutionAddress, true);
                }
                catch (Exception)
                {
                    LogHelper.AddLog(MsgLevel.Warn, "方案加载失败！", true);
                }
            }
        }

        /// <summary>
        /// 按下快捷键事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShotKeySavePressed(object sender, EventArgs e)
        {
            保存方案ToolStripMenuItem_Click(null, null);
        }

        private void FrmOperatorLocker_OperatorLockerChanged(object sender, bool e)
        {
            SetLockStatus(!e);
        }

        /// <summary>
        /// 界面锁住状态
        /// </summary>
        private void SetLockStatus(bool isLock)
        {
            toolStrip1.Enabled = !isLock;
            文件ToolStripMenuItem.Enabled = !isLock;
            设置ToolStripMenuItem.Enabled = !isLock;
            if (isLock)
                锁定ToolStripMenuItem.Image = Resources.锁定;
            else
                锁定ToolStripMenuItem.Image = Resources.解锁;
        }

        /// <summary>
        /// 主窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 主程序关闭提醒
            if(MessageBox.Show("确认关闭程序？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            // 检测任务正在运行提示
            if (Solution.Instance.IsRunning)
            {
                e.Cancel = true;
                MessageBox.Show("请先停止当前任务再关闭！");
                return;
            }

            // 释放方案资源
            ReleaseSol();

            // 海康相机SDK反序列化
            CameraHik.Finalize();

            // 保存主窗口布局
            this.dockPanel1.SaveAsXml(DockPanelConfig);
        }

        /// <summary>
        /// 释放方案
        /// </summary>
        private void ReleaseSol()
        {
            // 释放AI节点的内存
            foreach (var node in Solution.Instance.Nodes)
            {
                if (node is NodeHTAI nodeAi)
                {
                    nodeAi.ReleaseAIResult();
                    ((ParamFormHTAI)nodeAi.ParamForm).ReleaseAIHandle();
                }
            }

            // 释放硬件资源（光源、相机、PLC）
            foreach (var dev in Solution.Instance.AllDevices)
            {
                if (dev is ILight light)
                    light.Disconnect();
                if (dev is ICamera camera)
                    camera.Dispose();
                if (dev is IPlc plc)
                    plc.Disconnect();
            }
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

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                switch (item.Text)
                {
                    case "新建方案":
                        新建方案ToolStripMenuItem_Click(sender, e);
                        break;
                    case "打开方案":
                        打开方案ToolStripMenuItem_Click(sender, e);
                        break;
                    case "另存方案":
                        另存方案ToolStripMenuItem_Click(sender, e);
                        break;
                    case "保存方案":
                        保存方案ToolStripMenuItem_Click(sender, e);
                        break;
                    case "退出":
                        if (MessageBox.Show("确认退出？") == DialogResult.OK)
                            Close();
                        break;
                    default:
                        break;
                }
            }
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
                case "新建方案":
                    新建方案ToolStripMenuItem_Click(null, null);
                    break;
                case "打开方案":
                    打开方案ToolStripMenuItem_Click(null, null);
                    break;
                case "另存方案":
                    另存方案ToolStripMenuItem_Click(null, null);
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

        private async void 停止运行ToolStripMenuItem_Click(object value1, object value2)
        {
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

        /// <summary>
        /// 设置运行状态，启用/禁用控件
        /// </summary>
        /// <param name="enable"></param>
        private void SetRunStatus(bool isRunning)
        {
            // 运行和停止按钮
            tsbt_SolRunOnce.Enabled = !isRunning;
            tsbt_SolRunLoop.Enabled = !isRunning;
            tsbt_SolRunStop.Enabled = isRunning;

            // 其他设置禁用/启用
            tsbt_SolNew.Enabled = !isRunning;
            tsbt_SolOpen.Enabled = !isRunning;
            tsbt_SolSaveAs.Enabled = !isRunning;
            tsbt_SolSave.Enabled = !isRunning;
            tsbt_ProcessManager.Enabled = !isRunning;
            tsbt_LightManager.Enabled = !isRunning;
            tsbt_CameraManager.Enabled = !isRunning;
            tsbt_PlcManager.Enabled = !isRunning;

            // 文件
            文件ToolStripMenuItem.Enabled = !isRunning;
            视图ToolStripMenuItem.Enabled = !isRunning;
            设置ToolStripMenuItem.Enabled = !isRunning;
            帮助ToolStripMenuItem.Enabled = !isRunning;
        }

        private void 操作锁定ToolStripMenuItem_Click(object value1, object value2)
        {
            frmLocker.ShowDialog();
        }

        private void PLC管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmPLCAdd.ShowDialog();
        }
        private void 相机管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmCameraAdd.ShowDialog();
        }

        private void 光源管理ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmLightAdd.ShowDialog();
        }

        private void 另存方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "方案另存为";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Solution.Instance.Save(saveFileDialog1.FileName);
                    LogHelper.AddLog(MsgLevel.Info, $"方案另存成功！路径：{saveFileDialog1.FileName}", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"方案保存失败！原因：{ex.Message}");
                }
            }
        }

        private void 保存方案ToolStripMenuItem_Click(object value1, object value2)
        {
            if (Solution.Instance.SolFileName.IsNullOrEmpty())
            {
                saveFileDialog1.Title = "请选择方案保存路径";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    Solution.Instance.SolFileName = saveFileDialog1.FileName;
                else
                    return;
            }
            try
            {
                Solution.Instance.Save(Solution.Instance.SolFileName);
                LogHelper.AddLog(MsgLevel.Info, $"方案保存成功！路径：{Solution.Instance.SolFileName}", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"方案保存失败！原因：{ex.Message}");
            }
        }

        private void 打开方案ToolStripMenuItem_Click(object value1, object value2)
        {
            openFileDialog1.Title = "请选择要打开的方案";
            if (openFileDialog1.ShowDialog()  == DialogResult.OK)
            {
                Solution.Instance.Load(openFileDialog1.FileName, true);
            }
        }

        /// <summary>
        /// 新建方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 释放旧方案
            ReleaseSol();
            // 新建方案实际和调用加载空方案一样(传入false，表示不需要打印反序列化的信息因为新建方案实际上就是加载一个空的方案)
            Solution.Instance.Load(Application.StartupPath + "\\空方案.YtSol", false);
            LogHelper.AddLog(MsgLevel.Info, $"新建方案成功！", true);
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

        private void 锁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            操作锁定ToolStripMenuItem_Click(null, null);
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemSetting.ShowDialog();
        }
    }


}
