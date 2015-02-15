using System;
using System.Collections.Generic;
using ODM.Domain.Inspection;

namespace ODM.Domain.Reporting
{
    public class Report
    {
        public string Title { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int TotalCount { get; set; }
        public int AcceptedCount { get; set; }
        public int RejectedCount { get; set; }
        public double RejectedRate { get; set; }
        public string ExportFileName { get; set; }

        public IList<WorkpieceInfo> WorkpieceInfos { get; set; }
        public IList<WorkpieceInfo> RejectedWorkpieceInfos { get; set; }

        public IList<DefectInfoStatisticData> DefectInfoStatisticDatas { get; set; }

    }
}