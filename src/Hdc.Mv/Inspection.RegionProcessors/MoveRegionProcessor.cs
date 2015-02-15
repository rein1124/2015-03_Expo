using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class MoveRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.MoveRegion(Y, X);
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}