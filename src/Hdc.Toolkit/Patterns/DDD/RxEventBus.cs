using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Hdc.Patterns;

namespace Hdc.Patterns
{
    public class RxEventBus : IEventBus, IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

//        private readonly ConcurrentDictionary<Type, object> _obs
//            = new ConcurrentDictionary<Type, object>();


        public void Publish<T>(T @event) where T : IEvent
        {
            object subject;
            if (_subjects.TryGetValue(typeof (T), out subject))
            {
                ((ISubject<T>) subject).OnNext(@event);
            }
        }

        public IObservable<TEvent> GetEvents<TEvent>() where TEvent : IEvent
        {
            var subject = (ISubject<TEvent>) _subjects.GetOrAdd(
                typeof (TEvent), t => new Subject<TEvent>());
            return subject;
        }
    }
}