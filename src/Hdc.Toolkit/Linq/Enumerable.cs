using System;
using System.Collections.Generic;
using System.Linq;

namespace Hdc.Linq
{
    public static class Enumerable
    {
        public static bool Contains<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> func)
        {
            foreach (var source in enumerable)
            {
                if (!func(source))
                    return false;
            }
            return true;
        }

/*        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> action)
        {
            foreach (var source in enumerable)
            {
                action(source);
            }
        }*/

        public static void ForEach<TSource, TPara>(this IEnumerable<TSource> actors,
                                                   IEnumerable<TPara> paras,
                                                   Action<TSource, TPara> action)
        {
            var actorList = actors.ToList();
            var paraList = paras.ToList();

            if (actorList.Count > paraList.Count)
                throw new InvalidOperationException("actors count is more than paras count");

            for (int i = 0; i < actorList.Count; i++)
            {
                var actor = actorList[i];
                var para = paraList[i];
                action(actor, para);
            }
        }

        public static void OrderBySelf<TSource, TKey>(this IList<TSource> sources,
                                                      System.Func<TSource, TKey> keySelector)
        {
            var items = sources.ToList();
            sources.Clear();
            var sorteds = items.OrderBy(keySelector);
            sorteds.ForEach(sources.Add);
        }

/*
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> enumerable)
        {
            return enumerable.Count() == 0;
        }
*/
    }
}