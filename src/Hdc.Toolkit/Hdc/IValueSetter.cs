namespace Hdc
{
    public interface IValueSetter<in T>
    {
        T Value { set; }
    }
}