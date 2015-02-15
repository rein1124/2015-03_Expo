//MEF_Beta_2.zip
// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Hdc.Collections
{
    public static class EnumerableExtensions2
    {
        public static int Count(this IEnumerable source)
        {
            int count = 0;

            foreach (object o in source)
            {
                count++;
            }

            return count;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerable source)
        {
            foreach (object value in source)
            {
                yield return (T)value;
            }
        }

        public static List<object> ToObjectList(this IEnumerable source)
        {
            var enumerable = source.ToEnumerable<object>();

            return System.Linq.Enumerable.ToList(enumerable);
        }

        public static bool IsNullOrEmpty(this IEnumerable lst)
        {
            if (lst == null) return true;
            return !lst.GetEnumerator().MoveNext();
        }

        //NOTE: see: Catel 2.1
        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="CollectionHelper.cs" company="Catel development team">
        //   Copyright (c) 2008 - 2011 Catel development team. All rights reserved.
        // </copyright>
        // <summary>
        //   Collection helper class.
        // </summary>
        // --------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Checks whether a collection is the same as another collection.
        /// </summary>
        /// <param name="listA">The list A.</param>
        /// <param name="listB">The list B.</param>
        /// <returns>
        /// True if the two collections contain all the same items in the same order.
        /// </returns>
        public static bool IsEqualTo(IEnumerable listA, IEnumerable listB)
        {
            if (listA == listB)
            {
                return true;
            }

            if ((listA == null) || (listB == null))
            {
                return false;
            }

            IEnumerator enumeratorA = listA.GetEnumerator();
            IEnumerator enumeratorB = listB.GetEnumerator();

            bool enumAHasValue = enumeratorA.MoveNext();
            bool enumBHasValue = enumeratorB.MoveNext();

            while (enumAHasValue && enumBHasValue)
            {
                if (!enumeratorA.Current.Equals(enumeratorB.Current))
                {
                    return false;
                }

                enumAHasValue = enumeratorA.MoveNext();
                enumBHasValue = enumeratorB.MoveNext();
            }

            // If we get here, and both enumerables don't have any value left, they are equal
            return !(enumAHasValue || enumBHasValue);
        }
    }
}