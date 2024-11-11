namespace YTVisionPro.Node._4_Measurement.InjectionHole
{
    partial class NodeParamFormInjectionHoleMeasurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeParamFormInjectionHoleMeasurement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxCanny = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imageROIEditControl1 = new YTVisionPro.Forms.ShapeDraw.ImageROIEditControl();
            this.textBoxBlurSize = new System.Windows.Forms.TextBox();
            this.textBoxOKMaxR = new System.Windows.Forms.TextBox();
            this.pictureBoxResult1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBoxMoreParams = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxOKEnable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.checkBoxUseL2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxOKMinR = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxThreshold1 = new System.Windows.Forms.TextBox();
            this.textBoxThreshold2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCanny);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(476, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(467, 481);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边缘检测图像";
            // 
            // pictureBoxCanny
            // 
            this.pictureBoxCanny.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCanny.Location = new System.Drawing.Point(3, 23);
            this.pictureBoxCanny.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxCanny.Name = "pictureBoxCanny";
            this.pictureBoxCanny.Size = new System.Drawing.Size(461, 456);
            this.pictureBoxCanny.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCanny.TabIndex = 2;
            this.pictureBoxCanny.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imageROIEditControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(467, 481);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "原图(右键绘制ROI)";
            // 
            // imageROIEditControl1
            // 
            this.imageROIEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageROIEditControl1.Location = new System.Drawing.Point(3, 23);
            this.imageROIEditControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageROIEditControl1.Name = "imageROIEditControl1";
            this.imageROIEditControl1.Size = new System.Drawing.Size(461, 456);
            this.imageROIEditControl1.TabIndex = 4;
            // 
            // textBoxBlurSize
            // 
            this.textBoxBlurSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBlurSize.Location = new System.Drawing.Point(652, 121);
            this.textBoxBlurSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxBlurSize.Name = "textBoxBlurSize";
            this.textBoxBlurSize.Size = new System.Drawing.Size(100, 28);
            this.textBoxBlurSize.TabIndex = 1;
            this.textBoxBlurSize.Text = "5";
            this.textBoxBlurSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxOKMaxR
            // 
            this.textBoxOKMaxR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMaxR.Location = new System.Drawing.Point(288, 322);
            this.textBoxOKMaxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMaxR.Name = "textBoxOKMaxR";
            this.textBoxOKMaxR.Size = new System.Drawing.Size(126, 28);
            this.textBoxOKMaxR.TabIndex = 6;
            this.textBoxOKMaxR.Text = "110";
            this.textBoxOKMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBoxResult1
            // 
            this.pictureBoxResult1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult1.Location = new System.Drawing.Point(3, 23);
            this.pictureBoxResult1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxResult1.Name = "pictureBoxResult1";
            this.pictureBoxResult1.Size = new System.Drawing.Size(461, 456);
            this.pictureBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult1.TabIndex = 2;
            this.pictureBoxResult1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(293, 124);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新图像";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxMoreParams
            // 
            this.checkBoxMoreParams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxMoreParams.AutoSize = true;
            this.checkBoxMoreParams.Location = new System.Drawing.Point(64, 133);
            this.checkBoxMoreParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMoreParams.Name = "checkBoxMoreParams";
            this.checkBoxMoreParams.Size = new System.Drawing.Size(106, 22);
            this.checkBoxMoreParams.TabIndex = 5;
            this.checkBoxMoreParams.Text = "更多参数";
            this.checkBoxMoreParams.UseVisualStyleBackColor = true;
            this.checkBoxMoreParams.CheckedChanged += new System.EventHandler(this.checkBoxMoreParams_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Location = new System.Drawing.Point(316, 412);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 40);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxOKEnable
            // 
            this.checkBoxOKEnable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxOKEnable.AutoSize = true;
            this.checkBoxOKEnable.Checked = true;
            this.checkBoxOKEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOKEnable.Location = new System.Drawing.Point(316, 229);
            this.checkBoxOKEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxOKEnable.Name = "checkBoxOKEnable";
            this.checkBoxOKEnable.Size = new System.Drawing.Size(70, 22);
            this.checkBoxOKEnable.TabIndex = 5;
            this.checkBoxOKEnable.Text = "启用";
            this.checkBoxOKEnable.UseVisualStyleBackColor = true;
            this.checkBoxOKEnable.CheckedChanged += new System.EventHandler(this.checkBoxOKEnable_CheckedChanged_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "弱边缘(越小边缘噪点越多,影响边缘检测图像)";
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.Location = new System.Drawing.Point(82, 412);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(70, 40);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "执行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // checkBoxUseL2
            // 
            this.checkBoxUseL2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxUseL2.AutoSize = true;
            this.checkBoxUseL2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseL2.Checked = true;
            this.checkBoxUseL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseL2.Location = new System.Drawing.Point(691, 395);
            this.checkBoxUseL2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseL2.Name = "checkBoxUseL2";
            this.checkBoxUseL2.Size = new System.Drawing.Size(22, 21);
            this.checkBoxUseL2.TabIndex = 2;
            this.checkBoxUseL2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(404, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "高斯模糊(减少噪点,仅限奇数,影响边缘检测图像)";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.button3, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.nodeSubscription1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxMoreParams, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonSave, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.buttonRun, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxOKEnable, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBoxOKMinR, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBoxOKMaxR, 1, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(949, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(469, 481);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(27, 18);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(415, 60);
            this.nodeSubscription1.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 231);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "圆半径上下限OK判定";
            // 
            // textBoxOKMinR
            // 
            this.textBoxOKMinR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMinR.Location = new System.Drawing.Point(54, 322);
            this.textBoxOKMinR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMinR.Name = "textBoxOKMinR";
            this.textBoxOKMinR.Size = new System.Drawing.Size(126, 28);
            this.textBoxOKMinR.TabIndex = 6;
            this.textBoxOKMinR.Text = "70";
            this.textBoxOKMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxBlurSize, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold1, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold2, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxUseL2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(936, 452);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "强边缘(越大边缘噪点越少,影响边缘检测图像)";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(667, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "查看";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(422, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否使用 L2 范数（启用时边缘检测更精准但耗时）";
            // 
            // textBoxThreshold1
            // 
            this.textBoxThreshold1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold1.Location = new System.Drawing.Point(652, 211);
            this.textBoxThreshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold1.Name = "textBoxThreshold1";
            this.textBoxThreshold1.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold1.TabIndex = 1;
            this.textBoxThreshold1.Text = "50";
            this.textBoxThreshold1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThreshold2
            // 
            this.textBoxThreshold2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold2.Location = new System.Drawing.Point(652, 301);
            this.textBoxThreshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold2.Name = "textBoxThreshold2";
            this.textBoxThreshold2.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold2.TabIndex = 1;
            this.textBoxThreshold2.Text = "150";
            this.textBoxThreshold2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "参数调节最佳效果图";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1421, 970);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBoxResult1);
            this.groupBox4.Location = new System.Drawing.Point(3, 487);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(467, 481);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测到的圆";
            // 
            // groupBox3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(476, 488);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(942, 479);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "更多参数";
            this.groupBox3.Visible = false;
            // 
            // NodeParamFormInjectionHoleMeasurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1421, 970);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeParamFormInjectionHoleMeasurement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注液孔检测节点参数设置";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxCanny;
        private System.Windows.Forms.GroupBox groupBox2;
        private Forms.ShapeDraw.ImageROIEditControl imageROIEditControl1;
        private System.Windows.Forms.TextBox textBoxBlurSize;
        private System.Windows.Forms.TextBox textBoxOKMaxR;
        private System.Windows.Forms.PictureBox pictureBoxResult1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBoxMoreParams;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxOKEnable;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxOKMinR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxUseL2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxThreshold2;
        private System.Windows.Forms.TextBox textBoxThreshold1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}