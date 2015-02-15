using System.Collections.Generic;
using System.Collections.Specialized;

namespace Hdc.Patterns
{
    public class CollectionChangedEvent<T> :  IEvent
    {
        public NotifyCollectionChangedAction Action { get; set; }

        public IList<T> NewItems { get; set; }

        public IList<T> OldItems { get; set; }
    }
}