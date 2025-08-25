namespace TDJS_Vision.Forms.PLCAdd
{
    partial class FrmPLCNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPLCNew));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxName1 = new System.Windows.Forms.TextBox();
            this.buttonConfirm1 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.uiipTextBoxIP1 = new Sunny.UI.UIIPTextBox();
            this.textBoxPort1 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxCom1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxParity1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBaute1 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxDataBit1 = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBit1 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonConfirm2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxName2 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxName3 = new System.Windows.Forms.TextBox();
            this.buttonConfirm3 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.uiipTextBoxIP2 = new Sunny.UI.UIIPTextBox();
            this.textBoxPort2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(535, 443);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLC设备信息";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 414);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(521, 382);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "三菱MC(Binary)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(521, 382);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel6.Controls.Add(this.label15, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.textBoxName1, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.buttonConfirm1, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.uiipTextBoxIP1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.textBoxPort1, 1, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 37);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(515, 307);
            this.tableLayoutPanel6.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(3, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(174, 20);
            this.label15.TabIndex = 1;
            this.label15.Text = "端口";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(3, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(174, 20);
            this.label16.TabIndex = 2;
            this.label16.Text = "IP地址";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName1
            // 
            this.textBoxName1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxName1.Location = new System.Drawing.Point(235, 175);
            this.textBoxName1.Name = "textBoxName1";
            this.textBoxName1.Size = new System.Drawing.Size(224, 30);
            this.textBoxName1.TabIndex = 0;
            this.textBoxName1.Text = "三菱PLC设备1";
            this.textBoxName1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonConfirm1
            // 
            this.buttonConfirm1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonConfirm1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConfirm1.Location = new System.Drawing.Point(293, 249);
            this.buttonConfirm1.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonConfirm1.Name = "buttonConfirm1";
            this.buttonConfirm1.Size = new System.Drawing.Size(108, 37);
            this.buttonConfirm1.TabIndex = 7;
            this.buttonConfirm1.Text = "确认";
            this.buttonConfirm1.UseVisualStyleBackColor = true;
            this.buttonConfirm1.Click += new System.EventHandler(this.buttonConfirm1_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(3, 180);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(174, 20);
            this.label20.TabIndex = 11;
            this.label20.Text = "设备名称";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiipTextBoxIP1
            // 
            this.uiipTextBoxIP1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiipTextBoxIP1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiipTextBoxIP1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiipTextBoxIP1.Location = new System.Drawing.Point(237, 24);
            this.uiipTextBoxIP1.Margin = new System.Windows.Forms.Padding(4);
            this.uiipTextBoxIP1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiipTextBoxIP1.Name = "uiipTextBoxIP1";
            this.uiipTextBoxIP1.Padding = new System.Windows.Forms.Padding(1);
            this.uiipTextBoxIP1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.uiipTextBoxIP1.ShowText = false;
            this.uiipTextBoxIP1.Size = new System.Drawing.Size(220, 28);
            this.uiipTextBoxIP1.TabIndex = 12;
            this.uiipTextBoxIP1.Text = "127.0.0.1";
            this.uiipTextBoxIP1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiipTextBoxIP1.Value = ((System.Net.IPAddress)(resources.GetObject("uiipTextBoxIP1.Value")));
            // 
            // textBoxPort1
            // 
            this.textBoxPort1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPort1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPort1.Location = new System.Drawing.Point(235, 99);
            this.textBoxPort1.Name = "textBoxPort1";
            this.textBoxPort1.Size = new System.Drawing.Size(224, 30);
            this.textBoxPort1.TabIndex = 0;
            this.textBoxPort1.Text = "6000";
            this.textBoxPort1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(521, 382);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "松下Mewtocol";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(515, 376);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxCom1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxParity1, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxBaute1, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label17, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxDataBit1, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxStopBit1, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label19, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.buttonConfirm2, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.textBoxName2, 1, 5);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(16, 2);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(482, 372);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(3, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(235, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "串口号";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxCom1
            // 
            this.comboBoxCom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCom1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxCom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCom1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxCom1.FormattingEnabled = true;
            this.comboBoxCom1.Location = new System.Drawing.Point(244, 12);
            this.comboBoxCom1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCom1.Name = "comboBoxCom1";
            this.comboBoxCom1.Size = new System.Drawing.Size(235, 28);
            this.comboBoxCom1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(3, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(235, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "波特率";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxParity1
            // 
            this.comboBoxParity1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxParity1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxParity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxParity1.FormattingEnabled = true;
            this.comboBoxParity1.Items.AddRange(new object[] {
            "无",
            "奇",
            "偶"});
            this.comboBoxParity1.Location = new System.Drawing.Point(244, 224);
            this.comboBoxParity1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxParity1.Name = "comboBoxParity1";
            this.comboBoxParity1.Size = new System.Drawing.Size(235, 28);
            this.comboBoxParity1.TabIndex = 5;
            // 
            // comboBoxBaute1
            // 
            this.comboBoxBaute1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaute1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxBaute1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaute1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxBaute1.FormattingEnabled = true;
            this.comboBoxBaute1.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxBaute1.Location = new System.Drawing.Point(244, 65);
            this.comboBoxBaute1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxBaute1.Name = "comboBoxBaute1";
            this.comboBoxBaute1.Size = new System.Drawing.Size(235, 28);
            this.comboBoxBaute1.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 12F);
            this.label17.Location = new System.Drawing.Point(3, 228);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(235, 20);
            this.label17.TabIndex = 9;
            this.label17.Text = "校验位";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxDataBit1
            // 
            this.comboBoxDataBit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDataBit1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxDataBit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBit1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxDataBit1.FormattingEnabled = true;
            this.comboBoxDataBit1.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxDataBit1.Location = new System.Drawing.Point(244, 118);
            this.comboBoxDataBit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDataBit1.Name = "comboBoxDataBit1";
            this.comboBoxDataBit1.Size = new System.Drawing.Size(235, 28);
            this.comboBoxDataBit1.TabIndex = 3;
            // 
            // comboBoxStopBit1
            // 
            this.comboBoxStopBit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStopBit1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxStopBit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBit1.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxStopBit1.FormattingEnabled = true;
            this.comboBoxStopBit1.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboBoxStopBit1.Location = new System.Drawing.Point(244, 171);
            this.comboBoxStopBit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxStopBit1.Name = "comboBoxStopBit1";
            this.comboBoxStopBit1.Size = new System.Drawing.Size(235, 28);
            this.comboBoxStopBit1.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F);
            this.label18.Location = new System.Drawing.Point(3, 122);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(235, 20);
            this.label18.TabIndex = 3;
            this.label18.Text = "数据位";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F);
            this.label19.Location = new System.Drawing.Point(3, 175);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(235, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "停止位";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonConfirm2
            // 
            this.buttonConfirm2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonConfirm2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConfirm2.Location = new System.Drawing.Point(307, 326);
            this.buttonConfirm2.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonConfirm2.Name = "buttonConfirm2";
            this.buttonConfirm2.Size = new System.Drawing.Size(108, 37);
            this.buttonConfirm2.TabIndex = 14;
            this.buttonConfirm2.Text = "确认";
            this.buttonConfirm2.UseVisualStyleBackColor = true;
            this.buttonConfirm2.Click += new System.EventHandler(this.buttonConfirm2_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(235, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "设备名称";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName2
            // 
            this.textBoxName2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxName2.Location = new System.Drawing.Point(249, 276);
            this.textBoxName2.Name = "textBoxName2";
            this.textBoxName2.Size = new System.Drawing.Size(224, 30);
            this.textBoxName2.TabIndex = 13;
            this.textBoxName2.Text = "松下PLC设备1";
            this.textBoxName2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(521, 382);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "松下Mewtocol OverTcp";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(515, 376);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel5.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.textBoxName3, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.buttonConfirm3, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.uiipTextBoxIP2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.textBoxPort2, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(6, 35);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(502, 306);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "端口";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(3, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "IP地址";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxName3
            // 
            this.textBoxName3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxName3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxName3.Location = new System.Drawing.Point(226, 175);
            this.textBoxName3.Name = "textBoxName3";
            this.textBoxName3.Size = new System.Drawing.Size(224, 30);
            this.textBoxName3.TabIndex = 0;
            this.textBoxName3.Text = "松下PLC设备1";
            this.textBoxName3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonConfirm3
            // 
            this.buttonConfirm3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonConfirm3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConfirm3.Location = new System.Drawing.Point(284, 248);
            this.buttonConfirm3.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonConfirm3.Name = "buttonConfirm3";
            this.buttonConfirm3.Size = new System.Drawing.Size(108, 37);
            this.buttonConfirm3.TabIndex = 7;
            this.buttonConfirm3.Text = "确认";
            this.buttonConfirm3.UseVisualStyleBackColor = true;
            this.buttonConfirm3.Click += new System.EventHandler(this.buttonConfirm3_Click);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(3, 180);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(169, 20);
            this.label14.TabIndex = 11;
            this.label14.Text = "设备名称";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiipTextBoxIP2
            // 
            this.uiipTextBoxIP2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiipTextBoxIP2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiipTextBoxIP2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiipTextBoxIP2.Location = new System.Drawing.Point(228, 25);
            this.uiipTextBoxIP2.Margin = new System.Windows.Forms.Padding(4);
            this.uiipTextBoxIP2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiipTextBoxIP2.Name = "uiipTextBoxIP2";
            this.uiipTextBoxIP2.Padding = new System.Windows.Forms.Padding(1);
            this.uiipTextBoxIP2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.uiipTextBoxIP2.ShowText = false;
            this.uiipTextBoxIP2.Size = new System.Drawing.Size(220, 26);
            this.uiipTextBoxIP2.TabIndex = 12;
            this.uiipTextBoxIP2.Text = "127.0.0.1";
            this.uiipTextBoxIP2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiipTextBoxIP2.Value = ((System.Net.IPAddress)(resources.GetObject("uiipTextBoxIP2.Value")));
            // 
            // textBoxPort2
            // 
            this.textBoxPort2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPort2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPort2.Location = new System.Drawing.Point(226, 99);
            this.textBoxPort2.Name = "textBoxPort2";
            this.textBoxPort2.Size = new System.Drawing.Size(224, 30);
            this.textBoxPort2.TabIndex = 0;
            this.textBoxPort2.Text = "6000";
            this.textBoxPort2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmPLCNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 477);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPLCNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加PLC";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxCom1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxParity1;
        private System.Windows.Forms.ComboBox comboBoxBaute1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxDataBit1;
        private System.Windows.Forms.ComboBox comboBoxStopBit1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonConfirm2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxName2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxName1;
        private System.Windows.Forms.Button buttonConfirm1;
        private System.Windows.Forms.Label label20;
        private Sunny.UI.UIIPTextBox uiipTextBoxIP1;
        private System.Windows.Forms.TextBox textBoxPort1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxName3;
        private System.Windows.Forms.Button buttonConfirm3;
        private System.Windows.Forms.Label label14;
        private Sunny.UI.UIIPTextBox uiipTextBoxIP2;
        private System.Windows.Forms.TextBox textBoxPort2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}