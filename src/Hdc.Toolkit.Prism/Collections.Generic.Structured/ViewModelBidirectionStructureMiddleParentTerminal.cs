namespace Hdc.Collections.Generic
{
    public class ViewModelBidirectionStructureMiddleParentTerminal<
        TThis,
        TChild> : ViewModelBidirectionStructureMiddle<
                      TThis,
                      IBidirectionStructureParentTerminal<TThis>,
                      TChild>,
                  IBidirectionStructureMiddleParentTerminal<
                      TThis,
                      TChild>
        where TChild : IBidirectionStructureChild<
                           TChild,
                           TThis>
        where TThis : IBidirectionStructureMiddle<
                          TThis,
                          IBidirectionStructureParentTerminal<TThis>,
                          TChild>
    {
    }
}