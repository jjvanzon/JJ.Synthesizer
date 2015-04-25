using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    public static class SampleCalculatorFactory
    {
        public static ISampleCalculator CreateSampleCalculator(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            SampleDataTypeEnum sampleDataType = sample.GetSampleDataTypeEnum();
            InterpolationTypeEnum interpolationType = sample.GetInterpolationTypeEnum();

            switch (interpolationType)
            {
                case InterpolationTypeEnum.Block:
                    switch (sampleDataType)
                    {
                        case SampleDataTypeEnum.Int16:
                            return new Int16_BlockInterpolation_SampleCalculator(sample);

                        case SampleDataTypeEnum.Byte:
                            return new Byte_BlockInterpolation_SampleCalculator(sample);
                    }
                    break;

                case InterpolationTypeEnum.Line:
                    switch (sampleDataType)
                    {
                        case SampleDataTypeEnum.Int16:
                            return new Int16_LineInterpolation_SampleCalculator(sample);

                        case SampleDataTypeEnum.Byte:
                            return new Byte_LineInterpolation_SampleCalculator(sample);
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