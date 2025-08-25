using System;
using System.Windows.Forms;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.ProcessNew
{
    public partial class FormProcessRename : FormBase
    {
        Process process;
        public static event EventHandler<(string, string)> ProcessRenameChanged;
        public FormProcessRename()
        {
            InitializeComponent();
            Shown += FormProcessRename_Shown;
        }

        private void FormProcessRename_Shown(object sender, EventArgs e)
        {
            textBox1.Text = this.process.ProcessName;
        }

        public void SetProcess(Process process)
        {
            this.process = process;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string oldName = this.process.ProcessName;
                this.process.ProcessName = textBox1.Text;
                ProcessRenameChanged.Invoke(this, (oldName, textBox1.Text));
                Close();
            }
            else
            {
                MessageBoxTD.Show("流程名称不能为空！");
            }
        }
    }
}
