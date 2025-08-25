namespace TDJS_Vision.Forms.ImageViewer
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
            this.ytPictrueBox1 = new TDJS_Vision.Forms.ImageViewer.YTPictrueBox();
            this.SuspendLayout();
            // 
            // ytPictrueBox1
            // 
            this.ytPictrueBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ytPictrueBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ytPictrueBox1.Image = null;
            this.ytPictrueBox1.Location = new System.Drawing.Point(0, 0);
            this.ytPictrueBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ytPictrueBox1.Name = "ytPictrueBox1";
            this.ytPictrueBox1.Size = new System.Drawing.Size(636, 502);
            this.ytPictrueBox1.TabIndex = 0;
            // 
            // FrmSingleImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 502);
            this.Controls.Add(this.ytPictrueBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmSingleImage";
            this.ShowIcon = false;
            this.Text = "窗口标题";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSingleImage_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private TDJS_Vision.Forms.ImageViewer.YTPictrueBox ytPictrueBox1;
    }
}