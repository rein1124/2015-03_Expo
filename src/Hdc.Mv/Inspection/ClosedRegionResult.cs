using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class ClosedRegionResult
    {
        public ClosedRegionDefinition Definition { get; set; }

        public HRegion Region { get; set; }

        public string Name
        {
            get { return Definition.Name; }
        }
    }
}