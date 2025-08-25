using System.IO;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sunny.UI;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision
{
    public partial class FormAIConfigTool : FormBase
    {
        public FormAIConfigTool()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开配置文件1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen1_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath1.Text = openFileDialog1.FileName;
                try
                {
                    string json = File.ReadAllText(openFileDialog1.FileName);
                    string text = StringCipher.Decrypt(json);
                    textBox1.Text = text;
                }
                catch (System.Exception ex)
                {
                    MessageBoxTD.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 打开配置文件2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen2_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath2.Text = openFileDialog1.FileName;
                try
                {
                    string json = File.ReadAllText(openFileDialog1.FileName);
                    string text = StringCipher.Decrypt(json);
                    textBox2.Text = text;
                }
                catch (System.Exception ex)
                {
                    MessageBoxTD.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 保存配置1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave1_Click(object sender, System.EventArgs e)
        {
            try
            {
                File.WriteAllText(textBoxFilePath1.Text, StringCipher.Encrypt(textBox1.Text));
                MessageBoxTD.Show($"保存成功！路径：{textBoxFilePath1.Text}");
            }
            catch (System.Exception ex)
            {
                MessageBoxTD.Show(ex.Message);
            }
        }
        /// <summary>
        /// 保存配置2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave2_Click(object sender, System.EventArgs e)
        {
            try
            {
                File.WriteAllText(textBoxFilePath2.Text, StringCipher.Encrypt(textBox2.Text));
                MessageBoxTD.Show($"保存成功！路径：{textBoxFilePath2.Text}");
            }
            catch (System.Exception ex)
            {
                MessageBoxTD.Show(ex.Message);
            }
        }
    }
}
