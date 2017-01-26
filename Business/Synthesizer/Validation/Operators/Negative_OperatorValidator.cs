using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Negative_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Negative_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Negative,
                new[] { DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
