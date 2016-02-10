using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal class RandomCalculator_BlockInterpolation : RandomCalculatorBase
    {
        public override double GetValue(double time)
        {
            double t = time % SAMPLE_COUNT_DOUBLE;

            int t0 = (int)t;

            double x = _samples[t0];
            return x;
        }
    }
}
