using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LessThanOrEqual_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LessThanOrEqual_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.LessThanOrEqual,
                  new[] { DimensionEnum.A, DimensionEnum.B },
                  new[] { DimensionEnum.Number })
        { }
    }
}
