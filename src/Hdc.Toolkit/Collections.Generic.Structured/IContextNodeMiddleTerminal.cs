namespace Hdc.Collections.Generic
{
    public interface IContextNodeMiddleTerminal<TThis,
                                                TThisContext> :
                                                    IContextNodeMiddle<
                                                        TThis,
                                                        TThisContext,
                                                        IContextNodeParentTerminal<TThis, TThisContext>,
                                                        object,
                                                        IContextNodeChildTerminal<TThis, TThisContext>,
                                                        object>
        where TThis : IContextNodeMiddleTerminal<TThis, TThisContext>
    {
    }
}