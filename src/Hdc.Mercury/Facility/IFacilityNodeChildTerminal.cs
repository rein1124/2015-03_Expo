namespace Hdc.Mercury
{
    public interface IFacilityNodeChildTerminal<TParent> :
        IFacilityNodeChild<IFacilityNodeChildTerminal<TParent>, TParent>
        where TParent : IFacilityNodeParent<TParent, IFacilityNodeChildTerminal<TParent>>
    {
    }
}