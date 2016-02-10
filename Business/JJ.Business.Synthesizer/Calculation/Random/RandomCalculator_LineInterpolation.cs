using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal class RandomCalculator_LineInterpolation : RandomCalculatorBase
    {
        /// <summary> Minus 1 to always have an extra sample left for interpolation purposes. </summary>
        private const double SAMPLE_COUNT_DOUBLE_MINUS_ONE = SAMPLE_COUNT_DOUBLE - 1.0;

        public override double GetValue(double time)
        {
            double t = time % SAMPLE_COUNT_DOUBLE_MINUS_ONE;

            int t0 = (int)t;
            int t1 = t0 + 1;

            double x0 = _samples[t0];
            double x1 = _samples[t1];

            double x = x0 + (x1 - x0) * (t - t0);
            return x;
        }

        // Brainstorm to check if t0 + 1 could cause index out of range:
        // sample count = 10
        // sample count - 1 = 9
        //     0 % 9 = 0
        //     8 % 9 = 8
        //     9 % 9 = 0
        //    10 % 9 = 1
        // 9.001 % 9 = 0.001
        // 8.999 % 9 = 8.999

        // It will never become 9.
        // So t0 + 1 is max 9, the maximum array index.
    }
}
