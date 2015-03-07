using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation
{
    public static class SampleCalculatorFactory
    {
        public static ISampleCalculator CreateSampleCalculator(SampleChannel sampleChannel)
        {
            if (sampleChannel == null) throw new NullException(() => sampleChannel);

            SampleDataTypeEnum sampleDataType = sampleChannel.Sample.GetSampleDataTypeEnum();
            InterpolationTypeEnum interpolationType = sampleChannel.Sample.GetInterpolationTypeEnum();

            switch (sampleDataType)
            {
                case SampleDataTypeEnum.Int16:
                    switch (interpolationType)
                    {
                        case InterpolationTypeEnum.Block:
                            return new Int16BlockInterpolationSampleCalculator(sampleChannel);
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