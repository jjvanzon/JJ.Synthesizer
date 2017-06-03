using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MultiplyWithOrigin_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MultiplyWithOrigin_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MultiplyWithOrigin,
                new[] { DimensionEnum.A, DimensionEnum.B, DimensionEnum.Origin },
                new[] { DimensionEnum.Number })
        { }
    }
}
