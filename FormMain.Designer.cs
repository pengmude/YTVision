﻿namespace YTVisionPro
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检测结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画布设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用教程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.联系我们ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于YTVisionProToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.锁定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tsbt_SolNew = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolSave = new System.Windows.Forms.ToolStripButton();
            this.tsbt_ProcessManager = new System.Windows.Forms.ToolStripButton();
            this.tsbt_LightManager = new System.Windows.Forms.ToolStripButton();
            this.tsbt_CameraManager = new System.Windows.Forms.ToolStripButton();
            this.tsbt_PlcManager = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolRunOnce = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolRunLoop = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolRunStop = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbt_ModbusManager = new System.Windows.Forms.ToolStripButton();
            this.tsbt_TCPManager = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.timerLoadSol = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.锁定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1556, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建方案ToolStripMenuItem,
            this.打开方案ToolStripMenuItem,
            this.保存方案ToolStripMenuItem,
            this.另存方案ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建方案ToolStripMenuItem
            // 
            this.新建方案ToolStripMenuItem.Name = "新建方案ToolStripMenuItem";
            this.新建方案ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建方案ToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.新建方案ToolStripMenuItem.Text = "新建方案";
            this.新建方案ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 打开方案ToolStripMenuItem
            // 
            this.打开方案ToolStripMenuItem.Name = "打开方案ToolStripMenuItem";
            this.打开方案ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开方案ToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.打开方案ToolStripMenuItem.Text = "打开方案";
            this.打开方案ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 保存方案ToolStripMenuItem
            // 
            this.保存方案ToolStripMenuItem.Name = "保存方案ToolStripMenuItem";
            this.保存方案ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存方案ToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.保存方案ToolStripMenuItem.Text = "保存方案";
            this.保存方案ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 另存方案ToolStripMenuItem
            // 
            this.另存方案ToolStripMenuItem.Name = "另存方案ToolStripMenuItem";
            this.另存方案ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.另存方案ToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.另存方案ToolStripMenuItem.Text = "另存方案";
            this.另存方案ToolStripMenuItem.Click += new System.EventHandler(this.文件ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认视图ToolStripMenuItem,
            this.图像显示ToolStripMenuItem,
            this.检测结果ToolStripMenuItem,
            this.运行日志ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 默认视图ToolStripMenuItem
            // 
            this.默认视图ToolStripMenuItem.Checked = true;
            this.默认视图ToolStripMenuItem.CheckOnClick = true;
            this.默认视图ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.默认视图ToolStripMenuItem.Name = "默认视图ToolStripMenuItem";
            this.默认视图ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.默认视图ToolStripMenuItem.Text = "默认视图";
            this.默认视图ToolStripMenuItem.Click += new System.EventHandler(this.默认视图ToolStripMenuItem_Click);
            // 
            // 图像显示ToolStripMenuItem
            // 
            this.图像显示ToolStripMenuItem.Checked = true;
            this.图像显示ToolStripMenuItem.CheckOnClick = true;
            this.图像显示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.图像显示ToolStripMenuItem.Name = "图像显示ToolStripMenuItem";
            this.图像显示ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.图像显示ToolStripMenuItem.Text = "检测图像视图";
            this.图像显示ToolStripMenuItem.Click += new System.EventHandler(this.检测图像视图ToolStripMenuItem_Click);
            // 
            // 检测结果ToolStripMenuItem
            // 
            this.检测结果ToolStripMenuItem.Checked = true;
            this.检测结果ToolStripMenuItem.CheckOnClick = true;
            this.检测结果ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.检测结果ToolStripMenuItem.Name = "检测结果ToolStripMenuItem";
            this.检测结果ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.检测结果ToolStripMenuItem.Text = "检测结果视图";
            this.检测结果ToolStripMenuItem.Click += new System.EventHandler(this.检测结果视图ToolStripMenuItem_Click);
            // 
            // 运行日志ToolStripMenuItem
            // 
            this.运行日志ToolStripMenuItem.Checked = true;
            this.运行日志ToolStripMenuItem.CheckOnClick = true;
            this.运行日志ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.运行日志ToolStripMenuItem.Name = "运行日志ToolStripMenuItem";
            this.运行日志ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.运行日志ToolStripMenuItem.Text = "运行日志视图";
            this.运行日志ToolStripMenuItem.Click += new System.EventHandler(this.运行日志视图ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画布设置ToolStripMenuItem,
            this.系统设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 画布设置ToolStripMenuItem
            // 
            this.画布设置ToolStripMenuItem.Name = "画布设置ToolStripMenuItem";
            this.画布设置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.画布设置ToolStripMenuItem.Text = "图像画布设置";
            this.画布设置ToolStripMenuItem.Click += new System.EventHandler(this.画布设置ToolStripMenuItem_Click);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用教程ToolStripMenuItem,
            this.联系我们ToolStripMenuItem1,
            this.关于YTVisionProToolStripMenuItem1});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 使用教程ToolStripMenuItem
            // 
            this.使用教程ToolStripMenuItem.Name = "使用教程ToolStripMenuItem";
            this.使用教程ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.使用教程ToolStripMenuItem.Text = "使用教程";
            // 
            // 联系我们ToolStripMenuItem1
            // 
            this.联系我们ToolStripMenuItem1.Name = "联系我们ToolStripMenuItem1";
            this.联系我们ToolStripMenuItem1.Size = new System.Drawing.Size(182, 34);
            this.联系我们ToolStripMenuItem1.Text = "联系我们";
            this.联系我们ToolStripMenuItem1.Click += new System.EventHandler(this.联系我们ToolStripMenuItem_Click);
            // 
            // 关于YTVisionProToolStripMenuItem1
            // 
            this.关于YTVisionProToolStripMenuItem1.Name = "关于YTVisionProToolStripMenuItem1";
            this.关于YTVisionProToolStripMenuItem1.Size = new System.Drawing.Size(182, 34);
            this.关于YTVisionProToolStripMenuItem1.Text = "关于";
            this.关于YTVisionProToolStripMenuItem1.Click += new System.EventHandler(this.关于YTVisionProToolStripMenuItem1_Click);
            // 
            // 锁定ToolStripMenuItem
            // 
            this.锁定ToolStripMenuItem.Image = global::YTVisionPro.Properties.Resources.解锁;
            this.锁定ToolStripMenuItem.Name = "锁定ToolStripMenuItem";
            this.锁定ToolStripMenuItem.Size = new System.Drawing.Size(40, 28);
            this.锁定ToolStripMenuItem.ToolTipText = "界面已锁定";
            this.锁定ToolStripMenuItem.Click += new System.EventHandler(this.锁定ToolStripMenuItem_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockPanel1.Location = new System.Drawing.Point(0, 73);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel1.ShowAutoHideContentOnHover = false;
            this.dockPanel1.Size = new System.Drawing.Size(1556, 854);
            this.dockPanel1.TabIndex = 2;
            this.dockPanel1.Theme = this.vS2015LightTheme1;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "视觉方案文件(*.YtSol)|*.YtSol|所有文件(*.*)|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "视觉方案文件(*.YtSol)|*.YtSol|所有文件(*.*)|*.*";
            // 
            // tsbt_SolNew
            // 
            this.tsbt_SolNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolNew.Image = global::YTVisionPro.Properties.Resources.新建方案;
            this.tsbt_SolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolNew.Name = "tsbt_SolNew";
            this.tsbt_SolNew.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolNew.Text = "新建方案";
            this.tsbt_SolNew.ToolTipText = "新建方案";
            this.tsbt_SolNew.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolOpen
            // 
            this.tsbt_SolOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolOpen.Image = global::YTVisionPro.Properties.Resources.打开项目;
            this.tsbt_SolOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolOpen.Name = "tsbt_SolOpen";
            this.tsbt_SolOpen.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolOpen.Text = "打开方案";
            this.tsbt_SolOpen.ToolTipText = "打开方案";
            this.tsbt_SolOpen.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolSaveAs
            // 
            this.tsbt_SolSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolSaveAs.Image = global::YTVisionPro.Properties.Resources.另存为;
            this.tsbt_SolSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolSaveAs.Name = "tsbt_SolSaveAs";
            this.tsbt_SolSaveAs.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolSaveAs.Text = "另存方案";
            this.tsbt_SolSaveAs.ToolTipText = "另存方案";
            this.tsbt_SolSaveAs.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolSave
            // 
            this.tsbt_SolSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolSave.Image = global::YTVisionPro.Properties.Resources.保存项目;
            this.tsbt_SolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolSave.Name = "tsbt_SolSave";
            this.tsbt_SolSave.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolSave.Text = "保存方案";
            this.tsbt_SolSave.ToolTipText = "保存方案";
            this.tsbt_SolSave.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_ProcessManager
            // 
            this.tsbt_ProcessManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_ProcessManager.Image = global::YTVisionPro.Properties.Resources.添加流程;
            this.tsbt_ProcessManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_ProcessManager.Name = "tsbt_ProcessManager";
            this.tsbt_ProcessManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_ProcessManager.Text = "流程管理";
            this.tsbt_ProcessManager.ToolTipText = "流程管理";
            this.tsbt_ProcessManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_LightManager
            // 
            this.tsbt_LightManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_LightManager.Image = global::YTVisionPro.Properties.Resources.光源;
            this.tsbt_LightManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_LightManager.Name = "tsbt_LightManager";
            this.tsbt_LightManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_LightManager.Text = "光源管理";
            this.tsbt_LightManager.ToolTipText = "光源管理";
            this.tsbt_LightManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_CameraManager
            // 
            this.tsbt_CameraManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_CameraManager.Image = global::YTVisionPro.Properties.Resources.相机;
            this.tsbt_CameraManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_CameraManager.Name = "tsbt_CameraManager";
            this.tsbt_CameraManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_CameraManager.Text = "相机管理";
            this.tsbt_CameraManager.ToolTipText = "相机管理";
            this.tsbt_CameraManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_PlcManager
            // 
            this.tsbt_PlcManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_PlcManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbt_PlcManager.Image")));
            this.tsbt_PlcManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_PlcManager.Name = "tsbt_PlcManager";
            this.tsbt_PlcManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_PlcManager.Text = "PLC管理";
            this.tsbt_PlcManager.ToolTipText = "PLC管理";
            this.tsbt_PlcManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolRunOnce
            // 
            this.tsbt_SolRunOnce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolRunOnce.Image = global::YTVisionPro.Properties.Resources.单次执行;
            this.tsbt_SolRunOnce.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolRunOnce.Margin = new System.Windows.Forms.Padding(120, 2, 0, 3);
            this.tsbt_SolRunOnce.Name = "tsbt_SolRunOnce";
            this.tsbt_SolRunOnce.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolRunOnce.Text = "单次运行";
            this.tsbt_SolRunOnce.ToolTipText = "单次运行";
            this.tsbt_SolRunOnce.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolRunLoop
            // 
            this.tsbt_SolRunLoop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolRunLoop.Image = global::YTVisionPro.Properties.Resources.循环执行;
            this.tsbt_SolRunLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolRunLoop.Margin = new System.Windows.Forms.Padding(20, 2, 0, 3);
            this.tsbt_SolRunLoop.Name = "tsbt_SolRunLoop";
            this.tsbt_SolRunLoop.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolRunLoop.Text = "循环运行";
            this.tsbt_SolRunLoop.ToolTipText = "循环运行";
            this.tsbt_SolRunLoop.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolRunStop
            // 
            this.tsbt_SolRunStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolRunStop.Enabled = false;
            this.tsbt_SolRunStop.Image = global::YTVisionPro.Properties.Resources.停止执行;
            this.tsbt_SolRunStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolRunStop.Margin = new System.Windows.Forms.Padding(20, 2, 0, 3);
            this.tsbt_SolRunStop.Name = "tsbt_SolRunStop";
            this.tsbt_SolRunStop.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolRunStop.Text = "停止运行";
            this.tsbt_SolRunStop.ToolTipText = "停止运行";
            this.tsbt_SolRunStop.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbt_SolNew,
            this.tsbt_SolOpen,
            this.tsbt_SolSaveAs,
            this.tsbt_SolSave,
            this.tsbt_ProcessManager,
            this.tsbt_LightManager,
            this.tsbt_CameraManager,
            this.tsbt_PlcManager,
            this.tsbt_ModbusManager,
            this.tsbt_TCPManager,
            this.tsbt_SolRunOnce,
            this.tsbt_SolRunLoop,
            this.tsbt_SolRunStop,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1556, 41);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbt_ModbusManager
            // 
            this.tsbt_ModbusManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_ModbusManager.Image = global::YTVisionPro.Properties.Resources.Modbus;
            this.tsbt_ModbusManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_ModbusManager.Name = "tsbt_ModbusManager";
            this.tsbt_ModbusManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_ModbusManager.Text = "Modbus设备";
            this.tsbt_ModbusManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_TCPManager
            // 
            this.tsbt_TCPManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_TCPManager.Image = global::YTVisionPro.Properties.Resources.tcp;
            this.tsbt_TCPManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_TCPManager.Name = "tsbt_TCPManager";
            this.tsbt_TCPManager.Size = new System.Drawing.Size(36, 36);
            this.tsbt_TCPManager.Text = "TCP设备";
            this.tsbt_TCPManager.ToolTipText = "TCP设备";
            this.tsbt_TCPManager.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Mongolian Baiti", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.Brown;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(50, 2, 0, 3);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(307, 36);
            this.toolStripLabel1.Text = "方案加载中，请耐心等待……";
            this.toolStripLabel1.Visible = false;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(1556, 771);
            // 
            // timerLoadSol
            // 
            this.timerLoadSol.Interval = 300;
            this.timerLoadSol.Tick += new System.EventHandler(this.timerLoadSol_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1556, 927);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YTVisionPro V1.0.0";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检测结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画布设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用教程ToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ToolStripMenuItem 联系我们ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关于YTVisionProToolStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 锁定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbt_SolNew;
        private System.Windows.Forms.ToolStripButton tsbt_SolOpen;
        private System.Windows.Forms.ToolStripButton tsbt_SolSaveAs;
        private System.Windows.Forms.ToolStripButton tsbt_SolSave;
        private System.Windows.Forms.ToolStripButton tsbt_ProcessManager;
        private System.Windows.Forms.ToolStripButton tsbt_LightManager;
        private System.Windows.Forms.ToolStripButton tsbt_CameraManager;
        private System.Windows.Forms.ToolStripButton tsbt_PlcManager;
        private System.Windows.Forms.ToolStripButton tsbt_SolRunOnce;
        private System.Windows.Forms.ToolStripButton tsbt_SolRunLoop;
        private System.Windows.Forms.ToolStripButton tsbt_SolRunStop;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripButton tsbt_ModbusManager;
        private System.Windows.Forms.ToolStripButton tsbt_TCPManager;
        private System.Windows.Forms.Timer timerLoadSol;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

