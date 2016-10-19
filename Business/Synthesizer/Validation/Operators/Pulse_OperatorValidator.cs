using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Pulse_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Pulse_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Pulse,
                new DimensionEnum[] { DimensionEnum.Frequency, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}
