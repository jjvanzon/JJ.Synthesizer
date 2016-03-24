using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation
{
    public static class CalculationHelper
    {
        /// <summary>
        /// Avoid using Double.MaxValue, because it will easily result in NaN,
        /// even when you take a fraction of it or even the square root.
        /// </summary>
        public const double VERY_HIGH_VALUE = 1E24;

        /// <summary>
        /// Avoid using Double.MinValue, because it will easily result in NaN,
        /// even when you take a fraction of it or even the negative square root.
        /// </summary>
        public const double VERY_LOW_VALUE = -1E24;

        public static bool CanCastToNonNegativeInt32(double value)
        {
            return value >= 0.0 &&
                   value <= Int32.MaxValue &&
                   !Double.IsNaN(value) &&
                   !Double.IsInfinity(value);
        }
    }
}
