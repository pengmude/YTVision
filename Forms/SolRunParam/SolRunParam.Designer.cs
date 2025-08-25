namespace TDJS_Vision.Forms.SolRunParam
{
    partial class SolRunParamControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSaveAllParam = new System.Windows.Forms.Button();
            this.myDataGridViewForm1 = new TDJS_Vision.Forms.SolRunParam.MyDataGridViewForm();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxExposureTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxScoreThreshold = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxScoreNMS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOnce = new System.Windows.Forms.Button();
            this.uiSwitchContinue = new Sunny.UI.UISwitch();
            this.buttonCatalogueImg = new System.Windows.Forms.Button();
            this.buttonSingleImg = new System.Windows.Forms.Button();
            this.uiSwitch_Learning = new Sunny.UI.UISwitch();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveAllParam, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.myDataGridViewForm1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxExposureTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxScoreThreshold, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxScoreNMS, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxGain, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonOnce, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiSwitchContinue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCatalogueImg, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSingleImg, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiSwitch_Learning, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1063, 422);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonSaveAllParam
            // 
            this.buttonSaveAllParam.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSaveAllParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSaveAllParam.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSaveAllParam.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveAllParam.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSaveAllParam.Location = new System.Drawing.Point(885, 84);
            this.buttonSaveAllParam.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSaveAllParam.Name = "buttonSaveAllParam";
            this.buttonSaveAllParam.Size = new System.Drawing.Size(178, 42);
            this.buttonSaveAllParam.TabIndex = 5;
            this.buttonSaveAllParam.Text = "保存设置";
            this.buttonSaveAllParam.UseVisualStyleBackColor = false;
            this.buttonSaveAllParam.Click += new System.EventHandler(this.buttonSaveAllParam_Click);
            // 
            // myDataGridViewForm1
            // 
            this.myDataGridViewForm1.AutoScroll = true;
            this.myDataGridViewForm1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.myDataGridViewForm1, 6);
            this.myDataGridViewForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataGridViewForm1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myDataGridViewForm1.Location = new System.Drawing.Point(3, 130);
            this.myDataGridViewForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.myDataGridViewForm1.Name = "myDataGridViewForm1";
            this.myDataGridViewForm1.Size = new System.Drawing.Size(1057, 288);
            this.myDataGridViewForm1.TabIndex = 7;
            this.myDataGridViewForm1.Leave += new System.EventHandler(this.myDataGridViewForm1_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(66, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "曝光";
            // 
            // textBoxExposureTime
            // 
            this.textBoxExposureTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxExposureTime.Enabled = false;
            this.textBoxExposureTime.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxExposureTime.Location = new System.Drawing.Point(180, 3);
            this.textBoxExposureTime.Name = "textBoxExposureTime";
            this.textBoxExposureTime.Size = new System.Drawing.Size(171, 28);
            this.textBoxExposureTime.TabIndex = 1;
            this.textBoxExposureTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxExposureTime.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(39, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "AI得分阈值";
            // 
            // textBoxScoreThreshold
            // 
            this.textBoxScoreThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxScoreThreshold.Enabled = false;
            this.textBoxScoreThreshold.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxScoreThreshold.Location = new System.Drawing.Point(180, 87);
            this.textBoxScoreThreshold.Name = "textBoxScoreThreshold";
            this.textBoxScoreThreshold.Size = new System.Drawing.Size(171, 28);
            this.textBoxScoreThreshold.TabIndex = 1;
            this.textBoxScoreThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxScoreThreshold.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(393, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "AI抑制分数";
            // 
            // textBoxScoreNMS
            // 
            this.textBoxScoreNMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxScoreNMS.Enabled = false;
            this.textBoxScoreNMS.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxScoreNMS.Location = new System.Drawing.Point(534, 87);
            this.textBoxScoreNMS.Name = "textBoxScoreNMS";
            this.textBoxScoreNMS.Size = new System.Drawing.Size(171, 28);
            this.textBoxScoreNMS.TabIndex = 1;
            this.textBoxScoreNMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxScoreNMS.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(66, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "增益";
            // 
            // textBoxGain
            // 
            this.textBoxGain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGain.Enabled = false;
            this.textBoxGain.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxGain.Location = new System.Drawing.Point(180, 45);
            this.textBoxGain.Name = "textBoxGain";
            this.textBoxGain.Size = new System.Drawing.Size(171, 28);
            this.textBoxGain.TabIndex = 1;
            this.textBoxGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxGain.Leave += new System.EventHandler(this.textBoxExposureTime_Leave);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(402, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "连续采图";
            // 
            // buttonOnce
            // 
            this.buttonOnce.BackColor = System.Drawing.SystemColors.Control;
            this.buttonOnce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOnce.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOnce.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOnce.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOnce.Location = new System.Drawing.Point(534, 45);
            this.buttonOnce.Name = "buttonOnce";
            this.buttonOnce.Size = new System.Drawing.Size(171, 36);
            this.buttonOnce.TabIndex = 0;
            this.buttonOnce.Text = "单次采图";
            this.buttonOnce.UseVisualStyleBackColor = false;
            this.buttonOnce.Click += new System.EventHandler(this.buttonOnce_Click);
            // 
            // uiSwitchContinue
            // 
            this.uiSwitchContinue.ActiveColor = System.Drawing.Color.MediumTurquoise;
            this.uiSwitchContinue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSwitchContinue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitchContinue.Location = new System.Drawing.Point(561, 6);
            this.uiSwitchContinue.Margin = new System.Windows.Forms.Padding(30, 6, 30, 6);
            this.uiSwitchContinue.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitchContinue.Name = "uiSwitchContinue";
            this.uiSwitchContinue.Size = new System.Drawing.Size(117, 30);
            this.uiSwitchContinue.TabIndex = 8;
            this.uiSwitchContinue.Text = "uiSwitch1";
            this.uiSwitchContinue.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitchContinue_ValueChanged);
            // 
            // buttonCatalogueImg
            // 
            this.buttonCatalogueImg.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCatalogueImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCatalogueImg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCatalogueImg.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCatalogueImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCatalogueImg.Location = new System.Drawing.Point(888, 45);
            this.buttonCatalogueImg.Name = "buttonCatalogueImg";
            this.buttonCatalogueImg.Size = new System.Drawing.Size(172, 36);
            this.buttonCatalogueImg.TabIndex = 0;
            this.buttonCatalogueImg.Text = "多图测试";
            this.buttonCatalogueImg.UseVisualStyleBackColor = false;
            this.buttonCatalogueImg.Click += new System.EventHandler(this.buttonCatalogueImg_Click);
            // 
            // buttonSingleImg
            // 
            this.buttonSingleImg.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSingleImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSingleImg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSingleImg.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSingleImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSingleImg.Location = new System.Drawing.Point(711, 45);
            this.buttonSingleImg.Name = "buttonSingleImg";
            this.buttonSingleImg.Size = new System.Drawing.Size(171, 36);
            this.buttonSingleImg.TabIndex = 0;
            this.buttonSingleImg.Text = "单图测试";
            this.buttonSingleImg.UseVisualStyleBackColor = false;
            this.buttonSingleImg.Click += new System.EventHandler(this.buttonSingleImg_Click);
            // 
            // uiSwitch_Learning
            // 
            this.uiSwitch_Learning.ActiveColor = System.Drawing.Color.MediumTurquoise;
            this.uiSwitch_Learning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSwitch_Learning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitch_Learning.Location = new System.Drawing.Point(915, 6);
            this.uiSwitch_Learning.Margin = new System.Windows.Forms.Padding(30, 6, 30, 6);
            this.uiSwitch_Learning.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch_Learning.Name = "uiSwitch_Learning";
            this.uiSwitch_Learning.Size = new System.Drawing.Size(118, 30);
            this.uiSwitch_Learning.TabIndex = 8;
            this.uiSwitch_Learning.Text = "uiSwitch1";
            this.uiSwitch_Learning.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch_Learning_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(756, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "一键学习";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SolRunParamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SolRunParamControl";
            this.Size = new System.Drawing.Size(1063, 422);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonSingleImg;
        private System.Windows.Forms.TextBox textBoxExposureTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxScoreThreshold;
        private System.Windows.Forms.TextBox textBoxScoreNMS;
        private System.Windows.Forms.Button buttonCatalogueImg;
        private System.Windows.Forms.Button buttonSaveAllParam;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MyDataGridViewForm myDataGridViewForm1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonOnce;
        private Sunny.UI.UISwitch uiSwitchContinue;
        private Sunny.UI.UISwitch uiSwitch_Learning;
        private System.Windows.Forms.Label label6;
    }
}
