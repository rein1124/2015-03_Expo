using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public static class FiltersEx
    {
        public static HImage EnhanceEdgeArea4(this HImage image,
                                              int meanMaskWidth, int meanMaskHeight, int firstMinGray, int firstMaxGray,
                                              Order order,
                                              int empMaskWidth, int empMaskHeight, double empMaskFactor, int lastMinGray,
                                              int lastMaxGray)
        {
            HObject enhancedImage = null;
            HObject region = null;

            string orderString = null;
            if (order == Order.Increase) orderString = "true";
            if (order == Order.Decrease) orderString = "false";

            HDevelopExport.Singletone.EnhanceEdgeArea4(
                image,
                out enhancedImage,
                out region,
                meanMaskWidth,
                meanMaskHeight,
                firstMinGray,
                firstMaxGray,
                orderString,
                empMaskWidth,
                empMaskHeight,
                empMaskFactor,
                lastMinGray,
                lastMaxGray
                );

            region.Dispose();

            return new HImage(enhancedImage);
        }
    }
}