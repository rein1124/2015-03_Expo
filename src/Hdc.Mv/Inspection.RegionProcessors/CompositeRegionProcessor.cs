using System;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using HalconDotNet;
using Hdc.Mv.Halcon;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("Items")]
    public class CompositeRegionProcessor : Collection<IRegionProcessor>, IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            var processImage = region.MoveRegion(0,0);

            for (int i = 0; i < Items.Count; i++)
            {
                var processor = Items[i];
                var hRegion = processor.Process(processImage);
                //hRegion.RegionToBin(255, 0, 8192, 12500).WriteImageOfJpeg(@"D:\TestImage_" + i);

                processImage = hRegion;
            }

            return processImage;
        }
    }
}