namespace TDJS_Vision.Node._1_Acquisition.ImageSource
{
    partial class ParamFormImageSource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormImageSource));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelImgSource = new System.Windows.Forms.Label();
            this.comboBoxImgSource = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelChoiceImage = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
            this.textBoxImgPath = new System.Windows.Forms.TextBox();
            this.buttonChoiceImageCatalog = new System.Windows.Forms.Button();
            this.buttonChoiceImg = new System.Windows.Forms.Button();
            this.tableLayoutPanelCamera = new System.Windows.Forms.TableLayoutPanel();
            this.labelChoiceCamera = new System.Windows.Forms.Label();
            this.comboBoxChoiceCamera = new System.Windows.Forms.ComboBox();
            this.labelTriggerMode = new System.Windows.Forms.Label();
            this.comboBoxTriggerMode = new System.Windows.Forms.ComboBox();
            this.labelHardTrigger = new System.Windows.Forms.Label();
            this.comboBoxTriggerEdge = new System.Windows.Forms.ComboBox();
            this.labelTriggerDelay = new System.Windows.Forms.Label();
            this.numericUpDownTriggerDelay = new System.Windows.Forms.NumericUpDown();
            this.labelExposureTime = new System.Windows.Forms.Label();
            this.numericUpDownExposureTime = new System.Windows.Forms.NumericUpDown();
            this.labelGain = new System.Windows.Forms.Label();
            this.numericUpDownGain = new System.Windows.Forms.NumericUpDown();
            this.labelStrobe = new System.Windows.Forms.Label();
            this.comboBoxStrobe = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelSave = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanelChoiceImage.SuspendLayout();
            this.tableLayoutPanelCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTriggerDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposureTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGain)).BeginInit();
            this.tableLayoutPanelSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanelChoiceImage);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanelCamera);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanelSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(602, 654);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.89107F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.10893F));
            this.tableLayoutPanel1.Controls.Add(this.labelImgSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxImgSource, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(599, 51);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelImgSource
            // 
            this.labelImgSource.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelImgSource.AutoSize = true;
            this.labelImgSource.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelImgSource.Location = new System.Drawing.Point(118, 16);
            this.labelImgSource.Name = "labelImgSource";
            this.labelImgSource.Size = new System.Drawing.Size(62, 18);
            this.labelImgSource.TabIndex = 0;
            this.labelImgSource.Text = "图像源";
            // 
            // comboBoxImgSource
            // 
            this.comboBoxImgSource.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxImgSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImgSource.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxImgSource.FormattingEnabled = true;
            this.comboBoxImgSource.Items.AddRange(new object[] {
            "本地图像",
            "相机"});
            this.comboBoxImgSource.Location = new System.Drawing.Point(354, 12);
            this.comboBoxImgSource.Name = "comboBoxImgSource";
            this.comboBoxImgSource.Size = new System.Drawing.Size(188, 25);
            this.comboBoxImgSource.TabIndex = 1;
            this.comboBoxImgSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxImgSource_SelectedIndexChanged);
            // 
            // tableLayoutPanelChoiceImage
            // 
            this.tableLayoutPanelChoiceImage.ColumnCount = 2;
            this.tableLayoutPanelChoiceImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChoiceImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChoiceImage.Controls.Add(this.checkBoxAuto, 1, 2);
            this.tableLayoutPanelChoiceImage.Controls.Add(this.textBoxImgPath, 0, 0);
            this.tableLayoutPanelChoiceImage.Controls.Add(this.buttonChoiceImageCatalog, 1, 1);
            this.tableLayoutPanelChoiceImage.Controls.Add(this.buttonChoiceImg, 0, 1);
            this.tableLayoutPanelChoiceImage.Location = new System.Drawing.Point(3, 60);
            this.tableLayoutPanelChoiceImage.Name = "tableLayoutPanelChoiceImage";
            this.tableLayoutPanelChoiceImage.RowCount = 3;
            this.tableLayoutPanelChoiceImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelChoiceImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelChoiceImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelChoiceImage.Size = new System.Drawing.Size(599, 146);
            this.tableLayoutPanelChoiceImage.TabIndex = 1;
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Enabled = false;
            this.checkBoxAuto.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBoxAuto.Location = new System.Drawing.Point(371, 110);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(156, 22);
            this.checkBoxAuto.TabIndex = 2;
            this.checkBoxAuto.Text = "自动切换下一张";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            // 
            // textBoxImgPath
            // 
            this.textBoxImgPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelChoiceImage.SetColumnSpan(this.textBoxImgPath, 2);
            this.textBoxImgPath.Font = new System.Drawing.Font("宋体", 10.5F);
            this.textBoxImgPath.Location = new System.Drawing.Point(71, 10);
            this.textBoxImgPath.Name = "textBoxImgPath";
            this.textBoxImgPath.Size = new System.Drawing.Size(457, 27);
            this.textBoxImgPath.TabIndex = 0;
            // 
            // buttonChoiceImageCatalog
            // 
            this.buttonChoiceImageCatalog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonChoiceImageCatalog.Font = new System.Drawing.Font("宋体", 10.5F);
            this.buttonChoiceImageCatalog.Location = new System.Drawing.Point(395, 56);
            this.buttonChoiceImageCatalog.Name = "buttonChoiceImageCatalog";
            this.buttonChoiceImageCatalog.Size = new System.Drawing.Size(107, 31);
            this.buttonChoiceImageCatalog.TabIndex = 1;
            this.buttonChoiceImageCatalog.Text = "选择目录";
            this.buttonChoiceImageCatalog.UseVisualStyleBackColor = true;
            this.buttonChoiceImageCatalog.Click += new System.EventHandler(this.buttonChoiceImageCatalog_Click);
            // 
            // buttonChoiceImg
            // 
            this.buttonChoiceImg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonChoiceImg.Font = new System.Drawing.Font("宋体", 10.5F);
            this.buttonChoiceImg.Location = new System.Drawing.Point(95, 55);
            this.buttonChoiceImg.Name = "buttonChoiceImg";
            this.buttonChoiceImg.Size = new System.Drawing.Size(108, 33);
            this.buttonChoiceImg.TabIndex = 1;
            this.buttonChoiceImg.Text = "选择图片";
            this.buttonChoiceImg.UseVisualStyleBackColor = true;
            this.buttonChoiceImg.Click += new System.EventHandler(this.buttonChoiceImg_Click);
            // 
            // tableLayoutPanelCamera
            // 
            this.tableLayoutPanelCamera.ColumnCount = 2;
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCamera.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCamera.Controls.Add(this.labelChoiceCamera, 0, 0);
            this.tableLayoutPanelCamera.Controls.Add(this.comboBoxChoiceCamera, 1, 0);
            this.tableLayoutPanelCamera.Controls.Add(this.labelTriggerMode, 0, 1);
            this.tableLayoutPanelCamera.Controls.Add(this.comboBoxTriggerMode, 1, 1);
            this.tableLayoutPanelCamera.Controls.Add(this.labelHardTrigger, 0, 2);
            this.tableLayoutPanelCamera.Controls.Add(this.comboBoxTriggerEdge, 1, 2);
            this.tableLayoutPanelCamera.Controls.Add(this.labelTriggerDelay, 0, 3);
            this.tableLayoutPanelCamera.Controls.Add(this.numericUpDownTriggerDelay, 1, 3);
            this.tableLayoutPanelCamera.Controls.Add(this.labelExposureTime, 0, 4);
            this.tableLayoutPanelCamera.Controls.Add(this.numericUpDownExposureTime, 1, 4);
            this.tableLayoutPanelCamera.Controls.Add(this.labelGain, 0, 5);
            this.tableLayoutPanelCamera.Controls.Add(this.numericUpDownGain, 1, 5);
            this.tableLayoutPanelCamera.Controls.Add(this.labelStrobe, 0, 6);
            this.tableLayoutPanelCamera.Controls.Add(this.comboBoxStrobe, 1, 6);
            this.tableLayoutPanelCamera.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tableLayoutPanelCamera.Location = new System.Drawing.Point(3, 212);
            this.tableLayoutPanelCamera.Name = "tableLayoutPanelCamera";
            this.tableLayoutPanelCamera.RowCount = 7;
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelCamera.Size = new System.Drawing.Size(599, 390);
            this.tableLayoutPanelCamera.TabIndex = 2;
            // 
            // labelChoiceCamera
            // 
            this.labelChoiceCamera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelChoiceCamera.AutoSize = true;
            this.labelChoiceCamera.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelChoiceCamera.Location = new System.Drawing.Point(109, 18);
            this.labelChoiceCamera.Name = "labelChoiceCamera";
            this.labelChoiceCamera.Size = new System.Drawing.Size(80, 18);
            this.labelChoiceCamera.TabIndex = 0;
            this.labelChoiceCamera.Text = "选择相机";
            // 
            // comboBoxChoiceCamera
            // 
            this.comboBoxChoiceCamera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxChoiceCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChoiceCamera.Font = new System.Drawing.Font("宋体", 10.5F);
            this.comboBoxChoiceCamera.FormattingEnabled = true;
            this.comboBoxChoiceCamera.Location = new System.Drawing.Point(355, 14);
            this.comboBoxChoiceCamera.Name = "comboBoxChoiceCamera";
            this.comboBoxChoiceCamera.Size = new System.Drawing.Size(188, 25);
            this.comboBoxChoiceCamera.TabIndex = 1;
            this.comboBoxChoiceCamera.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoiceCamera_SelectedIndexChanged);
            // 
            // labelTriggerMode
            // 
            this.labelTriggerMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTriggerMode.AutoSize = true;
            this.labelTriggerMode.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelTriggerMode.Location = new System.Drawing.Point(109, 73);
            this.labelTriggerMode.Name = "labelTriggerMode";
            this.labelTriggerMode.Size = new System.Drawing.Size(80, 18);
            this.labelTriggerMode.TabIndex = 0;
            this.labelTriggerMode.Text = "触发方式";
            // 
            // comboBoxTriggerMode
            // 
            this.comboBoxTriggerMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTriggerMode.Font = new System.Drawing.Font("宋体", 10.5F);
            this.comboBoxTriggerMode.FormattingEnabled = true;
            this.comboBoxTriggerMode.Items.AddRange(new object[] {
            "软触发",
            "Line0",
            "Line1",
            "Line2",
            "Line3",
            "Line4"});
            this.comboBoxTriggerMode.Location = new System.Drawing.Point(355, 69);
            this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
            this.comboBoxTriggerMode.Size = new System.Drawing.Size(188, 25);
            this.comboBoxTriggerMode.TabIndex = 1;
            this.comboBoxTriggerMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxTriggerMode_SelectedIndexChanged);
            // 
            // labelHardTrigger
            // 
            this.labelHardTrigger.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelHardTrigger.AutoSize = true;
            this.labelHardTrigger.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelHardTrigger.Location = new System.Drawing.Point(109, 128);
            this.labelHardTrigger.Name = "labelHardTrigger";
            this.labelHardTrigger.Size = new System.Drawing.Size(80, 18);
            this.labelHardTrigger.TabIndex = 0;
            this.labelHardTrigger.Text = "硬触发沿";
            // 
            // comboBoxTriggerEdge
            // 
            this.comboBoxTriggerEdge.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxTriggerEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTriggerEdge.Enabled = false;
            this.comboBoxTriggerEdge.Font = new System.Drawing.Font("宋体", 10.5F);
            this.comboBoxTriggerEdge.FormattingEnabled = true;
            this.comboBoxTriggerEdge.Items.AddRange(new object[] {
            "上升沿",
            "下降沿",
            "高电平",
            "低电平"});
            this.comboBoxTriggerEdge.Location = new System.Drawing.Point(355, 124);
            this.comboBoxTriggerEdge.Name = "comboBoxTriggerEdge";
            this.comboBoxTriggerEdge.Size = new System.Drawing.Size(188, 25);
            this.comboBoxTriggerEdge.TabIndex = 1;
            // 
            // labelTriggerDelay
            // 
            this.labelTriggerDelay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTriggerDelay.AutoSize = true;
            this.labelTriggerDelay.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelTriggerDelay.Location = new System.Drawing.Point(91, 183);
            this.labelTriggerDelay.Name = "labelTriggerDelay";
            this.labelTriggerDelay.Size = new System.Drawing.Size(116, 18);
            this.labelTriggerDelay.TabIndex = 0;
            this.labelTriggerDelay.Text = "触发延迟(us)";
            // 
            // numericUpDownTriggerDelay
            // 
            this.numericUpDownTriggerDelay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownTriggerDelay.Font = new System.Drawing.Font("宋体", 10.5F);
            this.numericUpDownTriggerDelay.Location = new System.Drawing.Point(355, 179);
            this.numericUpDownTriggerDelay.Name = "numericUpDownTriggerDelay";
            this.numericUpDownTriggerDelay.Size = new System.Drawing.Size(188, 27);
            this.numericUpDownTriggerDelay.TabIndex = 2;
            this.numericUpDownTriggerDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelExposureTime
            // 
            this.labelExposureTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelExposureTime.AutoSize = true;
            this.labelExposureTime.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelExposureTime.Location = new System.Drawing.Point(109, 238);
            this.labelExposureTime.Name = "labelExposureTime";
            this.labelExposureTime.Size = new System.Drawing.Size(80, 18);
            this.labelExposureTime.TabIndex = 0;
            this.labelExposureTime.Text = "曝光(us)";
            // 
            // numericUpDownExposureTime
            // 
            this.numericUpDownExposureTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownExposureTime.Font = new System.Drawing.Font("宋体", 10.5F);
            this.numericUpDownExposureTime.Location = new System.Drawing.Point(355, 234);
            this.numericUpDownExposureTime.Name = "numericUpDownExposureTime";
            this.numericUpDownExposureTime.Size = new System.Drawing.Size(188, 27);
            this.numericUpDownExposureTime.TabIndex = 2;
            this.numericUpDownExposureTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelGain
            // 
            this.labelGain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelGain.AutoSize = true;
            this.labelGain.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelGain.Location = new System.Drawing.Point(127, 293);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(44, 18);
            this.labelGain.TabIndex = 0;
            this.labelGain.Text = "增益";
            // 
            // numericUpDownGain
            // 
            this.numericUpDownGain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownGain.Font = new System.Drawing.Font("宋体", 10.5F);
            this.numericUpDownGain.Location = new System.Drawing.Point(355, 289);
            this.numericUpDownGain.Name = "numericUpDownGain";
            this.numericUpDownGain.Size = new System.Drawing.Size(188, 27);
            this.numericUpDownGain.TabIndex = 2;
            this.numericUpDownGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelStrobe
            // 
            this.labelStrobe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelStrobe.AutoSize = true;
            this.labelStrobe.Font = new System.Drawing.Font("宋体", 10.5F);
            this.labelStrobe.Location = new System.Drawing.Point(37, 351);
            this.labelStrobe.Name = "labelStrobe";
            this.labelStrobe.Size = new System.Drawing.Size(224, 18);
            this.labelStrobe.TabIndex = 0;
            this.labelStrobe.Text = "每次执行都要设置相机参数";
            // 
            // comboBoxStrobe
            // 
            this.comboBoxStrobe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxStrobe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrobe.Font = new System.Drawing.Font("宋体", 10.5F);
            this.comboBoxStrobe.FormattingEnabled = true;
            this.comboBoxStrobe.Items.AddRange(new object[] {
            "否",
            "是"});
            this.comboBoxStrobe.Location = new System.Drawing.Point(355, 347);
            this.comboBoxStrobe.Name = "comboBoxStrobe";
            this.comboBoxStrobe.Size = new System.Drawing.Size(188, 25);
            this.comboBoxStrobe.TabIndex = 1;
            // 
            // tableLayoutPanelSave
            // 
            this.tableLayoutPanelSave.ColumnCount = 2;
            this.tableLayoutPanelSave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSave.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSave.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanelSave.Location = new System.Drawing.Point(3, 608);
            this.tableLayoutPanelSave.Name = "tableLayoutPanelSave";
            this.tableLayoutPanelSave.RowCount = 1;
            this.tableLayoutPanelSave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelSave.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelSave.Size = new System.Drawing.Size(599, 68);
            this.tableLayoutPanelSave.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanelSave.SetColumnSpan(this.button1, 2);
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.button1.Location = new System.Drawing.Point(245, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ParamFormImageSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 688);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamFormImageSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像源";
            this.Load += new System.EventHandler(this.ParamFormImageSource_Load);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanelChoiceImage.ResumeLayout(false);
            this.tableLayoutPanelChoiceImage.PerformLayout();
            this.tableLayoutPanelCamera.ResumeLayout(false);
            this.tableLayoutPanelCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTriggerDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposureTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGain)).EndInit();
            this.tableLayoutPanelSave.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelImgSource;
        private System.Windows.Forms.ComboBox comboBoxImgSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelChoiceImage;
        private System.Windows.Forms.TextBox textBoxImgPath;
        private System.Windows.Forms.Button buttonChoiceImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCamera;
        private System.Windows.Forms.Label labelChoiceCamera;
        private System.Windows.Forms.ComboBox comboBoxChoiceCamera;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSave;
        private System.Windows.Forms.Label labelTriggerMode;
        private System.Windows.Forms.ComboBox comboBoxTriggerMode;
        private System.Windows.Forms.Label labelHardTrigger;
        private System.Windows.Forms.ComboBox comboBoxTriggerEdge;
        private System.Windows.Forms.Label labelTriggerDelay;
        private System.Windows.Forms.NumericUpDown numericUpDownTriggerDelay;
        private System.Windows.Forms.Label labelExposureTime;
        private System.Windows.Forms.NumericUpDown numericUpDownExposureTime;
        private System.Windows.Forms.Label labelGain;
        private System.Windows.Forms.NumericUpDown numericUpDownGain;
        private System.Windows.Forms.Label labelStrobe;
        private System.Windows.Forms.ComboBox comboBoxStrobe;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonChoiceImageCatalog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxAuto;
    }
}