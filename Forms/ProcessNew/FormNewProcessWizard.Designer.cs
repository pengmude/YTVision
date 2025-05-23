﻿using YTVisionPro.Node;

namespace YTVisionPro.Forms.ProcessNew
{
    partial class FormNewProcessWizard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewProcessWizard));
            gCursorLib.TextShadower textShadower2 = new gCursorLib.TextShadower();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全部展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部折叠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.gCursor1 = new gCursorLib.gCursor(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.buttonRemove, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(691, 521);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(581, 60);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(79, 35);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "删除流程";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(583, 8);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(76, 35);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "添加流程";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(243, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel1.SetRowSpan(this.tabControl1, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(306, 517);
            this.tabControl1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(2, 54);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.tableLayoutPanel1.SetRowSpan(this.treeView1, 2);
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(237, 465);
            this.treeView1.TabIndex = 3;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            this.treeView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseMove);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部展开ToolStripMenuItem,
            this.全部折叠ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 全部展开ToolStripMenuItem
            // 
            this.全部展开ToolStripMenuItem.Name = "全部展开ToolStripMenuItem";
            this.全部展开ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全部展开ToolStripMenuItem.Text = "全部展开";
            this.全部展开ToolStripMenuItem.Click += new System.EventHandler(this.全部展开ToolStripMenuItem_Click);
            // 
            // 全部折叠ToolStripMenuItem
            // 
            this.全部折叠ToolStripMenuItem.Name = "全部折叠ToolStripMenuItem";
            this.全部折叠ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全部折叠ToolStripMenuItem.Text = "全部折叠";
            this.全部折叠ToolStripMenuItem.Click += new System.EventHandler(this.全部折叠ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "工具箱.png");
            this.imageList1.Images.SetKeyName(1, "图像采集");
            this.imageList1.Images.SetKeyName(2, "图像源");
            this.imageList1.Images.SetKeyName(3, "图像显示");
            this.imageList1.Images.SetKeyName(4, "图像处理");
            this.imageList1.Images.SetKeyName(5, "图像裁剪");
            this.imageList1.Images.SetKeyName(6, "图像旋转");
            this.imageList1.Images.SetKeyName(7, "图像分割");
            this.imageList1.Images.SetKeyName(8, "检测识别");
            this.imageList1.Images.SetKeyName(9, "AI检测");
            this.imageList1.Images.SetKeyName(10, "检测直线");
            this.imageList1.Images.SetKeyName(11, "检测圆");
            this.imageList1.Images.SetKeyName(12, "二维码识别");
            this.imageList1.Images.SetKeyName(13, "模版匹配");
            this.imageList1.Images.SetKeyName(14, "测量工具");
            this.imageList1.Images.SetKeyName(15, "长度测量");
            this.imageList1.Images.SetKeyName(16, "面积测量");
            this.imageList1.Images.SetKeyName(17, "直线平行度");
            this.imageList1.Images.SetKeyName(18, "注液孔测量");
            this.imageList1.Images.SetKeyName(19, "通信工具");
            this.imageList1.Images.SetKeyName(20, "打开光源");
            this.imageList1.Images.SetKeyName(21, "相机IO");
            this.imageList1.Images.SetKeyName(22, "PLC读");
            this.imageList1.Images.SetKeyName(23, "PLC写");
            this.imageList1.Images.SetKeyName(24, "PLC软触发");
            this.imageList1.Images.SetKeyName(25, "PLC发送结果");
            this.imageList1.Images.SetKeyName(26, "客户端请求");
            this.imageList1.Images.SetKeyName(27, "服务器响应");
            this.imageList1.Images.SetKeyName(28, "Modbus读取");
            this.imageList1.Images.SetKeyName(29, "Modbus写入");
            this.imageList1.Images.SetKeyName(30, "Modbus软触发");
            this.imageList1.Images.SetKeyName(31, "Modbus发送AI结果");
            this.imageList1.Images.SetKeyName(32, "逻辑工具");
            this.imageList1.Images.SetKeyName(33, "共享变量");
            this.imageList1.Images.SetKeyName(34, "延迟执行");
            this.imageList1.Images.SetKeyName(35, "结果处理");
            this.imageList1.Images.SetKeyName(36, "保存图片");
            this.imageList1.Images.SetKeyName(37, "检测结果显示");
            this.imageList1.Images.SetKeyName(38, "结果汇总");
            this.imageList1.Images.SetKeyName(39, "图片删除");
            this.imageList1.Images.SetKeyName(40, "导出检测表格");
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.ImageIndex = 0;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(72, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "工具箱";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gCursor1
            // 
            this.gCursor1.gBlackBitBack = false;
            this.gCursor1.gBoxShadow = true;
            this.gCursor1.gCursorImage = ((System.Drawing.Bitmap)(resources.GetObject("gCursor1.gCursorImage")));
            this.gCursor1.gEffect = gCursorLib.gCursor.eEffect.No;
            this.gCursor1.gFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gCursor1.gHotSpot = System.Drawing.ContentAlignment.MiddleCenter;
            this.gCursor1.gIBTransp = 80;
            this.gCursor1.gImage = null;
            this.gCursor1.gImageBorderColor = System.Drawing.Color.Black;
            this.gCursor1.gImageBox = new System.Drawing.Size(75, 56);
            this.gCursor1.gImageBoxColor = System.Drawing.Color.White;
            this.gCursor1.gITransp = 0;
            this.gCursor1.gScrolling = gCursorLib.gCursor.eScrolling.No;
            this.gCursor1.gShowImageBox = false;
            this.gCursor1.gShowTextBox = false;
            this.gCursor1.gTBTransp = 80;
            this.gCursor1.gText = "";
            this.gCursor1.gTextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.gCursor1.gTextAutoFit = gCursorLib.gCursor.eTextAutoFit.None;
            this.gCursor1.gTextBorderColor = System.Drawing.Color.Red;
            this.gCursor1.gTextBox = new System.Drawing.Size(100, 10);
            this.gCursor1.gTextBoxColor = System.Drawing.Color.Blue;
            this.gCursor1.gTextColor = System.Drawing.Color.Blue;
            this.gCursor1.gTextFade = gCursorLib.gCursor.eTextFade.Solid;
            this.gCursor1.gTextMultiline = false;
            this.gCursor1.gTextShadow = false;
            this.gCursor1.gTextShadowColor = System.Drawing.Color.Black;
            textShadower2.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            textShadower2.Blur = 2F;
            textShadower2.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            textShadower2.Offset = ((System.Drawing.PointF)(resources.GetObject("textShadower2.Offset")));
            textShadower2.Padding = new System.Windows.Forms.Padding(0);
            textShadower2.ShadowColor = System.Drawing.Color.Black;
            textShadower2.ShadowTransp = 128;
            textShadower2.Text = "Drop Shadow";
            textShadower2.TextColor = System.Drawing.Color.Blue;
            this.gCursor1.gTextShadower = textShadower2;
            this.gCursor1.gTTransp = 0;
            this.gCursor1.gType = gCursorLib.gCursor.eType.Text;
            // 
            // FormNewProcessWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 521);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewProcessWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "流程编辑";
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Form1_GiveFeedback);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private gCursorLib.gCursor gCursor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全部展开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部折叠ToolStripMenuItem;
    }
}