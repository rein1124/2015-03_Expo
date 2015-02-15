using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class ClosingCircleRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            if (Math.Abs(Radius) < 0.0000001)
                return region.MoveRegion(0, 0);

            var closingCircle = region.ClosingCircle(Radius);
            return closingCircle;
        }

        public double Radius { get; set; }
    }
}