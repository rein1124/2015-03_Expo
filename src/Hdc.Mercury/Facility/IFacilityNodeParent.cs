using Hdc.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public interface IFacilityNodeParent<TThis, TChild> : IContextNodeParent<
                                                              TThis,
                                                              IDeviceGroup,
                                                              TChild,
                                                              IDeviceGroup>
        where TChild : IFacilityNodeChild<TChild, TThis>
        where TThis : IFacilityNodeParent<TThis, TChild>
    {
    }
}