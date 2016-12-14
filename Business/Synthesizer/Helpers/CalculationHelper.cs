
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Helpers
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

        /// <summary>
        /// We need a lot of lenience in this code, because validity is dependent on user input,
        /// and this validity cannot be checked on the entity level, only when starting the calculation.
        /// In theory I could generate additional messages in the calculation optimization process,
        /// but we should keep it possible to reoptimize in runtime, and we cannot obtrusively interrupt
        /// the user with validation messages, because he is busy making music and the show must go on.
        /// </summary>
        public static bool CacheParametersAreValid(double start, double end, double samplingRate)
        {
            bool startIsValid = !DoubleHelper.IsSpecialValue(start);
            bool endIsValid = !DoubleHelper.IsSpecialValue(end);
            bool samplingRateIsValid = ConversionHelper.CanCastToInt32(samplingRate) && (int)samplingRate > 0;
            bool startComparedToEndIsValid = end > start;
            bool valuesAreValid = startIsValid &&
                                  endIsValid &&
                                  samplingRateIsValid &&
                                  startComparedToEndIsValid;
            return valuesAreValid;
        }
    }
}
