using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class Union1RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.Union1();
        }
    }
}