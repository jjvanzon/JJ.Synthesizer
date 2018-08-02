// 2016-06-25
// Copied from JJ.Framework.Mathematics
// to promote inlining and made class internal.

using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.CopiedCode.FromFramework
{
    /// <summary>
    /// NOTE: There are also separate methods for
    /// precalculating values once when you start interpolating between two numbers
    /// and then doing a faster calculation to derived the points in between.
    /// </summary>
    internal static class Interpolator
    {
        // Hermite (double)

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source:
        /// http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        /// <param name="t">The point between 0 and 1 between x0 and x1.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Hermite_4pt3oX(double yMinus1, double y0, double y1, double y2, double t)
        {
            (double c0, double c1, double c2, double c3) = Hermite_4pt3oX_PrecalculateVariables(yMinus1, y0, y1, y2);
            return Hermite_4pt4oX_FromPrecalculatedVariables(c0, c1, c2, c3, t);
        }

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source:
        /// http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        public static (double c0, double c1, double c2, double c3) Hermite_4pt3oX_PrecalculateVariables(
            double yMinus1,
            double y0,
            double y1,
            double y2)
        {
            double c0 = y0;
            double c1 = .5 * (y1 - yMinus1);
            double c2 = yMinus1 - 2.5 * y0 + 2 * y1 - .5 * y2;
            double c3 = .5 * (y2 - yMinus1) + 1.5 * (y0 - y1);

            return (c0, c1, c2, c3);
        }

        /// <param name="t">The point between 0 and 1 between x0 and x1.</param>
        public static double Hermite_4pt4oX_FromPrecalculatedVariables(double c0, double c1, double c2, double c3, double t)
            => ((c3 * t + c2) * t + c1) * t + c0;

        // Hermite (float)

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source:
        /// http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        /// <param name="t">The point between 0 and 1 between x0 and x1.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Hermite_4pt3oX(float yMinus1, float y0, float y1, float y2, float t)
        {
            (float c0, float c1, float c2, float c3) = Hermite_4pt3oX_PrecalculateVariables(yMinus1, y0, y1, y2);
            return Hermite_4pt4oX_FromPrecalculatedVariables(c0, c1, c2, c3, t);
        }

        /// <summary>
        /// Pretty good sound interpolation.
        /// Source:
        /// http://stackoverflow.com/questions/1125666/how-do-you-do-bicubic-or-other-non-linear-interpolation-of-re-sampled-audio-da
        /// </summary>
        public static (float c0, float c1, float c2, float c3) Hermite_4pt3oX_PrecalculateVariables(
            float yMinus1,
            float y0,
            float y1,
            float y2)
        {
            float c0 = y0;
            float c1 = .5f * (y1 - yMinus1);
            float c2 = yMinus1 - 2.5f * y0 + 2f * y1 - .5f * y2;
            float c3 = .5f * (y2 - yMinus1) + 1.5f * (y0 - y1);

            return (c0, c1, c2, c3);
        }

        /// <param name="t">The point between 0 and 1 between x0 and x1.</param>
        public static float Hermite_4pt4oX_FromPrecalculatedVariables(float c0, float c1, float c2, float c3, float t)
            => ((c3 * t + c2) * t + c1) * t + c0;

        // Cubic From T (Bezier Curves)

        /// <summary>
        /// t values between 0 and 1 trace a curve
        /// going from (x0, y0) to (x3, y3).
        /// It does not go through (x1, y1) and (x2, y2).
        /// Those are merely control points that indicate the direction in which the curve goes.
        /// This is called a Bezier curve.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cubic_FromT(
            double x0,
            double x1,
            double x2,
            double x3,
            double y0,
            double y1,
            double y2,
            double y3,
            double t,
            out double x,
            out double y)
        {
            double oneMinusT = 1.0 - t;
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
        /// This is called a Bezier curve.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cubic_FromT(
            float x0,
            float x1,
            float x2,
            float x3,
            float y0,
            float y1,
            float y2,
            float y3,
            float t,
            out float x,
            out float y)
        {
            float oneMinusT = 1f - t;
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

        // Cubic Smooth Slope

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic_SmoothSlope(
            double xMinus1,
            double x0,
            double x1,
            double x2,
            double yMinus1,
            double y0,
            double y1,
            double y2,
            double x)
        {
            (double a, double b, double c) = Cubic_SmoothSlope_PrecalculateVariables(xMinus1, x0, x1, x2, yMinus1, y0, y1, y2);
            return Cubic_SmoothSlope_FromPrecalculatedVariables(x0, y0, a, b, c, x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c) Cubic_SmoothSlope_PrecalculateVariables(
            double xMinus1,
            double x0,
            double x1,
            double x2,
            double yMinus1,
            double y0,
            double y1,
            double y2)
        {
            double dx = x1 - x0;
            double dy = y1 - y0;
            double dx2 = dx * dx;
            double slope0 = (y1 - yMinus1) / (x1 - xMinus1);
            double slope1 = (y2 - y0) / (x2 - x0);
            double a = slope0;
            double b = (3 * dy - dx * slope1 - 2 * a * dx) / dx2;
            double dx3 = dx2 * dx;
            double c = (dy - a * dx - b * dx2) / dx3;

            return (a, b, c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic_SmoothSlope_FromPrecalculatedVariables(double x0, double y0, double a, double b, double c, double x)
        {
            double ofs = x - x0;
            double y = y0 + ofs * (a + ofs * (b + ofs * c));
            return y;
        }

        // Cubic Smooth Slope Distance 1

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic_SmoothSlope_Distance1(double x0, double yMinus1, double y0, double y1, double y2, double x)
        {
            (double a, double b, double c) = Cubic_SmoothSlope_DistanceOne_PrecalculateVariables(yMinus1, y0, y1, y2);
            double y = Cubic_SmoothSlope_FromPrecalculatedVariables(x0, y0, a, b, c, x);
            return y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c) Cubic_SmoothSlope_DistanceOne_PrecalculateVariables(
            double yMinus1,
            double y0,
            double y1,
            double y2)
        {
            double dy = y1 - y0;
            double slope1 = (y2 - y0) / 2;
            double slope0 = (y1 - yMinus1) / 2;
            double a = slope0;
            double b = 3 * dy - slope1 - 2 * a;
            double c = dy - a - b;

            return (a, b, c);
        }
    }
}