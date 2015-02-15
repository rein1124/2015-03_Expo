namespace Hdc.Collections.Generic
{
    public interface IBidirectionStructureParentTerminal<TChild> : IBidirectionStructureParent<
                                                                       IBidirectionStructureParentTerminal<TChild>,
                                                                       TChild>
        where TChild : IBidirectionStructureChild<TChild, IBidirectionStructureParentTerminal<TChild>>
    {
    }
}