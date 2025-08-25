using System;
using System.Drawing;
using System.Windows.Forms;

namespace TDJS_Vision.Forms.YTMessageBox
{
    public partial class MessageBoxTD : FormBase
    {
        private DialogResult result = DialogResult.None;
        private MessageBoxButtons buttons;

        public MessageBoxTD()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="icon"></param>
        public void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.None:
                    pictureBox1.Visible = false;
                    tableLayoutPanel3.ColumnStyles[0].Width = 0; // 隐藏图标列
                    break;
                case MessageBoxIcon.Information:
                    pictureBox1.Image = imageList1.Images[0];
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = imageList1.Images[1];
                    break;
                case MessageBoxIcon.Warning:
                    pictureBox1.Image = imageList1.Images[2];
                    break;
                case MessageBoxIcon.Error:
                    pictureBox1.Image = imageList1.Images[3];
                    break;
                default:
                    pictureBox1.Visible = false;
                    tableLayoutPanel3.ColumnStyles[0].Width = 0; // 隐藏图标列
                    break;
            }
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var msgBox = new MessageBoxTD())
            {
                msgBox.Text = title; // 设置窗体标题
                msgBox.labelMessage.Text = message;

                // 设置按钮
                msgBox.SetButtons(buttons);
                msgBox.SetIcon(icon);
                msgBox.ShowDialog();
                return msgBox.result;
            }
        }
        public static DialogResult Show(string message)
        {
            using (var msgBox = new MessageBoxTD())
            {
                msgBox.Text = "";
                msgBox.labelMessage.Text = message;
                msgBox.SetButtons(MessageBoxButtons.OK); // 默认按钮为 OK
                msgBox.ShowDialog();
                return msgBox.result;
            }
        }

        private void SetButtons(MessageBoxButtons buttons)
        {
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                if(control is Button bt)
                {
                    tableLayoutPanel2.Controls.Remove(bt);
                }
            }
            this.buttons = buttons;
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    var btnOk = new Button { Text = "确定", Width = 80, DialogResult = DialogResult.Yes,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.LightSeaGreen,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    btnOk.Click += (s, e) => { result = DialogResult.OK; this.Close(); };
                    tableLayoutPanel2.Controls.Add(btnOk, 1, 1);
                    break;

                case MessageBoxButtons.OKCancel:
                    var btnCancel = new Button { Text = "取消", Width = 80, DialogResult = DialogResult.Cancel,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.IndianRed,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    var btnOk1 = new Button { Text = "确定", Width = 80, DialogResult = DialogResult.OK,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.LightSeaGreen,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    btnOk1.Click += (s, e) => { result = DialogResult.OK; this.Close(); };
                    btnCancel.Click += (s, e) => { result = DialogResult.Cancel; this.Close(); };
                    tableLayoutPanel2.Controls.Add(btnOk1, 1, 1);
                    tableLayoutPanel2.Controls.Add(btnCancel, 0, 1);
                    break;

                case MessageBoxButtons.YesNo:
                    var btnYes = new Button { Text = "是", Width = 80, DialogResult = DialogResult.Yes,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.LightSeaGreen,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    var btnNo = new Button { Text = "否", Width = 80, DialogResult = DialogResult.No,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.IndianRed,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    btnYes.Click += (s, e) => { result = DialogResult.Yes; this.Close(); };
                    btnNo.Click += (s, e) => { result = DialogResult.No; this.Close(); };
                    tableLayoutPanel2.Controls.Add(btnYes, 1, 1);
                    tableLayoutPanel2.Controls.Add(btnNo, 0, 1);
                    break;
                default:
                    var btnOk2 = new Button
                    {
                        Text = "确定",
                        Width = 80,
                        DialogResult = DialogResult.Yes,
                        Anchor = AnchorStyles.None,
                        BackColor = Color.LightSeaGreen,
                        ForeColor = SystemColors.Control,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };
                    btnOk2.Click += (s, e) => { result = DialogResult.OK; this.Close(); };
                    tableLayoutPanel2.Controls.Add(btnOk2, 1, 1);
                    break;
            }
        }

        internal static void Show(string v1, string v2, System.Windows.MessageBoxButton oK, MessageBoxIcon error)
        {
            throw new NotImplementedException();
        }
    }
}
