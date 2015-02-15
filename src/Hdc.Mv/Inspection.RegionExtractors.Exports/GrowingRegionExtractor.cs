using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrowingRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var foundRegion = image.GetRegionByGrowing(
                MedianRadius,
                GrowingRow,
                GrowingColumn,
                GrowingTolerance,
                GrowingMinSize,
                ClosingCircleRadius,
                ClosingRectangleWidth,
                ClosingRectangleHeight,
                AreaMin,
                AreaMax,
                RowMin,
                RowMax,
                ColumnMin,
                ColumnMax);
            return foundRegion;
        }

        public int MedianRadius { get; set; }
        public int GrowingRow { get; set; }
        public int GrowingColumn { get; set; }
        public double GrowingTolerance { get; set; }
        public int GrowingMinSize { get; set; }
        public double ClosingCircleRadius { get; set; }
        public double ClosingRectangleWidth { get; set; }
        public double ClosingRectangleHeight { get; set; }
        public double AreaMin { get; set; }
        public double AreaMax { get; set; }
        public double RowMin { get; set; }
        public double RowMax { get; set; }
        public double ColumnMin { get; set; }
        public double ColumnMax { get; set; }
    }
}