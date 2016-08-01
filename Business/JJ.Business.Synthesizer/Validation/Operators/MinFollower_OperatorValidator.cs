using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinFollower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public MinFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.MinFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
