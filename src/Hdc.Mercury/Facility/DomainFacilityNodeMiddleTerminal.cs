namespace Hdc.Mercury
{
    public class DomainFacilityNodeMiddleTerminal<TThis> :
        DomainFacilityNodeMiddle<TThis, IFacilityNodeParentTerminal<TThis>, IFacilityNodeChildTerminal<TThis>>,
        IFacilityNodeMiddleTerminal<TThis>
        where TThis : class, IFacilityNodeMiddleTerminal<TThis>
    {
    }
}