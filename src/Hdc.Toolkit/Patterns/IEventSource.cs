using System;

namespace Hdc.Patterns
{
    public interface IEventSource
    {
        //        void RegisterEvent<TEvent>() where TEvent : class;

        void RegisterEvent<TSourceEvent, TTargetEvent>(
            Func<TSourceEvent,
                TTargetEvent> convertFunc)
            where TSourceEvent : class
            where TTargetEvent : IEvent;

        //        void UnRegisterEvent<TEvent>() where TEvent : class;
//        void Start(string endpoint);
//
//        void Stop();
//
//        bool IsStarted { get; }
    }
}