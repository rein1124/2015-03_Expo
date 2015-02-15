using System;
using Aspose.Cells;
using Hdc.Patterns;
using Microsoft.Practices.Unity;
using ODM.Domain.Reporting;

namespace ODM.Domain.Reporting
{
    public class ExcelReportExporter : IReportExporter
    {
        [Dependency]
        public IEventBus EventBus { get; set; }

        public void Export(Report report)
        {
            Workbook workbook = new Workbook();

            //Get the first worksheet in the workbook
            Worksheet worksheet = workbook.Worksheets[0];

            var col = 0;
            int row = 0;
            row++;

            //
            row++;
            col = 0;
            worksheet.Cells[row, 0].PutValue("基本信息");
            row++;

            worksheet.Cells[row, 0].PutValue("标题");
            worksheet.Cells[row++, 1].PutValue(report.Title);
            worksheet.Cells[row, 0].PutValue("开始时间");
            worksheet.Cells[row++, 1].PutValue(report.FromDateTime.ToString());
            worksheet.Cells[row, 0].PutValue("结束时间");
            worksheet.Cells[row++, 1].PutValue(report.ToDateTime.ToString());
            worksheet.Cells[row, 0].PutValue("总数");
            worksheet.Cells[row++, 1].PutValue(report.TotalCount);
            worksheet.Cells[row, 0].PutValue("良品数");
            worksheet.Cells[row++, 1].PutValue(report.AcceptedCount);
            worksheet.Cells[row, 0].PutValue("良品率");
            worksheet.Cells[row++, 1].PutValue((1.0 - report.RejectedRate).ToString("P2"));
            worksheet.Cells[row, 0].PutValue("废品数");
            worksheet.Cells[row++, 1].PutValue(report.RejectedCount);
            worksheet.Cells[row, 0].PutValue("废品率");
            worksheet.Cells[row++, 1].PutValue(report.RejectedRate.ToString("P2"));
            worksheet.Cells[row, 0].PutValue("导出路径");
            worksheet.Cells[row++, 1].PutValue(report.ExportFileName);

            // RejectedWorkpieceInfos
            row++;
            col = 0;
            worksheet.Cells[row, 0].PutValue("废品信息");
            row++;
            worksheet.Cells[row, col++].PutValue("ID");
            worksheet.Cells[row, col++].PutValue("检测时间");
            worksheet.Cells[row, col++].PutValue("总序号");
            worksheet.Cells[row, col++].PutValue("当日序号");
            worksheet.Cells[row, col++].PutValue("班产序号");

            row++;
            for (int index = 0; index < report.RejectedWorkpieceInfos.Count; index++)
            {
                var rejectedWorkpieceInfo = report.RejectedWorkpieceInfos[index];

                var col2 = 0;
                worksheet.Cells[row, col2++].PutValue(rejectedWorkpieceInfo.Id);
                worksheet.Cells[row, col2++].PutValue(rejectedWorkpieceInfo.InspectDateTime.Value.ToString());
                worksheet.Cells[row, col2++].PutValue(rejectedWorkpieceInfo.IndexOfTotal);
                worksheet.Cells[row, col2++].PutValue(rejectedWorkpieceInfo.IndexOfDay);
                worksheet.Cells[row, col2++].PutValue(rejectedWorkpieceInfo.IndexOfJob);

                row++;
            }

            // DefectInfoStatisticDatas
            row++;
            col = 0;
            worksheet.Cells[row, 0].PutValue("缺陷数据分析");
            row++;
            worksheet.Cells[row, col++].PutValue("缺陷类型");
            worksheet.Cells[row, col++].PutValue("缺陷数量");
            worksheet.Cells[row, col++].PutValue("缺陷比例");

            row++;
            for (int index = 0; index < report.DefectInfoStatisticDatas.Count; index++)
            {
                var statisticData = report.DefectInfoStatisticDatas[index];

                var col2 = 0;
                worksheet.Cells[row, col2++].PutValue(statisticData.DefectType);
                worksheet.Cells[row, col2++].PutValue(statisticData.DefectCount);
                worksheet.Cells[row, col2++].PutValue(statisticData.DefectRate.ToString("P2"));

                row++;
            }

            worksheet.AutoFitColumns();

            try
            {
                workbook.Save(report.ExportFileName, new OoxmlSaveOptions(SaveFormat.Xlsx));

                EventBus.Publish(new ReportExportSuccessfulEvent() {FileName = report.ExportFileName});
            }
            catch (Exception e)
            {
                EventBus.Publish(new ReportExportFailedEvent() {Exception = e});
            }
        }
    }
}