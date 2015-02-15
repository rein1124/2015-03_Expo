using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public interface ICompositeNode<TContext,
                                    TChild,
                                    TChildContext> : IComplexNode<TContext>
        where TChild : IComplexNode<TChildContext>
    {
        IList<TChild> Children { get; }
    }
}