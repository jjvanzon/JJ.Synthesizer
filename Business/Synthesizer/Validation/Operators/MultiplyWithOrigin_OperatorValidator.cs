using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MultiplyWithOrigin_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MultiplyWithOrigin_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MultiplyWithOrigin,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
