using System;
using System.Collections.Generic;

namespace Hdc.Collections.Generic
{
    public static class SimpleTreeExtension
    {
        public static IList<ISimpleTree<TData>> Traverse<TData>(ISimpleTree<TData> tree)
        {
            return TreeExtensions.Traverse<TData, ISimpleTree<TData>>(tree);
        }

        public static IList<ISimpleTree<TData>> Traverse<TData>(
                ISimpleTree<TData> tree, Action<ISimpleTree<TData>> preAction)
        {
            return TreeExtensions.Traverse<TData, ISimpleTree<TData>>(tree, preAction);
        }

        public static IList<ISimpleTree<TData>> Traverse<TData>(
                ISimpleTree<TData> tree, Action<ISimpleTree<TData>> preAction, Action<ISimpleTree<TData>> postAction)
        {
            return TreeExtensions.Traverse<TData, ISimpleTree<TData>>(tree, preAction, postAction);
        }
    }
}