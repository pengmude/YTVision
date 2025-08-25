using System;

namespace TDJS_Vision.Forms.ProcessNew
{
    public partial class FormProcessGroupSetting : FormBase
    {
        private Process _process;
        public FormProcessGroupSetting(Process process)
        {
            InitializeComponent();
            Shown += FormProcessGroupSetting_show;
            _process = process;
        }

        private void FormProcessGroupSetting_show(object sender, EventArgs e)
        {
            switch (_process.Group)
            {
                case ProcessGroup.Group1:
                    comboBox1.SelectedIndex = 0;
                    break;
                case ProcessGroup.Group2:
                    comboBox1.SelectedIndex = 1;
                    break;
                case ProcessGroup.Group3:
                    comboBox1.SelectedIndex = 2;
                    break;
                case ProcessGroup.Group4:
                    comboBox1.SelectedIndex = 3;
                    break;
                case ProcessGroup.Group5:
                    comboBox1.SelectedIndex = 4;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    _process.Group = ProcessGroup.Group1;
                    break;
                case 1:
                    _process.Group = ProcessGroup.Group2;
                    break;
                case 2:
                    _process.Group = ProcessGroup.Group3;
                    break;
                case 3:
                    _process.Group = ProcessGroup.Group4;
                    break;
                case 4:
                    _process.Group = ProcessGroup.Group5;
                    break;
            }
            Hide();
        }
    }
}
