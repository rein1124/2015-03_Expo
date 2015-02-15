namespace Hdc.Mercury
{
    public interface IValueCommitExecutor<T>
    {
        void Commit(T value);
    }
}