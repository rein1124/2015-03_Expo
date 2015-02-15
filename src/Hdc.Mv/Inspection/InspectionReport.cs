namespace Hdc.Mv.Inspection
{
    public class InspectionReport
    {
        public InspectionResultCollection Results { get; set; }

        public InspectionReport()
        {
            Results = new InspectionResultCollection();
        }
    }
}