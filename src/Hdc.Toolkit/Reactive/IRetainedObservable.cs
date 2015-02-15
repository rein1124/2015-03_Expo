using System;

namespace Hdc.Reactive
{
    public interface IRetainedObservable<out T> : IObservable<T>, IValueGetter<T>
    {
    }
}