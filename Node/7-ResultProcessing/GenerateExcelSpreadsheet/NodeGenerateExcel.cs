using Logger;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OpenCvSharp.Aruco;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YTVisionPro.Node._3_Detection.HTAI;
using YTVisionPro.Node._7_ResultProcessing.ResultSummarize;
using static Logger.LogHelper;

namespace YTVisionPro.Node._7_ResultProcessing.GenerateExcelSpreadsheet
{
    internal class NodeGenerateExcel : NodeBase
    {
        ResultViewData[] results;
        public static int lastRow = 2;

        public NodeGenerateExcel(int nodeId, string nodeName, Process process, NodeType nodeType) : base(nodeId, nodeName, process, nodeType)
        {
            ParamForm = new ParamFormGenerateExcel();
            ParamForm.SetNodeBelong(this);
            Result = new NodeResultGenerateExcel();
        }

        /// <summary>
        /// 节点运行
        /// </summary>
        public override async Task Run(CancellationToken token)
        {
            DateTime startTime = DateTime.Now;

            // 参数合法性校验
            if (!Active)
            {
                SetRunResult(startTime, NodeStatus.Unexecuted);
                return;
            }
            if (ParamForm.Params == null)
            {
                LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行参数未设置或保存！", true);
                SetRunResult(startTime, NodeStatus.Failed);
                throw new Exception($"节点({ID}.{NodeName})运行参数未设置或保存！");
            }

            if (ParamForm is ParamFormGenerateExcel form)
            {
                if (form.Params is NodeParamGenerateExcel param)
                {
                    try
                    {
                        SetStatus(NodeStatus.Unexecuted, "*");
                        base.CheckTokenCancel(token);

                        #region 获取结果

                        results = new ResultViewData[10];

                        void ProcessResult(int index, Func<object> getResultFunc, string detectName)
                        {
                            var result = getResultFunc();
                            if (result != null && form.Dictionary[index])
                            {
                                if (result is string r1)
                                {
                                    results[index] = new ResultViewData();
                                    bool isValid = !string.IsNullOrEmpty(r1.Trim()) && r1.Trim() != "null";
                                    results[index].SingleRowDataList.Add(new Forms.ResultView.SingleResultViewData("", "", detectName, r1, isValid));
                                }
                                else if (result is ResultViewData r11)
                                {
                                    if (string.IsNullOrEmpty(r11.SingleRowDataList[0].DetectName.Trim()) || r11.SingleRowDataList[0].DetectName.Trim() == "null")
                                    {
                                        for (int i = 0; i < r11.SingleRowDataList.Count; i++)
                                        {
                                            if (string.IsNullOrEmpty(r11.SingleRowDataList[i].DetectName.Trim()) || r11.SingleRowDataList[i].DetectName.Trim() == "null")
                                            {
                                                r11.SingleRowDataList[i].DetectName = detectName;
                                            }
                                        }
                                    }
                                    results[index] = r11;
                                }
                            }
                        }

                        // 调用 ProcessResult 方法处理每个结果
                        ProcessResult(1, form.GetResult1, param.Texts[0]);
                        ProcessResult(2, form.GetResult2, param.Texts[2]);
                        ProcessResult(3, form.GetResult3, param.Texts[4]);
                        ProcessResult(4, form.GetResult4, param.Texts[6]);
                        ProcessResult(5, form.GetResult5, param.Texts[8]);
                        ProcessResult(6, form.GetResult6, param.Texts[10]);
                        ProcessResult(7, form.GetResult7, param.Texts[12]);
                        ProcessResult(8, form.GetResult8, param.Texts[14]);
                        ProcessResult(9, form.GetResult9, param.Texts[16]);
                        ProcessResult(10, form.GetResult10, param.Texts[18]);

                        #endregion

                        GenerateExcel(results, param.filePath);

                        long time = SetRunResult(startTime, NodeStatus.Successful);
                        LogHelper.AddLog(MsgLevel.Info, $"节点({ID}.{NodeName})运行成功！({time} ms)", true);
                    }
                    catch (OperationCanceledException)
                    {
                        LogHelper.AddLog(MsgLevel.Warn, $"节点({ID}.{NodeName})运行取消！", true);
                        SetRunResult(startTime, NodeStatus.Unexecuted);
                        throw new OperationCanceledException($"节点({ID}.{NodeName})运行取消！");
                    }
                    catch (Exception ex)
                    {
                        lastRow--;
                        LogHelper.AddLog(MsgLevel.Fatal, $"节点({ID}.{NodeName})运行失败！原因:{ex.Message}", true);
                        SetRunResult(startTime, NodeStatus.Failed);
                        throw new Exception($"节点({ID}.{NodeName})运行失败！原因:{ex.Message}");
                    }
                }
            }
        }


