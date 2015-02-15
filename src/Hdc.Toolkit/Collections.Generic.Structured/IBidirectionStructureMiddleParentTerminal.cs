namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureMiddleParentTerminal<TThis,
                                                               TChild> : IBidirectionStructureMiddle<
                                                                             TThis,
                                                                             IBidirectionStructureParentTerminal<TThis>,
                                                                             TChild>
        where TChild : IBidirectionStructureChild<TChild, TThis>
        where TThis : IBidirectionStructureMiddle<TThis, IBidirectionStructureParentTerminal<TThis>, TChild>
    {
    }
}