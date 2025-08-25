using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    /// <summary>
    /// Type-C检测结果解析类
    /// </summary>
    public class TypeCResultParse
    {
        /// <summary>
        /// 用于记录检测结果数量，自动学习模式下使用
        /// </summary>
        static int _count = 0;

        /// <summary>
        /// 用于自动学习模式下存储检测项数据
        /// </summary>
        private static Dictionary<string, List<float>> _autoStudyDatas = new Dictionary<string, List<float>>();

        /// <summary>
        /// 解析Type-C检测结果
        /// </summary>
        public static void Parse(List<DetResult> results, int resultCount, NodeParamTDAI param, ref NodeResultTDAI nodeResult)
        {
            // 如果检测项配置为空或没有启用的检测项，则直接返回，不进行后续处理
            if (param?.AIInputInfo?.DetectItems == null || !param.AIInputInfo.DetectItems.Any())
                return;

            // 用来保存检测结果的检测项OK/NG、带颜色的框、带颜色的文本
            var detectResults = new Dictionary<string, List<SingleDetectResult>>();
            var texts = new List<ColorText>();
            HashSet<string> addedRectKeys = new HashSet<string>(); // 非数量型仍需去重


            foreach (var item in param.AIInputInfo.DetectItems)
            {
                // 检测项没启用就不用处理
                if (!item.Enable) continue;

                string itemName = item.Name;
                bool hasData = false;

                #region 提取函数定义，某个检测项的值取自哪个class_id结果的什么属性？

                Func<DetResult, string> extractValueFunc = null;

                switch (itemName)
                {
                    case "Type-C数量":
                        extractValueFunc = r => r.ClassId == 0 ? "1" : null;
                        break;
                    case "正常焊点数":
                        extractValueFunc = r => r.ClassId == 1 ? "1" : null;
                        break;
                    case "正常焊点面积":
                        extractValueFunc = r => r.ClassId == 1 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "虚焊数量":
                        extractValueFunc = r => r.ClassId == 2 ? "1" : null;
                        break;
                    case "虚焊面积":
                        extractValueFunc = r => r.ClassId == 2 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "露线芯长度":
                        extractValueFunc = r => r.ClassId == 3 ? r.Box.Height.ToString("F2") : null;
                        break;
                    case "黑色线数量":
                        extractValueFunc = r => r.ClassId == 4 ? "1" : null;
                        break;
                    case "红色线数量":
                        extractValueFunc = r => r.ClassId == 5 ? "1" : null;
                        break;
                    case "露焊盘数":
                        extractValueFunc = r => r.ClassId == 6 ? "1" : null;
                        break;
                    case "露焊盘面积":
                        extractValueFunc = r => r.ClassId == 6 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "锡珠数量":
                        extractValueFunc = r => r.ClassId == 7 ? "1" : null;
                        break;
                    case "锡珠面积":
                        extractValueFunc = r => r.ClassId == 7 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "胶皮压伤数量":
                        extractValueFunc = r => r.ClassId == 8 ? "1" : null;
                        break;
                    case "胶皮压伤面积":
                        extractValueFunc = r => r.ClassId == 8 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "熔胶数量":
                        extractValueFunc = r => r.ClassId == 9 ? "1" : null;
                        break;
                    case "熔胶面积":
                        extractValueFunc = r => r.ClassId == 9 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "其他不良数量":
                        extractValueFunc = r => r.ClassId == 10 ? "1" : null;
                        break;
                    case "其他不良面积":
                        extractValueFunc = r => r.ClassId == 10 ? (r.Box.Width * r.Box.Height).ToString("F2") : null;
                        break;
                    case "放反料数量":
                        extractValueFunc = r => r.ClassId == 11 ? "1" : null;
                        break;
                    default:
                        extractValueFunc = null;
                        break;
                }

                if (extractValueFunc == null)
                    continue;

                #endregion

                #region 匹配结果提取

                var matchedResults = results.Where(r =>
                {
                    string value = extractValueFunc(r);
                    return value != null;
                }).ToList();

                #endregion

                var singleResults = new List<SingleDetectResult>();

                #region 正常情况: 检测项已启用，模型有检出该项的DetResult结果

                // 获取当前检测项是否为“数量型”
                bool isCountItem = item.IsCountItem;

                if (isCountItem)    
                {
                    // 数量型检测项：统计总数，只添加一次结果
                    int count = matchedResults.Count;
                    string valueStr = count.ToString();
                    bool isOk = false;

                    // 添加自动学习数据
                    if (param.IsAutoStudy)
                    {
                        if (!_autoStudyDatas.ContainsKey(itemName))
                            _autoStudyDatas.Add(itemName, new List<float>());
                        _autoStudyDatas[itemName].Add(count);

                    }
                    // 数量型检测项的NG类型列表
                    List<string> ngCountItems = new List<string>()
                    {
                        "虚焊数量",
                        "露焊盘数",
                        "锡珠数量",
                        "胶皮压伤数量",
                        "熔胶数量",
                        "其他不良数量",
                        "放反料数量"
                    };

                    #region 判断检测项是否OK

                    if (count > 0)
                    {
                        // 有结果时才判断上下限
                        bool minOk = float.TryParse(item.MinValue, out float min) && count >= min;
                        bool maxOk = float.TryParse(item.MaxValue, out float max) && count <= max;
                        isOk = minOk && maxOk;
                    }
                    else
                    {
                        // 没有结果时，默认值 0，直接标记为 NG
                        valueStr = "0";
                        isOk = ngCountItems.Contains(itemName) ? true : false; // NG型检测项没有结果时标记为OK
                    }

                    #endregion

                    #region 添加检测项结果

                    var singleResult = new SingleDetectResult
                    {
                        Name = itemName,
                        Value = valueStr,
                        IsOk = param.IsAutoStudy ? true : isOk // 如果是自动学习模式，则结果默认标记为OK
                    };
                    singleResults.Add(singleResult);

                    #endregion

                    #region 添加矩形框

                    if (count > 0)
                    {
                        foreach (var aiRes in matchedResults)
                        {
                            // 添加不重复矩形框
                            ParseCommon.AddRect(addedRectKeys, aiRes.Box, param.IsAutoStudy ? true : isOk, ref nodeResult);

                        }
                    }

                    #endregion

                    #region 添加文本信息

                    var isEnable = ParseCommon.GetItemEnableStatus(param, itemName);
                    if (isEnable && !texts.Any(t => t.Text.StartsWith($"{itemName}:")))
                    {
                        texts.Add(new ColorText(
                            $"{itemName}: {((param.IsAutoStudy ? true : isOk) ? "OK" : "NG")}",
                            (param.IsAutoStudy ? true : isOk) ? Color.Green : Color.Red // 如果是自动学习模式，则文本默认标记为绿色OK
                        ));
                    }

                    #endregion

                    // 设置当前检测项的值到结果中
                    ParseCommon.SetDetectItemCurValue(ref param, itemName, valueStr);

                    hasData = true;
                }
                else
                {
                    bool allOk = true;
                    var valueStr = "";

                    foreach (var result in matchedResults)
                    {
                        string rawValue = extractValueFunc(result);
                        if (!float.TryParse(rawValue, out float value))
                            continue;

                        // 是否需要转成物理值
                        if (param.NeedConvert)
                        {
                            value = value * param.Scale;
                            rawValue = value.ToString("F2"); // 保留两位小数
                        }

                        bool minOk = float.TryParse(item.MinValue, out float min) && value >= min;
                        bool maxOk = float.TryParse(item.MaxValue, out float max) && value <= max;
                        bool isOk = minOk && maxOk;

                        // 一旦有一个 false，则整体不是 OK
                        if (!isOk)
                            allOk = false;

                        var singleResult = new SingleDetectResult
                        {
                            Name = itemName,
                            Value = rawValue,
                            IsOk = param.IsAutoStudy ? true : isOk // 如果是自动学习模式，则结果默认标记为OK
                        };
                        valueStr += (rawValue + ","); // 拼接所有值
                        singleResults.Add(singleResult);

                        // 添加自动学习数据
                        if (param.IsAutoStudy)
                        {
                            if (!_autoStudyDatas.ContainsKey(itemName))
                                _autoStudyDatas.Add(itemName, new List<float>());
                            _autoStudyDatas[itemName].Add(value);
                        }

                        // 添加不重复矩形框
                        ParseCommon.AddRect(addedRectKeys, result.Box, param.IsAutoStudy ? true : isOk, ref nodeResult);

                        hasData = true;
                    }

                    // 遍历完成后统一添加文本信息
                    var isEnable = ParseCommon.GetItemEnableStatus(param, itemName);
                    if (isEnable && !texts.Any(t => t.Text.StartsWith($"{itemName}:")))
                    {
                        texts.Add(new ColorText(
                            $"{itemName}: {((param.IsAutoStudy ? true : allOk) ? "OK" : "NG")}",
                            (param.IsAutoStudy ? true : allOk) ? Color.Green : Color.Red // 如果是自动学习模式，则文本默认标记为绿色OK
                        ));
                    }

                    // 设置当前检测项的值到结果中
                    ParseCommon.SetDetectItemCurValue(ref param, itemName, valueStr.TrimEnd(','));
                }

                #endregion


                #region 特殊情况处理：检测项已启用，但模型没有检出一个该项的DetResult结果

                // 对于数量型检测项，在正常流程中已处理了这种情况（valueStr = "-1", isOk = false）
                // 所以这里只需处理非数量型检测项中的特殊案例："不良焊锡面积"

                if (!matchedResults.Any() && item.Enable && !isCountItem)
                {
                    string defaultValue = null;
                    bool isSpecialCaseOk = false;

                    #region 对于“存在”即NG的非数量型检测项要这样设置
                    if (itemName == "虚焊面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }
                    if (itemName == "露焊盘面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }
                    if (itemName == "锡珠面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }
                    if (itemName == "胶皮压伤面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }
                    if (itemName == "熔胶面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }
                    if (itemName == "其他不良面积")
                    {
                        defaultValue = "0";
                        isSpecialCaseOk = true;
                    }

                    #endregion

                    #region 对于“不存在”即NG的非数量型检测项要这样设置

                    if (defaultValue != null)
                    {
                        var singleResult = new SingleDetectResult
                        {
                            Name = itemName,
                            Value = defaultValue,
                            IsOk = isSpecialCaseOk
                        };
                        singleResults.Add(singleResult);

                        // 添加文本
                        var isEnable = ParseCommon.GetItemEnableStatus(param, itemName);
                        if (isEnable && !texts.Any(t => t.Text.StartsWith($"{itemName}:")))
                        {
                            texts.Add(new ColorText(
                                $"{itemName}: {(isSpecialCaseOk ? "OK" : "NG")}",
                                isSpecialCaseOk ? Color.Green : Color.Red
                            ));
                        }

                        hasData = true;
                    }

                    #endregion
                }

                #endregion

                // 添加一个检测项的结果数据
                if (hasData)
                {
                    detectResults[itemName] = singleResults;
                }
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
                        ParseCommon.SetDetectItemMinMax(param.AIInputInfo.DetectItems, _autoStudyDatas, param.StudyPercentage);
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

    }
}
