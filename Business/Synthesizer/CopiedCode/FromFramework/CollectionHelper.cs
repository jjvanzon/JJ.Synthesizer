﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JJ.Framework.Collections
{
    public static class CollectionHelper
    {
        /// <summary>
        /// This BinarySearch has not been thoroughly (unit) tested.
        /// It has been used in the development of audio processing,
        /// during which it was debugged, and produced expected audio output.
        /// The biggest concern is what happens with integer round-off for certain edge cases.
        /// </summary>
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
        /// This BinarySearch has not been thoroughly (unit) tested.
        /// It has been used in the development of audio processing,
        /// during which it was debugged, and produced expected audio output.
        /// The biggest concern is what happens with integer round-off for certain edge cases.
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