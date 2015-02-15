namespace Hdc.Patterns
{
    public interface IQueryableUnitOfWorkProvider
    {
        IQueryableUnitOfWork QueryableUnitOfWork { get; }
    }
}