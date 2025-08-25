namespace TDJS_Vision
{
    partial class FormAIConfigTool
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxFilePath1 = new System.Windows.Forms.TextBox();
            this.textBoxFilePath2 = new System.Windows.Forms.TextBox();
            this.buttonOpen1 = new System.Windows.Forms.Button();
            this.buttonOpen2 = new System.Windows.Forms.Button();
            this.buttonSave1 = new System.Windows.Forms.Button();
            this.buttonSave2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1117, 735);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxFilePath1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFilePath2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonOpen1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonOpen2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave2, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1111, 74);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxFilePath1
            // 
            this.textBoxFilePath1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxFilePath1.Location = new System.Drawing.Point(36, 6);
            this.textBoxFilePath1.Name = "textBoxFilePath1";
            this.textBoxFilePath1.Size = new System.Drawing.Size(767, 25);
            this.textBoxFilePath1.TabIndex = 0;
            // 
            // textBoxFilePath2
            // 
            this.textBoxFilePath2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxFilePath2.Location = new System.Drawing.Point(37, 43);
            this.textBoxFilePath2.Name = "textBoxFilePath2";
            this.textBoxFilePath2.Size = new System.Drawing.Size(764, 25);
            this.textBoxFilePath2.TabIndex = 0;
            // 
            // buttonOpen1
            // 
            this.buttonOpen1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOpen1.Location = new System.Drawing.Point(860, 3);
            this.buttonOpen1.Name = "buttonOpen1";
            this.buttonOpen1.Size = new System.Drawing.Size(94, 31);
            this.buttonOpen1.TabIndex = 1;
            this.buttonOpen1.Text = "打开配置1";
            this.buttonOpen1.UseVisualStyleBackColor = true;
            this.buttonOpen1.Click += new System.EventHandler(this.buttonOpen1_Click);
            // 
            // buttonOpen2
            // 
            this.buttonOpen2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOpen2.Location = new System.Drawing.Point(860, 40);
            this.buttonOpen2.Name = "buttonOpen2";
            this.buttonOpen2.Size = new System.Drawing.Size(94, 31);
            this.buttonOpen2.TabIndex = 1;
            this.buttonOpen2.Text = "打开配置2";
            this.buttonOpen2.UseVisualStyleBackColor = true;
            this.buttonOpen2.Click += new System.EventHandler(this.buttonOpen2_Click);
            // 
            // buttonSave1
            // 
            this.buttonSave1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave1.Location = new System.Drawing.Point(996, 3);
            this.buttonSave1.Name = "buttonSave1";
            this.buttonSave1.Size = new System.Drawing.Size(94, 31);
            this.buttonSave1.TabIndex = 1;
            this.buttonSave1.Text = "保存";
            this.buttonSave1.UseVisualStyleBackColor = true;
            this.buttonSave1.Click += new System.EventHandler(this.buttonSave1_Click);
            // 
            // buttonSave2
            // 
            this.buttonSave2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave2.Location = new System.Drawing.Point(996, 40);
            this.buttonSave2.Name = "buttonSave2";
            this.buttonSave2.Size = new System.Drawing.Size(94, 31);
            this.buttonSave2.TabIndex = 1;
            this.buttonSave2.Text = "保存";
            this.buttonSave2.UseVisualStyleBackColor = true;
            this.buttonSave2.Click += new System.EventHandler(this.buttonSave2_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 83);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1111, 649);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(558, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(550, 643);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(549, 643);
            this.textBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "AI配置文件(*.ai)|*.ai|所有文件(*.*)|*.*";
            // 
            // FormAIConfigTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 769);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormAIConfigTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI配置编辑工具";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxFilePath1;
        private System.Windows.Forms.TextBox textBoxFilePath2;
        private System.Windows.Forms.Button buttonOpen1;
        private System.Windows.Forms.Button buttonOpen2;
        private System.Windows.Forms.Button buttonSave1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonSave2;
    }
}