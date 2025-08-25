using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Logger;
using OpenCvSharp;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    public class LineCoreCrossSection
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
        /// 解析线芯截面模型结果
        /// </summary>
        /// <param name="results"></param>
        /// <param name="resultCount"></param>
        /// <param name="nodeResult"></param>
        public static void Parse(List<SegResult> results, int resultCount, NodeParamTDAI param, ref NodeResultTDAI nodeResult)
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

                Func<SegResult, string> extractValueFunc = null;

                switch (itemName)
                {
                    case "线芯数量":
                        extractValueFunc = r => r.ClassId == 0 ? "1" : null;
                        break;
                    case "线芯聚拢度":
                        extractValueFunc = r => r.ClassId == 0 ? $"{r.Box.X}-{r.Box.Y}-{r.Box.Width}-{r.Box.Height}" : null;
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
                    bool allOk = false;
                    var valueStr = "";

                    float.TryParse(item.MaxValue, out float max);
                    var (rectOKs, rectNGs) = SeparateOutlierRects(matchedResults, max, param, ref valueStr, singleResults, itemName);
                    // 一旦有一个 false，则整体不是 OK
                    if (rectNGs.Count == 0)
                        allOk = true;

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
                            $"{itemName}: {((param.IsAutoStudy ? true : allOk) ? "OK" : "NG")}",
                            (param.IsAutoStudy ? true : allOk) ? Color.Green : Color.Red
                        ));
                    }

                    // 设置当前检测项的值到结果中
                    ParseCommon.SetDetectItemCurValue(ref param, itemName, valueStr.TrimEnd(','));

                    if(singleResults.Count > 0)
                        hasData = true;
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

        /// <summary>
        /// 分离离群矩形框
        /// </summary>
        /// <param name="results">检测结果列表</param>
        /// <param name="distance">欧几里得距离</param>
        /// <returns>(非离群Rect列表, 离群Rect列表)</returns>
        public static (List<Rect>, List<Rect>) SeparateOutlierRects(
            List<SegResult> results, 
            float distance, 
            NodeParamTDAI param, 
            ref string resStr, 
            List<SingleDetectResult> singleDetectResults, 
            string itemName)
        {
            var inliers = new List<Rect>();
            var outliers = new List<Rect>();

            try
            {
                if (results == null || results.Count == 0)
                    return (new List<Rect>(), new List<Rect>());

                if (results.Count == 1)
                    return (new List<Rect>(), new List<Rect> { results[0].Box });

                // 1. 提取每个 Rect 的中心点
                var centers = results.Select(r =>
                {
                    var box = r.Box;
                    float cx = box.X + box.Width / 2.0f;
                    float cy = box.Y + box.Height / 2.0f;
                    return new PointF(cx, cy);
                }).ToList();

                // 2. 计算所有中心点的质心
                float centerX = centers.Average(p => p.X);
                float centerY = centers.Average(p => p.Y);

                // 3. 计算每个中心点到质心的欧几里得距离
                var distances = centers.Select(p =>
                {
                    double dx = p.X - centerX;
                    double dy = p.Y - centerY;
                    return Math.Sqrt(dx * dx + dy * dy);
                }).ToList();

                // 4. 分离非离群和离群的 Rect
                for (int i = 0; i < results.Count; i++)
                {
                    bool isOk = false;
                    var rawValue = distances[i].ToString("F2");
                    // 是否需要转成物理值
                    if (param.NeedConvert)
                    {
                        distances[i] = distances[i] * param.Scale;
                        rawValue = distances[i].ToString("F2"); // 保留两位小数
                    }

                    // 添加自动学习数据
                    if (param.IsAutoStudy)
                    {
                        if (!_autoStudyDatas.ContainsKey(itemName))
                            _autoStudyDatas.Add(itemName, new List<float>());
                        _autoStudyDatas[itemName].Add(float.Parse(rawValue));
                    }

                    if (distances[i] <= distance)
                    {
                        inliers.Add(results[i].Box);
                        isOk = true; // 在距离范围内，标记为OK
                    }
                    else
                        outliers.Add(results[i].Box);
                    // 添加单个检测结果
                    var singleResult = new SingleDetectResult
                    {
                        Name = itemName,
                        Value = rawValue,
                        IsOk = param.IsAutoStudy ? true : isOk // 如果是自动学习模式，则结果默认标记为OK
                    };
                    resStr += (rawValue + ",");  // 拼接所有值
                    singleDetectResults.Add(singleResult);

                }
            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, e.Message, true);
            }

            return (inliers, outliers);
        }
    }
}
