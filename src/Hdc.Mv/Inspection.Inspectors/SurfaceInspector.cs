using System.Collections.Generic;
using System.Linq;
using HalconDotNet;
using Hdc.Diagnostics;

namespace Hdc.Mv.Inspection
{
    public class SurfaceInspector : ISurfaceInspector
    {
        public SurfaceResult SearchSurface(HImage image, SurfaceDefinition definition)
        {
            var swSearchSurface = new NotifyStopwatch("SearchSurface: " + definition.Name);

            if (definition.SaveAllCacheImageEnabled)
            {
//                definition.SaveCacheImageEnabled = true;
                foreach (var part in definition.ExcludeParts)
                {
                    part.SaveCacheImageEnabled = true;
                }
                foreach (var part in definition.IncludeParts)
                {
                    part.SaveCacheImageEnabled = true;
                }
            }

            var surfaceResult = new SurfaceResult()
                                {
                                    Definition = definition.DeepClone(),
                                };

            var unionIncludeRegion = new HRegion();
            var unionIncludeDomain = new HRegion();
            unionIncludeRegion.GenEmptyRegion();
            unionIncludeDomain.GenEmptyRegion();

            var unionExcludeRegion = new HRegion();
            var unionExcludeDomain = new HRegion();
            unionExcludeRegion.GenEmptyRegion();
            unionExcludeDomain.GenEmptyRegion();

            foreach (var excludeRegion in definition.ExcludeParts)
            {
                var sw = new NotifyStopwatch("excludeRegion.Process: " + excludeRegion.Name);
                HRegion region = excludeRegion.Extract(image);
                sw.Dispose();
                var domain = excludeRegion.Domain;

                unionExcludeRegion = unionExcludeRegion.Union2(region);
                unionExcludeDomain = unionExcludeDomain.Union2(domain);

                if (excludeRegion.SaveCacheImageEnabled)
                {
                    var fileName = "SurfaceDefinition_" + definition.Name + "_Exclude_" + excludeRegion.Name;
                    image.SaveCacheImagesForRegion(domain, region, fileName);
                }

                surfaceResult.ExcludeRegionResults.Add(new RegionResult()
                                                       {
                                                           SurfaceGroupName = definition.GroupName,
                                                           SurfaceName = definition.Name,
                                                           RegionName = excludeRegion.Name,
                                                           Domain = domain,
                                                           Region = region,
                                                       });

                //                    region.Dispose();
                //                    domain.Dispose();
            }

            foreach (var includeRegion in definition.IncludeParts)
            {
                var domain = includeRegion.Domain;
                unionIncludeDomain = unionIncludeDomain.Union2(domain);

                var remainDomain = domain.Difference(unionExcludeRegion);
                var reducedImage = image.ChangeDomain(remainDomain);

                HRegion region;
                using (new NotifyStopwatch("includeRegion.Process: " + includeRegion.Name))
                    region = includeRegion.Extract(reducedImage);
                var remainRegion = region.Difference(unionExcludeRegion);
                unionIncludeRegion = unionIncludeRegion.Union2(remainRegion);

                if (includeRegion.SaveCacheImageEnabled)
                {
                    var fileName = "SurfaceDefinition_" + definition.Name + "_Include_" + includeRegion.Name;
                    //                        _hDevelopExportHelper.HImage.SaveCacheImagesForRegion(domain, remainRegion, fileName);
                    image.SaveCacheImagesForRegion(domain, remainRegion, unionExcludeRegion,
                        fileName);
                }

                surfaceResult.IncludeRegionResults.Add(new RegionResult()
                                                       {
                                                           SurfaceGroupName = definition.GroupName,
                                                           SurfaceName = definition.Name,
                                                           RegionName = includeRegion.Name,
                                                           Domain = domain,
                                                           Region = remainRegion,
                                                       });
            }

            if (definition.SaveCacheImageEnabled && definition.IncludeParts.Any())
            {
                var fileName = "SurfaceDefinition_" + definition.Name + "_Include";
                image.SaveCacheImagesForRegion(unionIncludeDomain, unionIncludeRegion,
                    unionExcludeRegion, fileName);
            }

            if (definition.SaveCacheImageEnabled && definition.ExcludeParts.Any())
            {
                var fileName = "SurfaceDefinition_" + definition.Name + "_Exclude";
                image.SaveCacheImagesForRegion(unionExcludeDomain, unionExcludeRegion,
                    fileName);
            }

            surfaceResult.ExcludeRegion = unionExcludeRegion;
            surfaceResult.IncludeRegion = unionIncludeRegion;

            swSearchSurface.Dispose();
            return surfaceResult;
        }
    }
}