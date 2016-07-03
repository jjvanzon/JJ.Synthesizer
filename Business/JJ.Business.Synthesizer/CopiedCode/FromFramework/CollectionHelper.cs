using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.CopiedCode.FromFramework
{
    public static class CollectionHelper
    {
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
    }
}
