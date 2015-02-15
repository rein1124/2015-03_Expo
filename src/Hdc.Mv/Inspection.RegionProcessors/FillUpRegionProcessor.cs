using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class FillUpRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.FillUp();
        }
    }
}