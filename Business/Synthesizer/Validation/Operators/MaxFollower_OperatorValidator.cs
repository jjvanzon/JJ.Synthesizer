using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MaxFollower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MaxFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MaxFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}