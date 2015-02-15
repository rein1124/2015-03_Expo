using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Hdc.Mvvm
{
    public class SplashMessageUpdatedEvent : PubSubEvent<SplashMessageUpdatedEvent>
    {
        public string Message { get; set; }

        public SplashMessageUpdatedEvent()
        {
        }

        public SplashMessageUpdatedEvent(string message = null)
        {
            Message = message;
        }
    }

    public static class SplashMessageUpdatedEventExtensions
    {
        public static SplashMessageUpdatedEvent WithTypeFullName(this SplashMessageUpdatedEvent evt, Type type)
        {
            evt.Message = type.FullName;
            return evt;
        }

        public static SplashMessageUpdatedEvent WithTypeFullName<T>(this SplashMessageUpdatedEvent evt)
        {
            return evt.WithTypeFullName(typeof (T));
        }

        public static void PublishSplashMessageUpdatedEvent(this IEventAggregator eventAggregator, SplashMessageUpdatedEvent evt)
        {
            eventAggregator
               .GetEvent<SplashMessageUpdatedEvent>()
               .Publish(evt);
        }

        public static void PublishSplashMessageUpdatedEventWithTypeFullName(this IEventAggregator eventAggregator, Type type)
        {
            eventAggregator.PublishSplashMessageUpdatedEvent(new SplashMessageUpdatedEvent().WithTypeFullName(type));
        }

        public static void PublishSplashMessageUpdatedEventWithTypeFullName<T>(this IEventAggregator eventAggregator)
        {
            eventAggregator.PublishSplashMessageUpdatedEventWithTypeFullName(typeof(T));
        }

        public static void PublishSplashMessageUpdatedEventWithTypeFullName(this IEventAggregator eventAggregator, object o)
        {
            eventAggregator.PublishSplashMessageUpdatedEventWithTypeFullName(o.GetType());
        }
    }
}