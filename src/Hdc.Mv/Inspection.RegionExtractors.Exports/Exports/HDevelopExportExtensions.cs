using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public static class HDevelopExportExtensions
    {
        public static HRegion GetRegionByGrayAndArea(this HImage image,
                                                     int medianRadius,
                                                     int empWidth, int empHeight, double empFactor,
                                                     int thresholdMinGray, int thresholdMaxGray,
                                                     int areaMin, int areaMax,
                                                     double closingRadius, double dilationRadius)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByGrayAndArea(image, out foundRegionObject, medianRadius,
                empWidth, empHeight, empFactor, thresholdMinGray, thresholdMaxGray, areaMin,
                areaMax,
                closingRadius, dilationRadius);

            return new HRegion(foundRegionObject);
        }

        public static HRegion GetRegionByGrowing(this HImage image,
                                                 int medianRadius,
                                                 int growingRow,
                                                 int growingColumn,
                                                 double growingTolerance,
                                                 int growingMinSize,
                                                 double closingCircleRadius,
                                                 double closingRectangleWidth,
                                                 double closingRectangleHeight,
                                                 double areaMin,
                                                 double areaMax,
                                                 double rowMin,
                                                 double rowMax,
                                                 double columnMin,
                                                 double columnMax)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByGrowing(image,
                out foundRegionObject,
                medianRadius,
                growingRow,
                growingColumn,
                growingTolerance,
                growingMinSize,
                closingCircleRadius,
                closingRectangleWidth,
                closingRectangleHeight,
                areaMin,
                areaMax,
                rowMin,
                rowMax,
                columnMin,
                columnMax);

            return new HRegion(foundRegionObject);
        }

        public static HRegion GetRegionByGrayInvert(this HImage image,
                                                    int meanMaskWidth,
                                                    int meanMaskHeight,
                                                    int empMaskWidth,
                                                    int empMaskHeight,
                                                    double empFactor1,
                                                    double empFactor2,
                                                    double thresholdMinGray,
                                                    double thresholdMaxGray,
                                                    double areaMin,
                                                    double areaMax)
        {
            HObject foundRegionObject;

            HDevelopExport.Singletone.GetRegionByGrayInvert(image,
                out foundRegionObject,
                meanMaskWidth,
                meanMaskHeight,
                empMaskWidth,
                empMaskHeight,
                empFactor1,
                empFactor2,
                thresholdMinGray,
                thresholdMaxGray,
                areaMin,
                areaMax);

            return new HRegion(foundRegionObject);
        }
    }
}