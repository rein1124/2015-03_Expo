using System;
using System.Collections.Generic;
using System.Windows;
using HalconDotNet;

namespace Hdc.Mv.Halcon
{
    public static class HRegionExtensions
    {
        public static int GetWidth(this HRegion region)
        {
            var value = region.RegionFeatures("width");
            var int32 = Convert.ToInt32(value);
            return int32;
        }

        public static int GetHeight(this HRegion region)
        {
            var value = region.RegionFeatures("height");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static int GetRow1(this HRegion region)
        {
            var value = region.RegionFeatures("row1");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static Point GetPoint1(this HRegion region)
        {
            var x = region.GetColumn1();
            var y = region.GetRow1();
            return new Point(x, y);
        }

        public static Point GetPoint2(this HRegion region)
        {
            var x = region.GetColumn2();
            var y = region.GetRow2();
            return new Point(x, y);
        }

        public static Point GetCenterPoint(this HRegion region)
        {
            var x = region.GetColumn();
            var y = region.GetRow();
            return new Point(x, y);
        }

        public static int GetColumn1(this HRegion region)
        {
            var value = region.RegionFeatures("column1");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static int GetRow(this HRegion region)
        {
            var value = region.RegionFeatures("row");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static int GetColumn(this HRegion region)
        {
            var value = region.RegionFeatures("column");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static int GetRow2(this HRegion region)
        {
            var value = region.RegionFeatures("row2");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static int GetColumn2(this HRegion region)
        {
            var value = region.RegionFeatures("column2");
            var int32Value = Convert.ToInt32(value);
            return int32Value;
        }

        public static double GetArea(this HRegion region)
        {
            var value = region.RegionFeatures("area");
            var int32Value = Convert.ToDouble(value);
            return int32Value;
        }

        public static IList<HRegion> ToList(this HRegion region)
        {
            IList<HRegion> list = new List<HRegion>();

            var count = region.CountObj();

            for (int i = 0; i < count; i++)
            {
                list.Add(region[i + 1]);
            }

            return list;
        }

        public static HRectangle2 GetSmallestHRectangle2(this HRegion region)
        {
            double row;
            double column;
            double phi;
            double length1;
            double length2;
            region.SmallestRectangle2(out row, out column, out phi, out length1, out length2);
            var smallestRect = new HRectangle2()
                               {
                                   Row = row,
                                   Column = column,
                                   Phi = phi,
                                   Length1 = length1,
                                   Length2 = length2,
                               };
            return smallestRect;
        }

        public static HRectangle1 GetSmallestRectangle1(this HRegion region)
        {
            int row1;
            int column1;
            int row2;
            int column2;
            region.SmallestRectangle1(out row1, out column1, out row2, out column2);
            var smallestRect = new HRectangle1()
                               {
                                   Row1 = row1,
                                   Column1 = column1,
                                   Row2 = row2,
                                   Column2 = column2,
                               };
            return smallestRect;
        }
    }
}