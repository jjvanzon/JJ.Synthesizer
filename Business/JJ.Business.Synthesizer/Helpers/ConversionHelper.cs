using System;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class ConversionHelper
    {
        public static bool CanCastToNonNegativeInt32(double value)
        {
            return value >= 0.0 &&
                   value <= Int32.MaxValue &&
                   !Double.IsNaN(value) &&
                   !Double.IsInfinity(value);
        }

        public static bool CanCastToInt32(double value)
        {
            return value >= Int32.MinValue &&
                   value <= Int32.MaxValue &&
                   !Double.IsNaN(value) &&
                   !Double.IsInfinity(value);
        }
    }
}