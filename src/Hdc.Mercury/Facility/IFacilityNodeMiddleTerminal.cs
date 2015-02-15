namespace Hdc.Mercury
{
    public interface IFacilityNodeMiddleTerminal<TThis> :
        IFacilityNodeMiddle<TThis, IFacilityNodeParentTerminal<TThis>, IFacilityNodeChildTerminal<TThis>>
        where TThis : IFacilityNodeMiddleTerminal<TThis>
    {
    }
}