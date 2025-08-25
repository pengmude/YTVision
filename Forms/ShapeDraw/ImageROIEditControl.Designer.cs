namespace TDJS_Vision.Forms.ShapeDraw
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
            this.清除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除上一个ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.绘制状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.圆形ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(517, 318);
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
            this.移除上一个ToolStripMenuItem,
            this.绘制状态ToolStripMenuItem,
            this.矩形ToolStripMenuItem1,
            this.圆形ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 162);
            // 
            // 清除全部ToolStripMenuItem
            // 
            this.清除全部ToolStripMenuItem.Name = "清除全部ToolStripMenuItem";
            this.清除全部ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.清除全部ToolStripMenuItem.Text = "清空所有ROI";
            this.清除全部ToolStripMenuItem.Click += new System.EventHandler(this.清除全部ToolStripMenuItem_Click);
            // 
            // 移除上一个ToolStripMenuItem
            // 
            this.移除上一个ToolStripMenuItem.Name = "移除上一个ToolStripMenuItem";
            this.移除上一个ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.移除上一个ToolStripMenuItem.Text = "移除上一个";
            this.移除上一个ToolStripMenuItem.Click += new System.EventHandler(this.移除上一个ToolStripMenuItem_Click);
            // 
            // 绘制状态ToolStripMenuItem
            // 
            this.绘制状态ToolStripMenuItem.Name = "绘制状态ToolStripMenuItem";
            this.绘制状态ToolStripMenuItem.Size = new System.Drawing.Size(210, 26);
            this.绘制状态ToolStripMenuItem.Text = "启用绘制状态";
            this.绘制状态ToolStripMenuItem.Click += new System.EventHandler(this.绘制状态ToolStripMenuItem_Click);
            // 
            // 矩形ToolStripMenuItem1
            // 
            this.矩形ToolStripMenuItem1.Checked = true;
            this.矩形ToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.矩形ToolStripMenuItem1.Name = "矩形ToolStripMenuItem1";
            this.矩形ToolStripMenuItem1.Size = new System.Drawing.Size(210, 26);
            this.矩形ToolStripMenuItem1.Text = "矩形ROI";
            this.矩形ToolStripMenuItem1.Click += new System.EventHandler(this.矩形ToolStripMenuItem1_Click);
            // 
            // 圆形ToolStripMenuItem1
            // 
            this.圆形ToolStripMenuItem1.Name = "圆形ToolStripMenuItem1";
            this.圆形ToolStripMenuItem1.Size = new System.Drawing.Size(210, 26);
            this.圆形ToolStripMenuItem1.Text = "圆形ROI";
            this.圆形ToolStripMenuItem1.Click += new System.EventHandler(this.圆形ToolStripMenuItem1_Click);
            // 
            // ImageROIEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ImageROIEditControl";
            this.Size = new System.Drawing.Size(517, 318);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矩形ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 圆形ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 移除上一个ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 绘制状态ToolStripMenuItem;
    }
}
