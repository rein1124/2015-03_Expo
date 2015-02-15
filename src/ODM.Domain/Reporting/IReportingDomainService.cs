using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ODM.Domain.Inspection;

namespace ODM.Domain.Reporting
{
    public interface IReportingDomainService
    {
        Report GetMonthReport(int year, int month);

        Report GetDayReport(int year, int month, int day);

        IList<WorkpieceInfoEntry> GetWorkpieceInfoEntriesByMonth(int year, int month);

        IList<WorkpieceInfoEntry> GetWorkpieceInfoEntriesByDay(int year, int month, int day);

        void ExportReport(Report report);

        void CleanOldWorkpieceInfos(DateTime beforeDateTime);

    }
}