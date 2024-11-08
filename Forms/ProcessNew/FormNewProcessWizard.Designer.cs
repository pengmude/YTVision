using YTVisionPro.Node;

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
            gCursorLib.TextShadower textShadower1 = new gCursorLib.TextShadower();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全部展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部折叠ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gCursor1 = new gCursorLib.gCursor(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonRemove, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonAdd, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1335, 975);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(1077, 117);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(20);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(180, 57);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "删除流程";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(1077, 20);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(20);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(180, 57);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "添加流程";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(403, 3);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel1.SetRowSpan(this.tabControl1, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 969);
            this.tabControl1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(7, 103);
            this.treeView1.Name = "treeView1";
            this.tableLayoutPanel1.SetRowSpan(this.treeView1, 2);
            this.treeView1.Size = new System.Drawing.Size(385, 865);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 64);
            // 
            // 全部展开ToolStripMenuItem
            // 
            this.全部展开ToolStripMenuItem.Name = "全部展开ToolStripMenuItem";
            this.全部展开ToolStripMenuItem.Size = new System.Drawing.Size(152, 30);
            this.全部展开ToolStripMenuItem.Text = "全部展开";
            this.全部展开ToolStripMenuItem.Click += new System.EventHandler(this.全部展开ToolStripMenuItem_Click);
            // 
            // 全部折叠ToolStripMenuItem
            // 
            this.全部折叠ToolStripMenuItem.Name = "全部折叠ToolStripMenuItem";
            this.全部折叠ToolStripMenuItem.Size = new System.Drawing.Size(152, 30);
            this.全部折叠ToolStripMenuItem.Text = "全部折叠";
            this.全部折叠ToolStripMenuItem.Click += new System.EventHandler(this.全部折叠ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("江西拙楷", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.ImageIndex = 28;
            this.label1.ImageList = this.imageList1;
            this.label1.Location = new System.Drawing.Point(128, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 48);
            this.label1.TabIndex = 4;
            this.label1.Text = "工具箱";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "光源控制工具.ico");
            this.imageList1.Images.SetKeyName(1, "打开光源.png");
            this.imageList1.Images.SetKeyName(2, "图像采集工具.ico");
            this.imageList1.Images.SetKeyName(3, "本地图片.png");
            this.imageList1.Images.SetKeyName(4, "相机拍照.png");
            this.imageList1.Images.SetKeyName(5, "图像预处理.png");
            this.imageList1.Images.SetKeyName(6, "图像裁剪.png");
            this.imageList1.Images.SetKeyName(7, "转为灰度图.png");
            this.imageList1.Images.SetKeyName(8, "Blob分析.png");
            this.imageList1.Images.SetKeyName(9, "检测识别.png");
            this.imageList1.Images.SetKeyName(10, "圆查找.png");
            this.imageList1.Images.SetKeyName(11, "模版匹配.png");
            this.imageList1.Images.SetKeyName(12, "几何测量.png");
            this.imageList1.Images.SetKeyName(13, "长度测量.png");
            this.imageList1.Images.SetKeyName(14, "面积测量.png");
            this.imageList1.Images.SetKeyName(15, "Ai检测工具.png");
            this.imageList1.Images.SetKeyName(16, "AI检测.png");
            this.imageList1.Images.SetKeyName(17, "plc通信工具.png");
            this.imageList1.Images.SetKeyName(18, "PLC读.png");
            this.imageList1.Images.SetKeyName(19, "PLC写.png");
            this.imageList1.Images.SetKeyName(20, "获取软触发信号.png");
            this.imageList1.Images.SetKeyName(21, "流程控制.png");
            this.imageList1.Images.SetKeyName(22, "延迟执行.png");
            this.imageList1.Images.SetKeyName(23, "结果处理.png");
            this.imageList1.Images.SetKeyName(24, "AI结果发送 .png");
            this.imageList1.Images.SetKeyName(25, "检测结果显示.png");
            this.imageList1.Images.SetKeyName(26, "保存图片.png");
            this.imageList1.Images.SetKeyName(27, "结果汇总.png");
            this.imageList1.Images.SetKeyName(28, "工具箱.png");
            this.imageList1.Images.SetKeyName(29, "直线查找.png");
            this.imageList1.Images.SetKeyName(30, "图像显示.png");
            this.imageList1.Images.SetKeyName(31, "Modbus读取.png");
            this.imageList1.Images.SetKeyName(32, "modbus写入.png");
            this.imageList1.Images.SetKeyName(33, "Modbus通信.png");
            this.imageList1.Images.SetKeyName(34, "TCP通信工具.png");
            this.imageList1.Images.SetKeyName(35, "TCP客户端请求.png");
            this.imageList1.Images.SetKeyName(36, "服务器响应.png");
            this.imageList1.Images.SetKeyName(37, "图像旋转.png");
            this.imageList1.Images.SetKeyName(38, "两直线平行度.png");
            this.imageList1.Images.SetKeyName(39, "Modbus软触发.png");
            this.imageList1.Images.SetKeyName(40, "Modbus发送AI结果.png");
            this.imageList1.Images.SetKeyName(41, "相机IO.png");
            this.imageList1.Images.SetKeyName(42, "IO控制.png");
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
            textShadower1.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            textShadower1.Blur = 2F;
            textShadower1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            textShadower1.Offset = ((System.Drawing.PointF)(resources.GetObject("textShadower1.Offset")));
            textShadower1.Padding = new System.Windows.Forms.Padding(0);
            textShadower1.ShadowColor = System.Drawing.Color.Black;
            textShadower1.ShadowTransp = 128;
            textShadower1.Text = "Drop Shadow";
            textShadower1.TextColor = System.Drawing.Color.Blue;
            this.gCursor1.gTextShadower = textShadower1;
            this.gCursor1.gTTransp = 0;
            this.gCursor1.gType = gCursorLib.gCursor.eType.Text;
            // 
            // FormNewProcessWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 975);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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