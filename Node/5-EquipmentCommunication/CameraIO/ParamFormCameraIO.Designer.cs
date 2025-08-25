namespace TDJS_Vision.Node._5_EquipmentCommunication.LightOpen
{
    partial class ParamFormCameraIO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParamFormCameraIO));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxLines = new System.Windows.Forms.ComboBox();
            this.labelSelectorCamera = new System.Windows.Forms.Label();
            this.comboBoxCameraList = new System.Windows.Forms.ComboBox();
            this.labelSelectorIO = new System.Windows.Forms.Label();
            this.comboBoxLineMode = new System.Windows.Forms.ComboBox();
            this.labelSelectorLines = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new TDJS_Vision.Node.NodeSubscription();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHoldTime = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.41963F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.58037F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxLines, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelSelectorCamera, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCameraList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelSelectorIO, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxLineMode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelSelectorLines, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxHoldTime, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 368);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxLines
            // 
            this.comboBoxLines.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLines.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxLines.FormattingEnabled = true;
            this.comboBoxLines.Location = new System.Drawing.Point(288, 139);
            this.comboBoxLines.Name = "comboBoxLines";
            this.comboBoxLines.Size = new System.Drawing.Size(187, 25);
            this.comboBoxLines.TabIndex = 1;
            // 
            // labelSelectorCamera
            // 
            this.labelSelectorCamera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorCamera.AutoSize = true;
            this.labelSelectorCamera.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorCamera.Location = new System.Drawing.Point(58, 21);
            this.labelSelectorCamera.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorCamera.Name = "labelSelectorCamera";
            this.labelSelectorCamera.Size = new System.Drawing.Size(80, 18);
            this.labelSelectorCamera.TabIndex = 0;
            this.labelSelectorCamera.Text = "选择相机";
            // 
            // comboBoxCameraList
            // 
            this.comboBoxCameraList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxCameraList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameraList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxCameraList.FormattingEnabled = true;
            this.comboBoxCameraList.Location = new System.Drawing.Point(288, 17);
            this.comboBoxCameraList.Name = "comboBoxCameraList";
            this.comboBoxCameraList.Size = new System.Drawing.Size(187, 25);
            this.comboBoxCameraList.TabIndex = 1;
            this.comboBoxCameraList.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameraList_SelectedIndexChanged);
            // 
            // labelSelectorIO
            // 
            this.labelSelectorIO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorIO.AutoSize = true;
            this.labelSelectorIO.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorIO.Location = new System.Drawing.Point(35, 82);
            this.labelSelectorIO.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorIO.Name = "labelSelectorIO";
            this.labelSelectorIO.Size = new System.Drawing.Size(125, 18);
            this.labelSelectorIO.TabIndex = 0;
            this.labelSelectorIO.Text = "选择输入/输出";
            // 
            // comboBoxLineMode
            // 
            this.comboBoxLineMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLineMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLineMode.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxLineMode.FormattingEnabled = true;
            this.comboBoxLineMode.Items.AddRange(new object[] {
            "输出",
            "输入"});
            this.comboBoxLineMode.Location = new System.Drawing.Point(288, 78);
            this.comboBoxLineMode.Name = "comboBoxLineMode";
            this.comboBoxLineMode.Size = new System.Drawing.Size(187, 25);
            this.comboBoxLineMode.TabIndex = 1;
            this.comboBoxLineMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxIO_SelectedIndexChanged);
            // 
            // labelSelectorLines
            // 
            this.labelSelectorLines.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorLines.AutoSize = true;
            this.labelSelectorLines.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorLines.Location = new System.Drawing.Point(58, 143);
            this.labelSelectorLines.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorLines.Name = "labelSelectorLines";
            this.labelSelectorLines.Size = new System.Drawing.Size(80, 18);
            this.labelSelectorLines.TabIndex = 0;
            this.labelSelectorLines.Text = "选择线路";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(49, 265);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出的条件";
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nodeSubscription1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nodeSubscription1.Location = new System.Drawing.Point(216, 246);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(231, 50);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(330, 57);
            this.nodeSubscription1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 204);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出保持时间(us)";
            // 
            // textBoxHoldTime
            // 
            this.textBoxHoldTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxHoldTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxHoldTime.Location = new System.Drawing.Point(301, 200);
            this.textBoxHoldTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxHoldTime.Name = "textBoxHoldTime";
            this.textBoxHoldTime.Size = new System.Drawing.Size(161, 27);
            this.textBoxHoldTime.TabIndex = 4;
            this.textBoxHoldTime.Text = "20";
            this.textBoxHoldTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.button1, 2);
            this.button1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(238, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ParamFormCameraIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 402);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamFormCameraIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机IO设置";
            this.Shown += new System.EventHandler(this.ParamFormCameraIO_Shown);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelSelectorCamera;
        private System.Windows.Forms.ComboBox comboBoxCameraList;
        private System.Windows.Forms.Label labelSelectorIO;
        private System.Windows.Forms.ComboBox comboBoxLineMode;
        private System.Windows.Forms.Label labelSelectorLines;
        private System.Windows.Forms.ComboBox comboBoxLines;
        private System.Windows.Forms.Label label1;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHoldTime;
    }
}