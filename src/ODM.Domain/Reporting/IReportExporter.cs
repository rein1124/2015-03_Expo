namespace ODM.Domain.Reporting
{
    public interface IReportExporter
    {
        void Export(Report report);
    }
}