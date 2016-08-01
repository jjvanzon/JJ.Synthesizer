using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SetDimension_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SetDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SetDimension,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
