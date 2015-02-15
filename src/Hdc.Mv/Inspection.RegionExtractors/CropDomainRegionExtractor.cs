using System;
using System.Windows.Markup;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("RegionExtractor")]
    public class CropDomainRegionExtractor : IRegionExtractor
    {
        public HRegion Extract(HImage image)
        {
            var domain = image.GetDomain();
            var offsetRow1 = domain.GetRow1();
            var offsetColumn1 = domain.GetColumn1();
            var croppedImage = image.CropDomain();

            var croppedRegion = RegionExtractor.Extract(croppedImage);

            var movedRegion = croppedRegion.MoveRegion(offsetRow1, offsetColumn1);

            croppedImage.Dispose();
            croppedRegion.Dispose();
            domain.Dispose();

            return movedRegion;
        }

        public IRegionExtractor RegionExtractor { get; set; }
    }
}