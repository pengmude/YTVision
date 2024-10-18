namespace YTVisionPro.Node.Tool.ImageShow
{
    partial class ParamFormImageShow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormImageShow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.WindowNameList = new System.Windows.Forms.ComboBox();
            this.WindowNameText = new System.Windows.Forms.Label();
            this.subImageText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.WindowNameList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.WindowNameText, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.subImageText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(424, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nodeSubscription1.Location = new System.Drawing.Point(163, 6);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(213, 49);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(245, 49);
            this.nodeSubscription1.TabIndex = 2;
            // 
            // WindowNameList
            // 
            this.WindowNameList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WindowNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WindowNameList.FormattingEnabled = true;
            this.WindowNameList.Location = new System.Drawing.Point(169, 80);
            this.WindowNameList.Margin = new System.Windows.Forms.Padding(2);
            this.WindowNameList.Name = "WindowNameList";
            this.WindowNameList.Size = new System.Drawing.Size(233, 26);
            this.WindowNameList.TabIndex = 1;
            // 
            // WindowNameText
            // 
            this.WindowNameText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WindowNameText.AutoSize = true;
            this.WindowNameText.Location = new System.Drawing.Point(34, 84);
            this.WindowNameText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WindowNameText.Name = "WindowNameText";
            this.WindowNameText.Size = new System.Drawing.Size(80, 18);
            this.WindowNameText.TabIndex = 0;
            this.WindowNameText.Text = "窗口名称";
            // 
            // subImageText
            // 
            this.subImageText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.subImageText.AutoSize = true;
            this.subImageText.Location = new System.Drawing.Point(34, 22);
            this.subImageText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subImageText.Name = "subImageText";
            this.subImageText.Size = new System.Drawing.Size(80, 18);
            this.subImageText.TabIndex = 0;
            this.subImageText.Text = "订阅图像";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(235, 138);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ParamFormImageShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 187);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ParamFormImageShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像显示";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label WindowNameText;
        private System.Windows.Forms.ComboBox WindowNameList;
        private System.Windows.Forms.Label subImageText;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Button button1;
    }
}