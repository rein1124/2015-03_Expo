namespace Hdc.Mercury
{
    public class DomainFacilityNodeMiddleParentTerminal<TThis, TChild> :
        DomainFacilityNodeMiddle<TThis, IFacilityNodeParentTerminal<TThis>, TChild>,
        IFacilityNodeMiddleParentTerminal<TThis, TChild>
        where TChild : IFacilityNodeChild<TChild, TThis>
        where TThis : class, IFacilityNodeMiddleParentTerminal<TThis, TChild>
    {
    }
}