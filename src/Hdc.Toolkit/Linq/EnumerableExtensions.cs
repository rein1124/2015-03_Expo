using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hdc.Collections.Generic;

namespace Hdc.Linq
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var oc = new ObservableCollection<T>();
            oc.AddRange(source);
            return oc;
        }

        public static int GetPageCount<T>(this IEnumerable<T> source, int pageSize)
        {
            return source.Count()/pageSize + 1;
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IEnumerable<T> queryable,
                                                                  int pageIndex,
                                                                  int pageSize,
                                                                  Func<T, TProperty> orderByExpression)
        {
            return queryable.GetPagedElements(x => true, pageIndex, pageSize, orderByExpression);
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IEnumerable<T> queryable,
                                                                    Func<T, bool> where,
                                                                    int pageIndex,
                                                                    int pageSize,
                                                                    Func<T, TProperty> orderByExpression)
        {
            return queryable.GetPagedElements(where, pageIndex, pageSize, orderByExpression, false);
        }

        public static IEnumerable<T> GetPagedElements<T, TProperty>(this IEnumerable<T> queryable,
                                                                    Func<T, bool> where,
                                                                    int pageIndex,
                                                                    int pageSize,
                                                                    Func<T, TProperty> orderByExpression,
                                                                    bool ascending)
        {
            return queryable
                .Where(where)
                .OrderBy(orderByExpression)
                .Skip((pageIndex*pageSize))
                .Take(pageSize);
        }        
        
        
        /// <summary>
        ///   Wraps an existing object into a collection.
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// </summary>
        public static IEnumerable<T> ToEnumerable<T>(this T t)
        {
            return new[] { t };
        }


        public static void Run<T>(this IEnumerable<T> enumerable)
        {
            foreach (var e in enumerable)
            {
                //var x = e;
            }
        }

        // 2014-04-27, rein
        // ref: http://blogs.msdn.com/b/pfxteam/archive/2012/03/05/10278165.aspx
        // ref: http://stackoverflow.com/questions/11879967/best-way-to-convert-callback-based-async-method-to-awaitable-task
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            return Task.WhenAll(
                from item in source
                select body(item));
        }
    }
}