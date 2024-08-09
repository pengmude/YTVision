namespace YTVisionPro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在线自动模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在线点检模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离线自动模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离线点检模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认布局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认布局ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.保存布局ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.图像显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检测结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画布设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用教程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.联系我们ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于YTVisionProToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbt_SolSet = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolNew = new System.Windows.Forms.ToolStripButton();
            this.tsbt_OpenSol = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolSave = new System.Windows.Forms.ToolStripButton();
            this.tsbt_CreateProcess = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbt_CameraSet = new System.Windows.Forms.ToolStripButton();
            this.tsbt_PlcSet = new System.Windows.Forms.ToolStripButton();
            this.tsbt_UserSet = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolRunStart = new System.Windows.Forms.ToolStripButton();
            this.tsbt_SolRunStop = new System.Windows.Forms.ToolStripButton();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.运行模式ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
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
            this.另存方案ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建方案ToolStripMenuItem
            // 
            this.新建方案ToolStripMenuItem.Name = "新建方案ToolStripMenuItem";
            this.新建方案ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.新建方案ToolStripMenuItem.Text = "新建方案";
            this.新建方案ToolStripMenuItem.Click += new System.EventHandler(this.新建方案ToolStripMenuItem_Click);
            // 
            // 打开方案ToolStripMenuItem
            // 
            this.打开方案ToolStripMenuItem.Name = "打开方案ToolStripMenuItem";
            this.打开方案ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.打开方案ToolStripMenuItem.Text = "打开方案";
            // 
            // 保存方案ToolStripMenuItem
            // 
            this.保存方案ToolStripMenuItem.Name = "保存方案ToolStripMenuItem";
            this.保存方案ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.保存方案ToolStripMenuItem.Text = "保存方案";
            // 
            // 另存方案ToolStripMenuItem
            // 
            this.另存方案ToolStripMenuItem.Name = "另存方案ToolStripMenuItem";
            this.另存方案ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.另存方案ToolStripMenuItem.Text = "另存方案";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 运行模式ToolStripMenuItem
            // 
            this.运行模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在线自动模式ToolStripMenuItem,
            this.在线点检模式ToolStripMenuItem,
            this.离线自动模式ToolStripMenuItem,
            this.离线点检模式ToolStripMenuItem});
            this.运行模式ToolStripMenuItem.Name = "运行模式ToolStripMenuItem";
            this.运行模式ToolStripMenuItem.Size = new System.Drawing.Size(98, 32);
            this.运行模式ToolStripMenuItem.Text = "运行模式";
            // 
            // 在线自动模式ToolStripMenuItem
            // 
            this.在线自动模式ToolStripMenuItem.Checked = true;
            this.在线自动模式ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.在线自动模式ToolStripMenuItem.Name = "在线自动模式ToolStripMenuItem";
            this.在线自动模式ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.在线自动模式ToolStripMenuItem.Text = "在线自动模式";
            this.在线自动模式ToolStripMenuItem.Click += new System.EventHandler(this.RunModeToolStripMenuItem_Click);
            // 
            // 在线点检模式ToolStripMenuItem
            // 
            this.在线点检模式ToolStripMenuItem.Name = "在线点检模式ToolStripMenuItem";
            this.在线点检模式ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.在线点检模式ToolStripMenuItem.Text = "在线点检模式";
            this.在线点检模式ToolStripMenuItem.Click += new System.EventHandler(this.RunModeToolStripMenuItem_Click);
            // 
            // 离线自动模式ToolStripMenuItem
            // 
            this.离线自动模式ToolStripMenuItem.Name = "离线自动模式ToolStripMenuItem";
            this.离线自动模式ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.离线自动模式ToolStripMenuItem.Text = "离线自动模式";
            this.离线自动模式ToolStripMenuItem.Click += new System.EventHandler(this.RunModeToolStripMenuItem_Click);
            // 
            // 离线点检模式ToolStripMenuItem
            // 
            this.离线点检模式ToolStripMenuItem.Name = "离线点检模式ToolStripMenuItem";
            this.离线点检模式ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.离线点检模式ToolStripMenuItem.Text = "离线点检模式";
            this.离线点检模式ToolStripMenuItem.Click += new System.EventHandler(this.RunModeToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认布局ToolStripMenuItem,
            this.图像显示ToolStripMenuItem,
            this.检测结果ToolStripMenuItem,
            this.日志显示ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 默认布局ToolStripMenuItem
            // 
            this.默认布局ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认布局ToolStripMenuItem1,
            this.保存布局ToolStripMenuItem1});
            this.默认布局ToolStripMenuItem.Name = "默认布局ToolStripMenuItem";
            this.默认布局ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.默认布局ToolStripMenuItem.Text = "窗口布局";
            // 
            // 默认布局ToolStripMenuItem1
            // 
            this.默认布局ToolStripMenuItem1.Name = "默认布局ToolStripMenuItem1";
            this.默认布局ToolStripMenuItem1.Size = new System.Drawing.Size(182, 34);
            this.默认布局ToolStripMenuItem1.Text = "默认布局";
            this.默认布局ToolStripMenuItem1.Click += new System.EventHandler(this.默认布局ToolStripMenuItem1_Click);
            // 
            // 保存布局ToolStripMenuItem1
            // 
            this.保存布局ToolStripMenuItem1.Name = "保存布局ToolStripMenuItem1";
            this.保存布局ToolStripMenuItem1.Size = new System.Drawing.Size(182, 34);
            this.保存布局ToolStripMenuItem1.Text = "保存布局";
            this.保存布局ToolStripMenuItem1.Click += new System.EventHandler(this.保存布局ToolStripMenuItem_Click);
            // 
            // 图像显示ToolStripMenuItem
            // 
            this.图像显示ToolStripMenuItem.CheckOnClick = true;
            this.图像显示ToolStripMenuItem.Name = "图像显示ToolStripMenuItem";
            this.图像显示ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.图像显示ToolStripMenuItem.Text = "图像显示";
            // 
            // 检测结果ToolStripMenuItem
            // 
            this.检测结果ToolStripMenuItem.CheckOnClick = true;
            this.检测结果ToolStripMenuItem.Name = "检测结果ToolStripMenuItem";
            this.检测结果ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.检测结果ToolStripMenuItem.Text = "检测结果";
            // 
            // 日志显示ToolStripMenuItem
            // 
            this.日志显示ToolStripMenuItem.CheckOnClick = true;
            this.日志显示ToolStripMenuItem.Name = "日志显示ToolStripMenuItem";
            this.日志显示ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.日志显示ToolStripMenuItem.Text = "日志显示";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.画布设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.系统设置ToolStripMenuItem.Text = "系统全局设置";
            // 
            // 画布设置ToolStripMenuItem
            // 
            this.画布设置ToolStripMenuItem.Name = "画布设置ToolStripMenuItem";
            this.画布设置ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.画布设置ToolStripMenuItem.Text = "图像画布设置";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用教程ToolStripMenuItem,
            this.联系我们ToolStripMenuItem1,
            this.关于YTVisionProToolStripMenuItem1});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 使用教程ToolStripMenuItem
            // 
            this.使用教程ToolStripMenuItem.Name = "使用教程ToolStripMenuItem";
            this.使用教程ToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.使用教程ToolStripMenuItem.Text = "使用教程";
            // 
            // 联系我们ToolStripMenuItem1
            // 
            this.联系我们ToolStripMenuItem1.Name = "联系我们ToolStripMenuItem1";
            this.联系我们ToolStripMenuItem1.Size = new System.Drawing.Size(248, 34);
            this.联系我们ToolStripMenuItem1.Text = "联系我们";
            // 
            // 关于YTVisionProToolStripMenuItem1
            // 
            this.关于YTVisionProToolStripMenuItem1.Name = "关于YTVisionProToolStripMenuItem1";
            this.关于YTVisionProToolStripMenuItem1.Size = new System.Drawing.Size(248, 34);
            this.关于YTVisionProToolStripMenuItem1.Text = "关于YTVisionPro";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbt_SolSet,
            this.tsbt_SolNew,
            this.tsbt_OpenSol,
            this.tsbt_SolSave,
            this.tsbt_CreateProcess,
            this.toolStripButton1,
            this.tsbt_CameraSet,
            this.tsbt_PlcSet,
            this.tsbt_UserSet,
            this.tsbt_SolRunStart,
            this.tsbt_SolRunStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 32);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1556, 41);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbt_SolSet
            // 
            this.tsbt_SolSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolSet.Image = global::YTVisionPro.Properties.Resources.方案设置;
            this.tsbt_SolSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolSet.Name = "tsbt_SolSet";
            this.tsbt_SolSet.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolSet.Text = "方案设置";
            this.tsbt_SolSet.ToolTipText = "方案设置";
            this.tsbt_SolSet.Click += new System.EventHandler(this.ToolStripButton_Click);
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
            // tsbt_OpenSol
            // 
            this.tsbt_OpenSol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_OpenSol.Image = global::YTVisionPro.Properties.Resources.打开项目;
            this.tsbt_OpenSol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_OpenSol.Name = "tsbt_OpenSol";
            this.tsbt_OpenSol.Size = new System.Drawing.Size(36, 36);
            this.tsbt_OpenSol.Text = "打开方案";
            this.tsbt_OpenSol.ToolTipText = "打开方案";
            this.tsbt_OpenSol.Click += new System.EventHandler(this.ToolStripButton_Click);
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
            // tsbt_CreateProcess
            // 
            this.tsbt_CreateProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_CreateProcess.Image = global::YTVisionPro.Properties.Resources.添加流程;
            this.tsbt_CreateProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_CreateProcess.Name = "tsbt_CreateProcess";
            this.tsbt_CreateProcess.Size = new System.Drawing.Size(36, 36);
            this.tsbt_CreateProcess.Text = "创建流程";
            this.tsbt_CreateProcess.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::YTVisionPro.Properties.Resources.光源;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "全局光源";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_CameraSet
            // 
            this.tsbt_CameraSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_CameraSet.Image = global::YTVisionPro.Properties.Resources.相机;
            this.tsbt_CameraSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_CameraSet.Name = "tsbt_CameraSet";
            this.tsbt_CameraSet.Size = new System.Drawing.Size(36, 36);
            this.tsbt_CameraSet.Text = "全局相机";
            this.tsbt_CameraSet.ToolTipText = "全局相机";
            this.tsbt_CameraSet.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_PlcSet
            // 
            this.tsbt_PlcSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_PlcSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbt_PlcSet.Image")));
            this.tsbt_PlcSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_PlcSet.Name = "tsbt_PlcSet";
            this.tsbt_PlcSet.Size = new System.Drawing.Size(36, 36);
            this.tsbt_PlcSet.Text = "全局通信";
            this.tsbt_PlcSet.ToolTipText = "全局PLC";
            this.tsbt_PlcSet.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_UserSet
            // 
            this.tsbt_UserSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_UserSet.Image = global::YTVisionPro.Properties.Resources.用户登录;
            this.tsbt_UserSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_UserSet.Name = "tsbt_UserSet";
            this.tsbt_UserSet.Size = new System.Drawing.Size(36, 36);
            this.tsbt_UserSet.Text = "用户登录";
            this.tsbt_UserSet.ToolTipText = "用户登录";
            this.tsbt_UserSet.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolRunStart
            // 
            this.tsbt_SolRunStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolRunStart.Image = global::YTVisionPro.Properties.Resources.单次执行;
            this.tsbt_SolRunStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolRunStart.Margin = new System.Windows.Forms.Padding(120, 2, 0, 3);
            this.tsbt_SolRunStart.Name = "tsbt_SolRunStart";
            this.tsbt_SolRunStart.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolRunStart.Text = "开始运行";
            this.tsbt_SolRunStart.ToolTipText = "开始运行";
            this.tsbt_SolRunStart.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // tsbt_SolRunStop
            // 
            this.tsbt_SolRunStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_SolRunStop.Image = global::YTVisionPro.Properties.Resources.停止执行;
            this.tsbt_SolRunStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_SolRunStop.Margin = new System.Windows.Forms.Padding(20, 2, 0, 3);
            this.tsbt_SolRunStop.Name = "tsbt_SolRunStop";
            this.tsbt_SolRunStop.Size = new System.Drawing.Size(36, 36);
            this.tsbt_SolRunStop.Text = "停止运行";
            this.tsbt_SolRunStop.ToolTipText = "停止运行";
            this.tsbt_SolRunStop.Click += new System.EventHandler(this.ToolStripButton_Click);
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
            this.MaximumSize = new System.Drawing.Size(2560, 1540);
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbt_SolSet;
        private System.Windows.Forms.ToolStripButton tsbt_SolNew;
        private System.Windows.Forms.ToolStripButton tsbt_OpenSol;
        private System.Windows.Forms.ToolStripButton tsbt_SolSave;
        private System.Windows.Forms.ToolStripButton tsbt_CameraSet;
        private System.Windows.Forms.ToolStripButton tsbt_PlcSet;
        private System.Windows.Forms.ToolStripButton tsbt_UserSet;
        private System.Windows.Forms.ToolStripButton tsbt_SolRunStart;
        private System.Windows.Forms.ToolStripButton tsbt_SolRunStop;
        private System.Windows.Forms.ToolStripMenuItem 新建方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认布局ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检测结果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日志显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画布设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用教程ToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ToolStripMenuItem 默认布局ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 保存布局ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 联系我们ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关于YTVisionProToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 运行模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在线自动模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在线点检模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离线自动模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离线点检模式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbt_CreateProcess;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

