using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Framework.Validation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Cache : OperatorValidator_Base
    {
        public OperatorValidator_Cache(Operator obj)
            : base(obj, OperatorTypeEnum.Cache, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string startTimeString = OperatorDataParser.GetString(Object, PropertyNames.StartTime);
            string endTimeString = OperatorDataParser.GetString(Object, PropertyNames.EndTime);
            string samplingRateString = OperatorDataParser.GetString(Object, PropertyNames.SamplingRate);
            string speakerSetupString = OperatorDataParser.GetString(Object, PropertyNames.SpeakerSetup);
            string interpolationTypeString = OperatorDataParser.GetString(Object, PropertyNames.InterpolationType);

            For(() => startTimeString, PropertyDisplayNames.StartTime)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();

            For(() => endTimeString, PropertyDisplayNames.EndTime)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();

            For(() => samplingRateString, PropertyDisplayNames.SamplingRate)
                .NotNullOrEmpty()
                .IsInteger();

            For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                .NotNullOrEmpty()
                .IsEnum<InterpolationTypeEnum>()
                .IsNot(InterpolationTypeEnum.Undefined);

            For(() => speakerSetupString, PropertyDisplayNames.SpeakerSetup)
                .NotNullOrEmpty()
                .IsEnum<SpeakerSetupEnum>()
                .IsNot(SpeakerSetupEnum.Undefined);

            double startTime;
            if (Double.TryParse(startTimeString, out startTime))
            {
                double endTime;
                if (Double.TryParse(endTimeString, out endTime))
                {
                    if (endTime < startTime)
                    {
                        ValidationMessageFormatter.NotAbove(PropertyDisplayNames.EndTime, PropertyDisplayNames.StartTime);
                    }
                }
            }

            int samplingRate;
            if (Int32.TryParse(samplingRateString, out samplingRate))
            {
                For(() => samplingRate, PropertyDisplayNames.SamplingRate)
                    .GreaterThan(0);
            }
        }
    }
}
