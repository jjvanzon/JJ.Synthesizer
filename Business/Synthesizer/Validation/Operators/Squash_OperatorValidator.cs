using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Squash_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Squash_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Squash,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}
