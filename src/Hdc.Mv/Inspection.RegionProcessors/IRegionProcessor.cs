using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface IRegionProcessor
    {
        HRegion Process(HRegion region);
    }
}