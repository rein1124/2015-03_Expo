namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureMiddle<TThis,
                                                 TParent,
                                                 TChild> : IBidirectionStructureParent<TThis, TChild>,
                                                           IBidirectionStructureChild<TThis, TParent>
        where TParent : IBidirectionStructureParent<TParent, TThis>
        where TChild : IBidirectionStructureChild<TChild, TThis>
        where TThis : IBidirectionStructureMiddle<TThis, TParent, TChild>
    {
    }
}