using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Range_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Range_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Range,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}