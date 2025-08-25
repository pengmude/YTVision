using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    /// <summary>
    /// 超日胶壳模型结果解析类
    /// </summary>
    public class GelCoat
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
        /// 解析胶壳检测结果
        /// </summary>
        /// <param name="results"></param>
        /// <param name="resultCount"></param>
        /// <param name="nodeResult"></param>
        public static void Parse(List<ObbResult> results, int resultCount, NodeParamTDAI param, ref NodeResultTDAI nodeResult)
        {
            // 如果检测项配置为空或没有启用的检测项，则直接返回，不进行后续处理
            if (param?.AIInputInfo?.DetectItems == null || !param.AIInputInfo.DetectItems.Any())
                return;

            // 用来保存检测结果的检测项OK/NG、带颜色的框、带颜色的文本
            var detectResults = new Dictionary<string, List<SingleDetectResult>>();
            var texts = new List<ColorText>();

            foreach (var item in param.AIInputInfo.DetectItems)
            {
                // 检测项没启用就不用处理
                if (!item.Enable) continue;

                string itemName = item.Name;
                bool hasData = false;

                #region 提取函数定义，某个检测项的值取自哪个class_id结果的什么属性？

                Func<ObbResult, string> extractValueFunc = null;

                switch (itemName)
                {
                    case "胶壳数量":
                        extractValueFunc = r => r.ClassId == 0 ? "1" : null;
                        break;
                    case "胶壳角度":
                        extractValueFunc = r => r.ClassId == 0 ? r.RotateBox.angle.ToString("F2") : null;
                        break;
                    case "弹片数量":
                        extractValueFunc = r => r.ClassId == 1 ? "1" : null;
                        break;
                    case "弹片长度":
                        extractValueFunc = r => r.ClassId == 1 ? r.RotateBox.size.height.ToString("F2") : null;
                        break;
                    case "针尖数量":
                        extractValueFunc = r => r.ClassId == 2 ? "1" : null;
                        break;
                    case "针尖长度":
                        extractValueFunc = r => r.ClassId == 2 ? r.RotateBox.size.height.ToString("F2") : null;
                        break;
                    case "针尖角度":
                        extractValueFunc = r => r.ClassId == 2 ? r.RotateBox.angle.ToString("F2") : null;
                        break;
                    case "外观不良":
                        extractValueFunc = r => r.ClassId == 3 ? "1" : null;
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

                #region 正常情况: 检测项已启用，模型有检出该项的 ObbResult 结果

                // 获取当前检测项是否为“数量型”
                bool isCountItem = item.IsCountItem;
                HashSet<string> addedRectKeys = new HashSet<string>(); // 非数量型仍需去重


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
                        isOk = false;

                        // 外观不良数量为0时标记为OK
                        if (itemName == "外观不良")
                        {
                            isOk = true;
                        }
                    }

                    #endregion

                    #region 添加检测项结果

                    var singleResult = new SingleDetectResult
                    {
                        Name = itemName,
                        Value = valueStr,
                        IsOk = param.IsAutoStudy ? true : isOk
                    };
                    singleResults.Add(singleResult);

                    #endregion

                    #region 添加矩形框

                    if (count > 0)
                    {
                        foreach (var aiRes in matchedResults)
                        {
                            ParseCommon.AddRect(addedRectKeys, aiRes.RotateBox, param.IsAutoStudy ? true : isOk, ref nodeResult);
                        }
                    }

                    #endregion

                    #region 添加文本信息

                    texts.Add(new ColorText(
                        $"{itemName}: {((param.IsAutoStudy ? true : isOk) ? "OK" : "NG")}",
                        (param.IsAutoStudy ? true : isOk) ? Color.Green : Color.Red
                    ));

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

                        ParseCommon.AddRect(addedRectKeys, result.RotateBox, param.IsAutoStudy ? true : isOk, ref nodeResult);

                        hasData = true;
                    }

                    // 添加 Text（仅首次出现该检测项时添加，避免重复）
                    if (!texts.Any(t => t.Text.StartsWith($"{itemName}:")))
                    {
                        texts.Add(new ColorText(
                            $"{itemName}: {((param.IsAutoStudy ? true : allOk) ? "OK" : "NG")}",
                            (param.IsAutoStudy ? true : allOk) ? Color.Green : Color.Red
                        ));
                    }

                    // 设置当前检测项的值到结果中
                    ParseCommon.SetDetectItemCurValue(ref param, itemName, valueStr);
                }

                #endregion

                #region 特殊情况处理：检测项已启用，但模型没有检出一个该项的 ObbResult 结果

                // 对于数量型检测项，在正常流程中已处理了这种情况（valueStr = "-1", isOk = false）
                // 所以这里只需处理非数量型检测项中的特殊案例

                if (!matchedResults.Any() && item.Enable && !isCountItem)
                {
                    string defaultValue = null;
                    bool isSpecialCaseOk = false;

                    // 对于“存在”即NG的检测项要这样设置
                    //if (itemName == "剥口偏移")
                    //{
                    //    defaultValue = "0";
                    //    isSpecialCaseOk = true;
                    //}

                    // 对于“不存在”即NG的检测项要这样设置
                    if (defaultValue != null)
                    {
                        var singleResult = new SingleDetectResult
                        {
                            Name = itemName,
                            Value = defaultValue,
                            IsOk = isSpecialCaseOk
                        };
                        singleResults.Add(singleResult);

                        // 添加全局提示文本（例如“未检测到不良焊锡”）
                        texts.Add(new ColorText(
                            $"{itemName}: {(isSpecialCaseOk ? "OK" : "NG")}",
                            isSpecialCaseOk ? Color.Green : Color.Red
                        ));

                        hasData = true;
                    }
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
