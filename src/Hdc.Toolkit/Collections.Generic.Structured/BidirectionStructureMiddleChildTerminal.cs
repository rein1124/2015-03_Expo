namespace Hdc.Collections.Generic
{
    public class BidirectionStructureMiddleChildTerminal<
        TThis,
        TParent> : BidirectionStructureMiddle<
                       TThis,
                       TParent,
                       IBidirectionStructureChildTerminal<TThis>>,
                   IBidirectionStructureMiddleChildTerminal<
                       TThis,
                       TParent>
        where TParent : IBidirectionStructureParent<
                            TParent,
                            TThis>
        where TThis : IBidirectionStructureMiddle<
                          TThis,
                          TParent,
                          IBidirectionStructureChildTerminal<TThis>>
    {
    }
}