namespace Hdc.Collections.Generic
{
    public class ViewModelBidirectionStructureMiddleChildTerminal<
        TThis,
        TParent> : ViewModelBidirectionStructureMiddle<
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