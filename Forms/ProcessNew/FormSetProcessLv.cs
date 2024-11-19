using Logger;
using System;
using System.Windows.Forms;

namespace YTVisionPro.Forms.ProcessNew
{
    internal partial class FormSetProcessLv : Form
    {
        private Process _process;
        public FormSetProcessLv(Process process)
        {
            InitializeComponent();
            Shown += FormSetProcessLv_Shown;
            _process = process;
        }

        private void FormSetProcessLv_Shown(object sender, EventArgs e)
        {
            switch (_process.RunLv)
            {
                case ProcessLvEnum.Lv1:
                    comboBox1.SelectedIndex = 0;
                    break;
                case ProcessLvEnum.Lv2:
                    comboBox1.SelectedIndex = 1;
                    break;
                case ProcessLvEnum.Lv3:
                    comboBox1.SelectedIndex = 2;
                    break;
                case ProcessLvEnum.Lv4:
                    comboBox1.SelectedIndex = 3;
                    break;
                case ProcessLvEnum.Lv5:
                    comboBox1.SelectedIndex = 4;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    _process.RunLv = ProcessLvEnum.Lv1;
                    break;
                case 1:
                    _process.RunLv = ProcessLvEnum.Lv2;
                    break;
                case 2:
                    _process.RunLv = ProcessLvEnum.Lv3;
                    break;
                case 3:
                    _process.RunLv = ProcessLvEnum.Lv4;
                    break;
                case 4:
                    _process.RunLv = ProcessLvEnum.Lv5;
                    break;
            }
            Hide();
        }
    }
}
