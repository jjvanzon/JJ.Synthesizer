using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LessThanOrEqual_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LessThanOrEqual_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.LessThanOrEqual,
                  new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new[] { DimensionEnum.Undefined })
        { }
    }
}
