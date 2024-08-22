namespace YTVisionPro.Forms.ImageViewer
{
    partial class FrmSingleImage
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
            this.ytPictrueBox1 = new YTVisionPro.Forms.ImageViewer.YTPictrueBox();
            this.SuspendLayout();
            // 
            // ytPictrueBox1
            // 
            this.ytPictrueBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ytPictrueBox1.DisplayImageType = YTVisionPro.Forms.ImageViewer.DisplayImageType.RENDERIMG;
            this.ytPictrueBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ytPictrueBox1.Location = new System.Drawing.Point(0, 0);
            this.ytPictrueBox1.Name = "ytPictrueBox1";
            this.ytPictrueBox1.RenderImage = null;
            this.ytPictrueBox1.Size = new System.Drawing.Size(715, 603);
            this.ytPictrueBox1.SrcImage = null;
            this.ytPictrueBox1.TabIndex = 0;
            // 
            // FrmSingleImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 603);
            this.Controls.Add(this.ytPictrueBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSingleImage";
            this.ShowIcon = false;
            this.Text = "窗口标题";
            this.ResumeLayout(false);

        }

        #endregion

        private YTVisionPro.Forms.ImageViewer.YTPictrueBox ytPictrueBox1;
    }
}