namespace YTVisionPro.Node._4_Detection.FindLine
{
    partial class NodeParamFormFindLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeParamFormFindLine));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxCanny = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imageROIEditControl1 = new YTVisionPro.Forms.ShapeDraw.ImageROIEditControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxResult2 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBoxResult1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.checkBoxMoreParams = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBlurSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxThreshold1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxThreshold2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxUseL2 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMinLength = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxMaxDistance = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1313, 866);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCanny);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(462, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 427);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边缘检测图像";
            // 
            // pictureBoxCanny
            // 
            this.pictureBoxCanny.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCanny.Location = new System.Drawing.Point(3, 24);
            this.pictureBoxCanny.Name = "pictureBoxCanny";
            this.pictureBoxCanny.Size = new System.Drawing.Size(447, 400);
            this.pictureBoxCanny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCanny.TabIndex = 2;
            this.pictureBoxCanny.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imageROIEditControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(453, 427);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "原图(右键绘制ROI)";
            // 
            // imageROIEditControl1
            // 
            this.imageROIEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageROIEditControl1.Location = new System.Drawing.Point(3, 24);
            this.imageROIEditControl1.Name = "imageROIEditControl1";
            this.imageROIEditControl1.Size = new System.Drawing.Size(447, 400);
            this.imageROIEditControl1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxResult2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(462, 436);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(453, 427);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选后的直线";
            // 
            // pictureBoxResult2
            // 
            this.pictureBoxResult2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult2.Location = new System.Drawing.Point(3, 24);
            this.pictureBoxResult2.Name = "pictureBoxResult2";
            this.pictureBoxResult2.Size = new System.Drawing.Size(447, 400);
            this.pictureBoxResult2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult2.TabIndex = 2;
            this.pictureBoxResult2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBoxResult1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 436);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(453, 427);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测到的直线";
            // 
            // pictureBoxResult1
            // 
            this.pictureBoxResult1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult1.Location = new System.Drawing.Point(3, 24);
            this.pictureBoxResult1.Name = "pictureBoxResult1";
            this.pictureBoxResult1.Size = new System.Drawing.Size(447, 400);
            this.pictureBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult1.TabIndex = 2;
            this.pictureBoxResult1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(921, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(389, 860);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel3.Controls.Add(this.buttonRun, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.buttonSave, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.button3, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.nodeSubscription1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMoreParams, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(389, 190);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.Location = new System.Drawing.Point(62, 138);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(70, 40);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "执行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Location = new System.Drawing.Point(256, 138);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 40);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(233, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新图像";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // nodeSubscription1
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSubscription1.Location = new System.Drawing.Point(3, 2);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(383, 60);
            this.nodeSubscription1.TabIndex = 3;
            // 
            // checkBoxMoreParams
            // 
            this.checkBoxMoreParams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxMoreParams.AutoSize = true;
            this.checkBoxMoreParams.Location = new System.Drawing.Point(44, 83);
            this.checkBoxMoreParams.Name = "checkBoxMoreParams";
            this.checkBoxMoreParams.Size = new System.Drawing.Size(106, 22);
            this.checkBoxMoreParams.TabIndex = 5;
            this.checkBoxMoreParams.Text = "更多参数";
            this.checkBoxMoreParams.UseVisualStyleBackColor = true;
            this.checkBoxMoreParams.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxBlurSize, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxUseL2, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCount, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBoxMinLength, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.textBoxMaxDistance, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 0, 7);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 196);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.92657F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(389, 664);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 36);
            this.label3.TabIndex = 0;
            this.label3.Text = "高斯模糊(减少噪点,仅限奇数)";
            // 
            // textBoxBlurSize
            // 
            this.textBoxBlurSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBlurSize.Location = new System.Drawing.Point(241, 27);
            this.textBoxBlurSize.Name = "textBoxBlurSize";
            this.textBoxBlurSize.Size = new System.Drawing.Size(100, 28);
            this.textBoxBlurSize.TabIndex = 1;
            this.textBoxBlurSize.Text = "5";
            this.textBoxBlurSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "弱边缘(越小边缘噪点越多)";
            // 
            // textBoxThreshold1
            // 
            this.textBoxThreshold1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold1.Location = new System.Drawing.Point(241, 110);
            this.textBoxThreshold1.Name = "textBoxThreshold1";
            this.textBoxThreshold1.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold1.TabIndex = 1;
            this.textBoxThreshold1.Text = "50";
            this.textBoxThreshold1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 36);
            this.label2.TabIndex = 0;
            this.label2.Text = "强边缘(越大边缘噪点越少)";
            // 
            // textBoxThreshold2
            // 
            this.textBoxThreshold2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold2.Location = new System.Drawing.Point(241, 193);
            this.textBoxThreshold2.Name = "textBoxThreshold2";
            this.textBoxThreshold2.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold2.TabIndex = 1;
            this.textBoxThreshold2.Text = "150";
            this.textBoxThreshold2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 54);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否使用 L2 范数（启用时边缘检测更精准但耗时）";
            // 
            // checkBoxUseL2
            // 
            this.checkBoxUseL2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxUseL2.AutoSize = true;
            this.checkBoxUseL2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseL2.Checked = true;
            this.checkBoxUseL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseL2.Location = new System.Drawing.Point(280, 280);
            this.checkBoxUseL2.Name = "checkBoxUseL2";
            this.checkBoxUseL2.Size = new System.Drawing.Size(22, 21);
            this.checkBoxUseL2.TabIndex = 2;
            this.checkBoxUseL2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 355);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 36);
            this.label7.TabIndex = 0;
            this.label7.Text = "直线投票数(直线特征越明显值越大)";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCount.Location = new System.Drawing.Point(241, 359);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(100, 28);
            this.textBoxCount.TabIndex = 1;
            this.textBoxCount.Text = "80";
            this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 438);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 36);
            this.label8.TabIndex = 0;
            this.label8.Text = "直线的最短长度(小于设定值时直线被忽略)";
            // 
            // textBoxMinLength
            // 
            this.textBoxMinLength.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMinLength.Location = new System.Drawing.Point(241, 442);
            this.textBoxMinLength.Name = "textBoxMinLength";
            this.textBoxMinLength.Size = new System.Drawing.Size(100, 28);
            this.textBoxMinLength.TabIndex = 1;
            this.textBoxMinLength.Text = "50";
            this.textBoxMinLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 512);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 54);
            this.label9.TabIndex = 0;
            this.label9.Text = "两直线的最大距离(大于设定值时直线被忽略)";
            // 
            // textBoxMaxDistance
            // 
            this.textBoxMaxDistance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMaxDistance.Location = new System.Drawing.Point(241, 525);
            this.textBoxMaxDistance.Name = "textBoxMaxDistance";
            this.textBoxMaxDistance.Size = new System.Drawing.Size(100, 28);
            this.textBoxMaxDistance.TabIndex = 1;
            this.textBoxMaxDistance.Text = "100";
            this.textBoxMaxDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.SetColumnSpan(this.comboBox1, 2);
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "输出最长的一条",
            "输出最短的一条",
            "输出最上面一条",
            "输出最下面一条",
            "输出最左边一条",
            "输出最右边一条",
            "所有直线"});
            this.comboBox1.Location = new System.Drawing.Point(45, 609);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(299, 26);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // NodeParamFormFindLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 866);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeParamFormFindLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找直线参数";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.PictureBox pictureBoxResult1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxThreshold1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxThreshold2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBlurSize;
        private System.Windows.Forms.CheckBox checkBoxUseL2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMinLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxMaxDistance;
        private System.Windows.Forms.PictureBox pictureBoxCanny;
        private System.Windows.Forms.PictureBox pictureBoxResult2;
        private Forms.ShapeDraw.ImageROIEditControl imageROIEditControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonSave;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBoxMoreParams;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}