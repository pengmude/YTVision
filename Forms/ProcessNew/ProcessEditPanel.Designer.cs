﻿namespace YTVisionPro.Forms.ProcessNew
{
    partial class ProcessEditPanel
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiLedBulb1 = new Sunny.UI.UILedBulb();
            this.uiSwitchEnable = new Sunny.UI.UISwitch();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置流程优先级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置流程组别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.uiLedBulb1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiSwitchEnable, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRun, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(548, 51);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // uiLedBulb1
            // 
            this.uiLedBulb1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiLedBulb1.Color = System.Drawing.Color.DarkGray;
            this.uiLedBulb1.Location = new System.Drawing.Point(258, 12);
            this.uiLedBulb1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uiLedBulb1.Name = "uiLedBulb1";
            this.uiLedBulb1.Size = new System.Drawing.Size(28, 27);
            this.uiLedBulb1.TabIndex = 0;
            this.uiLedBulb1.Text = "uiLedBulb1";
            // 
            // uiSwitchEnable
            // 
            this.uiSwitchEnable.Active = true;
            this.uiSwitchEnable.ActiveText = "启用";
            this.uiSwitchEnable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.uiSwitchEnable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSwitchEnable.InActiveText = "禁用";
            this.uiSwitchEnable.Location = new System.Drawing.Point(444, 13);
            this.uiSwitchEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uiSwitchEnable.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitchEnable.Name = "uiSwitchEnable";
            this.uiSwitchEnable.Size = new System.Drawing.Size(95, 24);
            this.uiSwitchEnable.TabIndex = 0;
            this.uiSwitchEnable.Text = "uiSwitch1";
            this.uiSwitchEnable.ValueChanged += new Sunny.UI.UISwitch.OnValueChanged(this.uiSwitch1_ValueChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRun.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.buttonRun.Image = global::YTVisionPro.Properties.Resources.单次执行;
            this.buttonRun.Location = new System.Drawing.Point(335, 2);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(92, 46);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.UseVisualStyleBackColor = false;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "节点数:0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "流程耗时:0ms";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoScroll = true;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 591);
            this.panel1.TabIndex = 1;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.NodeEditPanel_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.NodeEditPanel_DragEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置流程优先级ToolStripMenuItem,
            this.设置流程组别ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 80);
            // 
            // 设置流程优先级ToolStripMenuItem
            // 
            this.设置流程优先级ToolStripMenuItem.Name = "设置流程优先级ToolStripMenuItem";
            this.设置流程优先级ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.设置流程优先级ToolStripMenuItem.Text = "设置流程优先级";
            this.设置流程优先级ToolStripMenuItem.Click += new System.EventHandler(this.设置流程优先级ToolStripMenuItem_Click);
            // 
            // 设置流程组别ToolStripMenuItem
            // 
            this.设置流程组别ToolStripMenuItem.Name = "设置流程组别ToolStripMenuItem";
            this.设置流程组别ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.设置流程组别ToolStripMenuItem.Text = "设置流程组别";
            this.设置流程组别ToolStripMenuItem.Click += new System.EventHandler(this.设置流程组别ToolStripMenuItem_Click);
            // 
            // ProcessEditPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProcessEditPanel";
            this.Size = new System.Drawing.Size(548, 642);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.NodeEditPanel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.NodeEditPanel_DragEnter);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UISwitch uiSwitchEnable;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UILedBulb uiLedBulb1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置流程优先级ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置流程组别ToolStripMenuItem;
    }
}
