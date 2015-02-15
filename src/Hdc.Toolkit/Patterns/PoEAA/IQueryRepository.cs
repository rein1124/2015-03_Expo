using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hdc.Patterns
{
    public interface IQueryRepository<T> where T : class
    {
        T GetById(long id);

        Task<T> GetByIdAsync(long id);


        /// <summary>
        /// Return strongly typed IQueryable
        /// </summary>
        IQueryable<T> GetQuery();

        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <typeparam name="T">the entity type to load</typeparam>
        /// <param name="where">where condition</param>
        /// <returns>the loaded entity</returns>
        T Get(Expression<Func<T, bool>> whereCondition);

        Task<T> GetAsync(Expression<Func<T, bool>> whereCondition);

        IQueryable<T> GetMany(Expression<Func<T, bool>> whereCondition);

        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> whereCondition);

        /*       /// <summary>
               /// Provides explicit loading of object properties
               /// </summary>
               void LoadProperty(T entity, Expression<Func<T, object>> selector);*/

        /// <summary>
        /// Returns all entities for a given type
        /// </summary>
        /// <returns>All entities</returns>
        List<T> GetAll();

        Task<List<T>> GetAllAsync();


        //        IEnumerable<T> GetPagedElements(int pageIndex, int pageCount);
        IEnumerable<T> GetPagedElements<TProperty>(int pageIndex, int pageCount,
                                                   Expression<Func<T, TProperty>> orderByExpression, bool ascending);


        IEnumerable<T> GetPagedElements<TProperty>(Expression<Func<T, bool>> where, int pageIndex, int pageCount,
                                                   Expression<Func<T, TProperty>> orderByExpression, bool ascending);


        IEnumerable<T> GetElements<TProperty>(Expression<Func<T, bool>> where);

        IEnumerable<T> GetElements<TProperty>(Expression<Func<T, bool>> where,
                                              Expression<Func<T, TProperty>> orderByExpression, bool ascending);



        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IQueryable<T> GetQuery(ISpecification<T> criteria);


        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        IEnumerable<T> Find(ISpecification<T> criteria);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        T FindOne(ISpecification<T> criteria);

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(ISpecification<T> criteria);
    }
}