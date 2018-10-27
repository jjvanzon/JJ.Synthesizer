using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Framework.Collections
{
    // Do not use JetBrains.Annotations.
    // There is an app that statically includes and compiles this code
    // at run-time, and JetBrains.Annotations is not included there.

    public static class CollectionHelper
    {
        /// <param name="sortedArray"> Not checked for null, for performance. </param>
        public static (double valueBefore, double valueAfter) BinarySearchInexact(double[] sortedArray, double input)
        {
            BinarySearchInexact(sortedArray, input, out double valueBefore, out double valueAfter);
            return (valueBefore, valueAfter);
        }

        /// <param name="sortedArray"> Not checked for null, for performance. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BinarySearchInexact(
            double[] sortedArray,
            double input,
            out double valueBefore,
            out double valueAfter)
        {
            int count = sortedArray.Length;
            double min = sortedArray[0];
            double max = sortedArray[count - 1];
            int halfCount = count >> 1;

            BinarySearchInexact(sortedArray, halfCount, min, max, input, out valueBefore, out valueAfter);
        }

        /// <summary>
        /// Overload with more values you supply yourself: halfLength, min and max,
        /// that you could cache yourself for performance.
        /// </summary>
        /// <param name="sortedArray"> Not checked for null, for performance. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double valueBefore, double valueAfter) BinarySearchInexact(
            double[] sortedArray,
            int halfCount,
            double min,
            double max,
            double input)
        {
            BinarySearchInexact(sortedArray, halfCount, min, max, input, out double valueBefore, out double valueAfter);
            return (valueBefore, valueAfter);
        }

        /// <summary>
        /// Overload with more values you supply yourself: halfLength, min and max,
        /// that you could cache yourself for performance.
        /// </summary>
        /// <param name="sortedArray"> Not checked for null, for performance. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BinarySearchInexact(
            double[] sortedArray,
            int halfCount,
            double min,
            double max,
            double input,
            out double valueBefore,
            out double valueAfter)
        {
            int sampleIndex = halfCount;
            int jump = halfCount;

            valueBefore = min;
            valueAfter = max;

            while (jump != 0)
            {
                double sample = sortedArray[sampleIndex];

                jump = jump >> 1;

                if (input >= sample)
                {
                    valueBefore = sample;

                    sampleIndex += jump;
                }
                else
                {
                    valueAfter = sample;

                    sampleIndex -= jump;
                }
            }
        }

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