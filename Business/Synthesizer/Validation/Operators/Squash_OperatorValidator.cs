using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Squash_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Squash_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Squash,
                new[] { DimensionEnum.Signal, DimensionEnum.Factor, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal })
        { }
    }
}
