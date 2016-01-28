using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Framework.Validation.Resources;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Spectrum : OperatorValidator_Base
    {
        public OperatorValidator_Spectrum(Operator obj)
            : base(obj, OperatorTypeEnum.Spectrum, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string startTimeString = OperatorDataParser.GetString(Object, PropertyNames.StartTime);
            string endTimeString = OperatorDataParser.GetString(Object, PropertyNames.EndTime);
            string frequencyCountString = OperatorDataParser.GetString(Object, PropertyNames.FrequencyCount);

            For(() => startTimeString, PropertyDisplayNames.StartTime)
                .NotNullOrEmpty()
                .IsDouble();

            For(() => endTimeString, PropertyDisplayNames.EndTime)
                .NotNullOrEmpty()
                .IsDouble();

            For(() => frequencyCountString, PropertyDisplayNames.FrequencyCount)
                .NotNullOrEmpty()
                .IsInteger();


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

            int frequencyCount;
            if (Int32.TryParse(frequencyCountString, out frequencyCount))
            {
                For(() => frequencyCount, PropertyDisplayNames.FrequencyCount)
                    .GreaterThanOrEqual(2);

                if (frequencyCount % 2 != 0)
                {
                    string message = MessageFormatter.MustBeMultipleOf2(PropertyDisplayNames.FrequencyCount);
                    ValidationMessages.Add(PropertyDisplayNames.FrequencyCount, message);
                }
            }
        }
    }
}
