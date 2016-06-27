// 2016-06-25
// Copied from JJ.Framework.Mathematics
// to promote inlining and made class internal.

using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Copies.FromFramework
{
    public static class Maths
    {
        public const double SQRT_2 = 1.4142135623730950;
        public const float FLOAT_SQRT_2 = 1.4142136f;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogRatio(double x0, double x1, double x)
        {
            double ratio = Math.Log(x - x0) / Math.Log(x1 - x0);
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
    }
}
