using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SetDimension_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SetDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SetDimension,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
