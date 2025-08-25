using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;

namespace TDJS_Vision.Node._3_Detection.MatchTemplate
{
    /// <summary>
    /// 模版创建窗口类
    /// </summary>
    public partial class TemplateCreate : FormBase
    {
        Bitmap Bitmap { get; set; }
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
                if(checkBoxUseSub.Checked)
                    bitmap = nodeSubscription1.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
            }
            catch (Exception)
            {
                bitmap = null;
            }
            Bitmap = bitmap;
            imageROIEditControl1.SetImage(Bitmap);
        }

        /// <summary>
        /// 使用哪种图像来源创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseSub.Checked)
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
                Bitmap = new Bitmap(openFileDialog1.FileName);
                imageROIEditControl1.SetImage(Bitmap);
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
                Bitmap bmp = imageROIEditControl1.GetROIImages()[0].ToBitmap();
                try
                {
                    bmp.Save(saveFileDialog1.FileName);
                }
                catch (Exception)
                {
                    MessageBoxTD.Show("保存失败！");
                    return;
                }
                MessageBoxTD.Show("保存成功！");
                Close();
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
