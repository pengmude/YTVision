using YTVisionPro.Hardware.Camera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using YTVisionPro.Node.Light.PPX;
using YTVisionPro.Node.NodeLight.PPX;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class FrmCameraListView : Form
    {
        /// <summary>
        /// 设备信息弹窗
        /// </summary>
        FrmCameraInfo _infoWnd = new FrmCameraInfo();

        public FrmCameraListView()
        {
            InitializeComponent();
            FrmCameraInfo.AddCameraDevEvent += FrmCameraInfo_AddCameraDevEvent;
            SingleCamera.SelectedChange += SingleCamera_SingleCameraSelectedChanged;
            SingleCamera.SingleCameraRemoveEvent += SingleCamera_SingleCameraRemoveEvent;
        }

        /// <summary>
        /// 移除一个相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCamera_SingleCameraRemoveEvent(object sender, SingleCamera e)
        {
            // TODO: 移除设备需要判断当前是否有节点使用该设备
            //foreach (var camera in Solution.Instance.CameraDevices)
            //{
            //    foreach (var node in Solution.Nodes)
            //    {
            //        if (node is NodeLight lightNode
            //            && lightNode.Params is NodeParamLight paramLight
            //            && camera.UserDefinedName == paramLight.Light.UserDefinedName)
            //        {
            //            MessageBox.Show("当前方案的节点正在使用该光源，无法删除光源！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //}

            //移除的是被选中的则要清除它参数显示控件
            if (e.IsSelected)
                panel1.Controls.Remove(e.CameraParamsShowControl);
            //然后移除掉方案中的全局相机并释放相机内存
            Solution.Instance.Devices.Remove(e.Camera);
            e.Camera.Dispose();
            //最后移除掉光源控件
            flowLayoutPanel1.Controls.Remove(e);
        }

        /// <summary>
        /// 处理相机设备添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCameraInfo_AddCameraDevEvent(object sender, CameraParam e)
        {
            try
            {
                SingleCamera singleCamera = new SingleCamera(e);
                singleCamera.Anchor = AnchorStyles.Left;
                singleCamera.Anchor = AnchorStyles.Right;
                flowLayoutPanel1.Controls.Add(singleCamera);
                singleCamera.CameraParamsShowControl.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败！原因：" + ex.Message);
            }
        }
        
        /// <summary>
        /// 处理选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleCamera_SingleCameraSelectedChanged(object sender, SingleCamera e)
        {
            panel1.Controls.Clear();
            e.CameraParamsShowControl.Dock = DockStyle.Fill;
            e.CameraParamsShowControl.Show();
            panel1.Controls.Add(e.CameraParamsShowControl);
        }

        /// <summary>
        /// 点击添加单个相机控件（左侧）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            _infoWnd.ShowDialog();
        }

        /// <summary>
        /// 窗口关闭关掉所有相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCameraListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in Solution.Instance.CameraDevices)
            {
                item.Close();
            }
        }
    }
}
