// 2016-06-25
// Copied from JJ.Framework.Mathematics
// to promote inlining and made class internal.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.CopiedCode.FromFramework
{
    public static class MathHelper
    {
        public const double SQRT_2 = 1.4142135623730950;
        public const float SQRT_2_SINGLE = 1.4142136f;
        public const double TWO_PI = 6.2831853071795865;

        /// <summary>
        /// Integer variation of the Math.Pow function,
        /// that only works for non-negative exponents.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Pow(int n, int e)
        {
            // I doubt this is actually faster than just using the standard Math.Pow that takes double.
            int x = 1;
            for (int i = 0; i < e; i++)
            {
                x *= n;
            }
            return x;
        }

        /// <summary>
        /// Integer variation of the Math.Log function.
        /// It will only return integers,
        /// but will prevent rounding erros such as
        /// 1000 log 10 = 2.99999999996
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log(int value, int n)
        {
            int temp = value;
            int i = 0;
            while (temp >= n)
            {
                temp /= n;
                i++;
            }
            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(int x)
        {
            // With help of:
            // http://www.lomont.org/Software/Misc/FFT/LomontFFT.html

            bool isPowerOf2 = (x & (x - 1)) == 0;
            return isPowerOf2;
        }

        /// <summary>
        /// Calculates where x is in between x0 and x1 on a logarithmic scale.
        /// 0 means it is on point x0. 1 means it is on pont x1.
        /// between 0 and 1 means it is somewhere in between.
        /// 0.5 means it is precisely half-way x0 and x1 logarithmically.
        /// Note that it can also be outside the bounds 0 and 1 if it is not in between those numbers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogRatio(double x0, double x1, double x)
        {
            double ratio = Math.Log(x / x0) / Math.Log(x1 / x0);
            return ratio;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses Math.Round as a helper, which supports a wide range of values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundWithStep(double value, double step, double offset)
        {
            double temp = (value - offset) / step;
            return Math.Round(temp, MidpointRounding.AwayFromZero) * step + offset;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses Math.Round as a helper, which supports a wide range of values.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundWithStep(double value, double step)
        {
            double temp = value / step;
            return Math.Round(temp, MidpointRounding.AwayFromZero) * step;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses a cast to Int64 as a helper,
        /// which might be faster than Math.Round,
        /// but means you are stuck within the value bounds of long.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundWithStepWithInt64Bounds(double value, double step, double offset)
        {
            double temp = (value - offset) / step;

            // Correct for rounding away from 0.
            if (temp > 0.0) temp += 0.5;
            else temp -= 0.5;

            return (long)temp * step + offset;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses a cast to Int64 as a helper,
        /// which might be faster than Math.Round,
        /// but means you are stuck within the value bounds of long.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundWithStepWithInt64Bounds(double value, double step)
        {
            double temp = value / step;

            // Correct for rounding away from 0.
            if (temp > 0.0) temp += 0.5;
            else temp -= 0.5;

            return (long)temp * step;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses a cast to Int64 as a helper,
        /// which might be faster than Math.Round,
        /// but means you are stuck within the value bounds of long.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoundWithStepWithInt64Bounds(float value, float step, float offset)
        {
            float temp = (value - offset) / step;

            // Correct for rounding away from 0.
            if (temp > 0.0) temp += 0.5f;
            else temp -= 0.5f;

            return (long)temp * step + offset;
        }

        /// <summary>
        /// Rounds to multiples of step, with an offset.
        /// It uses a cast to Int64 as a helper,
        /// which might be faster than Math.Round,
        /// but means you are stuck within the value bounds of long.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoundWithStepWithInt64Bounds(float value, float step)
        {
            float temp = value / step;

            // Correct for rounding away from 0.
            if (temp > 0.0) temp += 0.5f;
            else temp -= 0.5f;

            return (long)temp * step;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ScaleLinearly(
            double input,
            double sourceValueA,
            double sourceValueB,
            double targetValueA,
            double targetValueB)
        {
            double sourceRange = sourceValueB - sourceValueA;
            double targetRange = targetValueB - targetValueA;
            double between0And1 = (input - sourceValueA) / sourceRange;
            double result = between0And1 * targetRange + targetValueA;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ScaleLinearly(double input, double sourceValueA, double targetValueA, double slope)
        {
            return (input - sourceValueA) * slope + targetValueA;
        }

        /// <summary> Equally spreads out a number indices over a different number of indices. </summary>
        public static Dictionary<int, int> Spread(int sourceIndex1, int sourceIndex2, int destIndex1, int destIndex2)
        {
            // TODO: There seem to be a lot of repeated principles here, compared to the overload that takes 2 int's.
            if (sourceIndex2 < sourceIndex1) throw new LessThanOrEqualException(() => sourceIndex2, () => sourceIndex1);
            if (destIndex2 < destIndex1) throw new LessThanOrEqualException(() => destIndex2, () => destIndex1);

            IList<int> sourceRange = Enumerable.Range(sourceIndex1, sourceIndex2).ToArray();
            IList<int> destRange = Enumerable.Range(destIndex1, destIndex2).ToArray();

            Dictionary<int, int> dictionary = Spread(sourceRange, destRange);

            return dictionary;
        }

        /// <summary> Equally spreads out items over another set of items. </summary>
        public static Dictionary<TSource, TDest> Spread<TSource, TDest>(IList<TSource> sourceList, IList<TDest> destList)
        {
            if (sourceList == null) throw new NullException(() => sourceList);
            if (destList == null) throw new NullException(() => destList);

            // TODO: This unncessarily created an intermediate dictionary, but at least it reuses code.
            Dictionary<int, int> intDictionary = Spread(sourceList.Count, destList.Count);

            Dictionary<TSource, TDest> destDictionary = intDictionary.ToDictionary(x => sourceList[x.Key], x => destList[x.Value]);

            return destDictionary;
        }

        /// <summary> Equally spreads out a number indices over a different number of indices. </summary>
        public static Dictionary<int, int> Spread(int sourceCount, int destCount)
        {
            if (sourceCount == 0 || destCount == 0)
            {
                return new Dictionary<int, int>(0);
            }

            int sourceMaxIndex = sourceCount - 1;
            if (sourceMaxIndex == 0)
            {
                return new Dictionary<int, int> { { 0, 0 } };
            }

            int destMaxIndex = destCount - 1;

            double step = (double)destMaxIndex / (double)sourceMaxIndex;

            double destIndexDouble = 0;

            var dictionary = new Dictionary<int, int>(sourceCount);
            for (int sourceIndex = 0; sourceIndex < sourceCount; sourceIndex++)
            {
                int destIndex = (int)Math.Round(destIndexDouble, 0, MidpointRounding.AwayFromZero);

                dictionary.Add(sourceIndex, destIndex);

                destIndexDouble += step;
            }

            return dictionary;
        }
    }
}