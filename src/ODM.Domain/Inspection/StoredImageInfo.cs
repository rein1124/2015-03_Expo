using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODM.Domain.Inspection
{
    public class StoredImageInfo
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("WorkpieceInfo")]
        public long WorkpieceInfoId { get; set; }
        
        [Required]
        public WorkpieceInfo WorkpieceInfo { get; set; }

//        public int SurfaceIndex { get; set; }
        public int SurfaceTypeIndex { get; set; }

        public Guid ImageGuid { get; set; }

        public bool ImageGuidEnabled { get; set; }

        public DateTime StoredDateTime { get; set; }

        public string OriginalImageFilePath { get; set; }

        public bool OriginalImageFilePathEnabled { get; set; }

        public string StoredImageFilePath { get; set; }
    }
}