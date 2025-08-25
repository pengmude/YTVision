using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TDJS_Vision.Forms.MyCSharpScript
{
    /// <summary>
    /// C# 脚本引擎（单例模式）
    /// 负责预热、编译、执行脚本，保持上下文
    /// </summary>
    public sealed class CSharpScriptEngine
    {
        // 🔒 单例实例（延迟加载 + 线程安全）
        private static readonly Lazy<CSharpScriptEngine> _instance =
            new Lazy<CSharpScriptEngine>(() => new CSharpScriptEngine(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static CSharpScriptEngine Instance => _instance.Value;

        // 📦 缓存的 ScriptOptions
        private readonly ScriptOptions _scriptOptions;

        // 🔄 当前脚本状态（可选：如果每个用户有自己的上下文，可改为 ConcurrentDictionary<string, ScriptState<object>>）
        private ScriptState<object> _scriptState;

        // 🔐 私有构造函数
        private CSharpScriptEngine()
        {
            // 初始化 ScriptOptions
            _scriptOptions = CreateScriptOptions();
        }

        /// <summary>
        /// 获取脚本选项
        /// </summary>
        public ScriptOptions GetScriptOptions() => _scriptOptions;

        /// <summary>
        /// 执行脚本（首次或继续）
        /// </summary>
        /// <param name="code">脚本代码</param>
        /// <param name="globals">全局变量（如 Solution.Instance）</param>
        /// <returns>执行结果</returns>
        public async Task<ScriptResult> ExecuteAsync(string code, object globals = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("脚本不能为空");

            try
            {
                ScriptState<object> state;

                if (_scriptState == null)
                {
                    // 首次执行
                    state = await CSharpScript.RunAsync(code, _scriptOptions, globals);
                }
                else
                {
                    // 继续执行（保持上下文）
                    state = await _scriptState.ContinueWithAsync(code, _scriptOptions);
                }

                _scriptState = state; // 更新状态
                return new ScriptResult(success: true, output: null, returnValue: state?.ReturnValue, exception: null);
            }
            catch (CompilationErrorException ex)
            {
                return new ScriptResult(success: false, output: null, returnValue: null, exception: null, compilationErrors: ex.Diagnostics.ToArray());
            }
            catch (Exception ex)
            {
                return new ScriptResult(success: false, output: null, returnValue: null, exception: ex);
            }
        }

        /// <summary>
        /// 重置脚本上下文（清除变量等）
        /// </summary>
        public void Reset()
        {
            _scriptState = null;
        }

        /// <summary>
        /// 预热脚本引擎（提前触发 JIT 和初始化）
        /// </summary>
        public void WarmUp()
        {
            try
            {
                // 执行一个空脚本，触发编译器初始化
                var task = CSharpScript.EvaluateAsync<int>("1", _scriptOptions);
                task.GetAwaiter().GetResult();
                System.Diagnostics.Debug.WriteLine("[CSharpScriptEngine] 预热完成");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CSharpScriptEngine] 预热失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 创建统一的 ScriptOptions（可复用）
        /// </summary>
        private ScriptOptions CreateScriptOptions()
        {
            var references = new[]
            {
                MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Threading.Tasks.Task).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Threading.Thread).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Windows.Forms.Form).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Windows.Forms.MessageBox).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Drawing.Point).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Drawing.Color).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Drawing.Font).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Numerics.Vector2).Assembly.Location)
            };

            return ScriptOptions.Default
                .WithReferences(references)
                .WithImports(
                    "System",
                    "System.IO",
                    "System.Console",
                    "System.Collections.Generic",
                    "System.Text",
                    "System.Linq",
                    "System.Threading",
                    "System.Threading.Tasks",
                    "System.Numerics",
                    "System.Windows.Forms",
                    "System.Drawing",
                    "System.ComponentModel",
                    // 当前程序的命名空间
                    "TDJS_Vision",
                    "TDJS_Vision.Node",
                    "TDJS_Vision.Forms",
                    "TDJS_Vision.Forms.YTMessageBox",
                    "TDJS_Vision.Device",
                    "TDJS_Vision.Device.Camera",
                    "TDJS_Vision.Device.Light",
                    "TDJS_Vision.Device.Modbus",
                    "TDJS_Vision.Device.PLC",
                    "TDJS_Vision.Device.TCP",
                    "TDJS_Vision.Forms.ImageViewer",
                    "Logger"
                )
                .WithLanguageVersion(LanguageVersion.CSharp7);
        }
    }

    /// <summary>
    /// 脚本执行结果
    /// </summary>
    public class ScriptResult
    {
        public bool Success { get; }
        public object ReturnValue { get; }
        public Exception Exception { get; }
        public Microsoft.CodeAnalysis.Diagnostic[] CompilationErrors { get; }
        public string Output { get; } // 可结合 Console 重定向

        public ScriptResult(bool success, string output, object returnValue, Exception exception, Diagnostic[] compilationErrors = null)
        {
            Success = success;
            Output = output;
            ReturnValue = returnValue;
            Exception = exception;
            CompilationErrors = compilationErrors;
        }
    }

}