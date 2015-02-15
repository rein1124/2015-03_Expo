using System;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    public class ConnectionRegionProcessor : IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            return region.Connection();
        }
    }
}