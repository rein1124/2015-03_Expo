using System;
using System.Linq;
using Aspose.Cells;
using Hdc.Patterns;
using Microsoft.Practices.Unity;

namespace ODM.Domain.Reporting
{
    public class ExcelReportExporter2 : IReportExporter
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

            worksheet.Cells[row, col++].PutValue("ID");
            worksheet.Cells[row, col++].PutValue("检测时间");
            worksheet.Cells[row, col++].PutValue("总序号");
            worksheet.Cells[row, col++].PutValue("当日序号");
            worksheet.Cells[row, col++].PutValue("班产序号");

            for (int i = 1; i < 5; i++)
            {
                worksheet.Cells[row, col++].PutValue("C" + i + ".X");
                worksheet.Cells[row, col++].PutValue("C" + i + ".Y");
                worksheet.Cells[row, col++].PutValue("C" + i + ".XDiff");
                worksheet.Cells[row, col++].PutValue("C" + i + ".YDiff");
                //            worksheet.Cells[row, col++].PutValue("D");
                //            worksheet.Cells[row, col++].PutValue("D Differ");
            }

            row++;

            var groups = report.WorkpieceInfos.GroupBy(x => x.Id).ToList();




//            for (int index = 0; index < report.WorkpieceInfos.Count; index++)
            for (int index = 0; index < groups.Count; index++)
            {
                worksheet.Cells[row++, col].PutValue("PCS " + index);

                var group = groups[index];
                var firstWpc = group.First();
                var mis = group.SelectMany(x => x.MeasurementInfos).ToList();
                var col2 = 0;
//                var wpi = report.WorkpieceInfos[index];

                worksheet.Cells[row, col2++].PutValue(firstWpc.Id);
                worksheet.Cells[row, col2++].PutValue(firstWpc.InspectDateTime.Value.ToString());
                worksheet.Cells[row, col2++].PutValue(firstWpc.IndexOfTotal);
                worksheet.Cells[row, col2++].PutValue(firstWpc.IndexOfDay);
                worksheet.Cells[row, col2++].PutValue(firstWpc.IndexOfJob);

                var tempRow = row;
                var tempCow = col2;
                for (int i = 1; i < 5; i++)
                {
                    int i1 = i;
                    var c = mis.Where(x => x.Index == i1);

//                    col2 = tempCow;
                    row = tempRow;

                    var tempCol2 = col2;
                    foreach (var mi in c)
                    {
                        col2 = tempCol2;

                        worksheet.Cells[row, col2++].PutValue(mi.StartPointXActualValue);
                        worksheet.Cells[row, col2++].PutValue(mi.StartPointYActualValue);
                        worksheet.Cells[row, col2++].PutValue(mi.EndPointXActualValue);
                        worksheet.Cells[row, col2++].PutValue(mi.EndPointYActualValue);
//                        worksheet.Cells[row++, col2].PutValue(mi.StartPointXActualValue);
//                        worksheet.Cells[row++, col2].PutValue(mi.StartPointYActualValue);
//                        worksheet.Cells[row++, col2].PutValue(mi.EndPointXActualValue);
//                        worksheet.Cells[row++, col2].PutValue(mi.EndPointYActualValue);

                        row++;
                    }
//                    col2++;
                }
                row++;


//                return;

                col = 0;
                worksheet.Cells[row, col++].PutValue("");
                worksheet.Cells[row, col++].PutValue("");
                worksheet.Cells[row, col++].PutValue("");
                worksheet.Cells[row, col++].PutValue("");
                worksheet.Cells[row, col++].PutValue("");


                var tempCol = col;
                for (int i = 1; i < 5; i++)
                {
                    worksheet.Cells[row, col++].PutValue("C" + i + ".X SUM");
                    worksheet.Cells[row, col++].PutValue("C" + i + ".Y SUM");
                    worksheet.Cells[row, col++].PutValue("C" + i + ".XDiff SUM");
                    worksheet.Cells[row, col++].PutValue("C" + i + ".YDiff SUM");
                }
                row++;

                col2 = tempCol;

                for (int i = 1; i < 5; i++)
                {
                    int i1 = i;
                    var c = mis.Where(x => x.Index == i1).ToList();


                    var tempCol3 = col2;


                    // Max
                    col2 = tempCol3;
                    worksheet.Cells[row, col2++].PutValue(c.Max(x => x.StartPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Max(x => x.StartPointYActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Max(x => x.EndPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Max(x => x.EndPointYActualValue));
                    row++;

                    // Min
                    col2 = tempCol3;
                    worksheet.Cells[row, col2++].PutValue(c.Min(x => x.StartPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Min(x => x.StartPointYActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Min(x => x.EndPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Min(x => x.EndPointYActualValue));
                    row++;

                    // Average
                    col2 = tempCol3;
                    worksheet.Cells[row, col2++].PutValue(c.Average(x => x.StartPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Average(x => x.StartPointYActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Average(x => x.EndPointXActualValue));
                    worksheet.Cells[row, col2++].PutValue(c.Average(x => x.EndPointYActualValue));
                    row++;

                    col2 -= 0;
                    row -= 3;
                }

                row++;
                row++;
                row++;
            }


            worksheet.AutoFitColumns();

            try
            {
                workbook.Save(report.ExportFileName, new OoxmlSaveOptions(SaveFormat.Xlsx));

                EventBus.Publish(new ReportExportSuccessfulEvent() { FileName = report.ExportFileName });
            }
            catch (Exception e)
            {
                EventBus.Publish(new ReportExportFailedEvent() { Exception = e });
            }
        }
    }
}