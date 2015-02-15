using System.Collections.Generic;
using HalconDotNet;
using Hdc.Diagnostics;
using Hdc.Mv.Halcon;
using Hdc.Reflection;
using Hdc.Windows.Media.Imaging;

namespace Hdc.Mv.Inspection
{
    public class PartInspector : IPartInspector
    {
        private string _cacheImageDir = typeof (Mv.Ex).Assembly.GetAssemblyDirectoryPath() + "\\CacheImages";

//        public IList<PartSearchingResult> SearchParts(HImage image, IList<PartSearchingDefinition> definitions)
        public PartSearchingResult SearchPart(HImage image, PartSearchingDefinition definition)
        {
            var swSearchPart = new NotifyStopwatch("SearchPart: " + definition.Name);

            int offsetX = 0;
            int offsetY = 0;
            HRegion domain;

            var result = new PartSearchingResult()
                         {
                             Definition = definition.DeepClone(),
                         };

            var reducedImage = HDevelopExport.Singletone.ChangeDomainForRectangle(
                image,
                line: definition.RoiLine,
                halfWidth: definition.RoiHalfWidth,
                margin: 0.5);

            if (definition.Domain_SaveCacheImageEnabled)
                reducedImage.CropDomain()
                    .ToBitmapSource()
                    .SaveToTiff(_cacheImageDir + "\\PartInspector_" + definition.Name + "_1_Domain.tif");

            domain = reducedImage.GetDomain();

            offsetX = domain.GetColumn1();
            offsetY = domain.GetRow1();
            var croppedImage = reducedImage.CropDomain();


            var region = definition.PartExtractor.Extract(croppedImage);

            var countObj = region.CountObj();
            if (countObj == 0)
            {
            }
            else
            {
                var movedRegion = region.MoveRegion(offsetY, offsetX);

                var rect2 = movedRegion.GetSmallestHRectangle2();
                var line = rect2.GetRoiLineFromRectangle2Phi();
                result.PartLine = line;
                result.PartHalfWidth = rect2.Length2;
                result.PartRegion = movedRegion;
            }
            swSearchPart.Dispose();
            return result;
        }
    }
}