using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class GrayOpeningAndDilationFilter : IImageFilter
    {
      public HImage Process(HImage image)
        {
            HObject enhancedImage = null;

            HDevelopExport.Singletone.GrayOpeningAndDilationFilter(
                image,
                out enhancedImage,
                GrayOpeningMaskHeight,
                GrayOpeningMaskWidth,
                GrayDilationMaskHeight,
                GrayDilationMaskWidth,
                SubImageMult,
                SubImageAdd
                );

            return new HImage(enhancedImage);
        }

      public int GrayOpeningMaskHeight { get; set; }
      public int GrayOpeningMaskWidth { get; set; }

      public int GrayDilationMaskHeight { get; set; }
      public int GrayDilationMaskWidth { get; set; }

      public double SubImageMult { get; set; }
      public double SubImageAdd { get; set; }
    }
}