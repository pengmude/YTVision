namespace YTVisionPro.Node._5_Measurement.InjectionHoleMeasurement
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxCanny = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxOKMinR = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxThreshold1 = new System.Windows.Forms.TextBox();
            this.textBoxThreshold2 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageROIEditControl1 = new YTVisionPro.Forms.ShapeDraw.ImageROIEditControl();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanny)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCanny);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(524, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(515, 397);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边缘检测图像";
            // 
            // pictureBoxCanny
            // 
            this.pictureBoxCanny.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCanny.Location = new System.Drawing.Point(3, 20);
            this.pictureBoxCanny.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxCanny.Name = "pictureBoxCanny";
            this.pictureBoxCanny.Size = new System.Drawing.Size(509, 375);
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
            this.groupBox2.Size = new System.Drawing.Size(515, 397);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "原图(右键绘制ROI)";
            // 
            // textBoxBlurSize
            // 
            this.textBoxBlurSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBlurSize.Location = new System.Drawing.Point(287, 56);
            this.textBoxBlurSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxBlurSize.Name = "textBoxBlurSize";
            this.textBoxBlurSize.Size = new System.Drawing.Size(89, 25);
            this.textBoxBlurSize.TabIndex = 1;
            this.textBoxBlurSize.Text = "5";
            this.textBoxBlurSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxOKMaxR
            // 
            this.textBoxOKMaxR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMaxR.Location = new System.Drawing.Point(275, 155);
            this.textBoxOKMaxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMaxR.Name = "textBoxOKMaxR";
            this.textBoxOKMaxR.Size = new System.Drawing.Size(112, 25);
            this.textBoxOKMaxR.TabIndex = 6;
            this.textBoxOKMaxR.Text = "110";
            this.textBoxOKMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBoxResult1
            // 
            this.pictureBoxResult1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult1.Location = new System.Drawing.Point(3, 20);
            this.pictureBoxResult1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxResult1.Name = "pictureBoxResult1";
            this.pictureBoxResult1.Size = new System.Drawing.Size(509, 376);
            this.pictureBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult1.TabIndex = 2;
            this.pictureBoxResult1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(279, 55);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 33);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新图像";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxMoreParams
            // 
            this.checkBoxMoreParams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxMoreParams.AutoSize = true;
            this.checkBoxMoreParams.Location = new System.Drawing.Point(65, 62);
            this.checkBoxMoreParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMoreParams.Name = "checkBoxMoreParams";
            this.checkBoxMoreParams.Size = new System.Drawing.Size(89, 19);
            this.checkBoxMoreParams.TabIndex = 5;
            this.checkBoxMoreParams.Text = "更多参数";
            this.checkBoxMoreParams.UseVisualStyleBackColor = true;
            this.checkBoxMoreParams.CheckedChanged += new System.EventHandler(this.checkBoxMoreParams_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Location = new System.Drawing.Point(300, 200);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(62, 33);
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
            this.checkBoxOKEnable.Location = new System.Drawing.Point(301, 110);
            this.checkBoxOKEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxOKEnable.Name = "checkBoxOKEnable";
            this.checkBoxOKEnable.Size = new System.Drawing.Size(59, 19);
            this.checkBoxOKEnable.TabIndex = 5;
            this.checkBoxOKEnable.Text = "启用";
            this.checkBoxOKEnable.UseVisualStyleBackColor = true;
            this.checkBoxOKEnable.CheckedChanged += new System.EventHandler(this.checkBoxOKEnable_CheckedChanged_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "弱边缘(越小边缘噪点越多,影响边缘检测图像)";
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.Location = new System.Drawing.Point(79, 200);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(62, 33);
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
            this.checkBoxUseL2.Location = new System.Drawing.Point(322, 476);
            this.checkBoxUseL2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseL2.Name = "checkBoxUseL2";
            this.checkBoxUseL2.Size = new System.Drawing.Size(18, 17);
            this.checkBoxUseL2.TabIndex = 2;
            this.checkBoxUseL2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 30);
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
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(442, 242);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "圆半径上下限OK判定";
            // 
            // textBoxOKMinR
            // 
            this.textBoxOKMinR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMinR.Location = new System.Drawing.Point(54, 155);
            this.textBoxOKMinR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMinR.Name = "textBoxOKMinR";
            this.textBoxOKMinR.Size = new System.Drawing.Size(112, 25);
            this.textBoxOKMinR.TabIndex = 6;
            this.textBoxOKMinR.Text = "70";
            this.textBoxOKMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxBlurSize, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxUseL2, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 244);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(442, 555);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 330);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "强边缘(越大边缘噪点越少,影响边缘检测图像)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 469);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否使用 L2 范数（启用时边缘检测更精准但耗时）";
            // 
            // textBoxThreshold1
            // 
            this.textBoxThreshold1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold1.Location = new System.Drawing.Point(287, 194);
            this.textBoxThreshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold1.Name = "textBoxThreshold1";
            this.textBoxThreshold1.Size = new System.Drawing.Size(89, 25);
            this.textBoxThreshold1.TabIndex = 1;
            this.textBoxThreshold1.Text = "50";
            this.textBoxThreshold1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThreshold2
            // 
            this.textBoxThreshold2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold2.Location = new System.Drawing.Point(287, 332);
            this.textBoxThreshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold2.Name = "textBoxThreshold2";
            this.textBoxThreshold2.Size = new System.Drawing.Size(89, 25);
            this.textBoxThreshold2.TabIndex = 1;
            this.textBoxThreshold2.Text = "150";
            this.textBoxThreshold2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1490, 803);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBoxResult1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 403);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(515, 398);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测到的圆";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1045, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(442, 799);
            this.panel1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(524, 404);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(515, 396);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边缘检测图像样例";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::YTVisionPro.Properties.Resources.注液孔检测样例图;
            this.pictureBox1.Location = new System.Drawing.Point(3, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(509, 372);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // imageROIEditControl1
            // 
            this.imageROIEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageROIEditControl1.Location = new System.Drawing.Point(3, 20);
            this.imageROIEditControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageROIEditControl1.Name = "imageROIEditControl1";
            this.imageROIEditControl1.Size = new System.Drawing.Size(509, 375);
            this.imageROIEditControl1.TabIndex = 4;
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(18, 2);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(231, 50);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(405, 50);
            this.nodeSubscription1.TabIndex = 3;
            // 
            // NodeParamFormInjectionHoleMeasurement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 803);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NodeParamFormInjectionHoleMeasurement";
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
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}