using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
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

                switch (inlet.GetDimensionEnum())
                {
                    case DimensionEnum.Signal:
                        signal = number;
                        break;

                    case DimensionEnum.Start:
                        start = number;
                        break;

                    case DimensionEnum.End:
                        end = number;
                        break;

                    case DimensionEnum.SamplingRate:
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
                    ValidationMessages.AddLessThanMessage(nameof(Cache_OperatorWrapper.End), ResourceFormatter.End, ResourceFormatter.Start);
                }
            }
        }
    }
}