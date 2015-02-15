using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class PolarTransRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
//            image.WriteImage("tiff", 0, "B:\\test-01-cropped.tif");

            var imageWidth = image.GetWidth();
            var imageHeight = image.GetHeight();

//            image.GrayRangeRect(55, 55).WriteImage("tiff", 0, "B:\\test-00-GrayRangeRect.tif");

            var houghRegion = HoughRegionExtractor.Extract(image);
//            image.PaintRegion(houghRegion, 200.0, "fill").WriteImage("tiff", 0, "B:\\test-02-houghRegion.tif");

            var houghCenterRegion = houghRegion.HoughCircles(HoughExpectRadius, HoughPercent, 0);
            var center = houghCenterRegion.GetCenterPoint();

//            image.PaintRegion(houghCenterRegion, 200.0, "fill").WriteImage("tiff", 0, "B:\\test-03-houghCenterRegion.tif");

            var phiAngleStart = AngleStart/180.0*3.1415926;
            var phiAngleEnd = AngleEnd/180.0*3.1415926;

            var finalRadiuStart = RadiusStart > RadiusEnd ? RadiusStart : RadiusEnd;
            var finalRadiuEnd = RadiusStart > RadiusEnd ? RadiusEnd : RadiusStart;

            int width = (int) ((Math.Abs(phiAngleStart - phiAngleEnd))*finalRadiuStart);
            int height = (int) Math.Abs(RadiusStart - RadiusEnd);

            var transImage = image.PolarTransImageExt(center.Y, center.X, phiAngleStart, phiAngleEnd, RadiusStart,
                RadiusEnd,
                width, height, Interpolation.ToHalconString());

//            transImage.WriteImage("tiff", 0, "B:\\test-04-transImage.tif");

            var transRegion = TargetRegionExtractor.Extract(transImage);
            var finalRegion = transRegion.PolarTransRegionInv(center.Y, center.X, phiAngleStart, phiAngleEnd,
                RadiusStart, RadiusEnd,
                width, height, imageWidth, imageHeight, Interpolation.ToHalconString());

//            image.PaintRegion(finalRegion, 200.0, "fill").WriteImage("tiff", 0, "B:\\test-04-transRegion.tif");

            return finalRegion;
        }

        public IRegionExtractor HoughRegionExtractor { get; set; }
        public IRegionExtractor TargetRegionExtractor { get; set; }

        public double HoughExpectRadius { get; set; }
        public int HoughPercent { get; set; }

        public double AngleStart { get; set; }
        public double AngleEnd { get; set; }

        public double RadiusStart { get; set; }
        public double RadiusEnd { get; set; }
        public Interpolation Interpolation { get; set; }
    }
}