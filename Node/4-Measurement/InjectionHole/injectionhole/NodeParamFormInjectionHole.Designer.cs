namespace YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement
{
    partial class NodeParamFormInjectionHole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeParamFormInjectionHole));
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.checkBoxUseL2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.nodeSubscription2 = new YTVisionPro.Node.NodeSubscription();
            this.textBoxOKMinR = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxThreshold2 = new System.Windows.Forms.TextBox();
            this.textBoxThreshold1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCanny);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(559, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(411, 411);
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
            this.pictureBoxCanny.Size = new System.Drawing.Size(405, 386);
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
            this.groupBox2.Size = new System.Drawing.Size(550, 411);
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
            this.imageROIEditControl1.Size = new System.Drawing.Size(544, 386);
            this.imageROIEditControl1.TabIndex = 4;
            // 
            // textBoxBlurSize
            // 
            this.textBoxBlurSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBlurSize.Enabled = false;
            this.textBoxBlurSize.Location = new System.Drawing.Point(1194, 27);
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
            this.textBoxOKMaxR.Location = new System.Drawing.Point(627, 191);
            this.textBoxOKMaxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMaxR.Name = "textBoxOKMaxR";
            this.textBoxOKMaxR.Size = new System.Drawing.Size(126, 28);
            this.textBoxOKMaxR.TabIndex = 6;
            this.textBoxOKMaxR.Text = "1.525";
            this.textBoxOKMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBoxResult1
            // 
            this.pictureBoxResult1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult1.Location = new System.Drawing.Point(3, 23);
            this.pictureBoxResult1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxResult1.Name = "pictureBoxResult1";
            this.pictureBoxResult1.Size = new System.Drawing.Size(405, 386);
            this.pictureBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult1.TabIndex = 2;
            this.pictureBoxResult1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(79, 267);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新订阅";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxMoreParams
            // 
            this.checkBoxMoreParams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxMoreParams.AutoSize = true;
            this.checkBoxMoreParams.Location = new System.Drawing.Point(637, 276);
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
            this.buttonSave.Location = new System.Drawing.Point(655, 349);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 40);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(930, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "弱边缘?";
            this.toolTip1.SetToolTip(this.label1, "越小边缘噪点越多,影响边缘检测图像");
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.Location = new System.Drawing.Point(379, 267);
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
            this.checkBoxUseL2.Enabled = false;
            this.checkBoxUseL2.Location = new System.Drawing.Point(1233, 276);
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
            this.label3.Location = new System.Drawing.Point(921, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "高斯模糊?";
            this.toolTip1.SetToolTip(this.label3, "减少噪点,仅限奇数,影响边缘检测图像");
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel4.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(328, 7);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(213, 50);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(448, 67);
            this.nodeSubscription1.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(98, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "订阅图像";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.checkBox1.Location = new System.Drawing.Point(69, 101);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 43);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "启用模版定位？";
            this.toolTip1.SetToolTip(this.checkBox1, "不需要手动绘制ROI定位注液孔，通过订阅模版匹配结果自动定位");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // nodeSubscription2
            // 
            this.nodeSubscription2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel4.SetColumnSpan(this.nodeSubscription2, 2);
            this.nodeSubscription2.Enabled = false;
            this.nodeSubscription2.Location = new System.Drawing.Point(328, 89);
            this.nodeSubscription2.Margin = new System.Windows.Forms.Padding(2);
            this.nodeSubscription2.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription2.Name = "nodeSubscription2";
            this.nodeSubscription2.Size = new System.Drawing.Size(448, 67);
            this.nodeSubscription2.TabIndex = 0;
            // 
            // textBoxOKMinR
            // 
            this.textBoxOKMinR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMinR.Location = new System.Drawing.Point(351, 191);
            this.textBoxOKMinR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMinR.Name = "textBoxOKMinR";
            this.textBoxOKMinR.Size = new System.Drawing.Size(126, 28);
            this.textBoxOKMinR.TabIndex = 6;
            this.textBoxOKMinR.Text = "1.475";
            this.textBoxOKMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "孔径OK上下限(mm)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(885, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否启用 L2 范数?";
            this.toolTip1.SetToolTip(this.label4, "启用时边缘检测更精准但会增加耗时");
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(930, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "强边缘?";
            this.toolTip1.SetToolTip(this.label2, "越大边缘噪点越少,影响边缘检测图像");
            // 
            // textBoxThreshold2
            // 
            this.textBoxThreshold2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold2.Enabled = false;
            this.textBoxThreshold2.Location = new System.Drawing.Point(1194, 191);
            this.textBoxThreshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold2.Name = "textBoxThreshold2";
            this.textBoxThreshold2.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold2.TabIndex = 1;
            this.textBoxThreshold2.Text = "150";
            this.textBoxThreshold2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThreshold1
            // 
            this.textBoxThreshold1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold1.Enabled = false;
            this.textBoxThreshold1.Location = new System.Drawing.Point(1194, 109);
            this.textBoxThreshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold1.Name = "textBoxThreshold1";
            this.textBoxThreshold1.Size = new System.Drawing.Size(100, 28);
            this.textBoxThreshold1.TabIndex = 1;
            this.textBoxThreshold1.Text = "50";
            this.textBoxThreshold1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1390, 830);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBoxResult1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(976, 2);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(411, 411);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测到的注液孔轮廓";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 3);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.nodeSubscription2, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.nodeSubscription1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.checkBoxUseL2, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.checkBox1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxBlurSize, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxOKMinR, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxOKMaxR, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxThreshold2, 4, 2);
            this.tableLayoutPanel4.Controls.Add(this.label2, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxThreshold1, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.button3, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label1, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonSave, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.checkBoxMoreParams, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.buttonRun, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label5, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.textBoxScale, 4, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 417);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1384, 411);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(845, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "比例(单位像素对应的毫米数)";
            // 
            // textBoxScale
            // 
            this.textBoxScale.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxScale.Enabled = false;
            this.textBoxScale.Location = new System.Drawing.Point(1181, 355);
            this.textBoxScale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(126, 28);
            this.textBoxScale.TabIndex = 10;
            this.textBoxScale.Text = "0.0083";
            this.textBoxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 3000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // NodeParamFormInjectionHole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 830);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeParamFormInjectionHole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注液孔检测节点参数设置";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
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
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxOKMinR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxUseL2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxThreshold2;
        private System.Windows.Forms.TextBox textBoxThreshold1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        private NodeSubscription nodeSubscription2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxScale;
    }
}