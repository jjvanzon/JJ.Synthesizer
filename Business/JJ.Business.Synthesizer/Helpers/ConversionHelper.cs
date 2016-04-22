using System;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class ConversionHelper
    {
        // TODO: Remove outcommented code.
        //public static int? ParseNullableInt32(string input)
        //{
        //    if (String.IsNullOrEmpty(input)) return null;
        //
        //    return Int32.Parse(input);
        //}

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