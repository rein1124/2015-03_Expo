#region Using

using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using System.Reactive.Subjects;

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
    public class EventPublisher : IEventPublisher
    {
        #region Private / Protected Fields

        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

        #endregion // Private / Protected Fields

        #region IEventPublisher Members

        #region Documentation
        /// <summary>
        /// Get event producer
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        #endregion // Documentation
        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = (ISubject<TEvent>)_subjects.GetOrAdd(
                typeof(TEvent), t => new Subject<TEvent>());
            return subject;
        }

        #region Documentation
        /// <summary>
        /// Produce event
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="sampleEvent"></param>
        #endregion // Documentation
        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;
            if (_subjects.TryGetValue(typeof(TEvent), out subject))
            {
                ((ISubject<TEvent>)subject).OnNext(sampleEvent);
            }
        }

        #endregion // IEventPublisher Members
    }

}
