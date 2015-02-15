#region Using

using System;

#endregion // Using
// use ctrl+M, ctrl+O to collapse all regions

// contrib. by José F. Romaniello
// http://jfromaniello.blogspot.com/2010/04/event-aggregator-example.html

namespace Hdc.Patterns
{
    #region Documentation
    /// <summary>
    /// Classic Pub/Sub implementation
    /// </summary>
    #endregion // Documentation
    public static class EventPublisherHost
    {
        #region Private / Protected Fields

        private static readonly IEventPublisher s_eventPublisher = new EventPublisher();

        #endregion // Private / Protected Fields

        #region Methods

        #region Documentation
        /// <summary>
        /// Get event producer
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        #endregion // Documentation
        public static IObservable<TEvent> GetEvent<TEvent>()
        {
            return s_eventPublisher.GetEvent<TEvent>();
        }

        #region Documentation
        /// <summary>
        /// Produce event
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="sampleEvent"></param>
        #endregion // Documentation
        public static void Publish<TEvent>(TEvent sampleEvent)
        {
            s_eventPublisher.Publish<TEvent>(sampleEvent);
        }

        #endregion // Methods
    }
}
