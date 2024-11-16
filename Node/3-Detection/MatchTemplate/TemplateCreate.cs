using System;
using System.Drawing;
using System.Windows.Forms;

namespace YTVisionPro.Node._3_Detection.MatchTemplate
{
    /// <summary>
    /// 模版创建窗口类
    /// </summary>
    internal partial class TemplateCreate : Form
    {
        public TemplateCreate()
        {
            InitializeComponent();
            Shown += Form_Shown;
            imageROIEditControl1.SetROIType2Draw(Forms.ShapeDraw.ROIType.Rectangle);
        }

        /// <summary>
        /// 每次显示窗口时刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Shown(object sender, EventArgs e)
        {
            UpdataImage();
        }

        /// <summary>
        /// 设置节点所属
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscription1.Init(node);
        }

        /// <summary>
        /// 更新图像
        /// </summary>
        public void UpdataImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = nodeSubscription1.GetValue<Bitmap>();
            }
            catch (Exception)
            {
                bitmap = null;
            }
            imageROIEditControl1.SetImage(bitmap);
        }

        /// <summary>
        /// 使用哪种图像来源创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
                nodeSubscription1.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                nodeSubscription1.Enabled = false;
                button2.Enabled = false;
            }
        }

        /// <summary>
        /// 选择图像文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                imageROIEditControl1.SetImage(new Bitmap(openFileDialog1.FileName));
            }
        }

        /// <summary>
        /// 保存模版图像文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = imageROIEditControl1.GetROIImages();
                try
                {
                    bmp.Save(saveFileDialog1.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败！");
                    return;
                }
                MessageBox.Show("保存成功！");
            }
        }

        /// <summary>
        /// 点击刷新图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            UpdataImage();
        }
    }
}
