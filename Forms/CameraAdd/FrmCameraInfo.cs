using Basler.Pylon;
using Logger;
using MvCameraControl;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTVisionPro.Device.Camera;

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
        private static int _HikCount = 1;

        /// <summary>
        /// 巴斯勒名称计数
        /// </summary>
        private static int _Baslercount = 1;

        /// <summary>
        /// 大恒名称计数
        /// </summary>
        private static int _DaHengcount = 1;

        /// <summary>
        /// 大华名称计数
        /// </summary>
        private static int _DaHuacount = 1;

        /// <summary>
        /// 相机信息列表
        /// </summary>
        private List<IDeviceInfo> infoList = CameraHik.FindCamera();

        /// <summary>
        /// 用来保存设备名对应的设备信息
        /// </summary>
        private Dictionary<string, IDeviceInfo> _mapCamera = new Dictionary<string, IDeviceInfo>();

        public FrmCameraInfo()
        {
            InitializeComponent();
            infoList = CameraHik.FindCamera();
            _mapCamera.Clear();
            foreach (var info in infoList)
                _mapCamera[CameraHik.GetDevNameByDevInfo(info)] = info;
            //Shown += FrmCaFrmCameraInfo_Shown;
        }
        private void FrmCameraInfo_Load(object sender, EventArgs e)
        {
            // 初始化相机品牌
            InitCameraBrandList();
            // 初始化相机列表
            InitCameraList(comboBoxCameraBrand.Text);
        }

        //private void FrmCaFrmCameraInfo_Shown(object sender, EventArgs e)
        //{
           
        //}

        /// <summary>
        /// 初始相机品牌下拉框
        /// </summary>
        private void InitCameraBrandList()
        {
            var addedBrands = new HashSet<string>();
            comboBoxCameraBrand.Items.Clear();
            foreach (var info in infoList)
            {
                string brand;
                switch (info.ManufacturerName)
                {
                    case "GEV":
                    case "Hikrobot":
                        brand = "海康";
                        if (addedBrands.Add(brand))
                        {
                            comboBoxCameraBrand.Items.Add(brand);
                        }
                        break;
                    case "Basler":
                        brand = "巴斯勒";
                        if (addedBrands.Add(brand))
                        {
                            comboBoxCameraBrand.Items.Add(brand);
                        }
                        break;
                    case "Daheng Imaging":
                        brand = "大恒";
                        if (addedBrands.Add(brand))
                        {
                            comboBoxCameraBrand.Items.Add(brand);
                        }
                        break;
                    case "Dahua Technology":
                        brand = "大华";
                        if (addedBrands.Add(brand))
                        {
                            comboBoxCameraBrand.Items.Add(brand);
                        }
                        break;
                }
            }

            if (comboBoxCameraBrand.Items.Count > 0)
                comboBoxCameraBrand.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化相机下拉框数据
        /// </summary>
        private void InitCameraList(string brand)
        {
            comboBoxCameraList.Items.Clear();
            switch (brand)
            {
                case "海康":
                    foreach (var info in infoList)
                    {
                        if (info.ManufacturerName == "Hikrobot" || info.ManufacturerName == "GEV")
                        {
                            comboBoxCameraList.Items.Add(CameraHik.GetDevNameByDevInfo(info));
                        }
                    }
                    break;
                case "巴斯勒":
                    foreach (var info in infoList)
                    {
                        if (info.ManufacturerName == "Basler")
                        {
                            comboBoxCameraList.Items.Add(CameraHik.GetDevNameByDevInfo(info));
                        }
                    }
                    break;
                case "大恒":
                    foreach (var info in infoList)
                    {
                        if (info.ManufacturerName == "Daheng Imaging")
                        {
                            comboBoxCameraList.Items.Add(CameraHik.GetDevNameByDevInfo(info));
                        }
                    }
                    break;
                case "大华":
                    foreach (var info in infoList)
                    {
                        if (info.ManufacturerName == "Dahua Technology")
                        {
                            comboBoxCameraList.Items.Add(CameraHik.GetDevNameByDevInfo(info));
                        }
                    }
                    break;
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
            infoList = CameraHik.FindCamera();
            _mapCamera.Clear();
            foreach (var info in infoList)
                _mapCamera[CameraHik.GetDevNameByDevInfo(info)] = info;
            // 初始化相机品牌和相机设备下拉列表
            InitCameraBrandList();
            InitCameraList(comboBoxCameraBrand.Text);
        }

        /// <summary>
        /// 相机品牌选中改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCameraBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCameraList(comboBoxCameraBrand.Text);
            switch (comboBoxCameraBrand.Text)
            {
                case "海康":
                    textBoxUserName.Text = $"{comboBoxCameraBrand.Text}相机{_HikCount}";
                    break;
                case "巴斯勒":
                    textBoxUserName.Text = $"{comboBoxCameraBrand.Text}相机{_Baslercount}";
                    break;
                case "大恒":
                    textBoxUserName.Text = $"{comboBoxCameraBrand.Text}相机{_DaHengcount}";
                    break;
                case "大华":
                    textBoxUserName.Text = $"{comboBoxCameraBrand.Text}相机{_DaHuacount}";
                    break;
            }
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
                switch (comboBoxCameraBrand.Text)
                {
                    case "海康":
                        info.Brand = CameraBrand.HiKVision;
                        _HikCount++;
                        break;
                    case "巴斯勒":
                        info.Brand = CameraBrand.Basler;
                        _Baslercount++;
                        break;
                    case "大恒":
                        info.Brand = CameraBrand.DaHeng;
                        _DaHengcount++;
                        break;
                    case "大华":
                        info.Brand = CameraBrand.DaHua;
                        _DaHuacount++;
                        break;
                }
                info.DevInfo = new CameraDevInfo(_mapCamera[comboBoxCameraList.Text]);
                info.UserDefinedName = textBoxUserName.Text;

                AddCameraDevEvent?.Invoke(this, info);
                this.Hide();
            }
            catch (Exception ex)
            {
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
        public string UserDefinedName;
    }
}
