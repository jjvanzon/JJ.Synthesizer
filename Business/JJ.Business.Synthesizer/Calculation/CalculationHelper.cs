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

        /// <summary> Avoid using Double.Epsilon, because it will easily result in NaN </summary>
        public const double VERY_SMALL_POSITIVE_VALUE = 1E-24;
    }
}
