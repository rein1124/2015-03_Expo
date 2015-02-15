using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Hdc.Prism.Events
{
    public static class EventAggregatorExtensions
    {
        public static void Publish<TMessageType>(this IEventAggregator aggregator, TMessageType message)
            where TMessageType : PubSubEvent<TMessageType>, new()
        {
            aggregator.Publish<TMessageType, TMessageType>(message);
        }

        public static void Publish<TMessageType>(this IEventAggregator aggregator)
            where TMessageType : PubSubEvent<TMessageType>, new()
        {
// degenerate case where content of message doesn't matter; type alone is sufficient
            aggregator.Publish<TMessageType, TMessageType>(default(TMessageType));
        }

        public static void Publish<TEventType, TPayload>(this IEventAggregator aggregator, TPayload payload)
            where TEventType : PubSubEvent<TPayload>, new()
        {
            if (null == aggregator) return;
            var message = aggregator.GetEvent<TEventType>();
            if (null == message) return;
            message.Publish(payload);
        }

        public static void Subscribe<TMessageType>(this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : PubSubEvent<TMessageType>, new()
        {
            aggregator.Subscribe<TMessageType, TMessageType>(action);
        }

        public static void Subscribe<TEventType, TPayload>(this IEventAggregator aggregator, Action<TPayload> action)
            where TEventType : PubSubEvent<TPayload>, new()
        {
            if (null == aggregator)
                return;
            var @event = aggregator.GetEvent<TEventType>();
            if (null == @event)
                return;
            @event.Subscribe(action);
        }
    }
}