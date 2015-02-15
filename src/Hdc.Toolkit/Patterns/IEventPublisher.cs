#region Using

using System;

#endregion // Using
// use ctrl+M, ctrl+O to collapse all regions

// contrib. by José F. Romaniello
// http://jfromaniello.blogspot.com/2010/04/event-aggregator-example.html

namespace Hdc.Patterns
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent sampleEvent);
        IObservable<TEvent> GetEvent<TEvent>();
    }
}
