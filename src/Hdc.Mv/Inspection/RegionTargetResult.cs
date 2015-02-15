using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    public class RegionTargetResult
    {
        public RegionTargetDefinition Definition { get; set; }

        public HRegion TargetRegion { get; set; }

        public bool HasError { get; set; }

        public int Index { get; set; }
    }
}