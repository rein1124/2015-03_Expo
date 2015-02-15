using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Hdc.Collections.Specialized
{
    public class NotifyCollectionChangedEventArgs<T>
    {
        private IList<T> _newItems = new List<T>();

        private IList<T> _oldItems = new List<T>();

        public NotifyCollectionChangedEventArgs()
        {
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            Action = action;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action,
                                                IList<T> newItems)
        {
            Action = action;
            NewItems = newItems;
        }

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action,
                                                IList<T> newItems,
                                                IList<T> oldItems)
        {
            Action = action;
            NewItems = newItems;
            OldItems = oldItems;
        }

        public NotifyCollectionChangedAction Action { get; set; }

        public IList<T> NewItems
        {
            get { return _newItems; }
            set { _newItems = value; }
        }

        public IList<T> OldItems
        {
            get { return _oldItems; }
            set { _oldItems = value; }
        }
    }
}