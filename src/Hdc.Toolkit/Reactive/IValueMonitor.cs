using System;

namespace Hdc.Reactive
{
    public interface IValueMonitor<T> : IRetainedSubject<T>
    {
        void Observe(IObservable<T> observable);

        void Observe(IObservable<T> observable, T initialObject);

        void Observe(IObservable<T> observable, T initialObject, Action<T> action);

        void Disconnect();
    }
}