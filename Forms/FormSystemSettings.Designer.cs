namespace YTVisionPro.Forms
{
    partial class FormSystemSettings
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.materialDrawer1 = new ReaLTaiizor.Controls.MaterialDrawer();
            this.nightForm1 = new ReaLTaiizor.Forms.NightForm();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.foreverGroupBox2 = new ReaLTaiizor.Controls.ForeverGroupBox();
            this.foreverGroupBox3 = new ReaLTaiizor.Controls.ForeverGroupBox();
            this.foreverGroupBox4 = new ReaLTaiizor.Controls.ForeverGroupBox();
            this.foreverGroupBox1 = new ReaLTaiizor.Controls.ForeverGroupBox();
            this.foreverGroupBox5 = new ReaLTaiizor.Controls.ForeverGroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.nightForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.foreverGroupBox5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.foreverGroupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.foreverGroupBox4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.foreverGroupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.foreverGroupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1389, 1091);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // materialDrawer1
            // 
            this.materialDrawer1.AutoHide = false;
            this.materialDrawer1.AutoShow = false;
            this.materialDrawer1.BackgroundWithAccent = false;
            this.materialDrawer1.BaseTabControl = null;
            this.materialDrawer1.Depth = 0;
            this.materialDrawer1.DrawerHideTabName = new string[0];
            this.materialDrawer1.DrawerNonClickTabPage = new System.Windows.Forms.TabPage[0];
            this.materialDrawer1.HighlightWithAccent = true;
            this.materialDrawer1.IndicatorWidth = 0;
            this.materialDrawer1.IsOpen = false;
            this.materialDrawer1.Location = new System.Drawing.Point(-729, 12);
            this.materialDrawer1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialDrawer1.Name = "materialDrawer1";
            this.materialDrawer1.ShowIconsWhenHidden = false;
            this.materialDrawer1.ShowTabControl = null;
            this.materialDrawer1.Size = new System.Drawing.Size(729, 493);
            this.materialDrawer1.TabIndex = 1;
            this.materialDrawer1.Text = "materialDrawer1";
            this.materialDrawer1.UseColors = false;
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nightForm1.Controls.Add(this.tableLayoutPanel1);
            this.nightForm1.Controls.Add(this.nightControlBox1);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = false;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(1389, 1122);
            this.nightForm1.TabIndex = 2;
            this.nightForm1.Text = "系统设置";
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = true;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(1250, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 0;
            // 
            // foreverGroupBox2
            // 
            this.foreverGroupBox2.ArrowColorF = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox2.ArrowColorH = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.foreverGroupBox2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverGroupBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.foreverGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.foreverGroupBox2.Name = "foreverGroupBox2";
            this.foreverGroupBox2.ShowArrow = true;
            this.foreverGroupBox2.ShowText = true;
            this.foreverGroupBox2.Size = new System.Drawing.Size(1383, 212);
            this.foreverGroupBox2.TabIndex = 5;
            this.foreverGroupBox2.Text = "画布设置";
            this.foreverGroupBox2.TextColor = System.Drawing.Color.LightGray;
            // 
            // foreverGroupBox3
            // 
            this.foreverGroupBox3.ArrowColorF = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox3.ArrowColorH = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.foreverGroupBox3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverGroupBox3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.foreverGroupBox3.Location = new System.Drawing.Point(3, 221);
            this.foreverGroupBox3.Name = "foreverGroupBox3";
            this.foreverGroupBox3.ShowArrow = true;
            this.foreverGroupBox3.ShowText = true;
            this.foreverGroupBox3.Size = new System.Drawing.Size(1383, 212);
            this.foreverGroupBox3.TabIndex = 6;
            this.foreverGroupBox3.Text = "自启动设置";
            this.foreverGroupBox3.TextColor = System.Drawing.Color.LightGray;
            // 
            // foreverGroupBox4
            // 
            this.foreverGroupBox4.ArrowColorF = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox4.ArrowColorH = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.foreverGroupBox4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverGroupBox4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.foreverGroupBox4.Location = new System.Drawing.Point(3, 439);
            this.foreverGroupBox4.Name = "foreverGroupBox4";
            this.foreverGroupBox4.ShowArrow = true;
            this.foreverGroupBox4.ShowText = true;
            this.foreverGroupBox4.Size = new System.Drawing.Size(1383, 212);
            this.foreverGroupBox4.TabIndex = 7;
            this.foreverGroupBox4.Text = "其他设置";
            this.foreverGroupBox4.TextColor = System.Drawing.Color.LightGray;
            // 
            // foreverGroupBox1
            // 
            this.foreverGroupBox1.ArrowColorF = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox1.ArrowColorH = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.foreverGroupBox1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverGroupBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.foreverGroupBox1.Location = new System.Drawing.Point(3, 657);
            this.foreverGroupBox1.Name = "foreverGroupBox1";
            this.foreverGroupBox1.ShowArrow = true;
            this.foreverGroupBox1.ShowText = true;
            this.foreverGroupBox1.Size = new System.Drawing.Size(1383, 212);
            this.foreverGroupBox1.TabIndex = 8;
            this.foreverGroupBox1.TextColor = System.Drawing.Color.LightGray;
            // 
            // foreverGroupBox5
            // 
            this.foreverGroupBox5.ArrowColorF = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox5.ArrowColorH = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox5.BackColor = System.Drawing.Color.Transparent;
            this.foreverGroupBox5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.foreverGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foreverGroupBox5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.foreverGroupBox5.Location = new System.Drawing.Point(3, 875);
            this.foreverGroupBox5.Name = "foreverGroupBox5";
            this.foreverGroupBox5.ShowArrow = true;
            this.foreverGroupBox5.ShowText = true;
            this.foreverGroupBox5.Size = new System.Drawing.Size(1383, 213);
            this.foreverGroupBox5.TabIndex = 9;
            this.foreverGroupBox5.TextColor = System.Drawing.Color.LightGray;
            // 
            // FormSystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1389, 1122);
            this.Controls.Add(this.nightForm1);
            this.Controls.Add(this.materialDrawer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(2560, 1540);
            this.MinimumSize = new System.Drawing.Size(190, 40);
            this.Name = "FormSystemSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dungeonForm1";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.nightForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ReaLTaiizor.Controls.MaterialDrawer materialDrawer1;
        private ReaLTaiizor.Forms.NightForm nightForm1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private ReaLTaiizor.Controls.ForeverGroupBox foreverGroupBox4;
        private ReaLTaiizor.Controls.ForeverGroupBox foreverGroupBox3;
        private ReaLTaiizor.Controls.ForeverGroupBox foreverGroupBox2;
        private ReaLTaiizor.Controls.ForeverGroupBox foreverGroupBox5;
        private ReaLTaiizor.Controls.ForeverGroupBox foreverGroupBox1;
    }
}