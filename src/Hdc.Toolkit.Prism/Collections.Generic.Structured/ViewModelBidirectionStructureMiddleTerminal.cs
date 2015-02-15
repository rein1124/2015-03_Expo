namespace Hdc.Collections.Generic
{
    public class ViewModelBidirectionStructureMiddleTerminal<TThis> : ViewModelBidirectionStructureMiddle<
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