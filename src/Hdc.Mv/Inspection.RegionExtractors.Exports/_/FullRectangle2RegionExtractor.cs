/*using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class FullRectangle2RegionExtractor : Rectangle2RegionExtractor
    {
        protected override HRegion GetRegion(HImage image)
        {
            var rect = new HRegion();
            rect.GenRectangle2(HalfHeight, HalfWidth, Angle, HalfWidth, HalfHeight);
            return rect;
        }
    }
}*/