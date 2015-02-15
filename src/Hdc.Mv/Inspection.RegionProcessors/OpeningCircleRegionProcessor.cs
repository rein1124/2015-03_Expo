using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class OpeningCircleRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            if (Math.Abs(Radius) < 0.0000001)
                return region.MoveRegion(0, 0);

            var openingCircle = region.OpeningCircle(Radius);
            return openingCircle;
        }

        public double Radius { get; set; }
    }
}