using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal static class SampleCalculatorFactory
    {
        /// <param name="bytes">nullable</param>
        public static ISampleCalculator CreateSampleCalculator(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);

            if (bytes == null || bytes.Length == 0 || !sample.IsActive)
            {
                return new SampleCalculator_Zero(sample.GetChannelCount());
            }

            InterpolationTypeEnum interpolationTypeEnum = sample.GetInterpolationTypeEnum();
            SpeakerSetupEnum speakerSetupEnum = sample.GetSpeakerSetupEnum();

            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            return new SampleCalculator_Block_SingleChannel(sample, bytes);

                        case SpeakerSetupEnum.Stereo:
                            return new SampleCalculator_Block_MultiChannel(sample, bytes);
                    }
                    break;

                case InterpolationTypeEnum.Stripe:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            return new SampleCalculator_Stripe_SingleChannel(sample, bytes);

                        case SpeakerSetupEnum.Stereo:
                            return new SampleCalculator_Stripe_MultiChannel(sample, bytes);
                    }
                    break;

                case InterpolationTypeEnum.Line:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            return new SampleCalculator_Line_SingleChannel(sample, bytes);

                        case SpeakerSetupEnum.Stereo:
                            return new SampleCalculator_Line_MultiChannel(sample, bytes);
                    }
                    break;

                case InterpolationTypeEnum.Cubic:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            return new SampleCalculator_Cubic_SingleChannel(sample, bytes);

                        case SpeakerSetupEnum.Stereo:
                            return new SampleCalculator_Cubic_MultiChannel(sample, bytes);
                    }
                    break;

                case InterpolationTypeEnum.Hermite:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            return new SampleCalculator_Hermite_SingleChannel(sample, bytes);

                        case SpeakerSetupEnum.Stereo:
                            return new SampleCalculator_Hermite_MultiChannel(sample, bytes);
                    }
                    break;
            }

            throw new Exception(string.Format(
                "{0} '{1}' combined with {2} '{3}' is not supported.",
                typeof(SpeakerSetupEnum).Name, speakerSetupEnum,
                typeof(InterpolationTypeEnum).Name, interpolationTypeEnum));
        }
    }
}