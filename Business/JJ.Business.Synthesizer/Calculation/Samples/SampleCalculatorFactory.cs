using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    // TODO: Expose through sample manager only and make this class internal.
    public static class SampleCalculatorFactory
    {
        /// <param name="bytes">nullable</param>
        public static ISampleCalculator CreateSampleCalculator(Sample sample, byte[] bytes)
        {
            if (sample == null) throw new NullException(() => sample);

            if (bytes == null || bytes.Length == 0)
            {
                return new Byteless_SampleCalculator(sample.GetChannelCount());
            }

            SampleDataTypeEnum sampleDataType = sample.GetSampleDataTypeEnum();
            InterpolationTypeEnum interpolationType = sample.GetInterpolationTypeEnum();

            switch (interpolationType)
            {
                case InterpolationTypeEnum.Block:
                    switch (sampleDataType)
                    {
                        case SampleDataTypeEnum.Int16:
                            return new Int16_BlockInterpolation_SampleCalculator(sample, bytes);

                        case SampleDataTypeEnum.Byte:
                            return new Byte_BlockInterpolation_SampleCalculator(sample, bytes);
                    }
                    break;

                case InterpolationTypeEnum.Line:
                    switch (sampleDataType)
                    {
                        case SampleDataTypeEnum.Int16:
                            return new Int16_LineInterpolation_SampleCalculator(sample, bytes);

                        case SampleDataTypeEnum.Byte:
                            return new Byte_LineInterpolation_SampleCalculator(sample, bytes);
                    }
                    break;
            }

            throw new Exception(String.Format(
                "{0} '{1}' combined with {2} '{3}' is not supported.",
                typeof(SampleDataTypeEnum).Name, sampleDataType,
                typeof(InterpolationTypeEnum).Name, interpolationType));
        }
    }
}