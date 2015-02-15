using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SkeletonRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.Skeleton();
        }
    }
}