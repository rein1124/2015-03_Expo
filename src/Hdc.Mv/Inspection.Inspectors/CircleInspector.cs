using System;
using System.Collections.Generic;
using System.IO;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Inspection
{
    public class CircleInspector : ICircleInspector
    {
        private string _cacheImageDir = typeof(Mv.Ex).Assembly.GetAssemblyDirectoryPath() + "\\CacheImages";

        public CircleSearchingResult SearchCircle(HImage image, CircleSearchingDefinition definition)
        {
            var swSearchCircle = new NotifyStopwatch("SearchCircle: " + definition.Name);

            var circleSearchingResult = new CircleSearchingResult
            {
                Definition = definition.DeepClone(),
                Name = definition.Name,
//                Index = index
            };

            if (definition.ImageFilter_Disabled)
                definition.ImageFilter = null;

            //                if (circleDefinition.RegionExtractor_Disabled)
            //                    circleDefinition.RegionExtractor = null;

            var topLeftX = definition.CenterX - definition.OuterRadius;
            var topLeftY = definition.CenterY - definition.OuterRadius;
            var bottomRightX = definition.CenterX + definition.OuterRadius;
            var bottomRightY = definition.CenterY + definition.OuterRadius;

            var reg = new HRegion();
            reg.GenRectangle1(topLeftY, topLeftX, bottomRightY, bottomRightX);
            var reducedImage = image.ReduceDomain(reg);
            reg.Dispose();

            if (definition.Domain_SaveCacheImageEnabled)
                reducedImage.WriteImageOfTiffLzwOfCropDomain(
                    _cacheImageDir + "\\SearchCircles_" + definition.Name + "_1_Domain.tif");

            /*                HRegion domain;
                            if (circleDefinition.RegionExtractor != null)
                            {
                                throw new NotImplementedException();
                                var oldDomain = reducedImage.GetDomain();
                                domain = circleDefinition.RegionExtractor.Process(reducedImage);
                                oldDomain.Dispose();

                                if (circleDefinition.ImageFilter_SaveCacheImageEnabled)
                                    reducedImage
                                        .ReduceDomain(domain)
                                        .CropDomain()
                                        .ToBitmapSource()
                                        .SaveToTiff(_cacheImageDir + "\\SearchCircles_" + circleDefinition.Name + "_2_ROI.tif");
                            }
                            else
                            {
                                domain = reducedImage.GetDomain();
                            }*/

            HRegion domain = reducedImage.GetDomain();
            int offsetX = domain.GetColumn1();
            int offsetY = domain.GetRow1();

            var roiImage = reducedImage.CropDomain();

            HImage filterImage = null;
            if (definition.ImageFilter != null)
            {
                var swImageFilter = new NotifyStopwatch("CircleInspector.ImageFilter: " + definition.Name);
                filterImage = definition.ImageFilter.Process(roiImage);
                swImageFilter.Dispose();

                if (definition.ImageFilter_SaveCacheImageEnabled)
                {
                    filterImage.WriteImageOfTiffLzwOfCropDomain(
                        _cacheImageDir + "\\SearchCircles_" + definition.Name + "_3_ImageFilter.tif");

                    //
                    var paintedImage = filterImage.PaintGrayOffset(image, offsetY, offsetX);
                    paintedImage.WriteImageOfJpeg(_cacheImageDir + "\\SearchCircles_" + definition.Name +
                                    "_3_ImageFilter_PaintGrayOffset.jpg");
                    paintedImage.Dispose();
                }
            }
            else
            {
                filterImage = roiImage;
            }

            var offsetCenterX = definition.CenterX - offsetX;
            var offsetCenterY = definition.CenterY - offsetY;

            var swFindCircle = new NotifyStopwatch("CircleInspector.FindCircle: " + definition.Name);
            var circle = definition.CircleExtractor.FindCircle(filterImage,
                offsetCenterX, offsetCenterY, definition.InnerRadius, definition.OuterRadius);
            swFindCircle.Dispose();

            if (circle.IsEmpty)
            {
                circleSearchingResult.HasError = true;
                circleSearchingResult.IsNotFound = true;
                //                    circleSearchingResult.Circle = new Circle(circleDefinition.CenterX, circleDefinition.CenterY);
            }
            else
            {
                var newCircle = new Circle(circle.CenterX + offsetX, circle.CenterY + offsetY, circle.Radius);
                circleSearchingResult.Circle = newCircle;
            }

            swSearchCircle.Dispose();
            return circleSearchingResult;
        }
    }
}