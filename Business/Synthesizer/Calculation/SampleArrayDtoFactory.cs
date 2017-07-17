using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.Samples;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
// ReSharper disable CoVariantArrayConversion

namespace JJ.Business.Synthesizer.Calculation
{
    internal static class SampleArrayDtoFactory
    {
        // These defaults may become variables in the future.
        private const double DEFAULT_VALUE_BEFORE = 0.0;
        private const double DEFAULT_VALUE_AFTER = 0.0;
        private const bool DEFAULT_IS_ROTATING = false;

        /// <param name="bytes">nullable</param>
        public static IList<ArrayDto> CreateArrayDtos(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);

            IValidator validator = new SampleValidator(sample);
            validator.Assert();

            double[][] samples;

            if (bytes == null || bytes.Length == 0 || !sample.IsActive)
            {
                samples = new double[0][];
            }
            else
            {
                samples = SampleReader.ReadSamples(sample, bytes);
            }

            InterpolationTypeEnum interpolationTypeEnum = sample.GetInterpolationTypeEnum();
            double rate = sample.SamplingRate / sample.TimeMultiplier; // TODO: Make extension method.

            IList<ArrayDto> arrayDtos = samples.Select(
                x => new ArrayDto
                {
                    Array = x,
                    InterpolationTypeEnum = interpolationTypeEnum,
                    Rate = rate,
                    ValueBefore = DEFAULT_VALUE_BEFORE,
                    ValueAfter = DEFAULT_VALUE_AFTER,
                    IsRotating = DEFAULT_IS_ROTATING
                }).ToArray();

            return arrayDtos;
        }
    }
}