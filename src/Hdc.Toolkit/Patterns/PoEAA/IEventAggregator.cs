using System;
using Hdc.Patterns;

namespace Hdc.Patterns
{
    public interface IEventAggregator
    {
        IObservable<TEvent> GetEvents<TEvent>() where TEvent : IEvent;
    }
}