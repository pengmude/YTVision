using Logger;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageDelete
{
    public partial class NodeParamFormImageDelete : FormBase, INodeParamForm
    {
        SaveConfiguration saveConfiguration = new SaveConfiguration();

        public INodeParam Params { get; set; }


        void INodeParamForm.SetNodeBelong(NodeBase node)
        {

        }

        public void SetParam2Form()
        {
            if (Params is NodeParamImageDelete param)
            {
                saveConfiguration = param.saveConfiguration;
                textBox1.Text = param.saveConfiguration.DeletePath;
                textBox2.Text = param.saveConfiguration.Days.ToString();
                uiSwitch1.Active = param.saveConfiguration.IsDelete;
                textBox3.Text = param.SavePath;
            }
        }

        public NodeParamFormImageDelete()
        {
            InitializeComponent();
        }

        private async void uiSwitch1_ValueChanged(object sender, bool value)
        {
            if (!int.TryParse(this.textBox2.Text, out int days))
            {
                MessageBoxTD.Show("请输入有效的天数!");
                return;
            }
            saveConfiguration.Days = days;
            saveConfiguration.IsDelete = value;

            string json = JsonConvert.SerializeObject(saveConfiguration, Formatting.Indented);

            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    File.WriteAllText(textBox3.Text, json);
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // 获取删除所选文件的路径
                saveConfiguration.DeletePath = folderBrowserDialog1.SelectedPath;
                this.textBox1.Text = saveConfiguration.DeletePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!int.TryParse(this.textBox2.Text, out int days))
                {
                    MessageBoxTD.Show("请输入有效的天数!");
                    return;
                }

                saveConfiguration.Days = days;
                saveConfiguration.IsDelete = uiSwitch1.Active;

                if (string.IsNullOrWhiteSpace(saveConfiguration.DeletePath))
                {
                    MessageBoxTD.Show("请输入删除所选文件的路径!");
                    return;
                }

                string json = JsonConvert.SerializeObject(saveConfiguration, Formatting.Indented);
                File.WriteAllText(saveFileDialog1.FileName, json);
                LogHelper.AddLog(MsgLevel.Info, $"保存配置成功", true);
                this.textBox3.Text = saveFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SaveParams())
                Hide();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <returns></returns>
        private bool SaveParams()
        {
            try
            {
                NodeParamImageDelete nodeParamDeleteImage = new NodeParamImageDelete();
                nodeParamDeleteImage.saveConfiguration = saveConfiguration;
                nodeParamDeleteImage.SavePath = this.textBox3.Text; 
                Params = nodeParamDeleteImage;
            }
            catch (Exception)
            {
                MessageBoxTD.Show("参数设置异常，请检查参数设置是否合理！");
                return false;
            }
            return true;
        }
    }

    public class SaveConfiguration
    {
        public string DeletePath = "";
        public int Days;
        public bool IsDelete;
    }
}
