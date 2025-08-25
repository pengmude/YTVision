using System;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.Login
{
    public partial class FormLogin : FormBase
    {
        public static EventHandler<UserRole> LoginEvent;
        public static EventHandler LogoutEvent;
        public FormLogin()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;//默认选择“操作员”
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBoxTD.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //高级
            if (comboBox1.SelectedIndex == 0 && textBoxPassword.Text == "tdjs")
            {
                LoginEvent?.Invoke(this, UserRole.Hight);
            }//中级
            else if (comboBox1.SelectedIndex == 1 && textBoxPassword.Text == "888")
            {
                LoginEvent?.Invoke(this, UserRole.Medium);
            }//初级
            else if (comboBox1.SelectedIndex == 2 && textBoxPassword.Text == "666")
            {
                LoginEvent?.Invoke(this, UserRole.Low);
            }//密码错误
            else
            {
                MessageBoxTD.Show("密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox1.Enabled = false;
            textBoxPassword.Enabled = false;
            buttonLogin.Enabled = false;
            buttonQuit.Enabled = true;
            this.Hide();
        }

        /// <summary>
        /// 登录角色测试
        /// </summary>
        /// <param name="role"></param>
        public static void LoginTest(UserRole role)
        {
            LoginEvent?.Invoke(null, role);
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            LogoutEvent?.Invoke(this, EventArgs.Empty);
            comboBox1.Enabled = true;
            textBoxPassword.Enabled = true;
            buttonLogin.Enabled = true;
            buttonQuit.Enabled = false;
            textBoxPassword.Text = string.Empty;
        }
    }
    /// <summary>
    /// 用户角色枚举
    /// </summary>
    public enum UserRole
    {
        Hight, // 最高权限
        Medium,  // 中等权限
        Low,  // 最低权限
        Unknown  // 未知权限
    }
}
