namespace Hdc.Collections.Generic
{
    public interface IContextNodeChildTerminal<TParent,
                                               TParentContext> :
                                                   IContextNodeChild<IContextNodeChildTerminal<
                                                       TParent,
                                                       TParentContext>,
                                                       object,
                                                       TParent,
                                                       TParentContext>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            IContextNodeChildTerminal<TParent,TParentContext>,
                            object>
    {
    }
}