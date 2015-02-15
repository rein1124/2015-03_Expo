namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureMiddleChildTerminal<TThis,
                                                              TParent> : IBidirectionStructureMiddle<
                                                                             TThis,
                                                                             TParent,
                                                                             IBidirectionStructureChildTerminal<TThis>>
        where TParent : IBidirectionStructureParent<TParent, TThis>
        where TThis : IBidirectionStructureMiddle<TThis, TParent, IBidirectionStructureChildTerminal<TThis>>
    {
    }
}