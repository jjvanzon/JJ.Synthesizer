using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sample_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        /// <param name="bytes">nullable</param>
        public Sample_OperatorCalculator(Sample sample, byte[] bytes)
        {
            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
        }

        public override double Calculate(double time, int channelIndex)
        {
            return _sampleCalculator.CalculateValue(time, channelIndex);
        }
    }

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

    internal class Sample_StereoToMono_OperatorCalculator : OperatorCalculatorBase
    {
        private ISampleCalculator _sampleCalculator;

        /// <param name="bytes">nullable</param>
        public Sample_StereoToMono_OperatorCalculator(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.GetSpeakerSetupEnum() != SpeakerSetupEnum.Stereo) throw new NotEqualException(() => sample.GetSpeakerSetupEnum(), SpeakerSetupEnum.Stereo);

            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
        }

        public override double Calculate(double time, int channelIndex)
        {
            // For now just return the first channel
            return _sampleCalculator.CalculateValue(time, 0);
        }
    }
}
