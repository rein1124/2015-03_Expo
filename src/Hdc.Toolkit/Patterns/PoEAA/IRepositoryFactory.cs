namespace Hdc.Patterns
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : class;
    }
}