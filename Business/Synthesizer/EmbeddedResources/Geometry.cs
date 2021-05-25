using System;
using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

namespace JJ.Framework.Mathematics
{
    // Do not use JetBrains.Annotations, because this code is statically compiled into code that does not support it.
    public static class Geometry
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRectangle(float x, float y, float x1, float y1, float x2, float y2)
            => x >= x1 &&
               x <= x2 &&
               y >= y1 &&
               y <= y2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRectangle(double x, double y, double x1, double y1, double x2, double y2)
            => x >= x1 &&
               x <= x2 &&
               y >= y1 &&
               y <= y2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCenter(double x1, double y1, double x2, double y2, out double x, out double y)
        {
            x = (x1 + x2) / 2.0;
            y = (y1 + y2) / 2.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCenter(float x1, float y1, float x2, float y2, out float x, out float y)
        {
            x = (x1 + x2) / 2f;
            y = (y1 + y2) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCenter_ByWidthAndHeight(double left, double top, double width, double height, out double x, out double y)
        {
            x = left + width / 2.0;
            y = top + height / 2.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetCenter_ByWidthAndHeight(float left, float top, float width, float height, out float x, out float y)
        {
            x = left + width / 2f;
            y = top + height / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteDistance(double x1, double x2)
        {
            double distance = x2 - x1;

            if (distance < 0.0)
            {
                distance = -distance;
            }

            return distance;
        }

        public static double AbsoluteDistance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }

        public static float AbsoluteDistance(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            var distance = (float)Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }
    }
}