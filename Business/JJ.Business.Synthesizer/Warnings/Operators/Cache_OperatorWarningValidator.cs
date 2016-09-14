using System;
using JJ.Business.Synthesizer.Extensions;
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
            double? start = null;
            double? end = null;
            double? samplingRate = null;

            foreach (Inlet inlet in Object.Inlets)
            {
                double? number = inlet.TryGetConstantNumber();

                switch (inlet.ListIndex)
                {
                    case OperatorConstants.CACHE_SIGNAL_INDEX:
                        signal = number;
                        break;

                    case OperatorConstants.CACHE_START_INDEX:
                        start = number;
                        break;

                    case OperatorConstants.CACHE_END_INDEX:
                        end = number;
                        break;

                    case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
                        samplingRate = number;
                        break;
                }
            }

            For(() => signal, PropertyDisplayNames.Signal)
                .NotInfinity()
                .NotNaN();

            For(() => start, PropertyDisplayNames.Start)
                .NotInfinity()
                .NotNaN();

            For(() => end, PropertyDisplayNames.End)
                .NotInfinity()
                .NotNaN();

            For(() => samplingRate, PropertyDisplayNames.SamplingRate)
                .IsInteger()
                .GreaterThan(0.0);

            if (start.HasValue && end.HasValue)
            {
                if (end.Value < start.Value)
                {
                    ValidationMessages.AddLessThanMessage(PropertyNames.EndTime, PropertyDisplayNames.EndTime, PropertyDisplayNames.StartTime);
                }
            }
        }
    }
}