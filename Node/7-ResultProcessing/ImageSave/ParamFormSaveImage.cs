using Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;
using TDJS_Vision.Node._1_Acquisition.ImageSource;
using TDJS_Vision.Node._3_Detection.TDAI;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace TDJS_Vision.Node._7_ResultProcessing.ImageSave
{
    public partial class ParamFormImageSave : FormBase, INodeParamForm
    {
        /// <summary>
        /// 节点参数
        /// </summary>
        public INodeParam Params { get; set; }

        public ParamFormImageSave()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 用于节点参数界面需要订阅结果的情况调用
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeBelong(NodeBase node)
        {
            nodeSubscriptionImg2Save.Init(node);
            nodeSubscriptionBarCode.Init(node);
            nodeSubscriptionAiRes.Init(node);
        }

        /// <summary>
        /// 存图路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// 是否启用读码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxBarCode_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Enabled = checkBoxBarCode.Checked;
        }

        /// <summary>
        /// 是否区分NG存图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSaveWithNG_CheckedChanged(object sender, EventArgs e)
        {
            panel6.Enabled = checkBoxSaveWithNG.Checked;
            radioButtonOKAndNG.Enabled = checkBoxSaveWithNG.Checked;
            radioButtonOK.Enabled = checkBoxSaveWithNG.Checked;
            radioButtonNG.Enabled = checkBoxSaveWithNG.Checked;
        }

        /// <summary>
        /// 是否区分早晚班存图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDayNight_CheckedChanged(object sender, EventArgs e)
        {
            panel7.Enabled = checkBoxDayNight.Checked;
        }

        /// <summary>
        /// 图片是否压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel9.Enabled = checkBoxCompress.Checked;
        }

        /// <summary>
        /// 获取图片订阅控件的图像
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImage()
        {
            try
            {
                return nodeSubscriptionImg2Save.GetValue<OutputImage>().Bitmaps[0].ToBitmap();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取二维码订阅控件的的值
        /// </summary>
        /// <returns></returns>
        public string GetBarCode()
        {
            try
            {
                return nodeSubscriptionBarCode.GetValue<string>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取AI结果订阅控件的值
        /// </summary>
        public AlgorithmResult GetAiResult()
        {
            try
            {
                return nodeSubscriptionAiRes.GetValue<AlgorithmResult>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBoxTD.Show("未选择存图路径");
                LogHelper.AddLog(MsgLevel.Fatal, "未选择存图路径", true);
                return;
            }
            if(Convert.ToUInt16(this.numericUpDown1.Value) > 100 && Convert.ToUInt16(this.numericUpDown1.Value) < 0)
            {
                MessageBoxTD.Show("压缩阈值范围不合法！");
                LogHelper.AddLog(MsgLevel.Fatal, "压缩阈值范围不合法！", true);
                return;
            }

            NodeParamSaveImage savaImageParam = new NodeParamSaveImage();
            savaImageParam.SavePath = textBox1.Text;
            savaImageParam.ImgSubText1 = nodeSubscriptionImg2Save.GetText1();
            savaImageParam.ImgSubText2 = nodeSubscriptionImg2Save.GetText2();
            savaImageParam.AiResSubText1 = nodeSubscriptionAiRes.GetText1();
            savaImageParam.AiResSubText2 = nodeSubscriptionAiRes.GetText2();
            savaImageParam.BarCodeSubText1 = nodeSubscriptionBarCode.GetText1();
            savaImageParam.BarCodeSubText2 = nodeSubscriptionBarCode.GetText2();

            // 二维码命名
            savaImageParam.IsBarCode = checkBoxBarCode.Checked;
            //是否需要区分OkNg子目录
            savaImageParam.NeedOkNg = checkBoxSaveWithNG.Checked;
            // 早晚班存图
            if (checkBoxDayNight.Checked)
            {
                savaImageParam.IsDayNight = true;
                savaImageParam.DayDataTime = dateTimePicker1.Value;
                savaImageParam.NightDataTime = dateTimePicker2.Value;
            }
            else
                savaImageParam.IsDayNight = false;
            // 图片压缩
            if (checkBoxCompress.Checked)
            {
                savaImageParam.NeedCompress = true;
                savaImageParam.CompressValue = Convert.ToUInt16(this.numericUpDown1.Value);
            }
            else
                savaImageParam.NeedCompress = false;

            // 保存什么图
            savaImageParam.ImageTypeToSave = radioButtonOKAndNG.Checked ? ImageTypeToSave.OkAndNg : 
                                             (radioButtonOK.Checked ? ImageTypeToSave.OnlyOk : ImageTypeToSave.OnlyNg);

            Params = savaImageParam;
            Hide();
        }
        /// <summary>
        /// 反序列化需要设置参数给回界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SetParam2Form()
        {
            if (Params is NodeParamSaveImage param)
            {
                textBox1.Text = param.SavePath;
                nodeSubscriptionImg2Save.SetText(param.ImgSubText1, param.ImgSubText2);
                checkBoxBarCode.Checked = param.IsBarCode;
                checkBoxSaveWithNG.Checked = param.NeedOkNg;
                checkBoxDayNight.Checked = param.IsDayNight;
                checkBoxCompress.Checked = param.NeedCompress;
                nodeSubscriptionAiRes.SetText(param.AiResSubText1, param.AiResSubText2);
                dateTimePicker1.Value = (param.DayDataTime < dateTimePicker1.MinDate || param.DayDataTime > dateTimePicker1.MaxDate) ? 
                                        dateTimePicker1.MinDate : param.DayDataTime;
                dateTimePicker2.Value = (param.NightDataTime < dateTimePicker2.MinDate || param.NightDataTime > dateTimePicker2.MaxDate) ?
                                        dateTimePicker2.MinDate : param.NightDataTime;
                numericUpDown1.Value = param.CompressValue;
                nodeSubscriptionBarCode.SetText(param.BarCodeSubText1, param.BarCodeSubText2);
                switch (param.ImageTypeToSave)
                {
                    case ImageTypeToSave.OkAndNg:
                        radioButtonOKAndNG.Checked = true;
                        break;
                    case ImageTypeToSave.OnlyOk:
                        radioButtonOK.Checked = true;
                        break;
                    case ImageTypeToSave.OnlyNg:
                        radioButtonNG.Checked = true;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
