using System;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("Items")]
    public class CompositeFilter: Collection<IImageFilter>, IImageFilter
    {
        public HImage Process(HImage image)
        {
            var processImage = image;

            foreach (var imageFilter in Items)
            {
                var hImage = imageFilter.Process(processImage);
//                processImage.Dispose();
                processImage = hImage;
            }

            return processImage;
        }
    }
}