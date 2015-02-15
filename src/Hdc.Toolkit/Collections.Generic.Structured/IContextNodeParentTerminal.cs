namespace Hdc.Collections.Generic
{
    public interface IContextNodeParentTerminal<TChild,
                                                TChildContext> :
                                                    IContextNodeParent<IContextNodeParentTerminal<
                                                        TChild,
                                                        TChildContext>,
                                                        object,
                                                        TChild,
                                                        TChildContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           IContextNodeParentTerminal<
                           TChild,
                           TChildContext>,
                           object>
    {
    }
}