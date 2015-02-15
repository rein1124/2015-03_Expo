namespace Hdc
{
    public interface IValueWrapper<T>: IValueGetter<T>, IValueSetter<T>
    {
        new T Value { get; set; }
    }
}