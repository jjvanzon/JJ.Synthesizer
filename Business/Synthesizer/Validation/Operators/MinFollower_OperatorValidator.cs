using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinFollower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MinFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MinFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined })
        { }
    }
}
