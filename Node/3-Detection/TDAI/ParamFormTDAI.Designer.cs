namespace TDJS_Vision.Node._3_Detection.TDAI
{
    partial class ParamFormTDAI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormTDAI));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new TDJS_Vision.Node.NodeSubscription();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxConfigPath = new System.Windows.Forms.TextBox();
            this.bt_ConfigSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxModelName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uiSwitch_Convert = new Sunny.UI.UISwitch();
            this.label_scale = new System.Windows.Forms.Label();
            this.textBox_Scale = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiSwitch_Learning = new Sunny.UI.UISwitch();
            this.label_studyNum = new System.Windows.Forms.Label();
            this.textBox_StudyNum = new System.Windows.Forms.TextBox();
            this.label_studyPercentage = new System.Windows.Forms.Label();
            this.textBox_studyPercentage = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxConfigPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.bt_ConfigSelect, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxModelName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.uiSwitch_Convert, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_scale, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Scale, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiSwitch_Learning, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_studyNum, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_StudyNum, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_studyPercentage, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_studyPercentage, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(675, 407);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(20, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "输入图像";
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(123, 9);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(231, 50);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(428, 50);
            this.nodeSubscription1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "模型配置";
            // 
            // textBoxConfigPath
            // 
            this.textBoxConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConfigPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxConfigPath, 2);
            this.textBoxConfigPath.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxConfigPath.Location = new System.Drawing.Point(123, 88);
            this.textBoxConfigPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxConfigPath.Name = "textBoxConfigPath";
            this.textBoxConfigPath.ReadOnly = true;
            this.textBoxConfigPath.Size = new System.Drawing.Size(428, 28);
            this.textBoxConfigPath.TabIndex = 3;
            // 
            // bt_ConfigSelect
            // 
            this.bt_ConfigSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_ConfigSelect.Cursor = System.Windows.Forms.Cursors.Default;
            this.bt_ConfigSelect.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_ConfigSelect.Location = new System.Drawing.Point(566, 84);
            this.bt_ConfigSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_ConfigSelect.Name = "bt_ConfigSelect";
            this.bt_ConfigSelect.Size = new System.Drawing.Size(96, 36);
            this.bt_ConfigSelect.TabIndex = 2;
            this.bt_ConfigSelect.Text = "选择";
            this.bt_ConfigSelect.UseVisualStyleBackColor = true;
            this.bt_ConfigSelect.Click += new System.EventHandler(this.bt_chooseLabel_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "模型名称";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.button1, 4);
            this.button1.Font = new System.Drawing.Font("宋体", 10.8F);
            this.button1.Location = new System.Drawing.Point(297, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxModelName
            // 
            this.comboBoxModelName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxModelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModelName.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxModelName.FormattingEnabled = true;
            this.comboBoxModelName.Items.AddRange(new object[] {
            "超日线芯",
            "超日胶壳",
            "超日连接器",
            "超日TypeC焊锡",
            "中厚12类端子",
            "线芯截面",
            "刺破机",
            "刺破机颜色线序"});
            this.comboBoxModelName.Location = new System.Drawing.Point(136, 157);
            this.comboBoxModelName.Name = "comboBoxModelName";
            this.comboBoxModelName.Size = new System.Drawing.Size(185, 26);
            this.comboBoxModelName.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "物理值转换";
            // 
            // uiSwitch_Convert
            // 
            this.uiSwitch_Convert.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiSwitch_Convert.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitch_Convert.Location = new System.Drawing.Point(191, 291);
            this.uiSwitch_Convert.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch_Convert.Name = "uiSwitch_Convert";
            this.uiSwitch_Convert.Size = new System.Drawing.Size(75, 29);
            this.uiSwitch_Convert.TabIndex = 11;
            this.uiSwitch_Convert.Text = "uiSwitch1";
            this.uiSwitch_Convert.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch_Convert_ValueChanged);
            // 
            // label_scale
            // 
            this.label_scale.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_scale.AutoSize = true;
            this.label_scale.Enabled = false;
            this.label_scale.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_scale.Location = new System.Drawing.Point(378, 297);
            this.label_scale.Name = "label_scale";
            this.label_scale.Size = new System.Drawing.Size(134, 18);
            this.label_scale.TabIndex = 4;
            this.label_scale.Text = "比例尺(mm/pix)";
            // 
            // textBox_Scale
            // 
            this.textBox_Scale.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Scale.Enabled = false;
            this.textBox_Scale.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Scale.Location = new System.Drawing.Point(564, 292);
            this.textBox_Scale.Name = "textBox_Scale";
            this.textBox_Scale.Size = new System.Drawing.Size(100, 28);
            this.textBox_Scale.TabIndex = 12;
            this.textBox_Scale.Text = "0.01";
            this.textBox_Scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(405, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "一键学习";
            // 
            // uiSwitch_Learning
            // 
            this.uiSwitch_Learning.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiSwitch_Learning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitch_Learning.Location = new System.Drawing.Point(577, 155);
            this.uiSwitch_Learning.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch_Learning.Name = "uiSwitch_Learning";
            this.uiSwitch_Learning.Size = new System.Drawing.Size(75, 29);
            this.uiSwitch_Learning.TabIndex = 9;
            this.uiSwitch_Learning.Text = "uiSwitch1";
            this.uiSwitch_Learning.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch_Learning_ValueChanged);
            // 
            // label_studyNum
            // 
            this.label_studyNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_studyNum.AutoSize = true;
            this.label_studyNum.Enabled = false;
            this.label_studyNum.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_studyNum.Location = new System.Drawing.Point(20, 229);
            this.label_studyNum.Name = "label_studyNum";
            this.label_studyNum.Size = new System.Drawing.Size(80, 18);
            this.label_studyNum.TabIndex = 4;
            this.label_studyNum.Text = "学习次数";
            // 
            // textBox_StudyNum
            // 
            this.textBox_StudyNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_StudyNum.Enabled = false;
            this.textBox_StudyNum.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_StudyNum.Location = new System.Drawing.Point(178, 224);
            this.textBox_StudyNum.Name = "textBox_StudyNum";
            this.textBox_StudyNum.Size = new System.Drawing.Size(100, 28);
            this.textBox_StudyNum.TabIndex = 12;
            this.textBox_StudyNum.Text = "3";
            this.textBox_StudyNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_studyPercentage
            // 
            this.label_studyPercentage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_studyPercentage.AutoSize = true;
            this.label_studyPercentage.Enabled = false;
            this.label_studyPercentage.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_studyPercentage.Location = new System.Drawing.Point(383, 229);
            this.label_studyPercentage.Name = "label_studyPercentage";
            this.label_studyPercentage.Size = new System.Drawing.Size(125, 18);
            this.label_studyPercentage.TabIndex = 4;
            this.label_studyPercentage.Text = "学习上下限(%)";
            // 
            // textBox_studyPercentage
            // 
            this.textBox_studyPercentage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_studyPercentage.Enabled = false;
            this.textBox_studyPercentage.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_studyPercentage.Location = new System.Drawing.Point(564, 224);
            this.textBox_studyPercentage.Name = "textBox_studyPercentage";
            this.textBox_studyPercentage.Size = new System.Drawing.Size(100, 28);
            this.textBox_studyPercentage.TabIndex = 12;
            this.textBox_studyPercentage.Text = "30";
            this.textBox_studyPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "AI配置文件(*.ai)|*.ai|所有文件(*.*)|*.*";
            // 
            // ParamFormTDAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 441);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamFormTDAI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI检测节点参数设置";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label12;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxConfigPath;
        private System.Windows.Forms.Button bt_ConfigSelect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UISwitch uiSwitch_Learning;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxModelName;
        private System.Windows.Forms.Label label4;
        private Sunny.UI.UISwitch uiSwitch_Convert;
        private System.Windows.Forms.Label label_scale;
        private System.Windows.Forms.TextBox textBox_Scale;
        private System.Windows.Forms.Label label_studyNum;
        private System.Windows.Forms.TextBox textBox_StudyNum;
        private System.Windows.Forms.Label label_studyPercentage;
        private System.Windows.Forms.TextBox textBox_studyPercentage;
    }
}