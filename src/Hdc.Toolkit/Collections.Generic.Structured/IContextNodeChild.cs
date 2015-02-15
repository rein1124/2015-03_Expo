namespace Hdc.Collections.Generic
{
    public interface IContextNodeChild<TThis,
                                       TThisContext,
                                       TParent,
                                       TParentContext> :
                                           IBidirectionStructureChild<TThis, TParent>,
                                           IContextNode<TThisContext>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
        where TThis : IContextNodeChild<
                          TThis,
                          TThisContext,
                          TParent,
                          TParentContext>
    {
    }
}