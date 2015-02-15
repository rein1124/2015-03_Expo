using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SmallestRectangle2RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            HTuple row, col, phi, len1, len2;
            region.SmallestRectangle2(out row, out col,out phi, out len1, out len2);
            var smallestRect2 = new HRegion();
            smallestRect2.GenRectangle2(row, col, phi, len1, len2);
            return smallestRect2;
        }
    }
}