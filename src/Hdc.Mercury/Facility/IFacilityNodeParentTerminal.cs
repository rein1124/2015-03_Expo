namespace Hdc.Mercury
{
    public interface IFacilityNodeParentTerminal<TChild> :
        IFacilityNodeParent<IFacilityNodeParentTerminal<TChild>, TChild>
        where TChild : IFacilityNodeChild<TChild, IFacilityNodeParentTerminal<TChild>>
    {
    }
}