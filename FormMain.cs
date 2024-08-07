using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using YTVisionPro.Forms.ProcessNew;
using YTVisionPro.Hardware;
using YTVisionPro.Hardware.Camera;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Hardware.PLC;

namespace YTVisionPro
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 图像显示栏
        /// </summary>
        static Forms.ImageViewer.FrmImageViewer FrmImgeDlg = new Forms.ImageViewer.FrmImageViewer();
        /// <summary>
        /// 结果数据显示栏
        /// </summary>
        static Forms.FrmResultView FrmResultDlg = new Forms.FrmResultView();
        /// <summary>
        /// 日志栏
        /// </summary>
        static FrmLogger FrmLoggerDlg = new FrmLogger();
        /// <summary>
        /// 流程创建窗口
        /// </summary>
        static FormNewProcessWizard FrmNewProcessWizard = new FormNewProcessWizard();
        /// <summary>
        /// 窗口布局配置
        /// </summary>
        private readonly string DockPanelConfig = Application.StartupPath + "\\DockPanel.config";
        /// <summary>
        /// 保存布局事件
        /// </summary>
        public static EventHandler SaveDockPanelEvent;
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
            var lights = new List<IDevice>() { new LightPPX("光源1", "COM1"), new LightPPX("光源2", "COM2"), new LightPPX("光源3", "COM3") };
            var camers = new List<IDevice>() { new CameraHik("相机1"), new CameraHik("相机2"), new CameraHik("相机3") };
            var plcs = new List<IDevice>() { new PlcPanasonic("PLC1"), new PlcPanasonic("PLC2"), new PlcPanasonic("PLC3") };
            Solution.Instance.AddDeviceList(lights);
            Solution.Instance.AddDeviceList(camers);
            Solution.Instance.AddDeviceList(plcs);
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

            else if (persistString == typeof(Forms.FrmResultView).ToString())
                return FrmResultDlg;

            else if (persistString == typeof(Forms.FrmResultView).ToString())
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
                case "创建流程":
                    创建流程ToolStripMenuItem_Click(null, null);
                    break;
                case "全局相机":
                    全局相机ToolStripMenuItem_Click(null, null);
                    break;
                case "全局通信":
                    全局通信ToolStripMenuItem_Click(null, null);
                    break;
                case "全局变量":
                    全局变量ToolStripMenuItem_Click(null, null);
                    break;
                case "用户登录":
                    用户登录ToolStripMenuItem_Click(null, null);
                    break;
                case "开始运行":
                    开始运行ToolStripMenuItem_Click(null, null);
                    break;
                case "停止运行":
                    停止运行ToolStripMenuItem_Click(null, null);
                    break;
            }
        }

        private void 创建流程ToolStripMenuItem_Click(object value1, object value2)
        {
            FrmNewProcessWizard.ShowDialog();
        }

        private void 停止运行ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("停止运行");
            //Project.Instance.StopRun();
        }

        private void 开始运行ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("开始运行");
            //Project.Instance.OnceRun();
        }

        private void 用户登录ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("用户登录");
        }

        private void 全局变量ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("全局变量");
            //CameraSet mCameraFormSet = new CameraSet();
            //CameraSet.mCamerasList = Project.Instance.mCamerasList;
            //mCameraFormSet.ShowDialog();
        }

        private void 全局通信ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("全局通信");
            //NetSet mCommunicationSet = new NetSet(Project.Instance.mEComList);
            //mCommunicationSet.ShowDialog();
        }

        private void 全局相机ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("全局相机");
            //VarSet mVarSetForm = new VarSet(Project.Instance.mSysVar);
            //mVarSetForm.ShowDialog();
        }

        private void 保存方案ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("保存方案");
            //mSaveFile.FileName = Project.Instance.Name + ".RV";
            //if (mSaveFile.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        Project.Instance.SaveData(mSaveFile.FileName, Project.Instance);
            //        Project.Instance.mSavePath = mSaveFile.FileName;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    }
            //}
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
                //Solution = new Solution();
                //Solution.SolFileName = saveFileDialog1.FileName;
                File.Create(saveFileDialog1.FileName).Close();
            }
        }

        private void 方案设置ToolStripMenuItem_Click(object value1, object value2)
        {
            MessageBox.Show("方案设置");
        }

        private void 保存布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dockPanel1.SaveAsXml(DockPanelConfig);
            SaveDockPanelEvent?.Invoke(this, EventArgs.Empty);
        }

        private void 默认布局ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadDefaultDockPanel();
        }

        private void 联系我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.ShowDialog();
        }

        /// <summary>
        /// 切换运行模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                MessageBox.Show("请先停止当前任务再切换！");
                return;
            }

            // 获取点击的 ToolStripMenuItem
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;

            // 遍历 toolStrip1 上的所有 ToolStripMenuItem
            foreach (ToolStripItem item in 运行模式ToolStripMenuItem.DropDownItems)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)item;

                // 如果点击的不是当前项，则取消选中状态
                if (menuItem != clickedItem)
                {
                    menuItem.Checked = false;
                }
                else
                {
                    // 设置点击的项为选中状态
                    menuItem.Checked = true;
                    if (clickedItem == 在线自动模式ToolStripMenuItem)
                        CurRunMode = RunMode.ONLINE_AUTO;
                    else if (clickedItem == 在线点检模式ToolStripMenuItem)
                        CurRunMode = RunMode.ONLINE_JOG;
                    else if (clickedItem == 离线自动模式ToolStripMenuItem)
                        CurRunMode = RunMode.OFFLINE_AUTO;
                    else if (clickedItem == 离线点检模式ToolStripMenuItem)
                        CurRunMode = RunMode.OFFLINE_JOG;
                    //MessageBox.Show($"{CurRunMode}");
                }
            }
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
        }

    }


}
