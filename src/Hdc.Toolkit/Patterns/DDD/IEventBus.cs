using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Patterns
{
    /// <summary>
    /// A bus allowing events to be published.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publish an event (notify all handler interested by this type of event).
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
