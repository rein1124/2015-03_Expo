using Hdc.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public interface IFacilityNodeMiddle<TThis,
                                         TParent,
                                         TChild> : IFacilityNodeChild<
                                                       TThis,
                                                       TParent>,
                                                   IFacilityNodeParent<
                                                       TThis,
                                                       TChild>,
                                                   IContextNodeMiddle<
                                                       TThis,
                                                       IDeviceGroup,
                                                       TParent,
                                                       IDeviceGroup,
                                                       TChild,
                                                       IDeviceGroup>,
                                                   IFacilityNode<TParent>
        where TParent : IFacilityNodeParent<
                            TParent,
                            TThis>
        where TChild : IFacilityNodeChild<
                           TChild,
                           TThis>
        where TThis : IFacilityNodeMiddle<
                          TThis,
                          TParent,
                          TChild>
    {
    }
}