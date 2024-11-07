namespace YTVisionPro.Node.ImageSrc.CameraIO
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
            this.comboBoxIO = new System.Windows.Forms.ComboBox();
            this.labelSelectorLines = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nodeSubscription1 = new YTVisionPro.Node.NodeSubscription();
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
            this.tableLayoutPanel1.Controls.Add(this.comboBoxIO, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelSelectorLines, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.nodeSubscription1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 443);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxLines
            // 
            this.comboBoxLines.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLines.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxLines.FormattingEnabled = true;
            this.comboBoxLines.Location = new System.Drawing.Point(318, 204);
            this.comboBoxLines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxLines.Name = "comboBoxLines";
            this.comboBoxLines.Size = new System.Drawing.Size(210, 32);
            this.comboBoxLines.TabIndex = 1;
            // 
            // labelSelectorCamera
            // 
            this.labelSelectorCamera.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorCamera.AutoSize = true;
            this.labelSelectorCamera.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorCamera.Location = new System.Drawing.Point(60, 33);
            this.labelSelectorCamera.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorCamera.Name = "labelSelectorCamera";
            this.labelSelectorCamera.Size = new System.Drawing.Size(98, 22);
            this.labelSelectorCamera.TabIndex = 0;
            this.labelSelectorCamera.Text = "选择相机";
            // 
            // comboBoxCameraList
            // 
            this.comboBoxCameraList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxCameraList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxCameraList.FormattingEnabled = true;
            this.comboBoxCameraList.Location = new System.Drawing.Point(318, 28);
            this.comboBoxCameraList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxCameraList.Name = "comboBoxCameraList";
            this.comboBoxCameraList.Size = new System.Drawing.Size(210, 32);
            this.comboBoxCameraList.TabIndex = 1;
            this.comboBoxCameraList.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameraList_SelectedIndexChanged);
            // 
            // labelSelectorIO
            // 
            this.labelSelectorIO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorIO.AutoSize = true;
            this.labelSelectorIO.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorIO.Location = new System.Drawing.Point(32, 121);
            this.labelSelectorIO.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorIO.Name = "labelSelectorIO";
            this.labelSelectorIO.Size = new System.Drawing.Size(153, 22);
            this.labelSelectorIO.TabIndex = 0;
            this.labelSelectorIO.Text = "选择输入/输出";
            // 
            // comboBoxIO
            // 
            this.comboBoxIO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIO.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxIO.FormattingEnabled = true;
            this.comboBoxIO.Items.AddRange(new object[] {
            "输出",
            "输入"});
            this.comboBoxIO.Location = new System.Drawing.Point(318, 116);
            this.comboBoxIO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxIO.Name = "comboBoxIO";
            this.comboBoxIO.Size = new System.Drawing.Size(210, 32);
            this.comboBoxIO.TabIndex = 1;
            this.comboBoxIO.SelectedIndexChanged += new System.EventHandler(this.comboBoxIO_SelectedIndexChanged);
            // 
            // labelSelectorLines
            // 
            this.labelSelectorLines.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSelectorLines.AutoSize = true;
            this.labelSelectorLines.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSelectorLines.Location = new System.Drawing.Point(60, 209);
            this.labelSelectorLines.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelSelectorLines.Name = "labelSelectorLines";
            this.labelSelectorLines.Size = new System.Drawing.Size(98, 22);
            this.labelSelectorLines.TabIndex = 0;
            this.labelSelectorLines.Text = "选择线路";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 297);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "订阅条件";
            // 
            // nodeSubscription1
            // 
            this.nodeSubscription1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSubscription1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nodeSubscription1.Location = new System.Drawing.Point(224, 266);
            this.nodeSubscription1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodeSubscription1.MinimumSize = new System.Drawing.Size(260, 60);
            this.nodeSubscription1.Name = "nodeSubscription1";
            this.nodeSubscription1.Size = new System.Drawing.Size(399, 84);
            this.nodeSubscription1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(335, 368);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 58);
            this.button1.TabIndex = 3;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ParamFormCameraIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 443);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ParamFormCameraIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相机IO设置";
            this.Shown += new System.EventHandler(this.ParamFormCameraIO_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelSelectorCamera;
        private System.Windows.Forms.ComboBox comboBoxCameraList;
        private System.Windows.Forms.Label labelSelectorIO;
        private System.Windows.Forms.ComboBox comboBoxIO;
        private System.Windows.Forms.Label labelSelectorLines;
        private System.Windows.Forms.ComboBox comboBoxLines;
        private System.Windows.Forms.Label label1;
        private NodeSubscription nodeSubscription1;
        private System.Windows.Forms.Button button1;
    }
}