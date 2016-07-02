using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Spectrum_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Spectrum_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            double? signal = null;
            double? startTime = null;
            double? endTime = null;
            double? frequencyCount = null;

            foreach (Inlet inlet in Object.Inlets)
            {
                double? number = ValidationHelper.TryGetConstantNumberFromInlet(inlet);

                switch (inlet.ListIndex)
                {
                    case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                        signal = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.SPECTRUM_START_TIME_INDEX:
                        startTime = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.SPECTRUM_END_TIME_INDEX:
                        endTime = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
                        break;

                    case OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX:
                        frequencyCount = ValidationHelper.TryGetConstantNumberFromInlet(inlet);
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

            For(() => frequencyCount, PropertyDisplayNames.FrequencyCount)
                .IsInteger()
                .GreaterThan(2.0);

            if (startTime.HasValue && endTime.HasValue)
            {
                if (endTime.Value < startTime.Value)
                {
                    ValidationMessages.AddLessThanMessage(PropertyNames.EndTime, PropertyDisplayNames.EndTime, PropertyDisplayNames.StartTime);
                }
            }

            if (frequencyCount.HasValue)
            {
                if (!Maths.IsPowerOf2((int)frequencyCount.Value))
                {
                    string message = MessageFormatter.MustBePowerOf2(PropertyDisplayNames.FrequencyCount);
                    ValidationMessages.Add(PropertyDisplayNames.FrequencyCount, message);
                }
            }
        }

    }
}