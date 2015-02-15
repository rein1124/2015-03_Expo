namespace Hdc.Collections.Generic
{
    using System.Collections.Generic;

    public interface ISimpleTree<TData> : ITree<TData, ISimpleTree<TData>>
    {
    }
}