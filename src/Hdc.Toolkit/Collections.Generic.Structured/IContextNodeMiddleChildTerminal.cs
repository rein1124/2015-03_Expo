namespace Hdc.Collections.Generic
{
    public interface IContextNodeMiddleChildTerminal<TThis,
                                                     TThisContext,
                                                     TParent,
                                                     TParentContext> :
                                                         IContextNodeMiddle<
                                                             TThis,
                                                             TThisContext,
                                                             TParent,
                                                             TParentContext,
                                                             IContextNodeChildTerminal<TThis, TThisContext>,
                                                             object>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
        where TThis : IContextNodeMiddleChildTerminal<
                          TThis,
                          TThisContext,
                          TParent,
                          TParentContext>
    {
    }
}