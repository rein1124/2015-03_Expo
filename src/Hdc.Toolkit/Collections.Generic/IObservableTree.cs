using System.Collections.Specialized;
using System.ComponentModel;

namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ComponentModel;

 

    public interface IObservableTree<TData, TTree> : ITree<TData, TTree>
            where TTree : ITree<TData, TTree>, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }
}