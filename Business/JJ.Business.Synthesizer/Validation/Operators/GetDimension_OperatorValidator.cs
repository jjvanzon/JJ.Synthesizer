using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class GetDimension_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public GetDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.GetDimension,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}