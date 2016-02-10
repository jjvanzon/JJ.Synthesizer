using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal class RandomCalculator_StripeInterpolation : RandomCalculatorBase
    {
        /// <summary> Minus 1 to always have an extra sample left for interpolation purposes. </summary>
        private const double SAMPLE_COUNT_DOUBLE_MINUS_ONE = SAMPLE_COUNT_DOUBLE - 1.0;

        public override double GetValue(double time)
        {
            double t = time % SAMPLE_COUNT_DOUBLE_MINUS_ONE;

            t += 0.5;

            int t0 = (int)t;

            double x = _samples[t0];
            return x;
        }
    }
}
