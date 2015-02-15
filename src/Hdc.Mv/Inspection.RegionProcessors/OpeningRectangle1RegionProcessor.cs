using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class OpeningRectangle1RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            if (Width < 1 || Height < 1)
                return region.MoveRegion(0, 0);

            var openingRectangle1 = region.OpeningRectangle1(Width, Height);
            return openingRectangle1;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}