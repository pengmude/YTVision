using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TDJS_Vision.Node._3_Detection.TDAI.Yolo8;

namespace TDJS_Vision.Node._3_Detection.TDAI.Parse
{
    public class Connector
    {
        /// <summary>
        /// 超日连接器检正反面模型结果解析
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
            var rects = new List<ColorRotatedRect>();
            var texts = new List<ColorText>();


            string itemName = "连接器NG";

            if (results.Any(r => r.ClassId == 0) && results.Any(r => r.ClassId == 1))
                itemName = "连接器正面";
            if (results.Any(r => r.ClassId == 0) && results.Any(r => r.ClassId == 2))
                itemName = "连接器反面";

            var singleResult = new SingleDetectResult
            {
                Name = itemName,
                Value = "1",
                IsOk = itemName == "连接器NG" ? false : true
            };
            detectResults[itemName] = new List<SingleDetectResult> { singleResult };

            // 设置当前检测项的值到结果中
            ParseCommon.SetDetectItemCurValue(ref param, itemName, "1");

            // 添加矩形框
            foreach (var aiRes in results)
            {
                var rect = new ColorRotatedRect(
                    (int)aiRes.Box.X,
                    (int)aiRes.Box.Y,
                    (int)aiRes.Box.Width,
                    (int)aiRes.Box.Height
                );
                rect.AddFlag(true);
                rects.Add(rect);
            }

            // 添加文本
            texts.Add(new ColorText($"{itemName}", Color.Green));

            // 结果赋值
            nodeResult.AlgorithmResult.DetectResults = detectResults;
            nodeResult.AlgorithmResult.Rects = rects;
            nodeResult.AlgorithmResult.Texts = texts;
        }

    }
}
