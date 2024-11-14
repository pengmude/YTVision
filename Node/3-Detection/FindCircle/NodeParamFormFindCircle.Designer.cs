namespace YTVisionPro.Node._3_Detection.FindCircle
{
    partial class NodeParamFormFindCircle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeParamFormFindCircle));
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
            this.button3 = new System.Windows.Forms.Button();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.checkBoxMoreParams = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.checkBoxOKEnable = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxOKMinR = new System.Windows.Forms.TextBox();
            this.textBoxOKMaxR = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxMaxR = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxMinR = new System.Windows.Forms.TextBox();
            this.checkBoxUseL2 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxThreshold2 = new System.Windows.Forms.TextBox();
            this.textBoxThreshold1 = new System.Windows.Forms.TextBox();
            this.textBoxBlurSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1676, 1034);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxCanny);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(589, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(580, 513);
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
            this.pictureBoxCanny.Size = new System.Drawing.Size(574, 488);
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
            this.groupBox2.Size = new System.Drawing.Size(580, 513);
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
            this.imageROIEditControl1.Size = new System.Drawing.Size(574, 488);
            this.imageROIEditControl1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxResult2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(589, 519);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(580, 513);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选后的圆";
            // 
            // pictureBoxResult2
            // 
            this.pictureBoxResult2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult2.Location = new System.Drawing.Point(3, 23);
            this.pictureBoxResult2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxResult2.Name = "pictureBoxResult2";
            this.pictureBoxResult2.Size = new System.Drawing.Size(574, 488);
            this.pictureBoxResult2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult2.TabIndex = 2;
            this.pictureBoxResult2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBoxResult1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 519);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(580, 513);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测到的圆";
            // 
            // pictureBoxResult1
            // 
            this.pictureBoxResult1.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult1.Location = new System.Drawing.Point(3, 23);
            this.pictureBoxResult1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxResult1.Name = "pictureBoxResult1";
            this.pictureBoxResult1.Size = new System.Drawing.Size(574, 488);
            this.pictureBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult1.TabIndex = 2;
            this.pictureBoxResult1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1175, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(498, 1030);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
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
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(498, 304);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(314, 80);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 40);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新图像";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel3.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(21, 6);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(456, 60);
            this.nodeSubscription1.TabIndex = 3;
            // 
            // checkBoxMoreParams
            // 
            this.checkBoxMoreParams.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxMoreParams.AutoSize = true;
            this.checkBoxMoreParams.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxMoreParams.Location = new System.Drawing.Point(64, 88);
            this.checkBoxMoreParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMoreParams.Name = "checkBoxMoreParams";
            this.checkBoxMoreParams.Size = new System.Drawing.Size(120, 25);
            this.checkBoxMoreParams.TabIndex = 5;
            this.checkBoxMoreParams.Text = "更多参数";
            this.checkBoxMoreParams.UseVisualStyleBackColor = true;
            this.checkBoxMoreParams.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(338, 253);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 40);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRun.Location = new System.Drawing.Point(89, 253);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(70, 40);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "执行";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // checkBoxOKEnable
            // 
            this.checkBoxOKEnable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxOKEnable.AutoSize = true;
            this.checkBoxOKEnable.Checked = true;
            this.checkBoxOKEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOKEnable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxOKEnable.Location = new System.Drawing.Point(334, 145);
            this.checkBoxOKEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxOKEnable.Name = "checkBoxOKEnable";
            this.checkBoxOKEnable.Size = new System.Drawing.Size(78, 25);
            this.checkBoxOKEnable.TabIndex = 5;
            this.checkBoxOKEnable.Text = "启用";
            this.checkBoxOKEnable.UseVisualStyleBackColor = true;
            this.checkBoxOKEnable.CheckedChanged += new System.EventHandler(this.checkBoxOKEnable_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(24, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(200, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "圆半径上下限OK判定";
            // 
            // textBoxOKMinR
            // 
            this.textBoxOKMinR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMinR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxOKMinR.Location = new System.Drawing.Point(61, 199);
            this.textBoxOKMinR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMinR.Name = "textBoxOKMinR";
            this.textBoxOKMinR.Size = new System.Drawing.Size(126, 31);
            this.textBoxOKMinR.TabIndex = 6;
            this.textBoxOKMinR.Text = "70";
            this.textBoxOKMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxOKMaxR
            // 
            this.textBoxOKMaxR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxOKMaxR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxOKMaxR.Location = new System.Drawing.Point(310, 199);
            this.textBoxOKMaxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxOKMaxR.Name = "textBoxOKMaxR";
            this.textBoxOKMaxR.Size = new System.Drawing.Size(126, 31);
            this.textBoxOKMaxR.TabIndex = 6;
            this.textBoxOKMaxR.Text = "110";
            this.textBoxOKMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxMaxR, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCount, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.textBoxMinR, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxUseL2, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.textBox2, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold2, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBoxThreshold1, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxBlurSize, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 308);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(498, 722);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Visible = false;
            // 
            // textBoxMaxR
            // 
            this.textBoxMaxR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMaxR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMaxR.Location = new System.Drawing.Point(323, 164);
            this.textBoxMaxR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMaxR.Name = "textBoxMaxR";
            this.textBoxMaxR.Size = new System.Drawing.Size(100, 31);
            this.textBoxMaxR.TabIndex = 1;
            this.textBoxMaxR.Text = "400";
            this.textBoxMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "输出最大的圆",
            "输出最小的圆",
            "输出最上面的圆",
            "输出最下面的圆",
            "输出最左边的圆",
            "输出最右边的圆"});
            this.comboBox1.Location = new System.Drawing.Point(273, 21);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 29);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(35, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(178, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "检测圆的最大半径";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(56, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "筛选输出的圆";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(14, 674);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "两圆心之间最小距离？";
            // 
            // textBoxCount
            // 
            this.textBoxCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCount.Location = new System.Drawing.Point(323, 669);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(100, 31);
            this.textBoxCount.TabIndex = 1;
            this.textBoxCount.Text = "15";
            this.textBoxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMinR
            // 
            this.textBoxMinR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxMinR.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMinR.Location = new System.Drawing.Point(323, 92);
            this.textBoxMinR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMinR.Name = "textBoxMinR";
            this.textBoxMinR.Size = new System.Drawing.Size(100, 31);
            this.textBoxMinR.TabIndex = 1;
            this.textBoxMinR.Text = "65";
            this.textBoxMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxUseL2
            // 
            this.checkBoxUseL2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxUseL2.AutoSize = true;
            this.checkBoxUseL2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxUseL2.Checked = true;
            this.checkBoxUseL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseL2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxUseL2.Location = new System.Drawing.Point(362, 601);
            this.checkBoxUseL2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxUseL2.Name = "checkBoxUseL2";
            this.checkBoxUseL2.Size = new System.Drawing.Size(22, 21);
            this.checkBoxUseL2.TabIndex = 2;
            this.checkBoxUseL2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(321, 524);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(104, 31);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "70";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(35, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "检测圆的最小半径";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(321, 452);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(105, 31);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "100";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThreshold2
            // 
            this.textBoxThreshold2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxThreshold2.Location = new System.Drawing.Point(323, 380);
            this.textBoxThreshold2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold2.Name = "textBoxThreshold2";
            this.textBoxThreshold2.Size = new System.Drawing.Size(100, 31);
            this.textBoxThreshold2.TabIndex = 1;
            this.textBoxThreshold2.Text = "150";
            this.textBoxThreshold2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxThreshold1
            // 
            this.textBoxThreshold1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxThreshold1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxThreshold1.Location = new System.Drawing.Point(323, 308);
            this.textBoxThreshold1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxThreshold1.Name = "textBoxThreshold1";
            this.textBoxThreshold1.Size = new System.Drawing.Size(100, 31);
            this.textBoxThreshold1.TabIndex = 1;
            this.textBoxThreshold1.Text = "50";
            this.textBoxThreshold1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxBlurSize
            // 
            this.textBoxBlurSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxBlurSize.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxBlurSize.Location = new System.Drawing.Point(323, 236);
            this.textBoxBlurSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxBlurSize.Name = "textBoxBlurSize";
            this.textBoxBlurSize.Size = new System.Drawing.Size(100, 31);
            this.textBoxBlurSize.TabIndex = 1;
            this.textBoxBlurSize.Text = "5";
            this.textBoxBlurSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(29, 601);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否使用 L2 范数?";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(61, 529);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "累加器阈值?";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(82, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "强边缘?";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(82, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "弱边缘?";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(72, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "高斯模糊?";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(55, 457);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "Canny高阈值?";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 3000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // NodeParamFormFindCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 1034);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeParamFormFindCircle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找圆参数";
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
        private System.Windows.Forms.TextBox textBoxMinR;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxMaxR;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxOKEnable;
        private System.Windows.Forms.TextBox textBoxOKMinR;
        private System.Windows.Forms.TextBox textBoxOKMaxR;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}