using Logger;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using YTVisionPro.Hardware.Light;
using YTVisionPro.Node.Light.PPX;
using YTVisionPro.Node.NodeLight.PPX;

namespace YTVisionPro.Forms.LightAdd
{
    internal partial class FrmLightListView : Form
    {
        FrmLightNew frmLightNew = new FrmLightNew();

        /// <summary>
        /// 已添加的光源所占用的Com列表
        /// 作用：解决相同COM号多个通道连接光源的问题
        /// </summary>
        public static HashSet<SerialPort> OccupiedComList = new HashSet<SerialPort>();

        public FrmLightListView()
        {
            InitializeComponent();
            frmLightNew.LightAddEvent += FrmLightNew_LightAddEvent;
            SingleLight.SelectedChange += SingleLight_SelectedChange;
            SingleLight.SingleLightRemoveEvent += SingleLight_SinglePLCRemoveEvent;
        }

        /// <summary>
        /// 移除设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleLight_SinglePLCRemoveEvent(object sender, SingleLight e)
        {
            //移除的是被选中的则要清除它参数显示控件
            if (e.IsSelected)
                panel1.Controls.Remove(e.LightParamsShowControl);
            //删除光源前先判断是否需要释放占用的串口
            //（当除了待删除的光源外，均没有使用和待删除光源的串口，则需要释放串口）
            int count = 0;
            foreach (var light in Solution.Instance.LightDevices)
            {
                if(light.LightParam.Port == e.Light.LightParam.Port)
                    count++;
            }
            //然后移除掉方案中的全局光源并释放串口资源
            Solution.Instance.Devices.Remove(e.Light);
            if (count == 1)
                e.Light.Disconnect();
            //最后移除掉光源控件和节点
            flowLayoutPanel1.Controls.Remove(e);
        }

        /// <summary>
        /// 切换选择的光源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleLight_SelectedChange(object sender, SingleLight e)
        {
            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control == e)
                {
                    panel1.Controls.Clear();
                    e.LightParamsShowControl.Dock = DockStyle.Fill;
                    panel1.Controls.Add(e.LightParamsShowControl);
                }
            }
        }

        /// <summary>
        /// 光源添加事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLightNew_LightAddEvent(object sender, LightParam e)
        {
            SingleLight singleLight = null;
            try
            {
                singleLight = new SingleLight(e);
                singleLight.Light.Connenct();
                singleLight.Anchor = AnchorStyles.Left;
                singleLight.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singleLight);
            }
            catch (Exception ex)
            {
                SingleLight.SingleLights.Remove(singleLight);
                LogHelper.AddLog(MsgLevel.Fatal, $"添加光源失败:{ex.Message}", true);
                MessageBox.Show($"添加光源失败:{ex.Message}");
            }
            //MessageBox.Show($"光源控件个数：{SingleLight.SingleLights.Count}，方案中光源个数：{Solution.Instance.LightDevices.Count}");
        }

        /// <summary>
        /// 点击添加光源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            frmLightNew.ShowDialog();
        }


        private void FrmPLCListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
