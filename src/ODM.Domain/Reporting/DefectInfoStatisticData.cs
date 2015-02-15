using ODM.Domain.Inspection;

namespace ODM.Domain.Reporting
{
    public class DefectInfoStatisticData
    {
        public DefectType DefectType { get; set; }
        public int DefectCount { get; set; }
        public double DefectRate { get; set; }
    }
}