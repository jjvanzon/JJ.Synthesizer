using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
// ReSharper disable CoVariantArrayConversion

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal static class SampleCalculatorFactory
    {
        /// <param name="bytes">nullable</param>
        public static IList<ICalculatorWithPosition> CreateSampleCalculators(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);

            IValidator validator = new SampleValidator(sample);
            validator.Assert();

            if (bytes == null || bytes.Length == 0 || !sample.IsActive)
            {
                // HACK: Optimize to a literal 0 instead.
                return new ICalculatorWithPosition[] { new ArrayCalculator_MinPositionZero_Block(new double[0], 0) };
            }

            InterpolationTypeEnum interpolationTypeEnum = sample.GetInterpolationTypeEnum();
            SpeakerSetupEnum speakerSetupEnum = sample.GetSpeakerSetupEnum();
            double rate = sample.SamplingRate / sample.TimeMultiplier;

            double[][] samples = SampleCalculatorHelper.ReadSamples(sample, bytes);

            IList<ICalculatorWithPosition> arrayCalculators = null;

            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Block(x, rate)).ToArray();
                            break;

                        case SpeakerSetupEnum.Stereo:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Block(x, rate)).ToArray();
                            break;
                    }
                    break;

                case InterpolationTypeEnum.Stripe:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Stripe(x, rate)).ToArray();
                            break;

                        case SpeakerSetupEnum.Stereo:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Stripe(x, rate)).ToArray();
                            break;
                    }
                    break;

                case InterpolationTypeEnum.Line:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Line(x, rate)).ToArray();
                            break;

                        case SpeakerSetupEnum.Stereo:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Line(x, rate)).ToArray();
                            break;
                    }
                    break;

                case InterpolationTypeEnum.Cubic:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Cubic(x, rate)).ToArray();
                            break;

                        case SpeakerSetupEnum.Stereo:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Cubic(x, rate)).ToArray();
                            break;
                    }
                    break;

                case InterpolationTypeEnum.Hermite:
                    switch (speakerSetupEnum)
                    {
                        case SpeakerSetupEnum.Mono:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Hermite(x, rate)).ToArray();
                            break;

                        case SpeakerSetupEnum.Stereo:
                            arrayCalculators = samples.Select(x => new ArrayCalculator_MinPositionZero_Hermite(x, rate)).ToArray();
                            break;
                    }
                    break;
            }

            if (arrayCalculators == null)
            {
                throw new Exception($"{new { speakerSetupEnum }} combined with {new { interpolationTypeEnum }} is not supported.");
            }

            return arrayCalculators;
        }
    }
}