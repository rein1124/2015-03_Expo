using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SmallestRectangle1RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            HTuple row1, row2, col1, col2;
            region.SmallestRectangle1(out row1, out col1, out row2, out col2);
            var smallestRect1 = new HRegion();
            smallestRect1.GenRectangle1(row1, col1, row2, col2);
            return smallestRect1;
        }
    }
}