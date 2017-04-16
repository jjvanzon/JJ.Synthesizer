using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sine_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Sine_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Sine,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Signal })
        { }
    }
}