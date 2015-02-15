using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Inspection
{
    internal class EdgeInspector : IEdgeInspector
    {
        private string _cacheImageDir = typeof (Mv.Ex).Assembly.GetAssemblyDirectoryPath() + "\\CacheImages";

        public EdgeSearchingResult SearchEdge(HImage image, EdgeSearchingDefinition definition)
        {
            if (definition.CropDomainEnabled)
            {
                return CropDomain(image, definition);
            }
            else
            {
                return NoCropDomain(image, definition);
            }
        }

        private EdgeSearchingResult CropDomain(HImage image, EdgeSearchingDefinition definition)
        {
            var swSearchEdge = new NotifyStopwatch("SearchEdge: " + definition.Name);

            var esr = new EdgeSearchingResult
                      {
                          Definition = definition.DeepClone(),
                          Name = definition.Name
                      };

            var rectImage = image.ChangeDomainForRoiRectangle(definition.Line, definition.ROIWidth);
            var rectDomain = rectImage.GetDomain();
            var rectDomainRect1 = rectDomain.GetSmallestRectangle1();
            var rectCroppedImage = rectImage.CropDomain();

            if (definition.Domain_SaveCacheImageEnabled)
            {
                rectCroppedImage.WriteImageOfTiffLzw(
                    _cacheImageDir + "\\SearchEdges_" + definition.Name + "_1_Domain_Cropped.tif");
            }

            // RegionExtractor
            HRegion roiDomain;
            if (esr.Definition.RegionExtractor != null)
            {
                var croppedRoiDomain = esr.Definition.RegionExtractor.Extract(rectCroppedImage);
                roiDomain = croppedRoiDomain.MoveRegion(rectDomainRect1.Row1, rectDomainRect1.Column1);
            }
            else
            {
                roiDomain = rectDomain;
            }
            var roiDomainRect1 = roiDomain.GetSmallestRectangle1();
            HImage roiCroppedImage = image.CropRectangle1(roiDomainRect1);


            // ImageFilter
            HImage filterImage = null;
            if (esr.Definition.ImageFilter != null)
            {
                var sw = new NotifyStopwatch("ImageFilter");
                filterImage = esr.Definition.ImageFilter.Process(roiCroppedImage);
                sw.Dispose();

                if (definition.ImageFilter_SaveCacheImageEnabled)
                {
                    var cropDomain = filterImage.CropDomain();
                    cropDomain.WriteImageOfTiffLzw(_cacheImageDir + "\\SearchEdges_" + definition.Name +
                                                   "_3_ImageFilter_Cropped.tif");
                    cropDomain.Dispose();

//                        var paintedImage = filterImage.PaintGrayOffset(image, offsetY, offsetX);
//                        paintedImage.WriteImageOfJpeg(_cacheImageDir + "\\SearchEdges_" + definition.Name +
//                                                      "_3_ImageFilter_PaintGrayOffset.jpg");
//                        paintedImage.Dispose();
                }
            }
            else
            {
                filterImage = roiCroppedImage;
            }

            Line offsetLine = new Line(esr.Definition.Line.X1 - roiDomainRect1.Column1,
                      esr.Definition.Line.Y1 - roiDomainRect1.Row1, esr.Definition.Line.X2 - roiDomainRect1.Column1,
                      esr.Definition.Line.Y2 - roiDomainRect1.Row1);

            var line = esr.Definition.LineExtractor.FindLine(filterImage, offsetLine);

            var translatedLine = new Line(line.X1 + roiDomainRect1.Column1,
                line.Y1 + roiDomainRect1.Row1, line.X2 + roiDomainRect1.Column1,
                line.Y2 + roiDomainRect1.Row1);

            esr.EdgeLine = translatedLine;

            if (line.IsEmpty)
            {
                esr.IsNotFound = true;
                Debug.WriteLine("Edge not found: " + esr.Name);
            }

            swSearchEdge.Dispose();
            return esr;
        }

        private EdgeSearchingResult NoCropDomain(HImage image, EdgeSearchingDefinition definition)
        {
            var swSearchEdge = new NotifyStopwatch("SearchEdge: " + definition.Name);

            var esr = new EdgeSearchingResult
                      {
                          Definition = definition.DeepClone(),
                          Name = definition.Name
                      };

            if (esr.Definition.ImageFilter_Disabled)
                esr.Definition.ImageFilter = null;

            if (esr.Definition.RegionExtractor_Disabled)
                esr.Definition.RegionExtractor = null;

            var rectImage = HDevelopExport.Singletone.ChangeDomainForRectangle(
                image,
                definition.Line,
                definition.ROIWidth);

            if (definition.Domain_SaveCacheImageEnabled)
            {
                rectImage.WriteImageOfTiffLzwOfCropDomain(
                    _cacheImageDir + "\\SearchEdges_" + definition.Name + "_1_Domain_Cropped.tif");
            }

            // RegionExtractor
            HImage roiImage = null;

            if (esr.Definition.RegionExtractor != null)
            {
                var rectDomain = rectImage.GetDomain();
                HRegion roiDomain;
                if (!esr.Definition.RegionExtractor_CropDomainEnabled)
                {
                    var swRegionExtractor = new NotifyStopwatch("EdgeInspector.RegionExtractor: " + definition.Name);
                    roiDomain = esr.Definition.RegionExtractor.Extract(rectImage);
                    swRegionExtractor.Dispose();

                    if (definition.RegionExtractor_SaveCacheImageEnabled)
                    {
                        rectImage.WriteImageOfTiffLzwOfCropDomain(roiDomain,
                            _cacheImageDir + "\\SearchEdges_" + definition.Name + "_2_ROI.tif");
                    }
                    roiImage = rectImage.ReduceDomain(roiDomain);
                    rectImage.Dispose();
                }
                else
                {
                    throw new NotImplementedException();
                    var domainOffsetRow1 = rectDomain.GetRow1();
                    var domainOffsetColumn1 = rectDomain.GetColumn1();

                    var croppedRectImage = rectImage.CropDomain();
                    var croppedRoiDomain = esr.Definition.RegionExtractor.Extract(croppedRectImage);
                    roiDomain = croppedRoiDomain.MoveRegion(domainOffsetRow1, domainOffsetColumn1);

                    throw new NotImplementedException();
                }
            }
            else
            {
                roiImage = rectImage;
            }

            // ImageFilter
            HImage filterImage = null;

            if (esr.Definition.ImageFilter != null)
            {
                if (!esr.Definition.ImageFilter_CropDomainEnabled)
                {
                    var swImageFilter = new NotifyStopwatch("EdgeInspector.ImageFilter: " + definition.Name);
                    filterImage = esr.Definition.ImageFilter.Process(roiImage);
                    swImageFilter.Dispose();

                    roiImage.Dispose();

                    if (definition.ImageFilter_SaveCacheImageEnabled)
                    {
                        var filterImageDomain = filterImage.GetDomain();
                        var offsetRow = filterImageDomain.GetRow1();
                        var offsetColumn = filterImageDomain.GetColumn1();
                        var cropDomain = filterImage.CropDomain();
                        cropDomain.WriteImageOfTiffLzw(_cacheImageDir + "\\SearchEdges_" + definition.Name +
                                                       "_3_ImageFilter_Cropped.tif");

                        var paintedImage = cropDomain.PaintGrayOffset(image, offsetRow, offsetColumn);
                        paintedImage.WriteImageOfJpeg(_cacheImageDir + "\\SearchEdges_" + definition.Name +
                                                      "_3_ImageFilter_PaintGrayOffset.jpg");

                        cropDomain.Dispose();
                        paintedImage.Dispose();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                    var roiDomain = roiImage.GetDomain();
                    int offsetX = 0;
                    int offsetY = 0;
                    offsetX = roiDomain.GetColumn1();
                    offsetY = roiDomain.GetRow1();
                    var croppedImage = roiImage.CropDomain();

                    var sw = new NotifyStopwatch("ImageFilter");
                    filterImage = esr.Definition.ImageFilter.Process(croppedImage);
                    sw.Dispose();

                    if (definition.ImageFilter_SaveCacheImageEnabled)
                    {
                        var cropDomain = filterImage.CropDomain();
                        cropDomain.WriteImageOfTiffLzw(_cacheImageDir + "\\SearchEdges_" + definition.Name +
                                                       "_3_ImageFilter_Cropped.tif");
                        cropDomain.Dispose();

                        var paintedImage = filterImage.PaintGrayOffset(image, offsetY, offsetX);
                        paintedImage.WriteImageOfJpeg(_cacheImageDir + "\\SearchEdges_" + definition.Name +
                                                      "_3_ImageFilter_PaintGrayOffset.jpg");
                        paintedImage.Dispose();
                    }


                    /*                        Line offsetLine = new Line(esd.Line.X1 - offsetX,
                                                esd.Line.Y1 - offsetY, esd.Line.X2 - offsetX,
                                                esd.Line.Y2 - offsetY);

                                            var line = esd.LineExtractor.FindLine(filterImage, offsetLine);

                                            var translatedLine = new Line(line.X1 + offsetX,
                                                line.Y1 + offsetY, line.X2 + offsetX,
                                                line.Y2 + offsetY);

                                            esr.EdgeLine = translatedLine;

                                            if (line.IsEmpty)
                                            {
                                                esr.IsNotFound = true;
                                                Debug.WriteLine("Edge not found: " + esr.Name);
                                            }*/

                    throw new NotImplementedException();
                }
            }
            else
            {
                filterImage = roiImage;
            }

            var swLineExtractor = new NotifyStopwatch("EdgeInspector.LineExtractor: " + definition.Name);
            var line = definition.LineExtractor.FindLine(filterImage, definition.Line);
            swLineExtractor.Dispose();

            if (line.IsEmpty)
            {
                esr.IsNotFound = true;
                Debug.WriteLine("Edge not found: " + esr.Name);
            }

            esr.EdgeLine = line;

            swSearchEdge.Dispose();
            return esr;
        }
    }
}