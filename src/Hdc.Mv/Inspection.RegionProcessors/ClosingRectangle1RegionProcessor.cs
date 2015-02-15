using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class ClosingRectangle1RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            if (Width < 1 || Height < 1)
                return region.MoveRegion(0, 0);

            var closingRectangle1 = region.ClosingRectangle1(Width, Height);
            return closingRectangle1;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}