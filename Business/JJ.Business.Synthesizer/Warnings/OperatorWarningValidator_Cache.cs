using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_Cache : OperatorWarningValidator_Base_AllInletsFilled
    {
        public OperatorWarningValidator_Cache(Operator obj)
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
                double? number = TryGetConstantNumber(inlet);

                switch (inlet.ListIndex)
                {
                    case OperatorConstants.CACHE_SIGNAL_INDEX:
                        signal = TryGetConstantNumber(inlet);
                        break;

                    case OperatorConstants.CACHE_START_TIME_INDEX:
                        startTime = TryGetConstantNumber(inlet);
                        break;

                    case OperatorConstants.CACHE_END_TIME_INDEX:
                        endTime = TryGetConstantNumber(inlet);
                        break;

                    case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
                        samplingRate = TryGetConstantNumber(inlet);
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

        private double? TryGetConstantNumber(Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // Be tolerant in warning validations.
            if (inlet.InputOutlet == null)
            {
                return null;
            }

            if (inlet.InputOutlet.Operator == null)
            {
                return null;
            }

            if (inlet.InputOutlet.Operator.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
            {
                return null;
            }

            double number;
            if (!Double.TryParse(inlet.InputOutlet.Operator.Data, out number)) // TODO: Refactor this (and use DataPropertyParser) when the number is encoded into the data property as a key-value pair.
            {
                return null;
            }

            return number;
        }
    }
}