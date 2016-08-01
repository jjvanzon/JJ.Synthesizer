using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxFollower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MaxFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MaxFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}