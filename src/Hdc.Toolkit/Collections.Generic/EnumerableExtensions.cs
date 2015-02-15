using System.Collections.ObjectModel;
using Hdc;

namespace Hdc.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

 

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Transforms the given enumeration into a <see cref="ReadOnlyCollection&lt;T&gt;" />.
        /// </summary>
        /// <typeparam name="T">The type of the members in <paramref name="this"/>.</typeparam>
        /// <param name="this">The enumeration to transform.</param>
        /// <returns>A <see cref="ReadOnlyCollection&lt;T&gt;" /> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> @this)
        {
            @this.CheckParameterForNull("@this");
            return new ReadOnlyCollection<T>(new List<T>(@this));
        }
       


        public static IEnumerable<TChild> CreateMany<TParent, TChild>(this TParent parent,
                                                                      Func<TParent, IList<TChild>> getList,
                                                                      int count,
                                                                      Func<int, TChild> createChild)
        {
            for (int i = 0; i < count; i++)
            {
                var child = createChild(i);
                var children = getList(parent);
                children.Add(child);
                yield return child;
            }
        }

        public static IEnumerable<TChild> CreateMany<TParent, TChild>(this IEnumerable<TParent> parent,
                                                                      Func<TParent, IList<TChild>> getList,
                                                                      int count,
                                                                      Func<int, TChild> createChild)
        {
            foreach (var item in parent)
            {
                //                for (int i = 0; i < t1Count; i++)
                //                {
                //                    var child = createT1(i);
                //                    var children = getList(item);
                //                    children.Add(child);
                //                    yield return child;
                //                }
                var enumerable = item.CreateMany(getList, count, createChild);

                foreach (var child in enumerable)
                {
                    yield return child;
                }
            }
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var oc = new ObservableCollection<T>();
            oc.AddRange(source);
            return oc;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> func)
        {
            foreach (var source in enumerable)
            {
                if (!func(source))
                    return false;
            }
            return true;
        }


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> lst)
        {
            if (lst == null) return true;
            return !lst.Any();
        }
    }
}