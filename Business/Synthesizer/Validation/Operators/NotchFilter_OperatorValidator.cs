using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class NotchFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public NotchFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.NotchFilter,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}
