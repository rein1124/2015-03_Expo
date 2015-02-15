namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureChild<TThis, TParent>: IChild<TParent>
        where TParent : IBidirectionStructureParent<TParent, TThis>
        where TThis : IBidirectionStructureChild<TThis, TParent>
    {
    }
}