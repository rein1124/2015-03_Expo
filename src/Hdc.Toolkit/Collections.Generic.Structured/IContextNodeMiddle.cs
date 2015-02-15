namespace Hdc.Collections.Generic
{
    public interface IContextNodeMiddle<TThis,
                                        TThisContext,
                                        TParent,
                                        TParentContext,
                                        TChild,
                                        TChildContext> : IBidirectionStructureMiddle<
                                                             TThis,
                                                             TParent,
                                                             TChild>,
                                                         IContextNodeChild<
                                                             TThis,
                                                             TThisContext,
                                                             TParent,
                                                             TParentContext>,
                                                         IContextNodeParent<
                                                             TThis,
                                                             TThisContext,
                                                             TChild,
                                                             TChildContext>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           TThis,
                           TThisContext>
        where TThis : IContextNodeMiddle<
                          TThis,
                          TThisContext,
                          TParent,
                          TParentContext,
                          TChild,
                          TChildContext>
    {
    }
}