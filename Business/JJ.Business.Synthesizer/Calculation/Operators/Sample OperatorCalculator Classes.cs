using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sample_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public Sample_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _sampleCalculator.CalculateValue(time, channelIndex);
        }
    }

    internal class Sample_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public Sample_MonoToStereo_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Return the single channel for both channels.
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }

    internal class Sample_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        public Sample_StereoToMono_OperatorCalculator(ISampleCalculator sampleCalculator)
        {
            if (sampleCalculator == null) throw new NullException(() => sampleCalculator);
            _sampleCalculator = sampleCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            // For now just return the first channel
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }
}
