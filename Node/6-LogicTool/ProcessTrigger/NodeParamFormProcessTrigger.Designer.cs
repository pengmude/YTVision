namespace TDJS_Vision.Node._6_LogicTool.ProcessTrigger
{
    partial class NodeParamFormProcessTrigger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeParamFormProcessTrigger));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelOK = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new TDJS_Vision.Node.NodeSubscription();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelNG = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxOKProcess = new System.Windows.Forms.ComboBox();
            this.comboBoxNGProcess = new System.Windows.Forms.ComboBox();
            this.comboBoxProcess = new System.Windows.Forms.ComboBox();
            this.buttonReset1 = new System.Windows.Forms.Button();
            this.buttonReset2 = new System.Windows.Forms.Button();
            this.buttonReset3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.labelOK, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelNG, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(497, 290);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelOK
            // 
            this.labelOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOK.AutoSize = true;
            this.labelOK.Enabled = false;
            this.labelOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOK.Location = new System.Drawing.Point(33, 75);
            this.labelOK.Name = "labelOK";
            this.labelOK.Size = new System.Drawing.Size(98, 18);
            this.labelOK.TabIndex = 0;
            this.labelOK.Text = "OK执行流程";
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Enabled = false;
            this.nodeSubscription1.Location = new System.Drawing.Point(194, 2);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(231, 50);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(274, 52);
            this.nodeSubscription1.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(22, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 22);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "订阅AI结果";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // labelNG
            // 
            this.labelNG.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelNG.AutoSize = true;
            this.labelNG.Enabled = false;
            this.labelNG.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNG.Location = new System.Drawing.Point(33, 131);
            this.labelNG.Name = "labelNG";
            this.labelNG.Size = new System.Drawing.Size(98, 18);
            this.labelNG.TabIndex = 0;
            this.labelNG.Text = "NG执行流程";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "直接执行流程";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(207, 238);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.comboBoxOKProcess, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxNGProcess, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxProcess, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonReset1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonReset2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonReset3, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(168, 59);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(326, 162);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // comboBoxOKProcess
            // 
            this.comboBoxOKProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxOKProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOKProcess.Enabled = false;
            this.comboBoxOKProcess.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxOKProcess.FormattingEnabled = true;
            this.comboBoxOKProcess.Location = new System.Drawing.Point(3, 14);
            this.comboBoxOKProcess.Name = "comboBoxOKProcess";
            this.comboBoxOKProcess.Size = new System.Drawing.Size(222, 26);
            this.comboBoxOKProcess.TabIndex = 1;
            // 
            // comboBoxNGProcess
            // 
            this.comboBoxNGProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxNGProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNGProcess.Enabled = false;
            this.comboBoxNGProcess.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxNGProcess.FormattingEnabled = true;
            this.comboBoxNGProcess.Location = new System.Drawing.Point(3, 68);
            this.comboBoxNGProcess.Name = "comboBoxNGProcess";
            this.comboBoxNGProcess.Size = new System.Drawing.Size(222, 26);
            this.comboBoxNGProcess.TabIndex = 1;
            // 
            // comboBoxProcess
            // 
            this.comboBoxProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcess.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxProcess.FormattingEnabled = true;
            this.comboBoxProcess.Location = new System.Drawing.Point(3, 122);
            this.comboBoxProcess.Name = "comboBoxProcess";
            this.comboBoxProcess.Size = new System.Drawing.Size(222, 26);
            this.comboBoxProcess.TabIndex = 1;
            // 
            // buttonReset1
            // 
            this.buttonReset1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReset1.Enabled = false;
            this.buttonReset1.Location = new System.Drawing.Point(239, 8);
            this.buttonReset1.Name = "buttonReset1";
            this.buttonReset1.Size = new System.Drawing.Size(75, 37);
            this.buttonReset1.TabIndex = 2;
            this.buttonReset1.Text = "重置";
            this.buttonReset1.UseVisualStyleBackColor = true;
            this.buttonReset1.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonReset2
            // 
            this.buttonReset2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReset2.Enabled = false;
            this.buttonReset2.Location = new System.Drawing.Point(239, 62);
            this.buttonReset2.Name = "buttonReset2";
            this.buttonReset2.Size = new System.Drawing.Size(75, 37);
            this.buttonReset2.TabIndex = 2;
            this.buttonReset2.Text = "重置";
            this.buttonReset2.UseVisualStyleBackColor = true;
            this.buttonReset2.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonReset3
            // 
            this.buttonReset3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReset3.Location = new System.Drawing.Point(239, 116);
            this.buttonReset3.Name = "buttonReset3";
            this.buttonReset3.Size = new System.Drawing.Size(75, 37);
            this.buttonReset3.TabIndex = 2;
            this.buttonReset3.Text = "重置";
            this.buttonReset3.UseVisualStyleBackColor = true;
            this.buttonReset3.Click += new System.EventHandler(this.button4_Click);
            // 
            // NodeParamFormProcessTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 324);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeParamFormProcessTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条件执行";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxOKProcess;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labelNG;
        private System.Windows.Forms.ComboBox comboBoxNGProcess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxProcess;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonReset1;
        private System.Windows.Forms.Button buttonReset2;
        private System.Windows.Forms.Button buttonReset3;
    }
}