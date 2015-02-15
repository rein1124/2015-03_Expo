namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Serialization;

    public class SimpleTree<TData> : Tree<TData, ISimpleTree<TData>>, ISimpleTree<TData>
    {
        public SimpleTree()
        {
        }

        public SimpleTree(params ISimpleTree<TData>[] children) : base(children)
        {
        }

        public SimpleTree(TData data, params ISimpleTree<TData>[] children) : base(data, children)
        {
        }
    }
}