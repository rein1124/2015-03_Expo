using System.Reactive.Subjects;

namespace Hdc.Reactive
{
    public interface IRetainedSubject<T> : ISubject<T>, IValueObservable<T>
    {
    }
}