namespace Logger
{
    partial class FrmLogger
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
            this.logHelper1 = new Logger.LogHelper();
            this.SuspendLayout();
            // 
            // logHelper1
            // 
            this.logHelper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logHelper1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logHelper1.Location = new System.Drawing.Point(0, 0);
            this.logHelper1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logHelper1.Name = "logHelper1";
            this.logHelper1.Size = new System.Drawing.Size(978, 525);
            this.logHelper1.TabIndex = 0;
            // 
            // FrmLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 525);
            this.Controls.Add(this.logHelper1);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmLogger";
            this.ShowIcon = false;
            this.Text = "程序运行日志";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogger_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogger_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmLogger_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Logger.LogHelper logHelper1;
    }
}