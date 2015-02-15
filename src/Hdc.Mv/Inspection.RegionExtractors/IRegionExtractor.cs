using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface IRegionExtractor
    {
        HRegion Extract(HImage image);
    }
}