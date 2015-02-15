using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODM.Domain.Inspection
{
    public class DefectInfo
    {
        [Key]
        public long Id { get; set; }

        public int Index { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public DefectType Type { get; set; }

        public int TypeCode { get; set; }

        public double Size { get; set; }

        //        public SurfaceType SurfaceType { get; set; }

        public int SurfaceTypeIndex { get; set; }

        [ForeignKey("WorkpieceInfo")]
        public long WorkpieceInfoId { get; set; }

        [Required]
        public WorkpieceInfo WorkpieceInfo { get; set; }

        //        public string SurfaceName { get; set; }


        [NotMapped]
        public double XActualValue { get; set; }
        [NotMapped]
        public double YActualValue { get; set; }
        [NotMapped]
        public double WidthActualValue { get; set; }
        [NotMapped]
        public double HeightActualValue { get; set; }
        [NotMapped]
        public double SizeActualValue { get; set; }
    }
}