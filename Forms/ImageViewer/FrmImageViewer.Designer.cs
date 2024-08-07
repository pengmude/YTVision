namespace YTVisionPro.Forms.ImageViewer
{
    partial class FrmImageViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示全部窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部隐藏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏活动窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.窗口ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel1.ShowAutoHideContentOnHover = false;
            this.dockPanel1.Size = new System.Drawing.Size(1192, 749);
            this.dockPanel1.SupportDeeplyNestedContent = true;
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Theme = this.vS2015LightTheme1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示全部窗口ToolStripMenuItem,
            this.全部隐藏ToolStripMenuItem,
            this.隐藏活动窗口ToolStripMenuItem,
            this.窗口ToolStripMenuItem,
            this.窗口ToolStripMenuItem1,
            this.窗口ToolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 184);
            // 
            // 显示全部窗口ToolStripMenuItem
            // 
            this.显示全部窗口ToolStripMenuItem.Name = "显示全部窗口ToolStripMenuItem";
            this.显示全部窗口ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.显示全部窗口ToolStripMenuItem.Text = "全部显示";
            // 
            // 全部隐藏ToolStripMenuItem
            // 
            this.全部隐藏ToolStripMenuItem.Name = "全部隐藏ToolStripMenuItem";
            this.全部隐藏ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.全部隐藏ToolStripMenuItem.Text = "全部隐藏";
            // 
            // 隐藏活动窗口ToolStripMenuItem
            // 
            this.隐藏活动窗口ToolStripMenuItem.Name = "隐藏活动窗口ToolStripMenuItem";
            this.隐藏活动窗口ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.隐藏活动窗口ToolStripMenuItem.Text = "隐藏活动窗口";
            // 
            // 窗口ToolStripMenuItem
            // 
            this.窗口ToolStripMenuItem.Name = "窗口ToolStripMenuItem";
            this.窗口ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.窗口ToolStripMenuItem.Text = "2窗口";
            this.窗口ToolStripMenuItem.Click += new System.EventHandler(this.窗口ToolStripMenuItem_Click);
            // 
            // 窗口ToolStripMenuItem1
            // 
            this.窗口ToolStripMenuItem1.Name = "窗口ToolStripMenuItem1";
            this.窗口ToolStripMenuItem1.Size = new System.Drawing.Size(240, 30);
            this.窗口ToolStripMenuItem1.Text = "4窗口";
            this.窗口ToolStripMenuItem1.Click += new System.EventHandler(this.窗口ToolStripMenuItem1_Click);
            // 
            // 窗口ToolStripMenuItem2
            // 
            this.窗口ToolStripMenuItem2.Name = "窗口ToolStripMenuItem2";
            this.窗口ToolStripMenuItem2.Size = new System.Drawing.Size(240, 30);
            this.窗口ToolStripMenuItem2.Text = "8窗口";
            // 
            // FrmImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 749);
            this.Controls.Add(this.dockPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmImageViewer";
            this.ShowIcon = false;
            this.Text = "检测图像";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示全部窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部隐藏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 隐藏活动窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 窗口ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 窗口ToolStripMenuItem2;
    }
}