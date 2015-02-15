using System.Collections.Generic;
using MvInspection;

namespace MvInspection
{
    public class InspectionInfo
    {
        public int Index { get; set; }
        public int SurfaceTypeIndex { get; set; }
        public bool HasError { get; set; }
        public List<DefectInfo> DefectInfos { get; set; }
        public List<MeasurementInfo> MeasurementInfos { get; set; }

        public InspectionInfo()
        {
            DefectInfos = new List<DefectInfo>();

            MeasurementInfos = new List<MeasurementInfo>();
        }
    }
}