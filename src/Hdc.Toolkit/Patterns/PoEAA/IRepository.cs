namespace Hdc.Patterns
{
    public interface IRepository<T> : IQueryRepository<T> where T : class
    {
        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        void Add(T entity);

        /// <summary>
        /// Update entity within the repository
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>The updated entity</returns>
        void Update(T entity);

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);


        void DeleteById(long id);


        IUnitOfWork UnitOfWork { get; }
    }
}