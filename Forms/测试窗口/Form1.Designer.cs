namespace YTVisionPro
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.logHelper1 = new Logger.LogHelper();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(680, 456);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "Fatal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(34, 456);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "Info";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(191, 456);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 46);
            this.button3.TabIndex = 1;
            this.button3.Text = "Debug";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(357, 456);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 46);
            this.button4.TabIndex = 1;
            this.button4.Text = "Warn";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(522, 456);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 46);
            this.button5.TabIndex = 1;
            this.button5.Text = "Exception";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // logHelper1
            // 
            this.logHelper1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logHelper1.Location = new System.Drawing.Point(34, 54);
            this.logHelper1.Name = "logHelper1";
            this.logHelper1.Size = new System.Drawing.Size(676, 347);
            this.logHelper1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 639);
            this.Controls.Add(this.logHelper1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private Logger.LogHelper logHelper1;
    }
}