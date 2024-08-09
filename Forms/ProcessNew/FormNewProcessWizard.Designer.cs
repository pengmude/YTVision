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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.nodeComboBox3 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox2 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox1 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox4 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox5 = new YTVisionPro.Node.NodeComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 751);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.nodeComboBox5);
            this.panel1.Controls.Add(this.nodeComboBox4);
            this.panel1.Controls.Add(this.nodeComboBox3);
            this.panel1.Controls.Add(this.nodeComboBox2);
            this.panel1.Controls.Add(this.nodeComboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 745);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(325, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(479, 745);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 67);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(473, 675);
            this.tabControl1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(299, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(60, 6, 60, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "删除流程";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(60, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(60, 6, 60, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 52);
            this.button2.TabIndex = 3;
            this.button2.Text = "添加流程";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nodeComboBox3
            // 
            this.nodeComboBox3.AutoSize = true;
            this.nodeComboBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox3.Expanded = true;
            this.nodeComboBox3.Location = new System.Drawing.Point(0, 150);
            this.nodeComboBox3.Name = "nodeComboBox3";
            this.nodeComboBox3.Size = new System.Drawing.Size(316, 75);
            this.nodeComboBox3.TabIndex = 2;
            // 
            // nodeComboBox2
            // 
            this.nodeComboBox2.AutoSize = true;
            this.nodeComboBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox2.Expanded = true;
            this.nodeComboBox2.Location = new System.Drawing.Point(0, 75);
            this.nodeComboBox2.Name = "nodeComboBox2";
            this.nodeComboBox2.Size = new System.Drawing.Size(316, 75);
            this.nodeComboBox2.TabIndex = 1;
            // 
            // nodeComboBox1
            // 
            this.nodeComboBox1.AutoSize = true;
            this.nodeComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox1.Expanded = true;
            this.nodeComboBox1.Location = new System.Drawing.Point(0, 0);
            this.nodeComboBox1.Name = "nodeComboBox1";
            this.nodeComboBox1.Size = new System.Drawing.Size(316, 75);
            this.nodeComboBox1.TabIndex = 0;
            // 
            // nodeComboBox4
            // 
            this.nodeComboBox4.AutoSize = true;
            this.nodeComboBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox4.Expanded = true;
            this.nodeComboBox4.Location = new System.Drawing.Point(0, 225);
            this.nodeComboBox4.Name = "nodeComboBox4";
            this.nodeComboBox4.Size = new System.Drawing.Size(316, 75);
            this.nodeComboBox4.TabIndex = 3;
            // 
            // nodeComboBox5
            // 
            this.nodeComboBox5.AutoSize = true;
            this.nodeComboBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox5.Expanded = true;
            this.nodeComboBox5.Location = new System.Drawing.Point(0, 300);
            this.nodeComboBox5.Name = "nodeComboBox5";
            this.nodeComboBox5.Size = new System.Drawing.Size(316, 75);
            this.nodeComboBox5.TabIndex = 4;
            // 
            // FormNewProcessWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 751);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewProcessWizard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "流程创建向导";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private NodeComboBox nodeComboBox1;
        private NodeComboBox nodeComboBox3;
        private NodeComboBox nodeComboBox2;
        private NodeComboBox nodeComboBox5;
        private NodeComboBox nodeComboBox4;
    }
}