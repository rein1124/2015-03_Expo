using System;
using System.Runtime.Serialization;
using Hdc.Patterns;

namespace ODM.Domain.Reporting
{
    public class ReportExportFailedEvent:IEvent
    {
        public Exception Exception { get; set; }
    }
}