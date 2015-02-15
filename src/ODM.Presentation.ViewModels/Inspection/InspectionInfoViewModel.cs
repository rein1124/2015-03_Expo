using System.Collections.Generic;

namespace ODM.Presentation.ViewModels.Inspection
{
    public class InspectionInfoViewModel
    {
        public int Index { get; set; }
        public bool HasError { get; set; }
        public int SurfaceTypeIndex { get; set; }
        public IList<DefectInfoViewModel> DefectInfos { get; set; }
        public IList<MeasurementInfoViewModel> MeasurementInfos { get; set; } 
    }
}