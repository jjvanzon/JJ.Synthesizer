using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class RangeOverDimension_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public RangeOverDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.RangeOverDimension,
                new[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}