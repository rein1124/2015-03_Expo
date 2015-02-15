namespace Hdc.Collections.Generic
{
    public interface IContextNodeParent<TThis,
                                        TThisContext,
                                        TChild,
                                        TChildContext> :
                                            IBidirectionStructureParent<TThis, TChild>,
                                            IContextNode<TThisContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           TThis,
                           TThisContext>
        where TThis : IContextNodeParent<
                          TThis,
                          TThisContext,
                          TChild,
                          TChildContext>
    {
    }
}