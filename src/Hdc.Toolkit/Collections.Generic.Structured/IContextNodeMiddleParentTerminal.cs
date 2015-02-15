namespace Hdc.Collections.Generic
{
    public interface IContextNodeMiddleParentTerminal<TThis,
                                                      TThisContext,
                                                      TChild,
                                                      TChildContext> :
                                                          IContextNodeMiddle<
                                                              TThis,
                                                              TThisContext,
                                                              IContextNodeParentTerminal<TThis, TThisContext>,
                                                              object,
                                                              TChild,
                                                              TChildContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           TThis,
                           TThisContext>
        where TThis : IContextNodeMiddleParentTerminal<
                          TThis,
                          TThisContext,
                          TChild,
                          TChildContext>
    {
    }
}