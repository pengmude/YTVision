namespace Tets_ResizePictrueBox
{
    partial class YTPictrueBox
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.还原ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存原图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.原图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.渲染图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存渲染图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(573, 466);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.还原ToolStripMenuItem,
            this.保存原图ToolStripMenuItem,
            this.保存渲染图ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 127);
            // 
            // 还原ToolStripMenuItem
            // 
            this.还原ToolStripMenuItem.Name = "还原ToolStripMenuItem";
            this.还原ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.还原ToolStripMenuItem.Text = "还原";
            this.还原ToolStripMenuItem.Click += new System.EventHandler(this.还原ToolStripMenuItem_Click);
            // 
            // 保存原图ToolStripMenuItem
            // 
            this.保存原图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.原图ToolStripMenuItem,
            this.渲染图ToolStripMenuItem});
            this.保存原图ToolStripMenuItem.Name = "保存原图ToolStripMenuItem";
            this.保存原图ToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.保存原图ToolStripMenuItem.Text = "显示";
            // 
            // 原图ToolStripMenuItem
            // 
            this.原图ToolStripMenuItem.Name = "原图ToolStripMenuItem";
            this.原图ToolStripMenuItem.Size = new System.Drawing.Size(164, 34);
            this.原图ToolStripMenuItem.Text = "原图";
            this.原图ToolStripMenuItem.Click += new System.EventHandler(this.原图ToolStripMenuItem_Click);
            // 
            // 渲染图ToolStripMenuItem
            // 
            this.渲染图ToolStripMenuItem.Name = "渲染图ToolStripMenuItem";
            this.渲染图ToolStripMenuItem.Size = new System.Drawing.Size(164, 34);
            this.渲染图ToolStripMenuItem.Text = "渲染图";
            this.渲染图ToolStripMenuItem.Click += new System.EventHandler(this.渲染图ToolStripMenuItem_Click);
            // 
            // 保存渲染图ToolStripMenuItem
            // 
            this.保存渲染图ToolStripMenuItem.Name = "保存渲染图ToolStripMenuItem";
            this.保存渲染图ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.保存渲染图ToolStripMenuItem.Text = "保存当前图片";
            this.保存渲染图ToolStripMenuItem.Click += new System.EventHandler(this.保存渲染图ToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "bmp图像 (*.bmp)|*.bmp|所有文件 (*.*)|*.*";
            // 
            // YTPictrueBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox1);
            this.Name = "YTPictrueBox";
            this.Size = new System.Drawing.Size(573, 466);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 还原ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存原图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存渲染图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 原图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 渲染图ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
