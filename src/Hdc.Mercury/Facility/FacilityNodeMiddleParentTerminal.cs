namespace Hdc.Mercury
{
    public abstract class FacilityNodeMiddleParentTerminal<TThis, TChild> :
        FacilityNodeMiddle<TThis, IFacilityNodeParentTerminal<TThis>, TChild>,
        IFacilityNodeMiddleParentTerminal<TThis, TChild>
        where TChild : IFacilityNodeChild<TChild, TThis>
        where TThis : class, IFacilityNodeMiddleParentTerminal<TThis, TChild>
    {
    }
}