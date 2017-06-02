using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Pulse_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Pulse_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Pulse,
                new[] { DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound })
        { }
    }
}
