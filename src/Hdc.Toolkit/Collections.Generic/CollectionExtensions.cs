using System;
using System.Collections.Generic;
using System.Linq;

namespace Hdc.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> enumerable)
        {
            foreach (var enumerable1 in enumerable)
            {
                list.Add(enumerable1);
            }
        }

        public static void RemoveWhere<T>(this ICollection<T> list, Func<T, bool> condition)
        {
            var js = list.Where(condition).ToList();
            foreach (var j in js)
            {
                list.Remove(j);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> removeItems)
        {
            foreach (var removeItem in removeItems)
            {
                collection.Remove(removeItem);
            }

            /*           for (int i = 0; i < removeItems.Count; i++)
            {
                var item = removeItems[i];
                collection.Remove(item);
            }*/
        }
    }
}