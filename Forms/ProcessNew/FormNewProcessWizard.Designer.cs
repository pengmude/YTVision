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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewProcessWizard));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.nodeComboBox5 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox3 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox2 = new YTVisionPro.Node.NodeComboBox();
            this.nodeComboBox1 = new YTVisionPro.Node.NodeComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1263, 772);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.nodeComboBox5);
            this.panel1.Controls.Add(this.nodeComboBox3);
            this.panel1.Controls.Add(this.nodeComboBox2);
            this.panel1.Controls.Add(this.nodeComboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 766);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.buttonRemove, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonAdd, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(381, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(879, 766);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemove.Image = global::YTVisionPro.Properties.Resources.流程删除;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(679, 198);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(20);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(180, 62);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "删除流程";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel2.SetRowSpan(this.tabControl1, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(653, 760);
            this.tabControl1.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.Image = global::YTVisionPro.Properties.Resources.流程添加;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(679, 45);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(20);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(180, 62);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "添加流程";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.button2_Click);
            // 
            // nodeComboBox5
            // 
            this.nodeComboBox5.AutoSize = true;
            this.nodeComboBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox5.Expanded = true;
            this.nodeComboBox5.Location = new System.Drawing.Point(0, 225);
            this.nodeComboBox5.Name = "nodeComboBox5";
            this.nodeComboBox5.Size = new System.Drawing.Size(372, 75);
            this.nodeComboBox5.TabIndex = 4;
            // 
            // nodeComboBox3
            // 
            this.nodeComboBox3.AutoSize = true;
            this.nodeComboBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox3.Expanded = true;
            this.nodeComboBox3.Location = new System.Drawing.Point(0, 150);
            this.nodeComboBox3.Name = "nodeComboBox3";
            this.nodeComboBox3.Size = new System.Drawing.Size(372, 75);
            this.nodeComboBox3.TabIndex = 2;
            // 
            // nodeComboBox2
            // 
            this.nodeComboBox2.AutoSize = true;
            this.nodeComboBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox2.Expanded = true;
            this.nodeComboBox2.Location = new System.Drawing.Point(0, 75);
            this.nodeComboBox2.Name = "nodeComboBox2";
            this.nodeComboBox2.Size = new System.Drawing.Size(372, 75);
            this.nodeComboBox2.TabIndex = 1;
            // 
            // nodeComboBox1
            // 
            this.nodeComboBox1.AutoSize = true;
            this.nodeComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.nodeComboBox1.Expanded = true;
            this.nodeComboBox1.Location = new System.Drawing.Point(0, 0);
            this.nodeComboBox1.Name = "nodeComboBox1";
            this.nodeComboBox1.Size = new System.Drawing.Size(372, 75);
            this.nodeComboBox1.TabIndex = 0;
            // 
            // FormNewProcessWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 772);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewProcessWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "流程编辑";
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
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private NodeComboBox nodeComboBox1;
        private NodeComboBox nodeComboBox3;
        private NodeComboBox nodeComboBox2;
        private NodeComboBox nodeComboBox5;
    }
}