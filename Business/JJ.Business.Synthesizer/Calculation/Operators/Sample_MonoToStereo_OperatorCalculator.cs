using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sample_MonoToStereo_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        /// <param name="bytes">nullable</param>
        public Sample_MonoToStereo_OperatorCalculator(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.GetSpeakerSetupEnum() != SpeakerSetupEnum.Mono) throw new NotEqualException(() => sample.GetSpeakerSetupEnum(), SpeakerSetupEnum.Mono);

            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
        }

        public override double Calculate(double time, int channelIndex)
        {
            // Return the single channel for both channels.
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }
}
