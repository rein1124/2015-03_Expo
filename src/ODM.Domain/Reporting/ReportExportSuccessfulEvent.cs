using Hdc.Patterns;

namespace ODM.Domain.Reporting
{
    public class ReportExportSuccessfulEvent:IEvent
    {
        public string FileName { get; set; }
    }
}