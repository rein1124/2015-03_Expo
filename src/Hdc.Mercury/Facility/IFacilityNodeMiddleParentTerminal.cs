namespace Hdc.Mercury
{
    public interface IFacilityNodeMiddleParentTerminal<TThis, TChild> :
        IFacilityNodeMiddle<TThis,IFacilityNodeParentTerminal<TThis>, TChild>
        where TChild : IFacilityNodeChild<TChild, TThis>
        where TThis : IFacilityNodeMiddleParentTerminal<TThis, TChild>
    {
    }
}