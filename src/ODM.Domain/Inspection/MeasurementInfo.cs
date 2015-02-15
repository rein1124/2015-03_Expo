using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODM.Domain.Inspection
{
    public class MeasurementInfo
    {
        [Key]
        public long Id { get; set; }
        public int Index { get; set; }
        public int TypeCode { get; set; }
        public double StartPointX { get; set; }
        public double StartPointY { get; set; }
        public double EndPointX { get; set; }
        public double EndPointY { get; set; }
        public double Value { get; set; }
        public int SurfaceTypeIndex { get; set; }
//        public int GroupIndex { get; set; }

        [ForeignKey("WorkpieceInfo")]
        public long WorkpieceInfoId { get; set; }

        [Required]
        public WorkpieceInfo WorkpieceInfo { get; set; }

//        [NotMapped]
        public double StartPointXActualValue { get; set; }
//        [NotMapped]
        public double StartPointYActualValue { get; set; }
//        [NotMapped]
        public double EndPointXActualValue { get; set; }
//        [NotMapped]
        public double EndPointYActualValue { get; set; }
//        [NotMapped]
        public double ValueActualValue { get; set; }

        public bool HasError { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string GroupName { get; set; }
        public double ExpectValue { get; set; } // in mm
    };
}