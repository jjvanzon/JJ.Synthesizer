using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AverageFollower_OperatorValidator : OperatorValidator_Base
    {
        public AverageFollower_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.AverageFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[0])
        { }
    }
}