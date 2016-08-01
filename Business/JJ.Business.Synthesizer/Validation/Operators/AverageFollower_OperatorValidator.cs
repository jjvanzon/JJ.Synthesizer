using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageFollower_OperatorValidator : OperatorValidator_Base
    {
        public AverageFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.AverageFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[0])
        { }
    }
}