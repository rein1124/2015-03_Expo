using System;
using System.Collections.ObjectModel;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class SelectShapeSeriesRegionProcessor : Collection<SelectShapeRegionProcessor>, IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            HRegion selectedRegion = region;
            foreach (var item in Items)
            {
                selectedRegion = item.Process(selectedRegion);
            }
            return selectedRegion;
        }
    }
}