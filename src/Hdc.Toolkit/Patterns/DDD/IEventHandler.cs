using System;
using Hdc.Patterns;

namespace Hdc.Patterns
{
    /// <summary>
    /// Represents a class that will react to a given event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<T>   where T : IEvent
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="event"></param>
//        void Handle(T @event);

//        IObservable<T> ObserverOn(IObservable<T> eventOb);

//        IScheduler Scheduler { get; }

        void Handle(IObservable<T> events);
    }
}