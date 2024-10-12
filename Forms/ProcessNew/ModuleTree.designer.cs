namespace YTVisionPro.Forms.ProcessNew
{
    partial class ModTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModTree));
            gCursorLib.TextShadower textShadower1 = new gCursorLib.TextShadower();
            this.VscrollBar = new System.Windows.Forms.VScrollBar();
            this.gCursor1 = new gCursorLib.gCursor(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // VscrollBar
            // 
            this.VscrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VscrollBar.Location = new System.Drawing.Point(242, 0);
            this.VscrollBar.Name = "VscrollBar";
            this.VscrollBar.Size = new System.Drawing.Size(21, 536);
            this.VscrollBar.TabIndex = 6;
            this.VscrollBar.ValueChanged += new System.EventHandler(this.VscrollBar_ValueChanged);
            // 
            // gCursor1
            // 
            this.gCursor1.gBlackBitBack = false;
            this.gCursor1.gBoxShadow = true;
            this.gCursor1.gCursorImage = ((System.Drawing.Bitmap)(resources.GetObject("gCursor1.gCursorImage")));
            this.gCursor1.gEffect = gCursorLib.gCursor.eEffect.No;
            this.gCursor1.gFont = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.gCursor1.gHotSpot = System.Drawing.ContentAlignment.MiddleCenter;
            this.gCursor1.gIBTransp = 80;
            this.gCursor1.gImage = null;
            this.gCursor1.gImageBorderColor = System.Drawing.Color.Black;
            this.gCursor1.gImageBox = new System.Drawing.Size(24, 24);
            this.gCursor1.gImageBoxColor = System.Drawing.Color.White;
            this.gCursor1.gITransp = 0;
            this.gCursor1.gScrolling = gCursorLib.gCursor.eScrolling.No;
            this.gCursor1.gShowImageBox = false;
            this.gCursor1.gShowTextBox = false;
            this.gCursor1.gTBTransp = 80;
            this.gCursor1.gText = "";
            this.gCursor1.gTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.gCursor1.gTextAutoFit = gCursorLib.gCursor.eTextAutoFit.All;
            this.gCursor1.gTextBorderColor = System.Drawing.Color.Red;
            this.gCursor1.gTextBox = new System.Drawing.Size(100, 20);
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
            this.gCursor1.gType = gCursorLib.gCursor.eType.Both;
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
            this.imageList1.Images.SetKeyName(6, "图像裁切.png");
            this.imageList1.Images.SetKeyName(7, "转为灰度图.png");
            this.imageList1.Images.SetKeyName(8, "提取ROI.png");
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
            this.imageList1.Images.SetKeyName(23, "常规工具.png");
            this.imageList1.Images.SetKeyName(24, "AI结果发送 .png");
            this.imageList1.Images.SetKeyName(25, "检测结果显示.png");
            this.imageList1.Images.SetKeyName(26, "保存图片.png");
            this.imageList1.Images.SetKeyName(27, "结果汇总.png");
            // 
            // ModTree
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.VscrollBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Name = "ModTree";
            this.Size = new System.Drawing.Size(273, 536);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.ModTree_GiveFeedback);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModelBoxtUI_MouseDown);
            this.MouseLeave += new System.EventHandler(this.ModelBoxtUI_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ModelBoxtUI_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ModelBoxtUI_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.VScrollBar VscrollBar;
        public gCursorLib.gCursor gCursor1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
