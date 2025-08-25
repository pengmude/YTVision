namespace TDJS_Vision.Forms.ImageViewer
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
            this.保存图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.默认大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上次大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.保存图片ToolStripMenuItem,
            this.还原ToolStripMenuItem,
            this.清空图像ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 127);
            // 
            // 保存图片ToolStripMenuItem
            // 
            this.保存图片ToolStripMenuItem.Name = "保存图片ToolStripMenuItem";
            this.保存图片ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.保存图片ToolStripMenuItem.Text = "保存";
            this.保存图片ToolStripMenuItem.Click += new System.EventHandler(this.保存图片ToolStripMenuItem_Click);
            // 
            // 还原ToolStripMenuItem
            // 
            this.还原ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.默认大小ToolStripMenuItem,
            this.上次大小ToolStripMenuItem});
            this.还原ToolStripMenuItem.Name = "还原ToolStripMenuItem";
            this.还原ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.还原ToolStripMenuItem.Text = "恢复";
            // 
            // 默认大小ToolStripMenuItem
            // 
            this.默认大小ToolStripMenuItem.Name = "默认大小ToolStripMenuItem";
            this.默认大小ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.默认大小ToolStripMenuItem.Text = "默认大小";
            this.默认大小ToolStripMenuItem.Click += new System.EventHandler(this.默认大小ToolStripMenuItem_Click);
            // 
            // 上次大小ToolStripMenuItem
            // 
            this.上次大小ToolStripMenuItem.Name = "上次大小ToolStripMenuItem";
            this.上次大小ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.上次大小ToolStripMenuItem.Text = "缩放前大小";
            this.上次大小ToolStripMenuItem.Click += new System.EventHandler(this.上次大小ToolStripMenuItem_Click);
            // 
            // 清空图像ToolStripMenuItem
            // 
            this.清空图像ToolStripMenuItem.Name = "清空图像ToolStripMenuItem";
            this.清空图像ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.清空图像ToolStripMenuItem.Text = "清除图像";
            this.清空图像ToolStripMenuItem.Click += new System.EventHandler(this.清空图像ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem 保存图片ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 清空图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 默认大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上次大小ToolStripMenuItem;
    }
}
