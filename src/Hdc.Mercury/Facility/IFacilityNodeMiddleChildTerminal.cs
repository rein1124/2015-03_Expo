namespace Hdc.Mercury
{
    public interface IFacilityNodeMiddleChildTerminal<TThis, TParent> :
        IFacilityNodeMiddle<TThis, TParent, IFacilityNodeChildTerminal<TThis>>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TThis : IFacilityNodeMiddleChildTerminal<TThis, TParent>
    {
    }
}