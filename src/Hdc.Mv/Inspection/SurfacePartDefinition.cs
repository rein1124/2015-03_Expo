using System;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SurfacePartDefinition
    {
        private HRegion _domain;
        public string Name { get; set; }
        public bool SaveCacheImageEnabled { get; set; }
        public IRectangle2 RoiRelativeRect { get; set; }
        public IRectangle2 RoiActualRect { get; set; }
        public IRegionExtractor RegionExtractor { get; set; }

        public virtual HRegion Extract(HImage image)
        {
            if (RegionExtractor == null)
                return Domain;

            var domainRect = Domain.GetSmallestRectangle1();
            var domainCropImage = image.CropRectangle1(domainRect);

            var region = RegionExtractor.Extract(domainCropImage);
            var movedRegion = region.MoveRegion(domainRect.Row1, domainRect.Column1);
            return movedRegion;
        }

        public virtual HRegion Extract(HImage image, HRegion domain)
        {
            var domainChangedImage = image.ChangeDomain(domain);

            if (RegionExtractor == null)
            {
                return domainChangedImage;
            }
            else
            {
                return RegionExtractor.Extract(domainChangedImage);
            }
        }

        public HRegion Domain
        {
            get { return _domain ?? (_domain = RoiActualRect.GenRegion()); }
            set { _domain = value; }
        }
    }
}