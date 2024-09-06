using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTVisionPro.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "退出")
            {
                button1.Text = "确定";
                // TODO:设置为不可编辑状态
            }
            else
            {
                if(textBox1.Text == "2024")
                {
                    button1.Text = "退出";
                    // TODO:设置为可编辑状态
                }
                else
                {
                    MessageBox.Show("不正确的操作码！");
                }
            }
        }
    }
}
