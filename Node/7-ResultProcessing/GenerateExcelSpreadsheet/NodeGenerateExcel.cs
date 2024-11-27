using Logger;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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

namespace YTVisionPro.Node._7_ResultProcessing.GenerateExcelSpreadsheet
{
    internal class NodeGenerateExcel : NodeBase
    {
        ResultViewData[] results = new ResultViewData[10];

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

                        var res1 = form.GetResult1();
                        if (res1!=null)
                        {
                            results[0] = res1;
                        }

                        var res2 = form.GetResult2();
                        if (res2 != null)
                        {
                            results[1] = res2;
                        }

                        var res3 = form.GetResult3();
                        if (res3 != null)
                        {
                            results[2] = res3;
                        }

                        var res4 = form.GetResult4();
                        if (res4 != null)
                        {
                            results[3] = res4;
                        }

                        var res5 = form.GetResult5();
                        if (res5 != null)
                        {
                            results[4] = res5;
                        }

                        var res6 = form.GetResult6();
                        if (res6 != null)
                        {
                            results[5] = res6;
                        }

                        var res7 = form.GetResult7();
                        if (res7 != null)
                        {
                            results[6] = res7;
                        }

                        var res8 = form.GetResult8();
                        if (res8 != null)
                        {
                            results[7] = res8;
                        }

                        var res9 = form.GetResult9();
                        if (res9 != null)
                        {
                            results[8] = res9;
                        }

                        var res10 = form.GetResult10();
                        if (res10 != null)
                        {
                            results[9] = res10;
                        }

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
            ExcelPackage package = new ExcelPackage(file.Exists ? file : new FileInfo(filePath));
            ExcelWorksheet worksheet = package.Workbook.Worksheets[file.Name];
            List<bool> IsOKS = new List<bool>();
            bool AllOK;

            foreach (var item in resultViewData)
            {
                if (item != null)
                {
                    string FirstLine = item.SingleRowDataList[0].DetectName;
                    string Price = item.SingleRowDataList[0].DetectResult;
                    string FirstLine2 = $"{FirstLine}结果";
                    string Price2 = item.SingleRowDataList[0].IsOk.ToString();
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
                if (!item)
                {
                    AllOK = false; 
                    break;
                }
            }

            string AllOK1 = AllOK.ToString();
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
                // 获取或添加一个工作表
                if (worksheet == null)
                {
                    worksheet = package.Workbook.Worksheets.Add(file.Name);
                }

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
                    worksheet.Cells[2, lastColumn + 1].Value = Price;
                }
                else
                {
                    int lastRow = GetLastNonEmptyRowInColumn(worksheet, exists);
                    worksheet.Cells[lastRow + 1, exists].Value = Price;
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
