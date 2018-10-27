using System;
using System.Collections.Generic;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Framework.Collections
{
    // Do not use JetBrains.Annotations.
    // There is an app that statically includes and compiles this code
    // at run-time, and JetBrains.Annotations is not included there.

    public static partial class CollectionHelper
    {
        public static IEnumerable<T> Repeat<T>(int count, Func<T> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            for (var i = 0; i < count; i++)
            {
                T item = selector();
                yield return item;
            }
        }

        public static IEnumerable<T> Repeat<T>(int count, Func<int, T> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            for (var i = 0; i < count; i++)
            {
                T item = selector(i);
                yield return item;
            }
        }
    }
}