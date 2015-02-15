using System;
using System.Windows;
// ReSharper disable InconsistentNaming

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class PartSearchingDefinition
    {
        public string Name { get; set; }
        public bool Domain_SaveCacheImageEnabled { get; set; }

        public Line RoiLine { get; set; }
        public Line RoiRelativeLine { get; set; }
        public double RoiHalfWidth { get; set; }

        public Line AreaLine { get; set; }
        public Line AreaRelativeLine { get; set; }
        public double AreaHalfWidth { get; set; }

        public IRegionExtractor RegionExtractor { get; set; }
        public bool RegionExtractor_Disabled { get; set; }
        public bool RegionExtractor_SaveCacheImageEnabled { get; set; }

        public IImageFilter ImageFilter { get; set; }
        public bool ImageFilter_Disabled { get; set; }
        public bool ImageFilter_SaveCacheImageEnabled { get; set; }

        public IRegionExtractor AreaExtractor { get; set; }
        public IRegionExtractor PartExtractor { get; set; }
    }
}

// ReSharper restore InconsistentNaming