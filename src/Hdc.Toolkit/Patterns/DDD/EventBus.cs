using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using Hdc.Patterns;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Patterns
{
    public class EventBus : IEventBus, IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }


        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventHost = GetOrAddSubject<TEvent>();

            if (!eventHost.IsInitialized)
            {
                eventHost.Initialize(ServiceLocator);
            }

            eventHost.Subject.OnNext(@event);

            return;

/*            object subjectObj;

            if (!_subjects.TryGetValue(typeof (TEvent), out subjectObj))
            {
                var subject = new Subject<TEvent>();
                _subjects.GetOrAdd(typeof (TEvent), subject);
                subjectObj = subject;

                var handlers = ServiceLocator.GetAllInstances<IEventHandler<TEvent>>();

                foreach (var handler in handlers)
                {
                    handler.Handle(subject);

                    continue;
                    /*    if (handler.Scheduler != null)
                    {
//                        subject.SubscribeOn(handler.Scheduler).Subscribe(handler);
                        subject.ObserveOnDispatcher().Subscribe(handler);
                    }
                    else
                    {
                        subject.Subscribe(handler);
                    }#1#
                }
            }

            ((ISubject<TEvent>) subjectObj).OnNext(@event);*/
        }

        public IObservable<TEvent> GetEvents<TEvent>() where TEvent : IEvent
        {
//            var subject = (ISubject<TEvent>) _subjects.GetOrAdd(
//                typeof (TEvent), arg => new Subject<TEvent>());
//            return subject;

            var eventHost = GetOrAddSubject<TEvent>();
            return eventHost.Subject;
        }

        private EventHost<TEvent> GetOrAddSubject<TEvent>() where TEvent : IEvent
        {
            object subjectObj;

            if (!_subjects.TryGetValue(typeof (TEvent), out subjectObj))
            {
                var subject = new EventHost<TEvent>();
                _subjects.GetOrAdd(typeof (TEvent), subject);
                subjectObj = subject;
            }

            return (EventHost<TEvent>) subjectObj;
        }

        private class EventHost<TEvent> where TEvent : IEvent
        {
            public EventHost()
            {
                Subject = new Subject<TEvent>();
            }

            public ISubject<TEvent> Subject { get; private set; }

            public bool IsInitialized { get; private set; }

            public void Initialize(IServiceLocator serviceLocator)
            {
                if (IsInitialized)
                {
                    throw new InvalidOperationException("EventHost has initialized, cannot initialize twice.");
                }

                var handlers = serviceLocator.GetAllInstances<IEventHandler<TEvent>>();

                foreach (var handler in handlers)
                {
                    handler.Handle(Subject);
                }

                IsInitialized = true;
            }
        }
    }
}