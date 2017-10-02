using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class ConversionHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanCastToNonNegativeInt32(double value)
        {
            return value >= 0.0 &&
                   value <= int.MaxValue &&
                   !double.IsNaN(value) &&
                   !double.IsInfinity(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanCastToInt32(double value)
        {
            return value >= int.MinValue &&
                   value <= int.MaxValue &&
                   !double.IsNaN(value) &&
                   !double.IsInfinity(value);
        }

        /// <param name="max">max is assumed to fit in an Int32.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanCastToNonNegativeInt32WithMax(double value, double max)
        {
            return value >= 0.0 &&
                   value <= max &&
                   !double.IsNaN(value) &&
                   !double.IsInfinity(value);
        }
    }
}