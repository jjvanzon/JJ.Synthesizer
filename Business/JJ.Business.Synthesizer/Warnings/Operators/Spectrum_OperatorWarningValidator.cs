using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Mathematics;

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
            double? start = null;
            double? end = null;
            double? frequencyCount = null;

            foreach (Inlet inlet in Object.Inlets)
            {
                double? number = inlet.TryGetConstantNumber();

                switch (inlet.ListIndex)
                {
                    case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                        signal = number;
                        break;

                    case OperatorConstants.SPECTRUM_START_INDEX:
                        start = number;
                        break;

                    case OperatorConstants.SPECTRUM_END_INDEX:
                        end = number;
                        break;

                    case OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX:
                        frequencyCount = number;
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

            For(() => frequencyCount, PropertyDisplayNames.FrequencyCount)
                .IsInteger()
                .GreaterThan(2.0);

            if (start.HasValue && end.HasValue)
            {
                if (end.Value < start.Value)
                {
                    ValidationMessages.AddLessThanMessage(PropertyNames.End, PropertyDisplayNames.End, PropertyDisplayNames.Start);
                }   
            }

            if (frequencyCount.HasValue)
            {
                if (!MathHelper.IsPowerOf2((int)frequencyCount.Value))
                {
                    string message = MessageFormatter.MustBePowerOf2(PropertyDisplayNames.FrequencyCount);
                    ValidationMessages.Add(PropertyDisplayNames.FrequencyCount, message);
                }
            }
        }
    }
}