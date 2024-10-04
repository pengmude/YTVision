using System;
using System.Windows.Forms;

namespace YTVisionPro.Forms
{
    public partial class FrmOperatorLocker : Form
    {
        public event EventHandler<bool> OperatorLockerChanged;
        public FrmOperatorLocker()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isUnlocking = button1.Text == "解锁";
            string expectedCode = "2024";

            if (textBox1.Text == expectedCode)
            {
                button1.Text = isUnlocking ? "加锁" : "解锁";
                textBox1.Text = "";
                OperatorLockerChanged?.Invoke(this, isUnlocking);
                Hide();
            }
            else
            {
                MessageBox.Show("不正确的操作码！");
            }
        }
    }
}
