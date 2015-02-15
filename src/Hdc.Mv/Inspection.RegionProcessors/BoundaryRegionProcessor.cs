using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class BoundaryRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.Boundary(BoundaryType.ToHalconString());
        }

        public BoundaryType BoundaryType { get; set; }
    }
}