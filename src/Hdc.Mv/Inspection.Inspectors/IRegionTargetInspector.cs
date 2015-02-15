using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public interface IRegionTargetInspector
    {
        RegionTargetResult SearchRegionTarget(HImage image, RegionTargetDefinition definition);
    }
}