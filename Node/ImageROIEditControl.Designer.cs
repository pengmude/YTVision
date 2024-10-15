namespace YTVisionPro.Node
{
    partial class ImageROIEditControl
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
            this.rOI绘制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圆形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卡尺工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.找直线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.找圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(582, 381);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除全部ToolStripMenuItem,
            this.rOI绘制ToolStripMenuItem,
            this.卡尺工具ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 94);
            // 
            // rOI绘制ToolStripMenuItem
            // 
            this.rOI绘制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.矩形ToolStripMenuItem,
            this.圆形ToolStripMenuItem});
            this.rOI绘制ToolStripMenuItem.Name = "rOI绘制ToolStripMenuItem";
            this.rOI绘制ToolStripMenuItem.Size = new System.Drawing.Size(152, 30);
            this.rOI绘制ToolStripMenuItem.Text = "ROI绘制";
            // 
            // 矩形ToolStripMenuItem
            // 
            this.矩形ToolStripMenuItem.Name = "矩形ToolStripMenuItem";
            this.矩形ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.矩形ToolStripMenuItem.Text = "矩形";
            this.矩形ToolStripMenuItem.Click += new System.EventHandler(this.矩形ToolStripMenuItem_Click);
            // 
            // 圆形ToolStripMenuItem
            // 
            this.圆形ToolStripMenuItem.Name = "圆形ToolStripMenuItem";
            this.圆形ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.圆形ToolStripMenuItem.Text = "圆形";
            this.圆形ToolStripMenuItem.Click += new System.EventHandler(this.圆形ToolStripMenuItem_Click);
            // 
            // 卡尺工具ToolStripMenuItem
            // 
            this.卡尺工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.找直线ToolStripMenuItem,
            this.找圆ToolStripMenuItem});
            this.卡尺工具ToolStripMenuItem.Name = "卡尺工具ToolStripMenuItem";
            this.卡尺工具ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.卡尺工具ToolStripMenuItem.Text = "卡尺工具绘制";
            // 
            // 找直线ToolStripMenuItem
            // 
            this.找直线ToolStripMenuItem.Name = "找直线ToolStripMenuItem";
            this.找直线ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.找直线ToolStripMenuItem.Text = "找直线";
            // 
            // 找圆ToolStripMenuItem
            // 
            this.找圆ToolStripMenuItem.Name = "找圆ToolStripMenuItem";
            this.找圆ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.找圆ToolStripMenuItem.Text = "找圆";
            // 
            // 清除全部ToolStripMenuItem
            // 
            this.清除全部ToolStripMenuItem.Name = "清除全部ToolStripMenuItem";
            this.清除全部ToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.清除全部ToolStripMenuItem.Text = "清除";
            this.清除全部ToolStripMenuItem.Click += new System.EventHandler(this.清除全部ToolStripMenuItem_Click);
            // 
            // ImageROIEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Name = "ImageROIEditControl";
            this.Size = new System.Drawing.Size(582, 381);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rOI绘制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圆形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 卡尺工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 找直线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 找圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除全部ToolStripMenuItem;
    }
}
