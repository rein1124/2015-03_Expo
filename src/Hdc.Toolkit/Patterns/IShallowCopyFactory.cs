namespace Hdc.Patterns
{
    public interface IShallowCopyFactory<T>
    {
        T Create(T target);
    }
}