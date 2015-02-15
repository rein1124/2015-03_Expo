using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class DilationRectangle1RegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            if (Width < 1 || Height < 1)
                return region.MoveRegion(0, 0);

            var dilation = region.DilationRectangle1(Width, Height);
            return dilation;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}