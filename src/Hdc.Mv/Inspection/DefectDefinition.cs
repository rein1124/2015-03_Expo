using System;
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("RegionExtractor")]
    public class DefectDefinition
    {
        public DefectDefinition()
        {
            References = new Collection<SurfacePartReference>();
        }

        public string Name { get; set; }

        public bool SaveCacheImageEnabled { get; set; }

        public Collection<SurfacePartReference> References { get; set; }

        public IImageFilter ImageFilter { get; set; }
        public IRegionExtractor RegionExtractor { get; set; }
        public IRegionProcessor RegionProcessor { get; set; }
    }
}