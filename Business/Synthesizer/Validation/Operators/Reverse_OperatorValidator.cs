using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reverse_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Reverse_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Reverse,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal })
        { }
    }
}