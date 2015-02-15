using System;
using System.Collections.Generic;
using System.Linq;

namespace Hdc.Collections.Generic
{
    public static class EnumerableExtensions2
    {
        //see: Best algorithm for synchronizing two IList in C# 2.0
        //http://stackoverflow.com/questions/161432/best-algorithm-for-synchronizing-two-ilist-in-c-2-0
        public static void CompareSortedCollections<T>(IEnumerable<T> source, IEnumerable<T> destination,
                                                       IComparer<T> comparer, Action<T> onLeftOnly,
                                                       Action<T> onRightOnly, Action<T, T> onBoth)
        {
            EnumerableIterator<T> sourceIterator = new EnumerableIterator<T>(source);
            EnumerableIterator<T> destinationIterator = new EnumerableIterator<T>(destination);

            while (sourceIterator.HasCurrent && destinationIterator.HasCurrent)
            {
                // While LHS < RHS, the items in LHS aren't in RHS
                while (sourceIterator.HasCurrent &&
                       (comparer.Compare(sourceIterator.Current, destinationIterator.Current) < 0))
                {
                    onLeftOnly(sourceIterator.Current);
                    sourceIterator.MoveNext();
                }

                // While RHS < LHS, the items in RHS aren't in LHS
                while (sourceIterator.HasCurrent && destinationIterator.HasCurrent &&
                       (comparer.Compare(sourceIterator.Current, destinationIterator.Current) > 0))
                {
                    onRightOnly(destinationIterator.Current);
                    destinationIterator.MoveNext();
                }

                // While LHS==RHS, the items are in both
                while (sourceIterator.HasCurrent && destinationIterator.HasCurrent &&
                       (comparer.Compare(sourceIterator.Current, destinationIterator.Current) == 0))
                {
                    onBoth(sourceIterator.Current, destinationIterator.Current);
                    sourceIterator.MoveNext();
                    destinationIterator.MoveNext();
                }
            }

            // Mop up.
            while (sourceIterator.HasCurrent)
            {
                onLeftOnly(sourceIterator.Current);
                sourceIterator.MoveNext();
            }

            while (destinationIterator.HasCurrent)
            {
                onRightOnly(destinationIterator.Current);
                destinationIterator.MoveNext();
            }
        }


        public static void CopyValuesTo<T>(this IEnumerable<T> sources,
                                           IEnumerable<T> targets,
                                           Action<T, T> onSourceToTarget)
        {
            EnumerableIterator<T> sourceIterator = new EnumerableIterator<T>(sources);
            EnumerableIterator<T> destinationIterator = new EnumerableIterator<T>(targets);

            while (sourceIterator.HasCurrent && destinationIterator.HasCurrent)
            {
                onSourceToTarget(sourceIterator.Current, destinationIterator.Current);
                sourceIterator.MoveNext();
                destinationIterator.MoveNext();
            }
        }

        public static void CopyValuesFrom<T>(this IEnumerable<T> targets,
                                             IEnumerable<T> sources,
                                             Action<T, T> onTargetToSource)
        {
            sources.CopyValuesTo<T>(targets, new Action<T, T>((s, t) => onTargetToSource(t, s)));
        }

        public static void CopyValuesTo<TSource, TTarget>(this IEnumerable<TSource> sources,
                                                          IEnumerable<TTarget> targets,
                                                          Action<TSource, TTarget> onSourceToTarget)
        {
            EnumerableIterator<TSource> sourceIterator = new EnumerableIterator<TSource>(sources);
            EnumerableIterator<TTarget> destinationIterator = new EnumerableIterator<TTarget>(targets);

            while (sourceIterator.HasCurrent && destinationIterator.HasCurrent)
            {
                onSourceToTarget(sourceIterator.Current, destinationIterator.Current);
                sourceIterator.MoveNext();
                destinationIterator.MoveNext();
            }
        }

        public static void CopyValuesFrom<TTarget, TSource>(this IEnumerable<TTarget> targets,
                                                            IEnumerable<TSource> sources,
                                                            Action<TTarget, TSource> onTargetToSource)
        {
            sources.CopyValuesTo<TSource, TTarget>(targets,
                                                   new Action<TSource, TTarget>((s, t) => onTargetToSource(t, s)));
        }
    }
}