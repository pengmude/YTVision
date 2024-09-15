namespace YTVisionPro.Node.AI.HTAI
{
    partial class ParamFormHTAI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormHTAI));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.tbTreeFlie = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btInitTreeFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNodes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbClasses = new System.Windows.Forms.ComboBox();
            this.tbMaxScore = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMinScore = new System.Windows.Forms.TextBox();
            this.tbMinArea = new System.Windows.Forms.TextBox();
            this.tbMaxArea = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMinNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMaxNum = new System.Windows.Forms.TextBox();
            this.btIsOK = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.WindowNameList = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 27);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(193, 592);
            this.treeView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(664, 72);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "选择模型";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbTreeFlie
            // 
            this.tbTreeFlie.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.tbTreeFlie, 2);
            this.tbTreeFlie.Location = new System.Drawing.Point(208, 76);
            this.tbTreeFlie.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbTreeFlie.Name = "tbTreeFlie";
            this.tbTreeFlie.ReadOnly = true;
            this.tbTreeFlie.Size = new System.Drawing.Size(447, 28);
            this.tbTreeFlie.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择tree文件:";
            // 
            // btInitTreeFile
            // 
            this.btInitTreeFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btInitTreeFile.Location = new System.Drawing.Point(788, 72);
            this.btInitTreeFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btInitTreeFile.Name = "btInitTreeFile";
            this.btInitTreeFile.Size = new System.Drawing.Size(117, 35);
            this.btInitTreeFile.TabIndex = 2;
            this.btInitTreeFile.Text = "初始化模型";
            this.btInitTreeFile.UseVisualStyleBackColor = true;
            this.btInitTreeFile.Click += new System.EventHandler(this.btInitTreeFile_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.47993F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.34994F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.30854F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.31165F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.54993F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbTreeFlie, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btInitTreeFile, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(915, 751);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 4);
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(208, 124);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(704, 623);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "节点配置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbNodes, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbClasses, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbMaxScore, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbMinScore, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.tbMinArea, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.tbMaxArea, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.label6, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label7, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label9, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.tbMinNum, 2, 7);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.tbMaxNum, 2, 8);
            this.tableLayoutPanel2.Controls.Add(this.btIsOK, 2, 9);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.WindowNameList, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 2, 11);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(698, 585);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(298, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "节点名称:";
            // 
            // cbNodes
            // 
            this.cbNodes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNodes.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbNodes.FormattingEnabled = true;
            this.cbNodes.Location = new System.Drawing.Point(513, 10);
            this.cbNodes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbNodes.Name = "cbNodes";
            this.cbNodes.Size = new System.Drawing.Size(136, 28);
            this.cbNodes.TabIndex = 0;
            this.cbNodes.SelectedIndexChanged += new System.EventHandler(this.cbNodes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(298, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "类别名称:";
            // 
            // cbClasses
            // 
            this.cbClasses.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasses.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbClasses.FormattingEnabled = true;
            this.cbClasses.Location = new System.Drawing.Point(513, 58);
            this.cbClasses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbClasses.Name = "cbClasses";
            this.cbClasses.Size = new System.Drawing.Size(136, 28);
            this.cbClasses.TabIndex = 0;
            this.cbClasses.SelectedIndexChanged += new System.EventHandler(this.cbClasses_SelectedIndexChanged);
            // 
            // tbMaxScore
            // 
            this.tbMaxScore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMaxScore.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMaxScore.Location = new System.Drawing.Point(513, 297);
            this.tbMaxScore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxScore.Name = "tbMaxScore";
            this.tbMaxScore.Size = new System.Drawing.Size(136, 30);
            this.tbMaxScore.TabIndex = 3;
            this.tbMaxScore.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(278, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "面积下限(含):";
            // 
            // tbMinScore
            // 
            this.tbMinScore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMinScore.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMinScore.Location = new System.Drawing.Point(513, 249);
            this.tbMinScore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMinScore.Name = "tbMinScore";
            this.tbMinScore.Size = new System.Drawing.Size(136, 30);
            this.tbMinScore.TabIndex = 3;
            this.tbMinScore.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // tbMinArea
            // 
            this.tbMinArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMinArea.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMinArea.Location = new System.Drawing.Point(513, 153);
            this.tbMinArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMinArea.Name = "tbMinArea";
            this.tbMinArea.Size = new System.Drawing.Size(136, 30);
            this.tbMinArea.TabIndex = 3;
            this.tbMinArea.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // tbMaxArea
            // 
            this.tbMaxArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMaxArea.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMaxArea.Location = new System.Drawing.Point(513, 201);
            this.tbMaxArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxArea.Name = "tbMaxArea";
            this.tbMaxArea.Size = new System.Drawing.Size(136, 30);
            this.tbMaxArea.TabIndex = 3;
            this.tbMaxArea.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(268, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "分数上限(不含):";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(268, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "面积上限(不含):";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(278, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "分数下限(含):";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(226, 24);
            this.label11.TabIndex = 5;
            this.label11.Text = "当前类别NG判定条件";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(258, 350);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "结果个数下限(含):";
            // 
            // tbMinNum
            // 
            this.tbMinNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMinNum.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMinNum.Location = new System.Drawing.Point(513, 345);
            this.tbMinNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMinNum.Name = "tbMinNum";
            this.tbMinNum.Size = new System.Drawing.Size(136, 30);
            this.tbMinNum.TabIndex = 3;
            this.tbMinNum.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(248, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "结果个数上限(不含):";
            // 
            // tbMaxNum
            // 
            this.tbMaxNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMaxNum.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMaxNum.Location = new System.Drawing.Point(513, 393);
            this.tbMaxNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxNum.Name = "tbMaxNum";
            this.tbMaxNum.Size = new System.Drawing.Size(136, 30);
            this.tbMaxNum.TabIndex = 3;
            this.tbMaxNum.TextChanged += new System.EventHandler(this.Tb_TextChanged);
            // 
            // btIsOK
            // 
            this.btIsOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btIsOK.AutoSize = true;
            this.btIsOK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btIsOK.Location = new System.Drawing.Point(533, 444);
            this.btIsOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btIsOK.Name = "btIsOK";
            this.btIsOK.Size = new System.Drawing.Size(95, 24);
            this.btIsOK.TabIndex = 2;
            this.btIsOK.Text = "强制OK";
            this.btIsOK.UseVisualStyleBackColor = true;
            this.btIsOK.CheckedChanged += new System.EventHandler(this.cbIsOK_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 491);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 26);
            this.label10.TabIndex = 6;
            this.label10.Text = "渲染图显示窗口";
            // 
            // WindowNameList
            // 
            this.WindowNameList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WindowNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WindowNameList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WindowNameList.FormattingEnabled = true;
            this.WindowNameList.Location = new System.Drawing.Point(257, 490);
            this.WindowNameList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.WindowNameList.Name = "WindowNameList";
            this.WindowNameList.Size = new System.Drawing.Size(182, 28);
            this.WindowNameList.TabIndex = 0;
            this.WindowNameList.SelectedIndexChanged += new System.EventHandler(this.cbClasses_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(525, 536);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(111, 40);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(199, 623);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择加载节点";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(58, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "订阅图像:";
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.nodeSubscription1, 2);
            this.nodeSubscription1.Location = new System.Drawing.Point(208, 3);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(447, 60);
            this.nodeSubscription1.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ParamFormHTAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 751);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamFormHTAI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI检测节点参数设置";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbTreeFlie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btInitTreeFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbNodes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbClasses;
        private System.Windows.Forms.CheckBox btIsOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbMaxNum;
        private System.Windows.Forms.TextBox tbMinNum;
        private System.Windows.Forms.TextBox tbMaxScore;
        private System.Windows.Forms.TextBox tbMinScore;
        private System.Windows.Forms.TextBox tbMaxArea;
        private System.Windows.Forms.TextBox tbMinArea;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label12;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox WindowNameList;
    }
}