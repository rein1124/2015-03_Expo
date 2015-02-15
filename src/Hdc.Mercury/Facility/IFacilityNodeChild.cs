using Hdc.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public interface IFacilityNodeChild<TThis, TParent> : IContextNodeChild<
                                                              TThis,
                                                              IDeviceGroup,
                                                              TParent,
                                                              IDeviceGroup>,
                                                          IFacilityNode<TParent>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TThis : IFacilityNodeChild<TThis, TParent>
    {
        int DisplayIndex { get; }
    }
}