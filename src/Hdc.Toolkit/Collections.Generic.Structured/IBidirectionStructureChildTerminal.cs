namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureChildTerminal<TParent> : IBidirectionStructureChild<
                                                                       IBidirectionStructureChildTerminal<TParent>,
                                                                       TParent>
        where TParent : IBidirectionStructureParent<TParent, IBidirectionStructureChildTerminal<TParent>>
    {
    }
}