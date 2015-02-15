using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hdc.Linq
{
    public static class QueryableExtensions
    {
        public static IEnumerable<T> GetPagedElements<T>(this IQueryable<T> queryable,
                                                                    int pageIndex,
                                                                    int pageSize)
        {
            return GetPagedElements(queryable, x => true, pageIndex, pageSize);
        }

        public static IEnumerable<T> GetPagedElements<T>(this IQueryable<T> queryable,
                                                                    Expression<Func<T, bool>> where,
                                                                    int pageIndex,
                                                                    int pageSize)
        {
            return queryable
                .Where(where)
                .Skip((pageIndex*pageSize))
                .Take(pageSize);
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IQueryable<T> queryable,
                                                                    int pageIndex,
                                                                    int pageSize,
                                                                    Expression<Func<T, TProperty>> orderByExpression)
        {
            return queryable.GetPagedElements(x => true, pageIndex, pageSize, orderByExpression);
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IQueryable<T> queryable,
                                                                    Expression<Func<T, bool>> where,
                                                                    int pageIndex,
                                                                    int pageSize,
                                                                    Expression<Func<T, TProperty>> orderByExpression)
        {
            return queryable.GetPagedElements(where, pageIndex, pageSize, orderByExpression, false);
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IQueryable<T> queryable,
                                                                    Expression<Func<T, bool>> where,
                                                                    int pageIndex,
                                                                    int pageSize,
                                                                    Expression<Func<T, TProperty>> orderByExpression,
                                                                    bool ascending)
        {
            return queryable
                .Where(where)
                .OrderBy(orderByExpression)
                .Skip((pageIndex*pageSize))
                .Take(pageSize);
        }

        public static int GetPageCount<T>(this IQueryable<T> source, Expression<Func<T, bool>> where, int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentException("pageSize cannot smaller than 1");
            }

            return source.Where(where).Count()/pageSize + 1;
        }

        public static IEnumerable<T> GetElements<T>(this IQueryable<T> source, Expression<Func<T, bool>> where)
        {
            return source.Where(where);
        }        
        
        
        /// <summary>
        ///   Wraps an existing object into a collection.
        ///  copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// </summary>
        public static IQueryable<T> ToQueryable<T>(this T t)
        {
            return t.ToEnumerable().AsQueryable();
        }
    }
}