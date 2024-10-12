namespace YTVisionPro.Node.ResultProcessing.ImageSave
{
    partial class ParamFormImageSave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormImageSave));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxCompress = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.nodeSubscriptionImg2Save = new YTVisionPro.Node.NodeSubscription();
            this.checkBoxBarCode = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxSaveWithNG = new System.Windows.Forms.CheckBox();
            this.checkBoxDayNight = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanelNG = new System.Windows.Forms.TableLayoutPanel();
            this.nodeSubscriptionAiRes = new YTVisionPro.Node.NodeSubscription();
            this.tableLayoutPanelDayNight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCompress = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelBarCode = new System.Windows.Forms.TableLayoutPanel();
            this.nodeSubscriptionBarCode = new YTVisionPro.Node.NodeSubscription();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tableLayoutPanelNG.SuspendLayout();
            this.tableLayoutPanelDayNight.SuspendLayout();
            this.tableLayoutPanelCompress.SuspendLayout();
            this.tableLayoutPanelBarCode.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 4);
            this.textBox1.Location = new System.Drawing.Point(230, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(651, 28);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(973, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "【存图路径】";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "【保存的图片】";
            // 
            // checkBoxCompress
            // 
            this.checkBoxCompress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxCompress.AutoSize = true;
            this.checkBoxCompress.Checked = true;
            this.checkBoxCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCompress.Location = new System.Drawing.Point(230, 405);
            this.checkBoxCompress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxCompress.Name = "checkBoxCompress";
            this.checkBoxCompress.Size = new System.Drawing.Size(106, 22);
            this.checkBoxCompress.TabIndex = 4;
            this.checkBoxCompress.Text = "是否压缩";
            this.checkBoxCompress.UseVisualStyleBackColor = true;
            this.checkBoxCompress.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(495, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 52);
            this.button2.TabIndex = 5;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "读取的条码:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "订阅AI检测结果:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscriptionImg2Save, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxBarCode, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxCompress, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxSaveWithNG, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxDayNight, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1114, 464);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "【图片命名】";
            // 
            // nodeSubscriptionImg2Save
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.nodeSubscriptionImg2Save, 3);
            this.nodeSubscriptionImg2Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSubscriptionImg2Save.Location = new System.Drawing.Point(192, 94);
            this.nodeSubscriptionImg2Save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscriptionImg2Save.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscriptionImg2Save.Name = "nodeSubscriptionImg2Save";
            this.nodeSubscriptionImg2Save.Size = new System.Drawing.Size(550, 88);
            this.nodeSubscriptionImg2Save.TabIndex = 3;
            // 
            // checkBoxBarCode
            // 
            this.checkBoxBarCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxBarCode.AutoSize = true;
            this.checkBoxBarCode.Location = new System.Drawing.Point(230, 219);
            this.checkBoxBarCode.Name = "checkBoxBarCode";
            this.checkBoxBarCode.Size = new System.Drawing.Size(106, 22);
            this.checkBoxBarCode.TabIndex = 7;
            this.checkBoxBarCode.Text = "读码命名";
            this.checkBoxBarCode.UseVisualStyleBackColor = true;
            this.checkBoxBarCode.CheckedChanged += new System.EventHandler(this.checkBoxBarCode_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "【存图子目录】";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 407);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 18);
            this.label11.TabIndex = 2;
            this.label11.Text = "【压缩选项】";
            // 
            // checkBoxSaveWithNG
            // 
            this.checkBoxSaveWithNG.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxSaveWithNG.AutoSize = true;
            this.checkBoxSaveWithNG.Location = new System.Drawing.Point(226, 311);
            this.checkBoxSaveWithNG.Name = "checkBoxSaveWithNG";
            this.checkBoxSaveWithNG.Size = new System.Drawing.Size(115, 22);
            this.checkBoxSaveWithNG.TabIndex = 7;
            this.checkBoxSaveWithNG.Text = "区分OK/NG";
            this.checkBoxSaveWithNG.UseVisualStyleBackColor = true;
            this.checkBoxSaveWithNG.CheckedChanged += new System.EventHandler(this.checkBoxSaveWithNG_CheckedChanged);
            // 
            // checkBoxDayNight
            // 
            this.checkBoxDayNight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxDayNight.AutoSize = true;
            this.checkBoxDayNight.Location = new System.Drawing.Point(387, 311);
            this.checkBoxDayNight.Name = "checkBoxDayNight";
            this.checkBoxDayNight.Size = new System.Drawing.Size(160, 22);
            this.checkBoxDayNight.TabIndex = 7;
            this.checkBoxDayNight.Text = "区分早晚班存图";
            this.checkBoxDayNight.UseVisualStyleBackColor = true;
            this.checkBoxDayNight.CheckedChanged += new System.EventHandler(this.checkBoxDayNight_CheckedChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label12, 2);
            this.label12.Location = new System.Drawing.Point(422, 221);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(278, 18);
            this.label12.TabIndex = 2;
            this.label12.Text = "(注：不勾选默认以当前时间命名)";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePicker2.CustomFormat = "HH:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(716, 38);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(122, 28);
            this.dateTimePicker2.TabIndex = 10;
            this.dateTimePicker2.Value = new System.DateTime(2024, 9, 3, 20, 0, 0, 0);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(272, 38);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(122, 28);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new System.DateTime(2024, 9, 3, 8, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "早班时间:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(510, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "晚班时间:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.tableLayoutPanelCompress.SetColumnSpan(this.label5, 2);
            this.label5.Location = new System.Drawing.Point(68, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "图片压缩阈值:(0-100):";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDown1.Location = new System.Drawing.Point(390, 38);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(110, 28);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.tableLayoutPanelCompress.SetColumnSpan(this.label10, 2);
            this.label10.Location = new System.Drawing.Point(669, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(332, 18);
            this.label10.TabIndex = 2;
            this.label10.Text = "(注：值越小压缩越严重，占用空间越小)";
            // 
            // tableLayoutPanelNG
            // 
            this.tableLayoutPanelNG.ColumnCount = 5;
            this.tableLayoutPanelNG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelNG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelNG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelNG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelNG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelNG.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanelNG.Controls.Add(this.nodeSubscriptionAiRes, 1, 0);
            this.tableLayoutPanelNG.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelNG.Location = new System.Drawing.Point(0, 464);
            this.tableLayoutPanelNG.Name = "tableLayoutPanelNG";
            this.tableLayoutPanelNG.RowCount = 1;
            this.tableLayoutPanelNG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelNG.Size = new System.Drawing.Size(1114, 105);
            this.tableLayoutPanelNG.TabIndex = 10;
            this.tableLayoutPanelNG.Visible = false;
            // 
            // nodeSubscriptionAiRes
            // 
            this.tableLayoutPanelNG.SetColumnSpan(this.nodeSubscriptionAiRes, 2);
            this.nodeSubscriptionAiRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSubscriptionAiRes.Location = new System.Drawing.Point(225, 2);
            this.nodeSubscriptionAiRes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscriptionAiRes.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscriptionAiRes.Name = "nodeSubscriptionAiRes";
            this.nodeSubscriptionAiRes.Size = new System.Drawing.Size(438, 101);
            this.nodeSubscriptionAiRes.TabIndex = 8;
            // 
            // tableLayoutPanelDayNight
            // 
            this.tableLayoutPanelDayNight.ColumnCount = 5;
            this.tableLayoutPanelDayNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelDayNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelDayNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelDayNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelDayNight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelDayNight.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanelDayNight.Controls.Add(this.dateTimePicker1, 1, 0);
            this.tableLayoutPanelDayNight.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanelDayNight.Controls.Add(this.dateTimePicker2, 3, 0);
            this.tableLayoutPanelDayNight.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelDayNight.Location = new System.Drawing.Point(0, 569);
            this.tableLayoutPanelDayNight.Name = "tableLayoutPanelDayNight";
            this.tableLayoutPanelDayNight.RowCount = 1;
            this.tableLayoutPanelDayNight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDayNight.Size = new System.Drawing.Size(1114, 105);
            this.tableLayoutPanelDayNight.TabIndex = 10;
            this.tableLayoutPanelDayNight.Visible = false;
            // 
            // tableLayoutPanelCompress
            // 
            this.tableLayoutPanelCompress.ColumnCount = 5;
            this.tableLayoutPanelCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelCompress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelCompress.Controls.Add(this.numericUpDown1, 2, 0);
            this.tableLayoutPanelCompress.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanelCompress.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanelCompress.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelCompress.Location = new System.Drawing.Point(0, 674);
            this.tableLayoutPanelCompress.Name = "tableLayoutPanelCompress";
            this.tableLayoutPanelCompress.RowCount = 1;
            this.tableLayoutPanelCompress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelCompress.Size = new System.Drawing.Size(1114, 105);
            this.tableLayoutPanelCompress.TabIndex = 10;
            // 
            // tableLayoutPanelBarCode
            // 
            this.tableLayoutPanelBarCode.ColumnCount = 5;
            this.tableLayoutPanelBarCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelBarCode.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanelBarCode.Controls.Add(this.nodeSubscriptionBarCode, 1, 0);
            this.tableLayoutPanelBarCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelBarCode.Location = new System.Drawing.Point(0, 779);
            this.tableLayoutPanelBarCode.Name = "tableLayoutPanelBarCode";
            this.tableLayoutPanelBarCode.RowCount = 1;
            this.tableLayoutPanelBarCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBarCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanelBarCode.Size = new System.Drawing.Size(1114, 105);
            this.tableLayoutPanelBarCode.TabIndex = 10;
            this.tableLayoutPanelBarCode.Visible = false;
            // 
            // nodeSubscriptionBarCode
            // 
            this.tableLayoutPanelBarCode.SetColumnSpan(this.nodeSubscriptionBarCode, 2);
            this.nodeSubscriptionBarCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSubscriptionBarCode.Location = new System.Drawing.Point(225, 2);
            this.nodeSubscriptionBarCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscriptionBarCode.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscriptionBarCode.Name = "nodeSubscriptionBarCode";
            this.nodeSubscriptionBarCode.Size = new System.Drawing.Size(438, 101);
            this.nodeSubscriptionBarCode.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 872);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1114, 81);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // ParamFormImageSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1114, 953);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanelBarCode);
            this.Controls.Add(this.tableLayoutPanelCompress);
            this.Controls.Add(this.tableLayoutPanelDayNight);
            this.Controls.Add(this.tableLayoutPanelNG);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ParamFormImageSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存图片参数设置";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tableLayoutPanelNG.ResumeLayout(false);
            this.tableLayoutPanelNG.PerformLayout();
            this.tableLayoutPanelDayNight.ResumeLayout(false);
            this.tableLayoutPanelDayNight.PerformLayout();
            this.tableLayoutPanelCompress.ResumeLayout(false);
            this.tableLayoutPanelCompress.PerformLayout();
            this.tableLayoutPanelBarCode.ResumeLayout(false);
            this.tableLayoutPanelBarCode.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NodeSubscription nodeSubscriptionImg2Save;
        private System.Windows.Forms.CheckBox checkBoxCompress;
        private System.Windows.Forms.Button button2;
        private NodeSubscription nodeSubscriptionBarCode;
        private System.Windows.Forms.Label label4;
        private NodeSubscription nodeSubscriptionAiRes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCompress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDayNight;
        private System.Windows.Forms.CheckBox checkBoxBarCode;
        private System.Windows.Forms.CheckBox checkBoxSaveWithNG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBarCode;
        private System.Windows.Forms.CheckBox checkBoxDayNight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
    }
}