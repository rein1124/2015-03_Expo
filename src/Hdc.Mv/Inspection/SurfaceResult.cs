using System;
using System.Collections.Generic;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SurfaceResult
    {
        public int Index { get; set; }

        public SurfaceDefinition Definition { get; set; }

        public HRegion IncludeRegion { get; set; }

        public IList<RegionResult> IncludeRegionResults { get; set; }

        public HRegion ExcludeRegion { get; set; }

        public IList<RegionResult> ExcludeRegionResults { get; set; }

        public SurfaceResult()
        {
            IncludeRegionResults = new List<RegionResult>();
            ExcludeRegionResults = new List<RegionResult>();
        }
    }
}