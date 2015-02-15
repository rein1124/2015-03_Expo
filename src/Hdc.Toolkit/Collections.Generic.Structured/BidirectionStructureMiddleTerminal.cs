namespace Hdc.Collections.Generic
{
    public class BidirectionStructureMiddleTerminal<TThis> : BidirectionStructureMiddle<
                                                                 TThis,
                                                                 IBidirectionStructureParentTerminal<TThis>,
                                                                 IBidirectionStructureChildTerminal<TThis>>
        where TThis : IBidirectionStructureMiddle<
                          TThis,
                          IBidirectionStructureParentTerminal<TThis>,
                          IBidirectionStructureChildTerminal<TThis>>
    {
    }
}