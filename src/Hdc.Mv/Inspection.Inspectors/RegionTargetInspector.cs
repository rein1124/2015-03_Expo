using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;

namespace Hdc.Mv.Inspection
{
    public class RegionTargetInspector : IRegionTargetInspector
    {
        private string _cacheImageDir = typeof(Mv.Ex).Assembly.GetAssemblyDirectoryPath() + "\\CacheImages";

        public RegionTargetResult SearchRegionTarget(HImage image, RegionTargetDefinition definition)
        {
            var swSearchRegionTarget = new NotifyStopwatch("SearchRegionTarget: " + definition.Name);

            var result = new RegionTargetResult
                         {
                             Definition = definition.DeepClone(),
                         };

            var roiImage = HDevelopExport.Singletone.ChangeDomainForRectangle(
                image,
                definition.RoiActualLine,
                definition.RoiHalfWidth);

            if (definition.Domain_SaveCacheImageEnabled)
            {
                roiImage.WriteImageOfTiffLzwOfCropDomain(
                    _cacheImageDir + "\\SearchRegionTarget_" + definition.Name + "_1_Domain_Cropped.tif");
            }

            // Around
            HImage aroundFilterImage;
            if (definition.AroundImageFilter != null)
            {
                var swAroundImageFilter = new NotifyStopwatch("RegionTargetInspector.AroundImageFilter: " + definition.Name);
                aroundFilterImage = definition.AroundImageFilter.Process(roiImage);
                swAroundImageFilter.Dispose();
            }
            else
            {
                aroundFilterImage = roiImage;
            }

            HRegion aroundRegion;
            if (definition.AroundRegionExtractor != null)
            {
                var swAroundRegionExtractor = new NotifyStopwatch("RegionTargetInspector.AroundRegionExtractor: " + definition.Name);
                aroundRegion = definition.AroundRegionExtractor.Extract(aroundFilterImage);
                swAroundRegionExtractor.Dispose();
            }
            else
            {
                aroundRegion = aroundFilterImage.GetDomain();
            } 
            
            if (definition.AroundRegionExtractor_SaveCacheImageEnabled)
            {
//                var reducedImage = image.ChangeDomain(aroundRegion.Union1());
//                reducedImage.WriteImageOfTiffLzwOfCropDomain(
//                    _cacheImageDir + "\\SearchRegionTarget_" + definition.Name + "_1_AroundRegionExtractor.tif");
//                reducedImage.Dispose();
                image.WriteImageOfTiffLzwOfCropDomain(aroundRegion,
                    _cacheImageDir + "\\SearchRegionTarget_" + definition.Name + "_1_AroundRegionExtractor.tif");
            }

            HRegion aroundProcessedRegion;
            if (definition.AroundRegionProcessor != null)
            {
                var swAroundRegionProcessor = new NotifyStopwatch("RegionTargetInspector.AroundRegionProcessor: " + definition.Name);
                aroundProcessedRegion = definition.AroundRegionProcessor.Process(aroundRegion);
                swAroundRegionProcessor.Dispose();
            }
            else
            {
                aroundProcessedRegion = aroundRegion;
            }

            HImage aroundImage = image.ChangeDomain(aroundProcessedRegion);

            if (definition.AroundRegionProcessor_SaveCacheImageEnabled)
            {
                aroundImage.WriteImageOfTiffLzwOfCropDomain(
                    _cacheImageDir + "\\SearchRegionTarget_" + definition.Name + "_1_Around_Cropped.tif");
            }

            aroundProcessedRegion.Dispose();
            aroundFilterImage.Dispose();
            aroundRegion.Dispose();

            // Target

            HImage targetFilterImage;
            if (definition.TargetImageFilter != null)
            {
                var swTargetImageFilter = new NotifyStopwatch("RegionTargetInspector.TargetImageFilter: " + definition.Name);
                targetFilterImage = definition.TargetImageFilter.Process(aroundImage);
                swTargetImageFilter.Dispose();
            }
            else
            {
                targetFilterImage = aroundImage;
            }

            if (definition.TargetImageFilter_SaveCacheImageEnabled)
            {
                targetFilterImage.WriteImageOfTiffLzwOfCropDomain(
                    _cacheImageDir + "\\SearchRegionTarget_" + definition.Name + "_1_TargetImageFilter_Cropped.tif");
            }

            HRegion targetRegion;
            if (definition.TargetRegionExtractor != null)
            {
                var swTargetRegionExtractor = new NotifyStopwatch("RegionTargetInspector.TargetRegionExtractor: " + definition.Name);
                targetRegion = definition.TargetRegionExtractor.Extract(targetFilterImage);
                swTargetRegionExtractor.Dispose();
            }
            else
            {
                targetRegion = targetFilterImage.GetDomain();
            }

            HRegion targetProcessedRegion;
            if (definition.TargetRegionProcessor != null)
            {
                var swTargetRegionProcessor = new NotifyStopwatch("RegionTargetInspector.TargetRegionProcessor: " + definition.Name);
                targetProcessedRegion = definition.TargetRegionProcessor.Process(targetRegion);
                swTargetRegionProcessor.Dispose();
            }
            else
            {
                targetProcessedRegion = targetRegion;
            }

            targetFilterImage.Dispose();
            roiImage.Dispose();

            result.TargetRegion = targetProcessedRegion;

            if (targetProcessedRegion.CountObj() == 0)
                result.HasError = true;

            swSearchRegionTarget.Dispose();
            return result;
        }
    }
}