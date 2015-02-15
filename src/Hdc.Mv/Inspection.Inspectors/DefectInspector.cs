using System.Collections.Generic;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;

namespace Hdc.Mv.Inspection
{
    public class DefectInspector : IDefectInspector
    {
        private string _cacheImageDir = typeof (Mv.Ex).Assembly.GetAssemblyDirectoryPath() + "\\CacheImages";

        public IList<RegionDefectResult> SearchDefects(HImage image, DefectDefinition definition,
                                                       IList<SurfaceResult> surfaceResults)
        {
            var swSearchDefects = new NotifyStopwatch("SearchDefects: " + definition.Name);

            var rdrs = new List<RegionDefectResult>();

            foreach (var refer in definition.References)
            {
                var regionResult = surfaceResults.GetRegionResult(refer.SurfaceName, refer.RegionName);

                if (regionResult == null)
                    continue;

                var regionDefectResult = new RegionDefectResult {RegionResult = regionResult};

                var changeDomainImage = image.ChangeDomain(regionResult.Region);

                HImage filterImage;
                if (definition.ImageFilter != null)
                {
                    filterImage = definition.ImageFilter.Process(changeDomainImage);
                    changeDomainImage.Dispose();
                }
                else
                {
                    filterImage = changeDomainImage;
                }

                var blob = definition.RegionExtractor.Extract(filterImage);

                HRegion finalBlob;
                if (definition.RegionProcessor != null)
                {
                    finalBlob = definition.RegionProcessor.Process(blob);
                    blob.Dispose();
                }
                else
                {
                    finalBlob = blob;
                }

                if (definition.SaveCacheImageEnabled)
                {
                    var image2 = image.ChangeDomain(regionResult.Region);

                    if (finalBlob.CountObj() > 0)
                    {
                        var paintImage = image2.PaintRegion(finalBlob, 222.0, "fill");
                        paintImage.WriteImageOfTiffLzwOfCropDomain(_cacheImageDir + "\\SearchCircles_" + definition.Name +
                                                                   "_1_Domain.tif");
                    }
                    else
                    {
                        image2.WriteImageOfTiffLzwOfCropDomain(_cacheImageDir + "\\SearchCircles_" + definition.Name +
                                                               "_1_Domain.tif");
                    }
                }

                var blobs = finalBlob.ToList();

                int index = 0;
                foreach (var hRegion in blobs)
                {
                    var dr = new DefectResult
                             {
                                 Index = index,
                                 X = hRegion.GetColumn(),
                                 Y = hRegion.GetRow(),
                                 Width = hRegion.GetWidth(),
                                 Height = hRegion.GetHeight(),
                                 Name = definition.Name,
                             };
                    regionDefectResult.DefectResults.Add(dr);
                    index++;
                }

                rdrs.Add(regionDefectResult);
            }
            swSearchDefects.Dispose();
            return rdrs;
        }
    }
}