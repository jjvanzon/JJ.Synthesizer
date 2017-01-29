using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculator_Block_SingleChannel : SampleCalculatorBase
    {
        private readonly ArrayCalculator_MinPositionZero_Block _arrayCalculator;

        public SampleCalculator_Block_SingleChannel(Sample sample, byte[] bytes)
            : base(sample, bytes)
        {
            if (sample.GetChannelCount() != 1) throw new NotEqualException(() => sample.GetChannelCount(), 1);

            double[][] samples = SampleCalculatorHelper.ReadSamples(sample, bytes);
            double[] samples2 = samples.Single();

            _arrayCalculator = new ArrayCalculator_MinPositionZero_Block(samples2, _rate);
        }

        public override double CalculateValue(double time, int channelIndex)
        {
            return _arrayCalculator.CalculateValue(time);
        }
    }
}
