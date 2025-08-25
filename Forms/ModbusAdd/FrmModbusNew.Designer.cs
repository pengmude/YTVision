namespace TDJS_Vision.Forms.ModbusAdd
{
    partial class FrmModbusNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModbusNew));
            this.buttonTcpPoll = new System.Windows.Forms.Button();
            this.textBoxTcpPollDevName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxBaute = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxDataBit = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBit = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonRTUPoll = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.uiipTextBoxTcpPollIP = new Sunny.UI.UIIPTextBox();
            this.textBoxTcpPollPort = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelTCPSlave = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTcpSlaveDevName = new System.Windows.Forms.TextBox();
            this.buttonTcpSlave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxTcpSlavePort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxTcpSlaveID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxRTUDevName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanelTCPSlave.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonTcpPoll
            // 
            this.buttonTcpPoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTcpPoll.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTcpPoll.Location = new System.Drawing.Point(355, 193);
            this.buttonTcpPoll.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonTcpPoll.Name = "buttonTcpPoll";
            this.buttonTcpPoll.Size = new System.Drawing.Size(108, 37);
            this.buttonTcpPoll.TabIndex = 7;
            this.buttonTcpPoll.Text = "确认";
            this.buttonTcpPoll.UseVisualStyleBackColor = true;
            this.buttonTcpPoll.Click += new System.EventHandler(this.buttonTcpPoll_Click);
            // 
            // textBoxTcpPollDevName
            // 
            this.textBoxTcpPollDevName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTcpPollDevName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTcpPollDevName.Location = new System.Drawing.Point(297, 135);
            this.textBoxTcpPollDevName.Name = "textBoxTcpPollDevName";
            this.textBoxTcpPollDevName.Size = new System.Drawing.Size(224, 30);
            this.textBoxTcpPollDevName.TabIndex = 0;
            this.textBoxTcpPollDevName.Text = "ModbusTcp主站1";
            this.textBoxTcpPollDevName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(206, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "IP地址";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(626, 513);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备信息(选择其中一种)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(620, 486);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 452);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "RTU主站";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxCom, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxParity, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxBaute, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label17, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxDataBit, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxStopBit, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label19, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.buttonRTUPoll, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.textBoxRTUDevName, 1, 5);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(606, 381);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "串口号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxCom
            // 
            this.comboBoxCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCom.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCom.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBoxCom.FormattingEnabled = true;
            this.comboBoxCom.Location = new System.Drawing.Point(306, 13);
            this.comboBoxCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCom.Name = "comboBoxCom";
            this.comboBoxCom.Size = new System.Drawing.Size(297, 28);
            this.comboBoxCom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(3, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.comboBoxParity.Location = new System.Drawing.Point(306, 229);
            this.comboBoxParity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(297, 28);
            this.comboBoxParity.TabIndex = 5;
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
            this.comboBoxBaute.Location = new System.Drawing.Point(306, 67);
            this.comboBoxBaute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxBaute.Name = "comboBoxBaute";
            this.comboBoxBaute.Size = new System.Drawing.Size(297, 28);
            this.comboBoxBaute.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 12F);
            this.label17.Location = new System.Drawing.Point(3, 233);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(297, 20);
            this.label17.TabIndex = 9;
            this.label17.Text = "校验位";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.comboBoxDataBit.Location = new System.Drawing.Point(306, 121);
            this.comboBoxDataBit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxDataBit.Name = "comboBoxDataBit";
            this.comboBoxDataBit.Size = new System.Drawing.Size(297, 28);
            this.comboBoxDataBit.TabIndex = 3;
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
            this.comboBoxStopBit.Location = new System.Drawing.Point(306, 175);
            this.comboBoxStopBit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxStopBit.Name = "comboBoxStopBit";
            this.comboBoxStopBit.Size = new System.Drawing.Size(297, 28);
            this.comboBoxStopBit.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F);
            this.label18.Location = new System.Drawing.Point(3, 125);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(297, 20);
            this.label18.TabIndex = 3;
            this.label18.Text = "数据位";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F);
            this.label19.Location = new System.Drawing.Point(3, 179);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(297, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "停止位";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonRTUPoll
            // 
            this.buttonRTUPoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRTUPoll.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRTUPoll.Location = new System.Drawing.Point(400, 334);
            this.buttonRTUPoll.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonRTUPoll.Name = "buttonRTUPoll";
            this.buttonRTUPoll.Size = new System.Drawing.Size(108, 37);
            this.buttonRTUPoll.TabIndex = 14;
            this.buttonRTUPoll.Text = "确认";
            this.buttonRTUPoll.UseVisualStyleBackColor = true;
            this.buttonRTUPoll.Click += new System.EventHandler(this.buttonRTUPoll_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 452);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TCP主站";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxTcpPollDevName, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonTcpPoll, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.uiipTextBoxTcpPollIP, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxTcpPollPort, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(606, 243);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "设备名称";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiipTextBoxTcpPollIP
            // 
            this.uiipTextBoxTcpPollIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiipTextBoxTcpPollIP.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiipTextBoxTcpPollIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiipTextBoxTcpPollIP.Location = new System.Drawing.Point(299, 16);
            this.uiipTextBoxTcpPollIP.Margin = new System.Windows.Forms.Padding(4);
            this.uiipTextBoxTcpPollIP.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiipTextBoxTcpPollIP.Name = "uiipTextBoxTcpPollIP";
            this.uiipTextBoxTcpPollIP.Padding = new System.Windows.Forms.Padding(1);
            this.uiipTextBoxTcpPollIP.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(178)))), ((int)(((byte)(181)))));
            this.uiipTextBoxTcpPollIP.ShowText = false;
            this.uiipTextBoxTcpPollIP.Size = new System.Drawing.Size(220, 28);
            this.uiipTextBoxTcpPollIP.TabIndex = 12;
            this.uiipTextBoxTcpPollIP.Text = "127.0.0.1";
            this.uiipTextBoxTcpPollIP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiipTextBoxTcpPollIP.Value = ((System.Net.IPAddress)(resources.GetObject("uiipTextBoxTcpPollIP.Value")));
            // 
            // textBoxTcpPollPort
            // 
            this.textBoxTcpPollPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTcpPollPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTcpPollPort.Location = new System.Drawing.Point(297, 75);
            this.textBoxTcpPollPort.Name = "textBoxTcpPollPort";
            this.textBoxTcpPollPort.Size = new System.Drawing.Size(224, 30);
            this.textBoxTcpPollPort.TabIndex = 0;
            this.textBoxTcpPollPort.Text = "502";
            this.textBoxTcpPollPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanelTCPSlave);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(612, 452);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TCP从站";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelTCPSlave
            // 
            this.tableLayoutPanelTCPSlave.ColumnCount = 2;
            this.tableLayoutPanelTCPSlave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelTCPSlave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelTCPSlave.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.textBoxTcpSlaveDevName, 1, 3);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.buttonTcpSlave, 1, 4);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.textBoxTcpSlavePort, 1, 2);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.textBoxTcpSlaveID, 1, 0);
            this.tableLayoutPanelTCPSlave.Controls.Add(this.label10, 1, 1);
            this.tableLayoutPanelTCPSlave.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTCPSlave.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelTCPSlave.Name = "tableLayoutPanelTCPSlave";
            this.tableLayoutPanelTCPSlave.RowCount = 5;
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTCPSlave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelTCPSlave.Size = new System.Drawing.Size(606, 262);
            this.tableLayoutPanelTCPSlave.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(206, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "端口";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(3, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(206, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "IP连接";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTcpSlaveDevName
            // 
            this.textBoxTcpSlaveDevName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTcpSlaveDevName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTcpSlaveDevName.Location = new System.Drawing.Point(297, 167);
            this.textBoxTcpSlaveDevName.Name = "textBoxTcpSlaveDevName";
            this.textBoxTcpSlaveDevName.Size = new System.Drawing.Size(224, 30);
            this.textBoxTcpSlaveDevName.TabIndex = 0;
            this.textBoxTcpSlaveDevName.Text = "ModbusTcp从站1";
            this.textBoxTcpSlaveDevName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonTcpSlave
            // 
            this.buttonTcpSlave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTcpSlave.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTcpSlave.Location = new System.Drawing.Point(355, 216);
            this.buttonTcpSlave.Margin = new System.Windows.Forms.Padding(53, 3, 53, 3);
            this.buttonTcpSlave.Name = "buttonTcpSlave";
            this.buttonTcpSlave.Size = new System.Drawing.Size(108, 37);
            this.buttonTcpSlave.TabIndex = 7;
            this.buttonTcpSlave.Text = "确认";
            this.buttonTcpSlave.UseVisualStyleBackColor = true;
            this.buttonTcpSlave.Click += new System.EventHandler(this.buttonTcpSlave_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(3, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "设备名称";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTcpSlavePort
            // 
            this.textBoxTcpSlavePort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTcpSlavePort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTcpSlavePort.Location = new System.Drawing.Point(297, 115);
            this.textBoxTcpSlavePort.Name = "textBoxTcpSlavePort";
            this.textBoxTcpSlavePort.Size = new System.Drawing.Size(224, 30);
            this.textBoxTcpSlavePort.TabIndex = 0;
            this.textBoxTcpSlavePort.Text = "502";
            this.textBoxTcpSlavePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(206, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "从站ID";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTcpSlaveID
            // 
            this.textBoxTcpSlaveID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxTcpSlaveID.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTcpSlaveID.Location = new System.Drawing.Point(297, 11);
            this.textBoxTcpSlaveID.Name = "textBoxTcpSlaveID";
            this.textBoxTcpSlaveID.Size = new System.Drawing.Size(224, 30);
            this.textBoxTcpSlaveID.TabIndex = 0;
            this.textBoxTcpSlaveID.Text = "1";
            this.textBoxTcpSlaveID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(215, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(388, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "任意IP";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(632, 517);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // textBoxRTUDevName
            // 
            this.textBoxRTUDevName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxRTUDevName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxRTUDevName.Location = new System.Drawing.Point(342, 282);
            this.textBoxRTUDevName.Name = "textBoxRTUDevName";
            this.textBoxRTUDevName.Size = new System.Drawing.Size(224, 30);
            this.textBoxRTUDevName.TabIndex = 13;
            this.textBoxRTUDevName.Text = "ModbusRTU主站1";
            this.textBoxRTUDevName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(297, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "设备名称";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmModbusNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 551);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmModbusNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加Modbus";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanelTCPSlave.ResumeLayout(false);
            this.tableLayoutPanelTCPSlave.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonTcpPoll;
        private System.Windows.Forms.TextBox textBoxTcpPollDevName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Sunny.UI.UIIPTextBox uiipTextBoxTcpPollIP;
        private System.Windows.Forms.TextBox textBoxTcpPollPort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTCPSlave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTcpSlaveDevName;
        private System.Windows.Forms.Button buttonTcpSlave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxTcpSlavePort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxTcpSlaveID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxBaute;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxDataBit;
        private System.Windows.Forms.ComboBox comboBoxStopBit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button buttonRTUPoll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRTUDevName;
    }
}