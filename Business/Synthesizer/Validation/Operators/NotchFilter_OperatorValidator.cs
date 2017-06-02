using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class NotchFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public NotchFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.NotchFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound })
        { }
    }
}
