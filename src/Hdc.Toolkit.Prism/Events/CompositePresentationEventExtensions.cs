//ref: https://gist.github.com/1781190
//http://richarddingwall.name/2010/03/30/reactive-extensions-and-prism-linq-to-your-event-aggregator/

using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Hdc.Prism.Events
{
    public static class CompositePresentationEventExtensions
    {
        public static IObservable<TEventArgs> AsObservable<TEventArgs>(
            this PubSubEvent<TEventArgs> @event)
        {
            return new CompositePresentationEventObservable<TEventArgs>(@event);
        }
    }

    public class CompositePresentationEventObservable<TEventArgs> : IObservable<TEventArgs>
    {
        private readonly PubSubEvent<TEventArgs> @event;

        public CompositePresentationEventObservable(PubSubEvent<TEventArgs> @event)
        {
            if (@event == null) throw new ArgumentNullException("event");
            this.@event = @event;
        }

        public IDisposable Subscribe(IObserver<TEventArgs> observer)
        {
            return new CompositePresentationEventSubscription<TEventArgs>(@event, observer);
        }
    }

    public class CompositePresentationEventSubscription<TEventArgs> : IDisposable
    {
        private readonly IObserver<TEventArgs> observer;
        private readonly PubSubEvent<TEventArgs> @event;

        public CompositePresentationEventSubscription(PubSubEvent<TEventArgs> @event,
                                                      IObserver<TEventArgs> observer)
        {
            if (@event == null) throw new ArgumentNullException("event");
            if (observer == null) throw new ArgumentNullException("observer");

            this.@event = @event;
            this.observer = observer;

            @event.Subscribe(observer.OnNext, true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                @event.Unsubscribe(observer.OnNext);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CompositePresentationEventSubscription()
        {
            Dispose(false);
        }
    }
}