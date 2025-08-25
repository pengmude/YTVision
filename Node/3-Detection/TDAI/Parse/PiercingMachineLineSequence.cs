using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Logger;
using OpenCvSharp;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    /// <summary>
    /// 刺破机颜色线序模型结果解析类
    /// </summary>
    public class PiercingMachineLineSequence
    {
        /// <summary>
        /// 用于记录检测结果数量，自动学习模式下使用
        /// </summary>
        static int _count = 0;

        /// <summary>
        /// 用于自动学习模式下存储检测项数据
        /// </summary>
        private static Dictionary<string, List<string>> _autoStudyDatas = new Dictionary<string, List<string>>();

        /// <summary>
        /// 刺破机颜色线序模型结果解析
        /// </summary>
        /// <param name="results"></param>
        /// <param name="resultCount"></param>
        /// <param name="nodeResult"></param>
        public static void Parse(List<DetResult> results, int resultCount, NodeParamTDAI param, ref NodeResultTDAI nodeResult)
        {
            // 如果检测项配置为空或没有启用的检测项，则直接返回，不进行后续处理
            if (param?.AIInputInfo?.DetectItems == null || !param.AIInputInfo.DetectItems.Any())
                return;

            // 用来保存检测结果的检测项OK/NG、带颜色的框、带颜色的文本
            var detectResults = new Dictionary<string, List<SingleDetectResult>>();
            var texts = new List<ColorText>();
            HashSet<string> addedRectKeys = new HashSet<string>(); // 非数量型仍需去重
            // 弹片间距结果
            List<DetResult> shrapnelRes = new List<DetResult>();

            foreach (var item in param.AIInputInfo.DetectItems)
            {
                string itemName = item.Name;
                // 检测项没启用就不用处理
                if (!item.Enable) continue;

                var singleResults = new List<SingleDetectResult>();

                // item.MaxValue/item.MinValue都是提前设置好的线序
                bool allOk = false;
                string valueStr = string.Empty; // 用于存储检测项的值
                var (rectOKs, rectNGs) = GetColorSequenceMatchedRects(results, param, item.MaxValue, ref valueStr, singleResults, itemName);
                
                // 是否 OK
                if (rectNGs.Count == 0)
                    allOk = true;

                // 添加带颜色的矩形框
                if (param.IsAutoStudy)
                {
                    // 自动学习模式下，所有结果框都标记为 OK
                    foreach (var rect in rectOKs)
                    {
                        // 添加OK矩形框
                        ParseCommon.AddRect(addedRectKeys, rect, true, ref nodeResult);
                    }

                    foreach (var rect in rectNGs)
                    {
                        // 添加NG矩形框
                        ParseCommon.AddRect(addedRectKeys, rect, true, ref nodeResult);
                    }
                }
                else
                {
                    // 非自动学习模式下，所有结果框根据实际标记
                    foreach (var rect in rectOKs)
                    {
                        // 添加OK矩形框
                        ParseCommon.AddRect(addedRectKeys, rect, true, ref nodeResult);
                    }

                    foreach (var rect in rectNGs)
                    {
                        // 添加NG矩形框
                        ParseCommon.AddRect(addedRectKeys, rect, false, ref nodeResult);
                    }
                }


                // 遍历完成后统一添加文本信息
                var isEnable = ParseCommon.GetItemEnableStatus(param, itemName);
                if (isEnable && !texts.Any(t => t.Text.StartsWith($"{itemName}:")))
                {
                    texts.Add(new ColorText(
                        $"【设定线序】: {item.MaxValue}",
                        Color.Green
                    ));
                    texts.Add(new ColorText(
                        $"【当前线序】: {valueStr.TrimEnd('-')}",
                        (param.IsAutoStudy ? true : allOk) ? Color.Green : Color.Red
                    ));
                }

                // 设置当前检测项的值到结果中
                ParseCommon.SetDetectItemCurValue(ref param, itemName, valueStr.TrimEnd('-'));

                // 添加自动学习数据
                if (param.IsAutoStudy)
                {
                    if (!_autoStudyDatas.ContainsKey(itemName))
                        _autoStudyDatas.Add(itemName, new List<string>());
                    _autoStudyDatas[itemName].Add(valueStr.TrimEnd('-'));
                }

                detectResults[itemName] = singleResults;
            }

            // 更新 AlgorithmResult
            nodeResult.AlgorithmResult.DetectResults = detectResults;
            //nodeResult.AlgorithmResult.Rects = rects;
            nodeResult.AlgorithmResult.Texts = texts;

            // 自动学习模式下
            if (param.IsAutoStudy)
            {
                // _count 自增
                _count++;
                // 自动学习上下限完成
                if (param.StudyNum <= _count)
                {
                    try
                    {
                        // 设置检测项的上下限
                        ParseCommon.SetDetectItemMinMax(param.AIInputInfo.DetectItems, _autoStudyDatas);
                        ParseCommon.CloseAutoStudyEvent?.Invoke(null, param.NodeName);
                        _count = 0; // 重置计数器
                    }
                    catch (Exception)
                    {
                        _count = 0;
                        throw;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// 中文到英文映射（用于匹配输入字符串）
        /// </summary>
        static Dictionary<string, string> chineseToEnglish = new Dictionary<string, string>
        {
            { "红", "red" },
            { "橙", "orange" },
            { "黄", "yellow" },
            { "绿", "green" },
            { "青", "cyan" },
            { "蓝", "blue" },
            { "紫", "purple" },
            { "灰", "gray" },
            { "粉", "pink" },
            { "黑", "black" },
            { "白", "white" },
            { "棕", "brown" },
            { "其他", "other" }
        };
        /// <summary>
        /// 英文到中文的映射（用于匹配输入字符串）
        /// </summary>
        static Dictionary<string, string> englishToChinese = new Dictionary<string, string>
        {
            { "red", "红" },
            { "orange", "橙" },
            { "yellow", "黄" },
            { "green", "绿" },
            { "cyan", "青" },
            { "blue", "蓝" },
            { "purple", "紫" },
            { "gray", "灰" },
            { "pink", "粉" },
            { "black", "黑" },
            { "white", "白" },
            { "brown", "棕" },
            { "other", "其他" }
        };
        /// <summary>
        /// 判断检测结果是否符合颜色序列要求
        /// </summary>
        /// <param name="results"></param>
        /// <param name="colorSequence"></param>
        /// <returns></returns>
        public static (List<Rect>, List<Rect>) GetColorSequenceMatchedRects(
            List<DetResult> results, 
            NodeParamTDAI param, 
            string colorSequence, 
            ref string valueStr, 
            List<SingleDetectResult> singleDetectResults, 
            string itemName)
        {
            var matched = new List<Rect>();
            var mismatched = new List<Rect>();

            try
            {
                var classNames = param.AIInputInfo.ClassNames;

                if (param.IsAutoStudy)
                {
                    #region 一键学习时

                    // 1. 按 X 坐标从左到右排序
                    var sortedResults = results.OrderBy(r => r.Box.X).ToList();

                    // 2. 将每个检测结果的 ClassId 转为实际颜色名
                    var actualColors = sortedResults.Select(r =>
                    {
                        if (r.ClassId < 0 || r.ClassId >= classNames.Count)
                            return "other";
                        return classNames[r.ClassId];
                    }).ToList();

                    // 2.遍历每一根线颜色
                    for (int i = 0; i < sortedResults.Count; i++)
                    {
                        // 添加单个检测结果
                        var singleResult = new SingleDetectResult
                        {
                            Name = itemName,
                            Value = englishToChinese[actualColors[i]],
                            IsOk = true // 自动学习模式默认标记为OK
                        };
                        valueStr += (englishToChinese[actualColors[i]] + "-");  // 拼接所有值
                        singleDetectResults.Add(singleResult);
                    }
                    matched.AddRange(results.Select(r => r.Box));

                    #endregion
                }
                else
                {
                    #region 非一键学习时

                    // 解析 colorSequence 获取期望的颜色数量
                    var expectedColors = colorSequence.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(c => c.Trim())
                                                      .ToList();

                    // 1. 按 X 坐标从左到右排序
                    var sortedResults = results.OrderBy(r => r.Box.X).ToList();

                    // 2. 将每个检测结果的 ClassId 转为实际颜色名
                    var actualColors = sortedResults.Select(r =>
                    {
                        if (r.ClassId < 0 || r.ClassId >= classNames.Count)
                            return "other";
                        return classNames[r.ClassId];
                    }).ToList();

                    // 3. 将中文颜色序列转为英文（使用类内定义的 chineseToEnglish 字典）
                    var expectedEnglishColors = new List<string>();
                    foreach (var ch in expectedColors)
                    {
                        if (chineseToEnglish.TryGetValue(ch, out string eng))
                            expectedEnglishColors.Add(eng);
                        else
                            expectedEnglishColors.Add("other"); // 无法识别视为 "other"
                    }

                    // 4. 数量相等后，逐个对比颜色
                    for (int i = 0; i < sortedResults.Count; i++)
                    {
                        bool isOk = false;
                        if (i < actualColors.Count && i < expectedEnglishColors.Count &&
                            actualColors[i] == expectedEnglishColors[i])
                        {
                            matched.Add(sortedResults[i].Box);
                            isOk = true;
                        }
                        else
                        {
                            mismatched.Add(sortedResults[i].Box);
                        }

                        // 添加单个检测结果
                        var singleResult = new SingleDetectResult
                        {
                            Name = itemName,
                            Value = actualColors[i],
                            IsOk = param.IsAutoStudy ? true : isOk // 如果是自动学习模式，则结果默认标记为OK
                        };
                        valueStr += (englishToChinese[actualColors[i]] + "-");  // 拼接所有值
                        singleDetectResults.Add(singleResult);
                    }

                    #endregion
                }
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Fatal, e.Message, true);
                // 出错时，将所有检出框视为 NG
                if (results != null)
                {
                    mismatched.AddRange(results.Select(r => r.Box));
                }
            }

            return (matched, mismatched);
        }
    }
}
