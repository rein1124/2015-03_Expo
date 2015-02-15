namespace Hdc.Patterns
{
    //see jhicks-Db4oFramework-d4f0cbb.zip
    public interface IQueryableUnitOfWorkFactory
    {
        IQueryableUnitOfWork OpenSession();
    }
}