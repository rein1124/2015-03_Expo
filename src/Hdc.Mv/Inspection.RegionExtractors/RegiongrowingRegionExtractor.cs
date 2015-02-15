using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class RegiongrowingRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var region = image.Regiongrowing(Row, Column, Tolerance, MinSize);
            return region;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public double Tolerance { get; set; }
        public int MinSize { get; set; }
    }
}