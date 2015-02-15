namespace Hdc
{
    public interface ICustomEvent<out TSource, out TArgs>
    {
        TSource Source { get; }
        TArgs EventArgs { get; }
    }
}