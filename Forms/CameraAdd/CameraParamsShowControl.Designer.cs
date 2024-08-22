namespace YTVisionPro.Forms.CameraAdd
{
    partial class CameraParamsShowControl
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
            this.ytPictrueBox1 = new YTVisionPro.Forms.ImageViewer.YTPictrueBox();
            this.SuspendLayout();
            // 
            // ytPictrueBox1
            // 
            this.ytPictrueBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ytPictrueBox1.DisplayImageType = YTVisionPro.Forms.ImageViewer.DisplayImageType.SRCIMG;
            this.ytPictrueBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ytPictrueBox1.Location = new System.Drawing.Point(0, 0);
            this.ytPictrueBox1.Name = "ytPictrueBox1";
            this.ytPictrueBox1.RenderImage = null;
            this.ytPictrueBox1.Size = new System.Drawing.Size(486, 472);
            this.ytPictrueBox1.SrcImage = null;
            this.ytPictrueBox1.TabIndex = 0;
            // 
            // CameraParamsShowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ytPictrueBox1);
            this.Name = "CameraParamsShowControl";
            this.Size = new System.Drawing.Size(486, 472);
            this.ResumeLayout(false);

        }

        #endregion

        private YTVisionPro.Forms.ImageViewer.YTPictrueBox ytPictrueBox1;
    }
}
