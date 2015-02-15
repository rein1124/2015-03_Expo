/*using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Hdc
{
    public class ObjectWrapper<T> : IObjectWrapper<T>
    {
        private T _object;

        private Subject<T> _objectChangedEvent;

        public T Object
        {
            get { return _object; }
            set
            {
                if (Equals(_object, value))
                    return;

                _object = value;

                _objectChangedEvent.OnNext(value);
            }
        }

        public IObservable<T> ObjectChangedEvent
        {
            get { return _objectChangedEvent ?? (_objectChangedEvent = new Subject<T>()); }
        }
    }
}*/