namespace TDJS_Vision.Forms.SolRunParam
{
    partial class FormSolRunParam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSolRunParam));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelRTU = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBit = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBit = new System.Windows.Forms.ComboBox();
            this.comboBoxBaute = new System.Windows.Forms.ComboBox();
            this.buttonCon1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.labelConnectInfo1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanelTCP = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxHoldTime = new System.Windows.Forms.TextBox();
            this.buttonCon2 = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelConnectInfo2 = new System.Windows.Forms.Label();
            this.uiipTextBoxIP = new Sunny.UI.UIIPTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelCon = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.labelTestInfo = new System.Windows.Forms.Label();
            this.comboBoxCoilValue = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanelRTU.SuspendLayout();
            this.tableLayoutPanelTCP.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanelCon.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(2, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(909, 465);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(901, 433);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Modbus通信";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(895, 320);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanelRTU);
            this.groupBox1.Controls.Add(this.tableLayoutPanelTCP);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信连接";
            // 
            // tableLayoutPanelRTU
            // 
            this.tableLayoutPanelRTU.ColumnCount = 6;
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelRTU.Controls.Add(this.comboBoxParity, 3, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.comboBoxStopBit, 1, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.comboBoxDataBit, 5, 0);
            this.tableLayoutPanelRTU.Controls.Add(this.comboBoxBaute, 3, 0);
            this.tableLayoutPanelRTU.Controls.Add(this.buttonCon1, 5, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.labelConnectInfo1, 4, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanelRTU.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanelRTU.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanelRTU.Controls.Add(this.comboBoxCom, 1, 0);
            this.tableLayoutPanelRTU.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanelRTU.Location = new System.Drawing.Point(29, 28);
            this.tableLayoutPanelRTU.Name = "tableLayoutPanelRTU";
            this.tableLayoutPanelRTU.RowCount = 2;
            this.tableLayoutPanelRTU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRTU.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRTU.Size = new System.Drawing.Size(422, 104);
            this.tableLayoutPanelRTU.TabIndex = 4;
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxParity.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "无",
            "奇",
            "偶"});
            this.comboBoxParity.Location = new System.Drawing.Point(213, 64);
            this.comboBoxParity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(64, 28);
            this.comboBoxParity.TabIndex = 6;
            // 
            // comboBoxStopBit
            // 
            this.comboBoxStopBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStopBit.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBit.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxStopBit.FormattingEnabled = true;
            this.comboBoxStopBit.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboBoxStopBit.Location = new System.Drawing.Point(73, 64);
            this.comboBoxStopBit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxStopBit.Name = "comboBoxStopBit";
            this.comboBoxStopBit.Size = new System.Drawing.Size(64, 28);
            this.comboBoxStopBit.TabIndex = 5;
            // 
            // comboBoxDataBit
            // 
            this.comboBoxDataBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDataBit.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBit.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxDataBit.FormattingEnabled = true;
            this.comboBoxDataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxDataBit.Location = new System.Drawing.Point(353, 12);
            this.comboBoxDataBit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDataBit.Name = "comboBoxDataBit";
            this.comboBoxDataBit.Size = new System.Drawing.Size(66, 28);
            this.comboBoxDataBit.TabIndex = 4;
            // 
            // comboBoxBaute
            // 
            this.comboBoxBaute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBaute.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxBaute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaute.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxBaute.FormattingEnabled = true;
            this.comboBoxBaute.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBoxBaute.Location = new System.Drawing.Point(213, 12);
            this.comboBoxBaute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxBaute.Name = "comboBoxBaute";
            this.comboBoxBaute.Size = new System.Drawing.Size(64, 28);
            this.comboBoxBaute.TabIndex = 3;
            // 
            // buttonCon1
            // 
            this.buttonCon1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCon1.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCon1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCon1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCon1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCon1.Location = new System.Drawing.Point(353, 59);
            this.buttonCon1.Name = "buttonCon1";
            this.buttonCon1.Size = new System.Drawing.Size(66, 37);
            this.buttonCon1.TabIndex = 2;
            this.buttonCon1.Text = "连接";
            this.buttonCon1.UseVisualStyleBackColor = false;
            this.buttonCon1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(144, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "校验位";
            // 
            // labelConnectInfo1
            // 
            this.labelConnectInfo1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelConnectInfo1.AutoSize = true;
            this.labelConnectInfo1.Location = new System.Drawing.Point(315, 69);
            this.labelConnectInfo1.Name = "labelConnectInfo1";
            this.labelConnectInfo1.Size = new System.Drawing.Size(0, 18);
            this.labelConnectInfo1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "数据位";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "停止位";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "波特率";
            // 
            // comboBoxCom
            // 
            this.comboBoxCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCom.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCom.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxCom.FormattingEnabled = true;
            this.comboBoxCom.Location = new System.Drawing.Point(73, 12);
            this.comboBoxCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCom.Name = "comboBoxCom";
            this.comboBoxCom.Size = new System.Drawing.Size(64, 28);
            this.comboBoxCom.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "串口号";
            // 
            // tableLayoutPanelTCP
            // 
            this.tableLayoutPanelTCP.ColumnCount = 4;
            this.tableLayoutPanelTCP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTCP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTCP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTCP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelTCP.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelTCP.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelTCP.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanelTCP.Controls.Add(this.textBoxHoldTime, 1, 1);
            this.tableLayoutPanelTCP.Controls.Add(this.buttonCon2, 3, 1);
            this.tableLayoutPanelTCP.Controls.Add(this.textBoxPort, 3, 0);
            this.tableLayoutPanelTCP.Controls.Add(this.labelConnectInfo2, 2, 1);
            this.tableLayoutPanelTCP.Controls.Add(this.uiipTextBoxIP, 1, 0);
            this.tableLayoutPanelTCP.Location = new System.Drawing.Point(495, 29);
            this.tableLayoutPanelTCP.Name = "tableLayoutPanelTCP";
            this.tableLayoutPanelTCP.RowCount = 2;
            this.tableLayoutPanelTCP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTCP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTCP.Size = new System.Drawing.Size(366, 86);
            this.tableLayoutPanelTCP.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 36);
            this.label7.TabIndex = 0;
            this.label7.Text = "保持时间(ms)";
            // 
            // textBoxHoldTime
            // 
            this.textBoxHoldTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxHoldTime.Location = new System.Drawing.Point(94, 50);
            this.textBoxHoldTime.Name = "textBoxHoldTime";
            this.textBoxHoldTime.Size = new System.Drawing.Size(85, 28);
            this.textBoxHoldTime.TabIndex = 1;
            this.textBoxHoldTime.Text = "300";
            this.textBoxHoldTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonCon2
            // 
            this.buttonCon2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCon2.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCon2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCon2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCon2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCon2.Location = new System.Drawing.Point(276, 46);
            this.buttonCon2.Name = "buttonCon2";
            this.buttonCon2.Size = new System.Drawing.Size(87, 37);
            this.buttonCon2.TabIndex = 2;
            this.buttonCon2.Text = "连接";
            this.buttonCon2.UseVisualStyleBackColor = false;
            this.buttonCon2.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPort.Location = new System.Drawing.Point(276, 7);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(87, 28);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "502";
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelConnectInfo2
            // 
            this.labelConnectInfo2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelConnectInfo2.AutoSize = true;
            this.labelConnectInfo2.Location = new System.Drawing.Point(227, 55);
            this.labelConnectInfo2.Name = "labelConnectInfo2";
            this.labelConnectInfo2.Size = new System.Drawing.Size(0, 18);
            this.labelConnectInfo2.TabIndex = 0;
            // 
            // uiipTextBoxIP
            // 
            this.uiipTextBoxIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiipTextBoxIP.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiipTextBoxIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiipTextBoxIP.Location = new System.Drawing.Point(95, 7);
            this.uiipTextBoxIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiipTextBoxIP.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiipTextBoxIP.Name = "uiipTextBoxIP";
            this.uiipTextBoxIP.Padding = new System.Windows.Forms.Padding(1);
            this.uiipTextBoxIP.RectColor = System.Drawing.SystemColors.WindowText;
            this.uiipTextBoxIP.ShowText = false;
            this.uiipTextBoxIP.Size = new System.Drawing.Size(83, 29);
            this.uiipTextBoxIP.TabIndex = 3;
            this.uiipTextBoxIP.Text = "192.168.2.1";
            this.uiipTextBoxIP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiipTextBoxIP.Value = ((System.Net.IPAddress)(resources.GetObject("uiipTextBoxIP.Value")));
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanelCon);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(889, 154);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试连接";
            // 
            // tableLayoutPanelCon
            // 
            this.tableLayoutPanelCon.ColumnCount = 4;
            this.tableLayoutPanelCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCon.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanelCon.Controls.Add(this.textBoxAddress, 1, 0);
            this.tableLayoutPanelCon.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanelCon.Controls.Add(this.buttonWrite, 3, 1);
            this.tableLayoutPanelCon.Controls.Add(this.buttonRead, 2, 1);
            this.tableLayoutPanelCon.Controls.Add(this.labelTestInfo, 0, 1);
            this.tableLayoutPanelCon.Controls.Add(this.comboBoxCoilValue, 3, 0);
            this.tableLayoutPanelCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelCon.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanelCon.Name = "tableLayoutPanelCon";
            this.tableLayoutPanelCon.RowCount = 2;
            this.tableLayoutPanelCon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCon.Size = new System.Drawing.Size(883, 127);
            this.tableLayoutPanelCon.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(501, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "写入线圈值";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxAddress.Location = new System.Drawing.Point(254, 17);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(152, 28);
            this.textBoxAddress.TabIndex = 1;
            this.textBoxAddress.Text = "1";
            this.textBoxAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "线圈地址";
            // 
            // buttonWrite
            // 
            this.buttonWrite.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonWrite.BackColor = System.Drawing.SystemColors.Control;
            this.buttonWrite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonWrite.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonWrite.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonWrite.Location = new System.Drawing.Point(713, 74);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(116, 41);
            this.buttonWrite.TabIndex = 2;
            this.buttonWrite.Text = "测试写入";
            this.buttonWrite.UseVisualStyleBackColor = false;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRead.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRead.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRead.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRead.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRead.Location = new System.Drawing.Point(492, 74);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(116, 41);
            this.buttonRead.TabIndex = 2;
            this.buttonRead.Text = "测试读取";
            this.buttonRead.UseVisualStyleBackColor = false;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // labelTestInfo
            // 
            this.labelTestInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTestInfo.AutoSize = true;
            this.tableLayoutPanelCon.SetColumnSpan(this.labelTestInfo, 2);
            this.labelTestInfo.Location = new System.Drawing.Point(220, 86);
            this.labelTestInfo.Name = "labelTestInfo";
            this.labelTestInfo.Size = new System.Drawing.Size(0, 18);
            this.labelTestInfo.TabIndex = 0;
            // 
            // comboBoxCoilValue
            // 
            this.comboBoxCoilValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxCoilValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCoilValue.FormattingEnabled = true;
            this.comboBoxCoilValue.Items.AddRange(new object[] {
            "0",
            "1"});
            this.comboBoxCoilValue.Location = new System.Drawing.Point(711, 20);
            this.comboBoxCoilValue.Name = "comboBoxCoilValue";
            this.comboBoxCoilValue.Size = new System.Drawing.Size(121, 26);
            this.comboBoxCoilValue.TabIndex = 3;
            // 
            // FormSolRunParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 499);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSolRunParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "运行参数设置";
            this.Shown += new System.EventHandler(this.FormSolRunParam_Shown);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanelRTU.ResumeLayout(false);
            this.tableLayoutPanelRTU.PerformLayout();
            this.tableLayoutPanelTCP.ResumeLayout(false);
            this.tableLayoutPanelTCP.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanelCon.ResumeLayout(false);
            this.tableLayoutPanelCon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxHoldTime;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCon2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTCP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCon;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelConnectInfo2;
        private System.Windows.Forms.Label labelTestInfo;
        private Sunny.UI.UIIPTextBox uiipTextBoxIP;
        private System.Windows.Forms.ComboBox comboBoxCoilValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRTU;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCom;
        private System.Windows.Forms.ComboBox comboBoxBaute;
        private System.Windows.Forms.ComboBox comboBoxDataBit;
        private System.Windows.Forms.ComboBox comboBoxStopBit;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.Button buttonCon1;
        private System.Windows.Forms.Label labelConnectInfo1;
    }
}