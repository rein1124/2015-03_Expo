using System.ComponentModel;

namespace Hdc.Collections.Generic
{
    public interface IComplexNode<TContext>
    {
        void Initialize(TContext context, int index = 0);

        TContext Context { get; }

        int Index { get; }

        int DisplayIndex { get; }
    }
}