using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Cache_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Cache_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            double? signal = null;
            double? startTime = null;
            double? endTime = null;
            double? samplingRate = null;

            foreach (Inlet inlet in Object.Inlets)
            {
                double? number = ValidationHelper.TryGetConstantNumberFromInlet(inlet);

                switch (inlet.ListIndex)
                {
                    case OperatorConstants.CACHE_SIGNAL_INDEX:
                        signal = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.CACHE_START_TIME_INDEX:
                        startTime = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.CACHE_END_TIME_INDEX:
                        endTime = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
                        samplingRate = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;
                }
            }

            For(() => signal, PropertyDisplayNames.Signal)
                .NotInfinity()
                .NotNaN();

            For(() => startTime, PropertyDisplayNames.StartTime)
                .NotInfinity()
                .NotNaN();

            For(() => endTime, PropertyDisplayNames.EndTime)
                .NotInfinity()
                .NotNaN();

            For(() => samplingRate, PropertyDisplayNames.SamplingRate)
                .IsInteger()
                .GreaterThan(0.0);

            if (startTime.HasValue && endTime.HasValue)
            {
                if (endTime.Value < startTime.Value)
                {
                    ValidationMessages.AddLessThanMessage(PropertyNames.EndTime, PropertyDisplayNames.EndTime, PropertyDisplayNames.StartTime);
                }
            }
        }
    }
}