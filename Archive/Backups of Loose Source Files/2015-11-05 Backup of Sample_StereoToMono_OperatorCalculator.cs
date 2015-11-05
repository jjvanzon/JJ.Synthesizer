//using JJ.Business.Synthesizer.Calculation.Samples;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Data.Synthesizer;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Sample_StereoToMono_OperatorCalculator : OperatorCalculatorBase
//    {
//        private ISampleCalculator _sampleCalculator;

//        /// <param name="bytes">nullable</param>
//        public Sample_StereoToMono_OperatorCalculator(Sample sample, byte[] bytes)
//        {
//            if (sample == null) throw new NullException(() => sample);
//            if (sample.GetSpeakerSetupEnum() != SpeakerSetupEnum.Stereo) throw new NotEqualException(() => sample.GetSpeakerSetupEnum(), SpeakerSetupEnum.Stereo);

//            _sampleCalculator = SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            // For now just return the first channel
//            return _sampleCalculator.CalculateValue(time, 0);
//        }
//    }
//}
