namespace Hdc
{
    public interface IValueGetter<out T>
    {
        T Value { get; }
    }
}