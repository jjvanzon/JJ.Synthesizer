using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal abstract class OperatorValidator_Base_Aggregate : OperatorValidator_Base
    {
        public OperatorValidator_Base_Aggregate(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : base(obj, operatorTypeEnum, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string sampleCountString = OperatorDataParser.GetString(Object, PropertyNames.SampleCount);
            string timeSliceDurationString = OperatorDataParser.GetString(Object, PropertyNames.TimeSliceDuration);

            For(() => sampleCountString, PropertyDisplayNames.SampleCount)
                .NotNullOrEmpty()
                .IsInteger();

            For(() => timeSliceDurationString, PropertyDisplayNames.TimeSliceDuration)
                .NotNullOrEmpty()
                .IsDouble();

            int sampleCount;
            if (Int32.TryParse(sampleCountString, out sampleCount))
            {
                For(() => sampleCount, PropertyDisplayNames.SampleCount)
                    .GreaterThan(0);
            }

            double timeSliceDuration;
            if (Double.TryParse(timeSliceDurationString, out timeSliceDuration))
            {
                For(() => timeSliceDuration, PropertyDisplayNames.TimeSliceDuration)
                    .GreaterThan(0.0);
            }
        }
    }
}
