namespace Hdc.Patterns
{
    public interface IShallowCopyable<in T>
    {
        void CopyTo(T target);

        void CopyFrom(T source);
    }
}