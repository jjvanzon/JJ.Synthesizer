// 2016-06-25
// Copied from JJ.Framework.Mathematics
// to promote inlining and made class internal.

using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.CodeCopies.FromFramework
{
    internal static class Interpolator
    {
        ///// <summary>
        ///// Derived from the following source:
        ///// http://www.virtualdj.com/forums/171269/VirtualDJ_Plugins/_Release__CDJ_Vinyl_Brake_Effect.html
        ///// </summary>
        //public static short Interpolate_CubicEquidistant_Original(float x, short y1, short y2, short y3, short y4)
        //{
        //    float result = ((y2 / 2 - y1 / 6 - y3 / 2 + y4 / 6) * x * x * x) +
        //                   ((y1 / 2 - y2 + y3 / 2) * x * x) +
        //                   ((y3 - y2 / 2 - y1 / 3 - y4 / 6) * x) +
        //                    y2; //y2 is d
        //    return (short)result;
        //}

        /// <summary>
        /// Derived from the following source:
        /// http://www.virtualdj.com/forums/171269/VirtualDJ_Plugins/_Release__CDJ_Vinyl_Brake_Effect.html
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Interpolate_Cubic_Equidistant(double yMinus1, double y0, double y1, double y2, double t)
        {
            double y = (y0 / 2 - yMinus1 / 6 - y1 / 2 + y2 / 6) * t * t * t +
                       (yMinus1 / 2 - y0 + y1 / 2) * t * t +
                       (y1 - y0 / 2 - yMinus1 / 3 - y2 / 6) * t +
                       y0; // y0 is d
            return y;
        }

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source: http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Interpolate_Hermite_4pt3oX(float x0, float x1, float x2, float x3, float t)
        {
            float c0 = x1;
            float c1 = .5F * (x2 - x0);
            float c2 = x0 - (2.5F * x1) + (2 * x2) - (.5F * x3);
            float c3 = (.5F * (x3 - x0)) + (1.5F * (x1 - x2));
            return (((((c3 * t) + c2) * t) + c1) * t) + c0;
        }

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source: http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Interpolate_Hermite_4pt3oX(double x0, double x1, double x2, double x3, double t)
        {
            double c0 = x1;
            double c1 = .5 * (x2 - x0);
            double c2 = x0 - 2.5 * x1 + 2 * x2 - .5 * x3;
            double c3 = .5 * (x3 - x0) + 1.5 * (x1 - x2);
            return ((c3 * t + c2) * t + c1) * t + c0;
        }

        ///// <summary>
        ///// Source: http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        ///// </summary>
        //public static float Interpolate_Hermite_4pt3oX_Original(float x0, float x1, float x2, float x3, float t)
        //{
        //    float c0 = x1;
        //    float c1 = .5F * (x2 - x0);
        //    float c2 = x0 - (2.5F * x1) + (2 * x2) - (.5F * x3);
        //    float c3 = (.5F * (x3 - x0)) + (1.5F * (x1 - x2));
        //    return (((((c3 * t) + c2) * t) + c1) * t) + c0;
        //}

        /// <summary>
        /// t values between 0 and 1 trace a curve
        /// going from (x0, y0) to (x3, y3).
        /// It does not go through (x1, y1) and (x2, y2).
        /// Those are merely control points that indicate the direction in which the curve goes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate_Cubic_FromT(
            double x0, double x1, double x2, double x3,
            double y0, double y1, double y2, double y3,
            double t, out double x, out double y)
        {
            double oneMinusT = (1.0 - t);
            double oneMinusTSquared = oneMinusT * oneMinusT;
            double oneMinusTCubed = oneMinusTSquared * oneMinusT;
            double tSquared = t * t;
            double tCubed = tSquared * t;

            double a = oneMinusTCubed;
            double b = 3 * oneMinusTSquared * t;
            double c = 3 * oneMinusT * tSquared;
            double d = tCubed;

            x = a * x0 + 
                b * x1 + 
                c * x2 + 
                d * x3;

            y = a * y0 + 
                b * y1 + 
                c * y2 + 
                d * y3;
        }

        /// <summary>
        /// t values between 0 and 1 trace a curve
        /// going from (x0, y0) to (x3, y3).
        /// It does not go through (x1, y1) and (x2, y2).
        /// Those are merely control points that indicate the direction in which the curve goes.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Interpolate_Cubic_FromT(
            float x0, float x1, float x2, float x3,
            float y0, float y1, float y2, float y3,
            float t, out float x, out float y)
        {
            float oneMinusT = (1f - t);
            float oneMinusTSquared = oneMinusT * oneMinusT;
            float oneMinusTCubed = oneMinusTSquared * oneMinusT;
            float tSquared = t * t;
            float tCubed = tSquared * t;

            float a = oneMinusTCubed;
            float b = 3 * oneMinusTSquared * t;
            float c = 3 * oneMinusT * tSquared;
            float d = tCubed;

            x = a * x0 +
                b * x1 +
                c * x2 +
                d * x3;

            y = a * y0 +
                b * y1 +
                c * y2 +
                d * y3;
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Interpolate_Cubic_FromX(
            double x0, double x1, double x2, double x3, 
            double y0, double y1, double y2, double y3,
            double x)
        {
            throw new NotImplementedException();
        }

        // From 1999

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Interpolate_Cubic_SmoothSlope(
            double xMinus1, double x0, double x1, double x2,
            double yMinus1, double y0, double y1, double y2,
            double x)
        {
            double incl0 = (y1 - yMinus1) / (x1 - xMinus1);
            double incl1 = (y2 - y0) / (x2 - x0);
            double y = Interpolate_Cubic_SmoothSlope(x0, x1, y0, y1, incl0, incl1, x);
            return y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Interpolate_Cubic_SmoothSlope(
            double x0, double x1,
            double y0, double y1, 
            double incl0, double incl1,
            double x)
        {
            double dx = x1 - x0;
            double dx2 = dx * dx;
            double dx3 = dx2 * dx;
            double dy = y1 - y0;
            double ofs = x - x0;
            double a = incl0;
            double b = (3 * dy - dx * incl1 - 2 * a * dx) / dx2;
            double c = (dy - a * dx - b * dx2) / dx3;
            double y = y0 + ofs * (a + (ofs * (b + (c * ofs))));
            return y;
        }
    }
}
