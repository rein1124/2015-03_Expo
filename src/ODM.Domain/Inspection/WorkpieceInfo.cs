using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace ODM.Domain.Inspection
{
    public class WorkpieceInfo
    {
        [Key]
        public long Id { get; set; }

        [NotMapped]
        public int Index { get; set; }

        public virtual List<DefectInfo> DefectInfos { get; set; }
        public virtual List<MeasurementInfo> MeasurementInfos { get; set; }
        public virtual List<StoredImageInfo> StoredImageInfo { get; set; }

        public WorkpieceInfo()
        {
            DefectInfos = new List<DefectInfo>();
            MeasurementInfos = new List<MeasurementInfo>();
            StoredImageInfo = new List<StoredImageInfo>();
        }

        [DataType(DataType.DateTime)]
        public DateTime? InspectDateTime { get; set; }

        public int IndexOfTotal { get; set; }

        public int IndexOfDay { get; set; }

        public int IndexOfJob { get; set; }

        public bool IsReject { get; set; }

        public void DeleteImages()
        {
            throw new NotImplementedException();
        }
    }

}