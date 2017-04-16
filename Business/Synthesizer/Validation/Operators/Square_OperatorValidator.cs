using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Square_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Square_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Square,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Signal })
        { }
    }
}