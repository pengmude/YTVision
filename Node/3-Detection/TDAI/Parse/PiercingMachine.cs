using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    /// <summary>
    /// 刺破机结果解析类
    /// </summary>
    public class PiercingMachine
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
        /// 刺破机模型结果解析
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

                bool hasData = false;

                #region 提取函数定义，某个检测项的值取自哪个class_id结果的什么属性？

                Func<DetResult, string> extractValueFunc = null;

                switch (itemName)
                {
                    case "护套数量":
                        extractValueFunc = r => r.ClassId == 0 ? "1" : null;
                        break;
                    case "弹片数量":
                        extractValueFunc = r => r.ClassId == 1 ? "1" : null;
                        break;
                    case "弹片间距":
                        extractValueFunc = r => r.ClassId == 1 ? "1" : null;
                        break;
                    case "防错筋数量":
                        extractValueFunc = r => r.ClassId == 2 ? "1" : null;
                        break;
                    case "边码数量":
                        extractValueFunc = r => r.ClassId == 3 ? "1" : null;
                        break;
                    case "护套外观":
                        extractValueFunc = r => r.ClassId == 4 ? "1" : null;
                        break;
                    case "弹片外观":
                        extractValueFunc = r => r.ClassId == 5 ? "1" : null;
                        break;
                    case "护套全通口数量":
                        extractValueFunc = r => r.ClassId == 6 ? "1" : null;
                        break;
                    case "护套半通口数量":
                        extractValueFunc = r => r.ClassId == 7 ? "1" : null;
                        break;
                    case "护套不通口数量":
                        extractValueFunc = r => r.ClassId == 8 ? "1" : null;
                        break;
                    case "导线数量":
                        extractValueFunc = r => r.ClassId == 9 ? "1" : null;
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
                    string v = extractValueFunc(r);
                    return v != null;
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
                    List<string> ngCountItems = new List<string>()
                    {
                        "护套外观",
                        "弹片外观"
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
                            (param.IsAutoStudy ? true : isOk) ? Color.Green : Color.Red
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

                    if (itemName == "弹片间距")
                    {
                        shrapnelRes = matchedResults;
                        continue;
                    }
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
                            (param.IsAutoStudy ? true : allOk) ? Color.Green : Color.Red
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
                    // 12类端子无
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

            #region 单独处理“弹片间距”结果


            var resultList = new List<SingleDetectResult>();
            var res = new SingleDetectResult();
            string valueRes = string.Empty;
            var itemInfo = param.AIInputInfo.DetectItems.Find(i => i.Name == "弹片间距");
            float minI = float.Parse(itemInfo.MinValue);
            float maxI = float.Parse(itemInfo.MaxValue);
            res.Name = "弹片间距";
            // 每对弹片距离是否OK
            res.IsOk = ValidateRectPairs(shrapnelRes, minI, maxI, param, 20, addedRectKeys, nodeResult, ref valueRes).TrueForAll(r => r);
            res.Value = valueRes.TrimEnd(',');
            resultList.Add(res);
            detectResults["弹片间距"] = resultList;

            // 遍历完成后统一添加文本信息
            var enable = ParseCommon.GetItemEnableStatus(param, "弹片间距");
            if (enable && !texts.Any(t => t.Text.StartsWith("弹片间距:")))
            {
                texts.Add(new ColorText(
                    $"{"弹片间距"}: {(res.IsOk ? "OK" : "NG")}",
                    res.IsOk ? Color.Green : Color.Red
                ));
            }

            // 设置当前检测项的值到结果中
            ParseCommon.SetDetectItemCurValue(ref param, "弹片间距", res.Value);
            #endregion

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

        /// <summary>
        /// 检查所有上下弹片距离是否小于设定的距离
        /// </summary>
        /// <param name="detResults">弹片结果</param>
        /// <param name="distance">设定的距离</param>
        /// <param name="delta">x坐标在偏差delta范围内判定为上下一对弹片</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private static List<bool> ValidateRectPairs(List<DetResult> detResults, float dMin, float dMax, NodeParamTDAI param, int delta, HashSet<string> addedRectKeys, NodeResultTDAI nodeResult, ref string value)
        {
            if (detResults == null || detResults.Count % 2 != 0)
                throw new ArgumentException("弹片检测结果列表必须包含偶数个元素。");

            int n = detResults.Count;
            bool[] used = new bool[n];  // 标记每个矩形是否已被配对
            int pairCount = 0;
            List<bool> bools = new List<bool>(); // 保存每个弹片距离是否OK结果

            for (int i = 0; i < n; i++)
            {
                if (used[i]) continue;

                var rectA = detResults[i].Box;

                for (int j = i + 1; j < n; j++)
                {
                    if (used[j]) continue;

                    var rectB = detResults[j].Box;

                    // 检查是否属于同一列（X方向差值在delta范围内）
                    if (Math.Abs(rectA.X - rectB.X) <= delta)
                    {
                        // 确定上下关系
                        Rect topRect = rectA.Y < rectB.Y ? rectA : rectB;
                        Rect bottomRect = rectA.Y < rectB.Y ? rectB : rectA;

                        // 计算间距
                        float d = bottomRect.Top - topRect.Bottom;

                        // 是否需要转成物理值
                        if (param.NeedConvert)
                            d = d * param.Scale;

                        // 添加自动学习数据
                        if (param.IsAutoStudy)
                        {
                            if (!_autoStudyDatas.ContainsKey("咬胶占比"))
                                _autoStudyDatas.Add("咬胶占比", new List<float>());
                            _autoStudyDatas["咬胶占比"].Add(d);
                        }

                        value += d.ToString("F2") + ",";

                        if (d > dMax || d < dMin)
                        {
                            // 添加上下弹片矩形框
                            ParseCommon.AddRect(addedRectKeys, rectA, param.IsAutoStudy ? true : false, ref nodeResult);
                            ParseCommon.AddRect(addedRectKeys, rectB, param.IsAutoStudy ? true : false, ref nodeResult);
                            bools.Add(false);
                        }
                        else
                        {
                            // 添加上下弹片矩形框
                            ParseCommon.AddRect(addedRectKeys, rectA, true, ref nodeResult);
                            ParseCommon.AddRect(addedRectKeys, rectB, true, ref nodeResult);
                            bools.Add(true);
                        }

                        // 标记为已使用
                        used[i] = true;
                        used[j] = true;
                        pairCount++;
                        break;  // 找到配对后跳出内层循环
                    }
                }
            }

            // 检查是否成功找到 n / 2 对
            if (pairCount != n / 2)
            {
                throw new InvalidOperationException("无法正确匹配所有弹片上下对，可能数据不符合要求。");
            }

            return bools;
        }

    }
}
