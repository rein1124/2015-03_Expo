namespace Hdc.Reactive
{
    public interface IValueObservable<T> : IRetainedObservable<T>
    {
        new T Value { get; set; }
    }
}