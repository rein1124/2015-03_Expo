using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface IComplexCompositeNode<TThis, TThisContext, TChild, TChildContext> :
        IComplexNode<TThisContext>
        where TChild : IComplexNode<TChildContext>
        where TThis : IComplexCompositeNode<TThis, TThisContext, TChild, TChildContext>
    {
        IList<TChild> Children { get; }
    }

    public interface IComplexDualNode<TThis,
                                      TThisContext,
                                      TParent,
                                      TParentContext,
                                      TChild,
                                      TChildContext> : IComplexNode<TThisContext>
        where TChild : IComplexNode<TChildContext>
        where TThis : IComplexCompositeNode<TThis,
                          TThisContext,
                          TChild,
                          TChildContext>
        where TParent : IComplexCompositeNode<TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
    {
        TParent Parent { get; set; }

        IList<TChild> Children { get; }
    }
}