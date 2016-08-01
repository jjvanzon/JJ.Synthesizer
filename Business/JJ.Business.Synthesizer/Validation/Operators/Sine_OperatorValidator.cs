using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Sine_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Sine_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Sine,
                new DimensionEnum[] { DimensionEnum.Frequency, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}