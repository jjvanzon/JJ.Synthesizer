using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SumFollower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SumFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SumFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