        public void GenerateExcel(ResultViewData[] resultViewData, string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            ExcelPackage package;
            // 如果文件存在，则打开它；否则创建一个新的 ExcelPackage
            if (file.Exists)
            {
                package = new ExcelPackage(file);
            }
            else
            {
                package = new ExcelPackage(new FileInfo(filePath));
                lastRow = 2;
            }

            ExcelWorksheet worksheet = package.Workbook.Worksheets[file.Name];

            // 获取或添加一个工作表
            if (worksheet == null)
            {
                worksheet = package.Workbook.Worksheets.Add(file.Name);
            }

            // 此时，worksheet.Dimension 为 null，因为没有任何数据
            if (worksheet.Dimension == null)
            {
                lastRow = 2;
            }
            else
            {
                // 获取最后有数据的一行
                lastRow = worksheet.Dimension.End.Row + 1;
            }          

            // 获取最后有数据的一行
            List<bool> IsOKS = new List<bool>();
            bool AllOK;

            foreach (var item in resultViewData)
            {
                if (item != null)
                {
                    string FirstLine = item.SingleRowDataList[0].DetectName;
                    string Price = item.SingleRowDataList[0].DetectResult;
                    string FirstLine2 = $"{FirstLine}结果";
                    string Price2 = "NG";
                    if (item.SingleRowDataList.Count > 1)
                    {
                        bool allOk = true;
                        foreach (var singleRow in item.SingleRowDataList)
                        {
                            if (!singleRow.IsOk)
                            {
                                allOk = false;
                                IsOKS.Add(allOk);
                                break;
                            }
                        }
                        if (allOk)
                        {
                            Price2 = "OK";
                        }
                    }
                    else
                    {
                        Price2 = item.SingleRowDataList[0].IsOk ? "OK" : "NG";
                    }

                    IsOKS.Add(item.SingleRowDataList[0].IsOk);

                    GenerateTable(ref FirstLine, ref Price, ref filePath, ref file, ref package, ref worksheet);
                    GenerateTable(ref FirstLine2, ref Price2, ref filePath, ref file, ref package, ref worksheet);
                }
            }

            
            int lastRowTime = GetLastNonEmptyRowInColumn(worksheet, 1);
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            worksheet.Cells[lastRowTime + 1, 1].Value = formattedDateTime;

            AllOK = true;
            foreach (var item in IsOKS)
            {
                int i = 0;
                if (!item)
                {
                    AllOK = false;
                    break;
                }
            }

            string AllOK1 = AllOK ? "OK" : "NG";
            string AllOK2 = "汇总结果";
            GenerateTable(ref AllOK2, ref AllOK1, ref filePath, ref file, ref package, ref worksheet);

            // 保存Excel文件
            package.Save();
            package.Dispose();
        }

        /// <summary>
        /// 操作Excel文件
        /// </summary>
        /// <param name="FirstLine">行名</param>
        /// <param name="Price">值</param>
        /// <param name="filePath">Excel文件路径</param>
        public void GenerateTable(ref string FirstLine, ref string Price, ref string filePath, ref FileInfo file, ref ExcelPackage package, ref ExcelWorksheet worksheet) 
        {     
            try
            {
                // 查找第一行
                int firstRow = worksheet.Dimension?.Start.Row ?? 1;
                // 获取有数据的最后一列
                int lastColumn = worksheet.Dimension?.End.Column ?? 0;

                // 检查单元格值是否为 null 或空字符串
                if (lastColumn == 0)
                {
                    //如果没有任何数据，添加标题
                    worksheet.Cells[1, 1].Value = "Time";
                }

                // 更新获取有数据的最后一列
                lastColumn = worksheet.Dimension?.End.Column ?? 0;

                //获取FirstLine所在的列数
                int exists = GetColumnIndexInFirstRow(worksheet, FirstLine);

                if (exists == -1)
                {
                    worksheet.Cells[firstRow, lastColumn + 1].Value = FirstLine;
                    worksheet.Cells[lastRow, lastColumn + 1].Value = Price;
                }
                else
                {
                    worksheet.Cells[lastRow, exists].Value = Price;
                }

            }
            catch (Exception e)
            {
                LogHelper.AddLog(MsgLevel.Exception, $"文件写入失败,异常原因：{e.Message}", true);
            }
        }

        /// <summary>
        /// 获取第一行是否存在valueToCheck
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="valueToCheck"></param>
        /// <returns></returns>
        static int GetColumnIndexInFirstRow(ExcelWorksheet worksheet, string valueToCheck)
        {
            // 获取第一行的最大列数
            int maxColumn = worksheet.Dimension.End.Column;

            // 遍历第一行的所有单元格
            for (int col = 1; col <= maxColumn; col++)
            {
                var cellValue = worksheet.Cells[1, col].Value;

                // 检查单元格值是否与要检查的值相等
                if (cellValue != null && cellValue.ToString() == valueToCheck)
                {
                    return col; // 返回找到的列索引
                }
            }

            return -1; // 如果没有找到，返回 -1
        }

        /// <summary>
        /// 获取columnIndex列的最后一个不为空的行
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        static int GetLastNonEmptyRowInColumn(ExcelWorksheet worksheet, int columnIndex)
        {
            // 获取工作表的最大行数
            int maxRow = worksheet.Dimension.End.Row;

            // 从最后一行开始向上查找
            for (int row = maxRow; row >= 1; row--)
            {
                var cellValue = worksheet.Cells[row, columnIndex].Value;

                // 检查单元格是否非空
                if (cellValue != null)
                {
                    return row; // 返回最后一个非空单元格的行索引
                }
            }
            return -1; // 如果该列没有非空单元格，返回 -1
        }
    }
}
