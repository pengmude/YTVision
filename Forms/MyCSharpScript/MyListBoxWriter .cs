using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDJS_Vision.Forms.MyCSharpScript
{
    public class ListBoxWriter : TextWriter
    {
        private readonly ListBox _listBox;
        private readonly int _maxLines = 1000; // 防止 ListBox 太多行

        public ListBoxWriter(ListBox listBox)
        {
            _listBox = listBox;
        }

        public override void WriteLine(string value)
        {
            if (_listBox.InvokeRequired)
            {
                _listBox.Invoke(new Action(() => WriteLineToBox(value)));
            }
            else
            {
                WriteLineToBox(value);
            }
        }

        public override void Write(string value)
        {
            WriteLine(value); // 通常也按行处理
        }

        private void WriteLineToBox(string value)
        {
            _listBox.Items.Add($"[输出] {value}");

            // 限制行数，防止内存溢出
            while (_listBox.Items.Count > _maxLines)
            {
                _listBox.Items.RemoveAt(0);
            }

            // 滚动到底部
            _listBox.TopIndex = _listBox.Items.Count - 1;
        }

        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }
}
