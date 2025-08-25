namespace TDJS_Vision.Forms.MyCSharpScript
{
    partial class CSharpScriptEditor
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSharpScriptEditor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClean = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.fctb = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.常用操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.消息弹窗ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.控制台输出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.task启动任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.执行延迟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modbus操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plc操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.流程操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实测值修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCV操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.fctb, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.documentMap1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(870, 486);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRun,
            this.toolStripButtonClean,
            this.toolStripButtonSaveAs,
            this.toolStripButtonSave,
            this.toolStripButtonOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 312);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(870, 54);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRun.Image = global::TDJS_Vision.Properties.Resources.单次执行;
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Margin = new System.Windows.Forms.Padding(0, 1, 20, 2);
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(40, 51);
            this.toolStripButtonRun.Text = "toolStripButton2";
            this.toolStripButtonRun.ToolTipText = "运行脚本";
            this.toolStripButtonRun.Click += new System.EventHandler(this.toolStripButtonRun_Click);
            // 
            // toolStripButtonClean
            // 
            this.toolStripButtonClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClean.Image = global::TDJS_Vision.Properties.Resources.打扫;
            this.toolStripButtonClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClean.Margin = new System.Windows.Forms.Padding(0, 1, 30, 2);
            this.toolStripButtonClean.Name = "toolStripButtonClean";
            this.toolStripButtonClean.Size = new System.Drawing.Size(40, 51);
            this.toolStripButtonClean.Text = "toolStripButton3";
            this.toolStripButtonClean.ToolTipText = "清理脚本上下文";
            this.toolStripButtonClean.Click += new System.EventHandler(this.toolStripButtonClean_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = global::TDJS_Vision.Properties.Resources.另存为;
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Margin = new System.Windows.Forms.Padding(0, 1, 30, 2);
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(40, 51);
            this.toolStripButtonSaveAs.Text = "toolStripButton5";
            this.toolStripButtonSaveAs.ToolTipText = "另存脚本";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::TDJS_Vision.Properties.Resources.保存项目;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Margin = new System.Windows.Forms.Padding(0, 1, 30, 2);
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(40, 51);
            this.toolStripButtonSave.Text = "toolStripButton1";
            this.toolStripButtonSave.ToolTipText = "保存脚本";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click_1);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::TDJS_Vision.Properties.Resources.打开项目;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Margin = new System.Windows.Forms.Padding(0, 1, 30, 2);
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(40, 51);
            this.toolStripButtonOpen.Text = "toolStripButton4";
            this.toolStripButtonOpen.ToolTipText = "打开脚本";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // fctb
            // 
            this.fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(33, 26);
            this.fctb.BackBrush = null;
            this.fctb.BackColor = System.Drawing.SystemColors.Control;
            this.fctb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fctb.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctb.CaretColor = System.Drawing.Color.DarkTurquoise;
            this.fctb.CharHeight = 26;
            this.fctb.CharWidth = 11;
            this.fctb.ContextMenuStrip = this.contextMenuStrip1;
            this.fctb.CurrentLineColor = System.Drawing.Color.DarkTurquoise;
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DelayedEventsInterval = 200;
            this.fctb.DelayedTextChangedInterval = 200;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.FoldingIndicatorColor = System.Drawing.Color.SpringGreen;
            this.fctb.Font = new System.Drawing.Font("Courier New", 10.8F);
            this.fctb.ImeMode = System.Windows.Forms.ImeMode.On;
            this.fctb.IsReplaceMode = false;
            this.fctb.Language = FastColoredTextBoxNS.Language.CSharp;
            this.fctb.LeftBracket = '(';
            this.fctb.LeftBracket2 = '{';
            this.fctb.LineInterval = 6;
            this.fctb.LineNumberColor = System.Drawing.Color.RoyalBlue;
            this.fctb.Location = new System.Drawing.Point(4, 3);
            this.fctb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fctb.Name = "fctb";
            this.fctb.Paddings = new System.Windows.Forms.Padding(0);
            this.fctb.RightBracket = ')';
            this.fctb.RightBracket2 = '}';
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb.ServiceColors")));
            this.fctb.ServiceLinesColor = System.Drawing.Color.WhiteSmoke;
            this.fctb.Size = new System.Drawing.Size(712, 306);
            this.fctb.TabIndex = 3;
            this.fctb.Zoom = 100;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.常用操作ToolStripMenuItem,
            this.相机操作ToolStripMenuItem,
            this.modbus操作ToolStripMenuItem,
            this.plc操作ToolStripMenuItem,
            this.流程操作ToolStripMenuItem,
            this.实测值修改ToolStripMenuItem,
            this.openCV操作ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 172);
            // 
            // 常用操作ToolStripMenuItem
            // 
            this.常用操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.消息弹窗ToolStripMenuItem,
            this.控制台输出ToolStripMenuItem,
            this.task启动任务ToolStripMenuItem,
            this.执行延迟ToolStripMenuItem});
            this.常用操作ToolStripMenuItem.Name = "常用操作ToolStripMenuItem";
            this.常用操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.常用操作ToolStripMenuItem.Text = "常用操作";
            // 
            // 消息弹窗ToolStripMenuItem
            // 
            this.消息弹窗ToolStripMenuItem.Name = "消息弹窗ToolStripMenuItem";
            this.消息弹窗ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.消息弹窗ToolStripMenuItem.Text = "消息弹窗";
            this.消息弹窗ToolStripMenuItem.Click += new System.EventHandler(this.消息弹窗ToolStripMenuItem_Click);
            // 
            // 控制台输出ToolStripMenuItem
            // 
            this.控制台输出ToolStripMenuItem.Name = "控制台输出ToolStripMenuItem";
            this.控制台输出ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.控制台输出ToolStripMenuItem.Text = "控制台输出";
            this.控制台输出ToolStripMenuItem.Click += new System.EventHandler(this.控制台输出ToolStripMenuItem_Click);
            // 
            // task启动任务ToolStripMenuItem
            // 
            this.task启动任务ToolStripMenuItem.Name = "task启动任务ToolStripMenuItem";
            this.task启动任务ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.task启动任务ToolStripMenuItem.Text = "Task启动任务";
            this.task启动任务ToolStripMenuItem.Click += new System.EventHandler(this.task启动任务ToolStripMenuItem_Click);
            // 
            // 执行延迟ToolStripMenuItem
            // 
            this.执行延迟ToolStripMenuItem.Name = "执行延迟ToolStripMenuItem";
            this.执行延迟ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.执行延迟ToolStripMenuItem.Text = "执行睡眠";
            this.执行延迟ToolStripMenuItem.Click += new System.EventHandler(this.执行延迟ToolStripMenuItem_Click);
            // 
            // 相机操作ToolStripMenuItem
            // 
            this.相机操作ToolStripMenuItem.Name = "相机操作ToolStripMenuItem";
            this.相机操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.相机操作ToolStripMenuItem.Text = "相机操作";
            this.相机操作ToolStripMenuItem.Click += new System.EventHandler(this.相机操作ToolStripMenuItem_Click);
            // 
            // modbus操作ToolStripMenuItem
            // 
            this.modbus操作ToolStripMenuItem.Name = "modbus操作ToolStripMenuItem";
            this.modbus操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.modbus操作ToolStripMenuItem.Text = "Modbus操作";
            this.modbus操作ToolStripMenuItem.Click += new System.EventHandler(this.modbus操作ToolStripMenuItem_Click);
            // 
            // plc操作ToolStripMenuItem
            // 
            this.plc操作ToolStripMenuItem.Name = "plc操作ToolStripMenuItem";
            this.plc操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.plc操作ToolStripMenuItem.Text = "PLC操作";
            this.plc操作ToolStripMenuItem.Click += new System.EventHandler(this.plc操作ToolStripMenuItem_Click);
            // 
            // 流程操作ToolStripMenuItem
            // 
            this.流程操作ToolStripMenuItem.Name = "流程操作ToolStripMenuItem";
            this.流程操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.流程操作ToolStripMenuItem.Text = "流程操作";
            this.流程操作ToolStripMenuItem.Click += new System.EventHandler(this.流程操作ToolStripMenuItem_Click);
            // 
            // 实测值修改ToolStripMenuItem
            // 
            this.实测值修改ToolStripMenuItem.Name = "实测值修改ToolStripMenuItem";
            this.实测值修改ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.实测值修改ToolStripMenuItem.Text = "检测结果修改";
            this.实测值修改ToolStripMenuItem.Click += new System.EventHandler(this.实测值修改ToolStripMenuItem_Click);
            // 
            // openCV操作ToolStripMenuItem
            // 
            this.openCV操作ToolStripMenuItem.Name = "openCV操作ToolStripMenuItem";
            this.openCV操作ToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.openCV操作ToolStripMenuItem.Text = "OpenCV操作";
            this.openCV操作ToolStripMenuItem.Click += new System.EventHandler(this.openCV操作ToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 2);
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(3, 369);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(864, 114);
            this.listBox1.TabIndex = 5;
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(723, 3);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(144, 306);
            this.documentMap1.TabIndex = 6;
            this.documentMap1.Target = this.fctb;
            this.documentMap1.Text = "documentMap1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "关键字");
            this.imageList1.Images.SetKeyName(1, "函数");
            this.imageList1.Images.SetKeyName(2, "片段");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CSharpScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CSharpScriptEditor";
            this.Size = new System.Drawing.Size(870, 486);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.ToolStripButton toolStripButtonClean;
        private FastColoredTextBoxNS.FastColoredTextBox fctb;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private FastColoredTextBoxNS.DocumentMap documentMap1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 相机操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plc操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 流程操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实测值修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCV操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modbus操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 常用操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 消息弹窗ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 控制台输出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem task启动任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 执行延迟ToolStripMenuItem;
    }
}
