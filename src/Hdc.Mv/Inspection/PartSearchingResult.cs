using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class PartSearchingResult
    {
        public HRegion PartRegion { get; set; }

        public HRegion Domain { get; set; }

        public HRegion AreaRegion { get; set; }

        public PartSearchingDefinition Definition { get; set; }

        public string SurfaceGroupName { get; set; }

        public string SurfaceName { get; set; }

        public string RegionName { get; set; }

        public bool HasError { get; set; }

        public Line PartLine { get; set; }
        public double PartHalfWidth { get; set; }
        public Line PartRelativeLine { get; set; }
        public int Index { get; set; }
    }
}