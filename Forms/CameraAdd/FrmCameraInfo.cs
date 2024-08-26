using Basler.Pylon;
using Logger;
using MvCameraControl;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Hardware.Camera;

namespace YTVisionPro.Forms.CameraAdd
{
    internal partial class FrmCameraInfo : Form
    {
        /// <summary>
        /// 添加相机设备事件
        /// </summary>
        public static event EventHandler<CameraParam> AddCameraDevEvent;

        /// <summary>
        /// 海康名称计数
        /// </summary>
        private static int _count1 = 0;

        /// <summary>
        /// 巴斯勒名称计数
        /// </summary>
        private static int _count2 = 0;

        /// <summary>
        /// 相机信息列表
        /// </summary>
        private List<IDeviceInfo> infoListHik = CameraHik.FindCamera();
        private List<ICameraInfo> infoListBasler = CameraBasler.FindCamera();

        /// <summary>
        /// 用来保存设备名对应的设备信息
        /// </summary>
        private Dictionary<string, IDeviceInfo> _mapHik = new Dictionary<string, IDeviceInfo>();
        private Dictionary<string, ICameraInfo> _mapBasler = new Dictionary<string, ICameraInfo>();

        public FrmCameraInfo()
        {
            InitializeComponent();
            comboBoxCameraBrand.SelectedIndex = 0;
            // 查找相机信息
            infoListHik = CameraHik.FindCamera();
            infoListBasler = CameraBasler.FindCamera();

            // 保存各品牌相机信息map
            _mapHik.Clear();
            _mapBasler.Clear();
            foreach (var info in infoListHik)
                _mapHik[CameraHik.GetDevName(info)] = info;
            foreach (var info in infoListBasler)
                _mapBasler[info[CameraInfoKey.ModelName]] = info;
            Shown += FrmCaFrmCameraInfo_Shown;
        }

        private void FrmCaFrmCameraInfo_Shown(object sender, EventArgs e)
        {
            // 初始化相机列表
            InitCameraList(comboBoxCameraBrand.SelectedIndex);
        }

        /// <summary>
        /// 初始化相机下拉框数据
        /// </summary>
        private void InitCameraList(int brandIndex)
        {
            comboBoxCameraList.Items.Clear();
            if (brandIndex == 0)
            {
                foreach (var info in infoListHik)
                    comboBoxCameraList.Items.Add(CameraHik.GetDevName(info));
            }
            else if (brandIndex == 1) 
            { 
                foreach (var info in infoListBasler)
                    comboBoxCameraList.Items.Add(info[CameraInfoKey.ModelName]);
            }

            #region 测试代码，测试数据

            //for (int i = 0; i < 5; i++)
            //{
            //    comboBoxCameraList.Items.Add($"Camera{i}");
            //}

            #endregion

            if (comboBoxCameraList.Items.Count > 0)
                comboBoxCameraList.SelectedIndex = 0;
        }

        /// <summary>
        /// 点击搜索相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // 查找相机信息
            infoListHik = CameraHik.FindCamera();
            infoListBasler = CameraBasler.FindCamera();

            // 保存各品牌相机信息map
            _mapHik.Clear();
            _mapBasler.Clear();
            foreach (var info in infoListHik)
                _mapHik[CameraHik.GetDevName(info)] = info;
            foreach (var info in infoListBasler)
                _mapBasler[info[CameraInfoKey.ModelName]] = info;

            // 清空相机下拉框
            comboBoxCameraList.Items.Clear();

            // 保存
            if (comboBoxCameraBrand.SelectedIndex == 0)
            {
                foreach (var info in infoListHik)
                    comboBoxCameraList.Items.Add(CameraHik.GetDevName(info));
            }
            else if (comboBoxCameraBrand.SelectedIndex == 1)
            {
                foreach (var info in infoListBasler)
                    comboBoxCameraList.Items.Add(info[CameraInfoKey.ModelName]);

            }
            if (comboBoxCameraList.Items.Count > 0)
                comboBoxCameraList.SelectedIndex = 0;
        }

        /// <summary>
        /// 相机品牌选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCameraBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCameraList(comboBoxCameraBrand.SelectedIndex);
            textBoxUserName.Text = $"{comboBoxCameraBrand.Text}相机" + (comboBoxCameraBrand.SelectedIndex == 0 ? (_count1 + 1) : (_count2 + 1));
        }

        /// <summary>
        /// 点击发送相机设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDevAdd_Click(object sender, EventArgs e)
        {
            //参数完整性判断
            if (comboBoxCameraList.Text.IsNullOrEmpty() || textBoxUserName.Text.IsNullOrEmpty())
            {
                LogHelper.AddLog(MsgLevel.Warn, "相机设备名或用户自定义名称不能为空！", true);
                MessageBox.Show("相机设备名或用户自定义名称不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //设备重复性判断
            foreach (var camera in Solution.Instance.CameraDevices)
            {
                if(camera.DevName == comboBoxCameraList.Text || camera.UserDefinedName == textBoxUserName.Text)
                {
                    LogHelper.AddLog(MsgLevel.Warn, "当前相机设备已存在或用户自定义名称已存在！", true);
                    MessageBox.Show("当前相机设备已存在或用户自定义名称已存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CameraParam info = new CameraParam();
            try
            {
                //通过事件传递相机参数
                info.Brand = comboBoxCameraBrand.SelectedIndex == 0 ? CameraBrand.HiKVision : CameraBrand.Basler;
                if(comboBoxCameraBrand.SelectedIndex == 0)
                    info.DevInfo = new CameraDevInfo(_mapHik[comboBoxCameraList.Text], null);
                else if(comboBoxCameraBrand.SelectedIndex == 1)
                    info.DevInfo = new CameraDevInfo(null, _mapBasler[comboBoxCameraList.Text]);
                info.UserDefineName = textBoxUserName.Text;

                AddCameraDevEvent?.Invoke(this, info);
                if (info.Brand == CameraBrand.HiKVision)
                    _count1++;
                else
                    _count2++;
                this.Hide();
            }
            catch (Exception ex)
            {
                AddCameraDevEvent?.Invoke(this, info);
                MessageBox.Show("添加相机异常：" + ex.Message);

                #region 测试代码
                
                //if (info.Brand == CameraBrand.HiKVision)
                //    _count1++;
                //else
                //    _count2++;
                //this.Hide();

                #endregion
            }
        }
    }

    /// <summary>
    /// 参数结构体
    /// </summary>
    public struct CameraParam
    {
        public CameraBrand Brand;
        public CameraDevInfo DevInfo;
        public string UserDefineName;
    }
}
