using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Microsoft.CodeAnalysis;
using ServiceStack;
using TDJS_Vision.Forms.YTMessageBox;

namespace TDJS_Vision.Forms.MyCSharpScript
{
    public partial class CSharpScriptEditor : UserControl
    {
        AutocompleteMenu popupMenu;
        string[] keywords = { "region", "endregion", "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do${\n^;\n}while();", "switch(^)\n{\ncase : break;\n}"};
        string[] declarationSnippets = {
               "public class ^\n{\n}", "private class ^\n{\n}", "public class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "public struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }",
               "#region ^\n\n#endregion\n"
               };
        string CurOpenFile = null; // 当前打开文件
        public CSharpScriptEditor()
        {
            InitializeComponent();
            saveFileDialog1.Title = "请选择保存的位置";
            saveFileDialog1.Filter = "C# 文件 (*.cs)|*.cs|所有文件 (*.*)|*.*";
            openFileDialog1.Filter = "C# 文件 (*.cs)|*.cs|所有文件 (*.*)|*.*";

            popupMenu = new AutocompleteMenu(fctb);
            popupMenu.Items.ImageList = imageList1;
            popupMenu.ForeColor = Color.White;
            popupMenu.BackColor = Color.Gray;
            popupMenu.SelectedColor = Color.Purple;
            popupMenu.SearchPattern = @"[\w\.]";
            popupMenu.AllowTabKey = true;
            popupMenu.AlwaysShowTooltip = true;

            BuildAutocompleteMenu();
        }
        private void BuildAutocompleteMenu()
        {
            // 静态项（立即创建）
            var staticItems = new List<AutocompleteItem>();

            foreach (var item in snippets)
                staticItems.Add(new SnippetAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in methods)
                staticItems.Add(new MethodAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                staticItems.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in keywords)
                staticItems.Add(new AutocompleteItem(item));

            staticItems.Add(new InsertSpaceSnippet());
            staticItems.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            staticItems.Add(new InsertEnterSnippet());

            // 动态项（延迟枚举）
            var dynamicCollection = new DynamicCollection(popupMenu);

            // 创建一个组合的 IEnumerable
            var combined = staticItems.Concat(dynamicCollection);

            // 传一个延迟枚举，确保 DynamicCollection 在需要时才执行
            popupMenu.Items.SetAutocompleteItems(combined);
        }

        /// <summary>
        /// 点击运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            string code = fctb.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("脚本不能为空！");
                return;
            }

            var originalOut = Console.Out;
            var listBoxWriter = new ListBoxWriter(listBox1);
            Console.SetOut(listBoxWriter);
            listBox1.Items.Clear();
            Console.WriteLine("正在执行脚本...");
            Console.WriteLine("");

            try
            {
                // 👇 使用单例引擎执行
                var result = await CSharpScriptEngine.Instance.ExecuteAsync(code);

                if (result.Success)
                {
                    sw.Stop();
                    Console.WriteLine("");
                    Console.WriteLine($"脚本执行完成，耗时: {sw.ElapsedMilliseconds} ms。");
                    if (result.ReturnValue != null)
                    {
                        Console.WriteLine($"返回值: {result.ReturnValue}");
                    }
                }
                else if (result.CompilationErrors != null)
                {
                    foreach (var diag in result.CompilationErrors)
                    {
                        if (diag.Severity == DiagnosticSeverity.Error)
                        {
                            Console.WriteLine($"[编译错误] {diag}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"[运行时错误] {result.Exception.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[异常] {ex.Message}");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
        /// <summary>
        /// 运行C#脚本代码
        /// </summary>
        public async void RunCode()
        {
            try
            {
                await CSharpScriptEngine.Instance.ExecuteAsync(fctb.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 重置上下文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonClean_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            CSharpScriptEngine.Instance.Reset(); // 重置上下文
            listBox1.Items.Add("[系统] 脚本上下文已重置。\r\n");
        }

        /// <summary>
        /// 另存脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (CurOpenFile.IsNullOrEmpty())
            {
                MessageBoxTD.Show("请先保存再另存为!");
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, fctb.Text);
                MessageBoxTD.Show($"另存成功！路径：{saveFileDialog1.FileName}");
            }
        }
        /// <summary>
        /// 打开脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CurOpenFile = openFileDialog1.FileName;
                string txt = File.ReadAllText(openFileDialog1.FileName);
                fctb.Text = txt;
            }
        }
        /// <summary>
        /// 保存脚本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSave_Click_1(object sender, EventArgs e)
        {
            if(CurOpenFile.IsNullOrEmpty())
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, fctb.Text);
                    CurOpenFile = saveFileDialog1.FileName;
                    MessageBoxTD.Show($"保存成功！路径：{saveFileDialog1.FileName}");
                }
                return;
            }
            File.WriteAllText(CurOpenFile, fctb.Text);
        }

        public string GetFileName() { return CurOpenFile; }
        public string GetCodeText() { return fctb.Text; }
        public void SetCodeText(string code) { fctb.Text = code; }

        private void 相机操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.相机操作;
        }

        private void modbus操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.Modbus操作;
        }

        private void plc操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.PLC操作;
        }

        private void 流程操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.流程操作;
        }

        private void 实测值修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.实测值修改;
        }

        private void openCV操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.OpenCV操作;
        }

        private void 消息弹窗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.消息弹窗;
        }

        private void 控制台输出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.控制台输出;
        }

        private void task启动任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.Task启动任务;
        }

        private void 执行延迟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fctb.Text += QuickCode.执行睡眠;
        }
    }
}
