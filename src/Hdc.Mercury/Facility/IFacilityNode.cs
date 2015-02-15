using Hdc.Collections.Generic;

namespace Hdc.Mercury
{
    public interface IFacilityNode<TParent> : IChild<TParent>,
                                              IContextNode<IDeviceGroup>
    {
    }
}