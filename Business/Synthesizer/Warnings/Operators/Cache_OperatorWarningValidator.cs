using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

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

            foreach (Inlet inlet in Obj.Inlets)
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

            For(() => signal, ResourceFormatter.Signal)
                .NotInfinity()
                .NotNaN();

            For(() => start, ResourceFormatter.Start)
                .NotInfinity()
                .NotNaN();

            For(() => end, ResourceFormatter.End)
                .NotInfinity()
                .NotNaN();

            For(() => samplingRate, ResourceFormatter.SamplingRate)
                .IsInteger()
                .GreaterThan(0.0);

            // ReSharper disable once InvertIf
            if (start.HasValue && end.HasValue)
            {
                if (end.Value < start.Value)
                {
                    ValidationMessages.AddLessThanMessage(PropertyNames.EndTime, ResourceFormatter.EndTime, ResourceFormatter.StartTime);
                }
            }
        }
    }
}