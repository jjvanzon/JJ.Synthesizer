using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Mathematics;
using JJ.Framework.Presentation.Resources;

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

            foreach (Inlet inlet in Obj.Inlets)
            {
                double? number = inlet.TryGetConstantNumber();

                DimensionEnum dimensionEnum = inlet.GetDimensionEnum();

                switch (dimensionEnum)
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

                    case DimensionEnum.FrequencyCount:
                        frequencyCount = number;
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

            For(() => frequencyCount, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Frequencies))
                .IsInteger()
                .GreaterThan(2.0);

            if (start.HasValue && end.HasValue)
            {
                if (end.Value < start.Value)
                {
                    ValidationMessages.AddLessThanMessage(nameof(Spectrum_OperatorWrapper.End), ResourceFormatter.End, ResourceFormatter.Start);
                }   
            }

            // ReSharper disable once InvertIf
            if (frequencyCount.HasValue)
            {
                // ReSharper disable once InvertIf
                if (!MathHelper.IsPowerOf2((int)frequencyCount.Value))
                {
                    string message = ResourceFormatter.MustBePowerOf2(CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Frequencies));
                    ValidationMessages.Add(nameof(Spectrum_OperatorWrapper.FrequencyCount), message);
                }
            }
        }
    }
}