using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Hdc;
using Hdc.IO;
using Hdc.Patterns;
using Hdc.Reflection;
using Microsoft.Practices.Unity;
using ODM.Domain.Inspection;

namespace ODM.Domain.Reporting
{
    internal class ReportingDomainService : IReportingDomainService
    {
        [Dependency]
        public IWorkpieceInfoRepository WorkpieceInfoRepository { get; set; }

        [Dependency]
        public IReportExporter ReportExporter { get; set; }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        [InjectionMethod]
        public void Init()
        {
        }

        public Report GetMonthReport(int year, int month)
        {
            var dt = new DateTime(year, month, 1);
            var title = "月度报表_" + dt.ToString("yyyy-MM");
            var fromDateTime = new DateTime(year, month, 1);
            var toDateTime = fromDateTime.AddMonths(1).AddDays(-0);
            var report = Report(fromDateTime, toDateTime, title);

            return report;
        }

        public Report GetDayReport(int year, int month, int day)
        {
            var dt = new DateTime(year, month, day);
            var title = "每日报表_" + dt.ToString("yyyy-MM-dd");
            var fromDateTime = new DateTime(year, month, day);
            var toDateTime = fromDateTime.AddDays(1);
            var report = Report(fromDateTime, toDateTime, title);

            return report;
        }

        private Report Report(DateTime fromDateTime, DateTime toDateTime, string title)
        {
            var all = GetWorkpieceInfosInRange(fromDateTime, toDateTime);

            if (!all.Any()) return null;

            var first = all.FirstOrDefault();
            var last = all.OrderByDescending(x=>x.Id).FirstOrDefault();
            var totalCount = last.IndexOfTotal - first.IndexOfTotal + 1;

//            var totalCount = all.Count();

            var rejectedWorkpieceInfos = all.Where(x => x.IsReject).ToList();
            var rejectedCount = rejectedWorkpieceInfos.Count();
            var acceptedCount = totalCount - rejectedCount;
            var rejectedRate = (double) rejectedCount/totalCount;


            var dis = rejectedWorkpieceInfos.SelectMany(x => x.DefectInfos).ToList();
            var disCount = dis.Count();

            var diSDs = dis.GroupBy(x => x.Type)
                           .Select(x =>
                                       {
                                           var count = x.Count();
                                           return new DefectInfoStatisticData
                                                      {
                                                          DefectType = x.Key,
                                                          DefectCount = count,
                                                          DefectRate = (double) count/disCount,
                                                      };
                                       })
                           .ToList();


            var report = new Report
                             {
                                 Title = title,
                                 TotalCount = totalCount,
                                 RejectedCount = rejectedCount,
                                 AcceptedCount = acceptedCount,
                                 FromDateTime = fromDateTime,
                                 ToDateTime = toDateTime,
                                 RejectedRate = rejectedRate,
                                 WorkpieceInfos = all.ToList(),
                                 RejectedWorkpieceInfos = rejectedWorkpieceInfos,
                                 DefectInfoStatisticDatas = diSDs,
                             };
            return report;
        }

        public IList<WorkpieceInfoEntry> GetWorkpieceInfoEntriesByMonth(int year, int month)
        {
            var fromDateTime = new DateTime(year, month, 1);
            var toDateTime = fromDateTime.AddMonths(1).AddDays(-0);

            var all = GetWorkpieceInfosInRange(fromDateTime, toDateTime).ToList();

            return all.Select(x => x.ToEntry()).ToList();
        }

        public IList<WorkpieceInfoEntry> GetWorkpieceInfoEntriesByDay(int year, int month, int day)
        {
            var fromDateTime = new DateTime(year, month, day);
            var toDateTime = fromDateTime.AddDays(1);

            var all = GetWorkpieceInfosInRange(fromDateTime, toDateTime).ToList();

            return all.Select(x => x.ToEntry()).ToList();
        }

        private IQueryable<WorkpieceInfo> GetWorkpieceInfosInRange(DateTime fromDateTime, DateTime toDateTime)
        {
            var all = WorkpieceInfoRepository.GetMany(x => x.InspectDateTime >= fromDateTime
                                                           && x.InspectDateTime < toDateTime);
            return all;
        }

        public void ExportReport(Report report)
        {
           ReportExporter.Export(report);
        }

        public void CleanOldWorkpieceInfos(DateTime beforeDateTime)
        {
            var currentDir = this.GetType().Assembly.GetAssemblyDirectoryPath();
            var imageStoreDir = currentDir.CombilePath("ImageStore");
            var dirs = Directory.GetDirectories(imageStoreDir).Select(x=>new DirectoryInfo(x));
            foreach (var dir in dirs)
            {
                if (!dir.Name.Contains("-") || dir.Name.Length != 10)
                    continue;

                var ss = dir.Name.Split('-');
                var y = ss[0].ToInt32();
                var m = ss[1].ToInt32();
                var d = ss[2].ToInt32();
                var day = new DateTime(y, m, d);

                if (day < beforeDateTime)
                {
                    dir.Delete(true);
                }
            }

            var olds = WorkpieceInfoRepository.GetQuery().Where(x => x.InspectDateTime < beforeDateTime);
            foreach (var old in olds)
            {
                WorkpieceInfoRepository.Delete(old);                
            }

            WorkpieceInfoRepository.UnitOfWork.Commit();
        }
    }
}